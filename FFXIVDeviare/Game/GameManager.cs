using FFXIVDeviare.Game.Events;
using FFXIVDeviare.Game.Inventory;
using FFXIVDeviare.Game.Market;
using FFXIVDeviare.Packets;
using FFXIVDeviare.Packets.Subpackets;
using FFXIVDeviare.Packets.Subpackets.NonMagicPackets.Sent;
using FFXIVDeviare.Packets.Subpackets.Received;
using FFXIVDeviare.Packets.Subpackets.Sent;
using FFXIVDeviare.Packets.Subpackets.Subpackets.Sent;
using FFXIVDeviare.Packets.Subpackets.Subpackets.Sent.ExpandedPackets;
using FFXIVDeviare.Utility;
using Nektra.Deviare2;
using SaintCoinach;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FFXIVDeviare.Game
{
    public partial class GameManager
    {
        public event EventHandler<GameEventArgs> PacketCaptured;
        public event EventHandler<GameEventArgs> InventoryPacketCaptured;
        public event EventHandler<GameEventArgs> MarketBoardPacketCaptured;
        //public event EventHandler<GameEventArgs> ChatPacketCaptured;


        public delegate void PacketResponse(Packet p);
        private NktProcess GameProcess { get; }
        
        private NktSpyMgr _spyMgr = new NktSpyMgr();
        private NktHook sendHook = null;
        private NktHook recvHook = null;
        private int currentZone = 0;
        private long characterId;
        private int gameSocket = 0;
        private int chatSocket = 0;
        private UInt16 token105 = 0;
        private UInt16 token209 = 0;
        private UInt16 token286 = 0;
        private List<UInt32> retainerIds = new List<UInt32>();
        public InventoryManager InventoryHandler { get; }
        public MarketManager MarketHandler { get; }

        public uint ActiveRetainer { get; private set; }
        public uint ActiveNPC { get; private set; }


        private ConcurrentDictionary<int,ConcurrentBag<PacketResponse>> callbackQueue = new ConcurrentDictionary<int, ConcurrentBag<PacketResponse>>();
        public GameManager(Process process, ARealmReversed realmData)
        {
            _spyMgr.Initialize();
            InventoryHandler = new InventoryManager(this);
            GameProcess = _spyMgr.ProcessFromPID(process.Id);
            MarketHandler = new MarketManager(this);
            if (Packet.RealmData == null) Packet.RealmData = realmData;
            //BagManager bm = new BagManager(GameProcess, "english");

        }



        public void Begin()
        {
            recvHook = _spyMgr.CreateHook("ws2_32.dll!recv", (int)(eNktHookFlags.flgRestrictAutoHookToSameExecutable | eNktHookFlags.flgOnlyPreCall));
            recvHook.OnFunctionCalled += new DNktHookEvents_OnFunctionCalledEventHandler(HandlePacket);

            recvHook.Hook(true);

            recvHook.Attach(GameProcess, true);

            sendHook = _spyMgr.CreateHook("ws2_32.dll!send", (int)(eNktHookFlags.flgRestrictAutoHookToSameExecutable | eNktHookFlags.flgOnlyPreCall));
            sendHook.OnFunctionCalled += new DNktHookEvents_OnFunctionCalledEventHandler(HandlePacket);

            sendHook.Hook(true);
            sendHook.Attach(GameProcess, true);
        }


        public void End()
        {
            recvHook.Detach(GameProcess);
            sendHook.Detach(GameProcess);
        }



        public void TestItemSearch()
        {
            MarketHandler.BuyAllItems(5439, 15);
        }




        private void SendPacket(Packet p)
        {
            NktProcessMemory procMem;
            long[] callParams;
            IntPtr remoteBuffer;
            string dllName = Application.StartupPath + "\\FFXIV_dll.dll";

            procMem = GameProcess.Memory();
            remoteBuffer = procMem.AllocMem(new IntPtr(8), false);
            _spyMgr.LoadCustomDll(GameProcess, dllName, true, true);
            callParams = new long[1] { remoteBuffer.ToInt64() };

            long timestamp = GenerateTimestamp();

            byte[] packetBytes = GetBytes(p);
            int size = packetBytes.Length;
            IntPtr socket = procMem.AllocMem(new IntPtr(4), false);
            IntPtr buf = procMem.AllocMem(new IntPtr(size), false);
            IntPtr len = procMem.AllocMem(new IntPtr(4), false);
            IntPtr flags = procMem.AllocMem(new IntPtr(4), false);
            NktParam param = procMem.BuildParam(socket, "int");
            IntPtr socketPointer = Marshal.AllocHGlobal(8);
            IntPtr bufPointer = Marshal.AllocHGlobal(size);
            IntPtr lenPointer = Marshal.AllocHGlobal(8);
            IntPtr flagsPointer = Marshal.AllocHGlobal(8);
            Marshal.Copy(BitConverter.GetBytes(gameSocket), 0, socketPointer, 4);
            Marshal.Copy(packetBytes, 0, bufPointer, size);
            Marshal.Copy(BitConverter.GetBytes(size), 0, lenPointer, 4);
            procMem.WriteMem(socket, socketPointer, new IntPtr(8));
            procMem.WriteMem(buf, bufPointer, new IntPtr(size));
            procMem.WriteMem(len, lenPointer, new IntPtr(8));
            long[] cparams = { gameSocket, buf.ToInt64(), size, 0 };
            long socketSendResponse = _spyMgr.CallCustomApi(GameProcess, dllName, "SocketSend", cparams.ToArray(), true);
            _spyMgr.UnloadCustomDll(GameProcess, dllName, true);
        }

        private void SendPacket(Packet p, PacketResponse callback, int responsePacketId)
        {
            SendPacket(p);
            if (!callbackQueue.ContainsKey(responsePacketId)) callbackQueue.TryAdd(responsePacketId, new ConcurrentBag<PacketResponse>());
            callbackQueue[responsePacketId].Add(callback);

        }

        public void SendPacket(byte[] bytes)
        {
            NktProcessMemory procMem;
            long[] callParams;
            IntPtr remoteBuffer;
            string dllName = Application.StartupPath + "\\FFXIV_dll.dll";

            procMem = GameProcess.Memory();
            remoteBuffer = procMem.AllocMem(new IntPtr(8), false);
            _spyMgr.LoadCustomDll(GameProcess, dllName, true, true);
            callParams = new long[1] { remoteBuffer.ToInt64() };

            long timestamp = GenerateTimestamp();

            int size = bytes.Length;
            IntPtr socket = procMem.AllocMem(new IntPtr(4), false);
            IntPtr buf = procMem.AllocMem(new IntPtr(size), false);
            IntPtr len = procMem.AllocMem(new IntPtr(4), false);
            IntPtr flags = procMem.AllocMem(new IntPtr(4), false);
            NktParam param = procMem.BuildParam(socket, "int");
            IntPtr socketPointer = Marshal.AllocHGlobal(8);
            IntPtr bufPointer = Marshal.AllocHGlobal(size);
            IntPtr lenPointer = Marshal.AllocHGlobal(8);
            IntPtr flagsPointer = Marshal.AllocHGlobal(8);
            Marshal.Copy(BitConverter.GetBytes(gameSocket), 0, socketPointer, 4);
            Marshal.Copy(bytes, 0, bufPointer, size);
            Marshal.Copy(BitConverter.GetBytes(size), 0, lenPointer, 4);
            procMem.WriteMem(socket, socketPointer, new IntPtr(8));
            procMem.WriteMem(buf, bufPointer, new IntPtr(size));
            procMem.WriteMem(len, lenPointer, new IntPtr(8));
            long[] cparams = { gameSocket, buf.ToInt64(), size, 0 };
            long socketSendResponse = _spyMgr.CallCustomApi(GameProcess, dllName, "SocketSend", cparams.ToArray(), true);
            _spyMgr.UnloadCustomDll(GameProcess, dllName, true);
        }

        private void SendPacket(byte[] bytes, PacketResponse callback, int responsePacketId)
        {
            SendPacket(bytes);
            if (!callbackQueue.ContainsKey(responsePacketId)) callbackQueue.TryAdd(responsePacketId, new ConcurrentBag<PacketResponse>());
            callbackQueue[responsePacketId].Add(callback);

        }


        public void SendPacket(List<Subpacket> subpackets, PacketResponse callback, int responsePacketId)
        {
            SendPacket(subpackets);
            if (!callbackQueue.ContainsKey(responsePacketId)) callbackQueue.TryAdd(responsePacketId, new ConcurrentBag<PacketResponse>());
            callbackQueue[responsePacketId].Add(callback);

        }

        public void SendPacket(List<Subpacket> subpackets)
        {

            int spCount = 0;

            List<byte> bytes = new List<byte>();
            foreach (Subpacket sp in subpackets)
            {


                if (spCount % 3 == 0) sp.SubpacketHeader = subpacketHeader((uint)(sp.GetStructureSize() + Subpacket.SubpacketHeaderSize), token286, (ushort) sp.SubpacketId);
                if (spCount % 3 == 1) sp.SubpacketHeader = subpacketHeader((uint)(sp.GetStructureSize() + Subpacket.SubpacketHeaderSize), token209, (ushort)sp.SubpacketId);
                if (spCount % 3 == 2) sp.SubpacketHeader = subpacketHeader((uint)(sp.GetStructureSize() + Subpacket.SubpacketHeaderSize), token105, (ushort)sp.SubpacketId);
                bytes.AddRange(ByteUtils.ConvertStructToByteArray(sp.SubpacketHeader));
                bytes.AddRange(ByteUtils.ConvertStructToByteArray(sp.PacketData));
            }

            long timestamp = GenerateTimestamp();
            int size = bytes.Count() + 40;

            PacketHeader packetHeader = new PacketHeader();
            unsafe
            {
                Marshal.Copy(Packet.ARR_HEADER_MAGIC, 0, (IntPtr)packetHeader.MagicHeader, 16);
                packetHeader.TimestampToken = (uint)timestamp; // uint32
                packetHeader.Version = 0x153; // uint32
                packetHeader.Size = (UInt32)size; // uint32
                packetHeader.UnkAlways0_1 = 0; // uint16
                packetHeader.SubpacketCount = (ushort) subpackets.Count(); // uint16
                packetHeader.h_unk6 = 1; // byte
                packetHeader.IsDeflated = 0; // byte
                packetHeader.UnkAlways0_2 = 0; // byte
                packetHeader.h_unk9 = 0; // byte
                packetHeader.UnkAlways0_3 = 0; // uint32
            }

            byte[] headerBytes = ByteUtils.ConvertStructToByteArray(packetHeader);
            bytes.InsertRange(0, headerBytes);
            SendPacket(bytes.ToArray());
        }


        private void HandlePacket(NktHook hook, NktProcess process, NktHookCallInfo hookCallInfo)
        {
            INktParamsEnum paramsEnum = hookCallInfo.Params();
            INktParam socket = paramsEnum.First();
            INktParam charbuf = paramsEnum.Next();
            INktParam len = paramsEnum.Next();
            INktParam flags = paramsEnum.Next();
            byte[] sdata = new byte[len.LongVal];
            GCHandle pinnedBuffer = GCHandle.Alloc(sdata, GCHandleType.Pinned);

            String typename = charbuf.TypeName;
            typename = charbuf.BasicType.ToString();

            process.Memory().ReadMem(pinnedBuffer.AddrOfPinnedObject(), charbuf.PointerVal, (IntPtr)len.LongVal);
            pinnedBuffer.Free();

            bool isSent = hook.FunctionName.Equals("ws2_32.dll!send");
            
            if (isSent && sdata[0] == 0 && sdata.Length == 64)
            {

                GetUpdate p = GetUpdate.ReadStruct(sdata);
                Debug.WriteLine(Utility.ByteUtils.StructToString(p));
            }
            else { 
                Packet p = new Packet(sdata, isSent, socket.LongVal);
                if (!p.FailedMagic && p.Subpackets.Count > 0)
                {

                    // let's set the unk3s
                    if (p.Subpackets[0].SubpacketHeader.Id == 105 || (p.Subpackets[0].SubpacketHeader.Id == 410 && p.Subpackets.Count == 1)) this.token105 = p.Subpackets[0].SubpacketHeader.Token;
                    if (p.Subpackets[0].SubpacketHeader.Id == 209 || (p.Subpackets[0].SubpacketHeader.Id == 410 && p.Subpackets.Count > 1)) this.token209 = p.Subpackets[0].SubpacketHeader.Token;
                    if (p.Subpackets.Count() > 1 && p.Subpackets[0].SubpacketHeader.Id == 410 && p.Subpackets[1].SubpacketHeader.Id == 410) this.token286 = p.Subpackets[1].SubpacketHeader.Token;

                    // let's get our retainer ids
                    if (p.Subpackets.Count() > 7 && p.Subpackets[0].SubpacketHeader.Id == 427 && p.Subpackets[1].SubpacketHeader.Id == 427 && p.Subpackets[2].SubpacketHeader.Id == 427 && p.Subpackets[3].SubpacketHeader.Id == 427 && p.Subpackets[4].SubpacketHeader.Id == 427 && p.Subpackets[5].SubpacketHeader.Id == 427 && p.Subpackets[6].SubpacketHeader.Id == 427 && p.Subpackets[7].SubpacketHeader.Id == 427)
                    {

                        int i = 0;
                        foreach (Subpacket subpacket in p.Subpackets)
                        {
                            SelectedSummoningBell_RetainerData1.Data castSubpacket = (SelectedSummoningBell_RetainerData1.Data)subpacket.PacketData;
                            bool alreadyContainsId = false;
                            foreach (UInt32 retainerId in this.retainerIds)
                            {
                                if (retainerId == castSubpacket.retainerId)
                                {
                                    alreadyContainsId = true;
                                }
                            }
                            if (!alreadyContainsId && castSubpacket.retainerId != 0) this.retainerIds.Add(castSubpacket.retainerId);
                            i++;
                            if (i == 8) break; 
                        }

                    }

                    IEnumerable<Subpacket> inQueue = p.Subpackets.Where(s => callbackQueue.Keys.Contains(s.SubpacketId)).GroupBy(id => id.SubpacketId).Select(g => g.First());
                    foreach(Subpacket sp in inQueue)
                        foreach (PacketResponse response in callbackQueue[sp.SubpacketId])
                            response.Invoke(p);

                    if (p.Subpackets[0].SubpacketHeader.ZoneId > 0) this.currentZone = p.Subpackets[0].SubpacketHeader.ZoneId;
                    if (p.isChatPacket) chatSocket = p.Socket;
                    else if (p.Socket != chatSocket) gameSocket = p.Socket;
                    if (p.Subpackets[0].SubpacketHeader.SourceId > 0 && p.IsSent) this.characterId = p.Subpackets[0].SubpacketHeader.SourceId;
                }


                GameEventArgs ev = new GameEventArgs();
                ev.Packet = p;
                UpdateGameStatus(p);    



                if (p.Subpackets.Where(s=> s.Type == SubpacketType.Inventory).Count() >= 1)
                {
                    if (InventoryPacketCaptured != null) InventoryPacketCaptured(this, ev);
                }
                if (p.Subpackets.Where(s => s.Type == SubpacketType.MarketBoard).Count() >= 1)
                {
                    if (MarketBoardPacketCaptured != null) MarketBoardPacketCaptured(this, ev);
                }
                if (PacketCaptured != null) PacketCaptured(this, ev);
            }
        }



        private void UpdateGameStatus(Packet p)
        {
            var npcDialogRequest = p.Subpackets.OfType<NPCDialogRequest>().FirstOrDefault();
            if(npcDialogRequest != null)
            {
                NPCDialogRequest.Data dialogData = (NPCDialogRequest.Data) npcDialogRequest.PacketData;
                ActiveNPC = dialogData.requestId;
            }

            var quitDialogRequest = p.Subpackets.OfType<QuitNPCDialog>().FirstOrDefault();
            if (quitDialogRequest != null)
            {
                ActiveNPC = 0;
            }

            var summonRetainer = p.Subpackets.OfType<SummonRetainer>().FirstOrDefault();
            if (summonRetainer != null)
            {
                SummonRetainer.Data retainerData = (SummonRetainer.Data)summonRetainer.PacketData;
                ActiveRetainer = retainerData.RetainerId;
            }

            var quitRetainer = p.Subpackets.OfType<QuitRetainer>().FirstOrDefault();
            if (quitRetainer != null)
            {
                ActiveNPC = 0;
                ActiveRetainer = 0;
            }
        }


        private SubpacketHeader subpacketHeader(UInt32 size, UInt16 token, UInt16 id)
        {

            SubpacketHeader subpacketHeader = new SubpacketHeader();

            subpacketHeader.Size = size; // uint32
            subpacketHeader.SourceId = (uint)characterId; // uin32
            subpacketHeader.TargetId = (uint)characterId; // uint32

            subpacketHeader.Unk1 = 3; // uint16
            subpacketHeader.Token = token; // uint16

            subpacketHeader.MagicNumber = 20; // uint16
            subpacketHeader.Id = id; // uint16
            subpacketHeader.Unk2 = 0; // uint16
            subpacketHeader.ZoneId = (ushort)currentZone; // uint16
            subpacketHeader.Timestamp = (UInt32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds; // uint32
            subpacketHeader.Unk3 = 0; // uint32

            return subpacketHeader;

        }

        /*
        The Goblet- Zone 73
        Mist- Zone 53
        Lavender Beds- 44
        Limsa Lower- Zone 59
        Limsa Upper - 58
        Middle La Nos – 3
        Lower La Nos- 4
        Eastern La Nos – 5
        Western La Nos – 10
        Upper La Nos – 11
        Outer La Nos – 12
        Wolves Den – 23
        New Gridania- 42
        Old Gridania – 43
        Central Shroud- 35
        East Shroud- 36
        South Shroud- 37
        North Shroud – 38
        Uldah – Steps of Nald – 51
        Uldah- Steps of Thal- 52
        Western Than – 71
        Central Than – 72
        Eastern Than – 65
        South Than – 66
        North Than – 67
        Gold Saucer – 70
        Foundation – 1
        Coerthas Central – 64
        Coerthas Western – 15
        Sea of Clouds – 22
        Azys Lla- 28
        Idyllshire – 57
        Dravanian Forelands – 34
        */

        /*
        SummoningBellDialogRequest
        SelectRetainer(retainerIndex)
        RepairAllArmor
        MarketBoardDialogRequest
        PostItemToMarketBoard(sourceBag, sourceSlot, quantity, price)
        RemoveItemFromMarketBoard(sourceSlot)
        */

        public void RepairAllArmor()
        {

            if (!InventoryHandler.Inventory.ContainsKey( (int) BagType.EquippedItems)) return;

            
            foreach(Item bagContent in InventoryHandler.Inventory[(int)BagType.EquippedItems].Values)
            {

                bagContent.Repair();

            }
            

        }

        public void SummoningBellDialogRequest()
        {

            NPCDialogRequest npcDialogRequest = new NPCDialogRequest();
            NPCDialogRequest.Data npcDialogRequestData = new NPCDialogRequest.Data();
            npcDialogRequestData.objectId = 4576916; // id for the summoning bell in the goblet
            npcDialogRequestData.unk2 = 1;           // seems to always be 1?
            npcDialogRequestData.requestId = 720906; // this seems to always be the market board request id
            npcDialogRequestData.unk4 = 0;           // seemingly random number
            npcDialogRequest.PacketData = npcDialogRequestData;

            SendPacket(new List<Subpacket>() { npcDialogRequest });
              
        }

        public void SelectRetainer(int retainerIndex)
        {

            UInt32 retainerId;
            if (retainerIndex < retainerIds.Count())
            {
                // there is a retainer id for this index
                retainerId = retainerIds[retainerIndex];
            }
            else
            {
                // return - there is no retainer id for this index
                return;
            }

            RetainerActions unkPacket480 = new RetainerActions();
            RetainerActions.Data unkPacket480Data = new RetainerActions.Data();
            unkPacket480Data.Unk1 = 720906;          // request id - may be summoning bell specific
            unkPacket480Data.ExpandedId = 519;        // magic?
            unkPacket480Data.Unk3 = 7864343;         // magic
            unkPacket480Data.Unk4 = retainerId;      // retainer id - replace w one of your own
            unkPacket480.PacketData = unkPacket480Data;

            SendPacket(new List<Subpacket>() { unkPacket480 });

        }

        public void MarketBoardDialogRequest()
        {



            //SEND: 450 401
            //RECV: 461 419 322 213 323
            //RECV: 322 460 419 323 451

            NPCDialogRequest npcDialogRequest = new NPCDialogRequest();
            NPCDialogRequest.Data npcDialogRequestData = new NPCDialogRequest.Data();
            npcDialogRequestData.objectId = 4632367; // id for the marketboard in the goblet
            npcDialogRequestData.unk2 = 1;           // seems to always be 1?
            npcDialogRequestData.requestId = 720935; // this seems to always be the market board request id
            npcDialogRequestData.unk4 = 0;           // seemingly random number
            npcDialogRequest.PacketData = npcDialogRequestData;

            PlayerSetTarget playerSetTarget = new PlayerSetTarget();
            PlayerSetTarget.Data playerSetTargetData = new PlayerSetTarget.Data();
            playerSetTargetData.ExpandedId = 3;
            playerSetTargetData.TargetId = 4632367;
            playerSetTargetData.Unk3 = 1;
            playerSetTargetData.Unk4 = 0;
            playerSetTargetData.Unk5 = 0;
            playerSetTargetData.Unk6 = 0;
            playerSetTargetData.Unk7 = 0;
            playerSetTargetData.Unk8 = 0;
            playerSetTarget.PacketData = playerSetTargetData;

            SendPacket(new List<Subpacket>() { npcDialogRequest, playerSetTarget });

        }

        public void PostItemToMarketBoard(int sourceBag, int sourceSlot, int quantity, int price)
        {

            bool foundValidSoureCoordinate = false;

            if (sourceBag >= 0 && sourceBag <= 3 && sourceSlot >= 0 && sourceSlot <= 24) foundValidSoureCoordinate = true;
            if (sourceBag == 2001 && sourceSlot >= 0 && sourceSlot <= 17) foundValidSoureCoordinate = true;
            if (sourceBag >= 10000 && sourceBag <= 10006 && sourceSlot >= 0 && sourceSlot <= 24) foundValidSoureCoordinate = true;
            if (sourceBag == 12001 && sourceSlot >= 0 && sourceSlot <= 17) foundValidSoureCoordinate = true;

            if (!foundValidSoureCoordinate)
            {
                Debug.WriteLine("Please enter a valid source coordinate.");
                return;
            }

            Item item = InventoryHandler.Inventory[sourceBag].Values.Where(s => s.Slot == sourceSlot).FirstOrDefault();

            if (item == null)
            {
                Debug.WriteLine("There isn't an item at this source coordinate.");
                return;
            }

            int destinationSlot = InventoryHandler.GetFirstAvailableRetainerSaleSlot();
            if (destinationSlot == -1)
            {
                Debug.WriteLine("The retainer has no more available sale slots.");
                return;
            }

            if (price == 0)
            {
                Debug.WriteLine("0 is not a valid price.");
                return;
            }

            byte operation = 8;
            int sourceCount = (int)item.Quantity;
            if (sourceCount > quantity) operation = 10;

            if (quantity < 1 || quantity > sourceCount)
            {
                Debug.WriteLine("Please enter a valid quantity.");
                return;
            }

            if (InventoryHandler.IncrementingBagOpValueSet)
            {
                InventoryHandler.IncrementingBagOpValue += 2;
            } else
            {
                InventoryHandler.IncrementingBagOpValueSet = true;
            }

            BagOperation bagOperation = new BagOperation();
            BagOperation.Data bagOperationData = new BagOperation.Data();
            bagOperationData.incrementingValue = (ushort)InventoryHandler.IncrementingBagOpValue;
            bagOperationData.someSize = 4096;
            bagOperationData.operation = operation; // 8 for all items at slot, 10 otherwise
            bagOperationData.sourceBag = (uint)sourceBag;
            bagOperationData.sourceSlot = (byte)sourceSlot;
            bagOperationData.sourceCount = (uint)sourceCount;
            bagOperationData.destinationBag = 12002;
            bagOperationData.destinationSlot = (ushort)destinationSlot;
            bagOperationData.quantity = (uint)quantity;
            bagOperation.PacketData = bagOperationData;

            RetainerSetItemPrice retainerSetItemPrice = new RetainerSetItemPrice();
            RetainerSetItemPrice.Data retainerSetItemPriceData = new RetainerSetItemPrice.Data();
            retainerSetItemPriceData.ExpandedId = 400;
            retainerSetItemPriceData.SlotId = (uint)destinationSlot;
            retainerSetItemPriceData.Price = (uint)price;
            retainerSetItemPrice.PacketData = retainerSetItemPriceData;

            SendPacket(new List<Subpacket>() { bagOperation, retainerSetItemPrice });

        }

        public void RemoveItemFromMarketBoard(int sourceSlot)
        {

            Item item = InventoryHandler.Inventory[12002].Values.Where(s => s.Slot == sourceSlot).FirstOrDefault();

            if (item == null)
            {
                Debug.WriteLine("There isn't an item at this source coordinate.");
                return;
            }

            bool itemIsACrystal = false;
            if (item.ItemId >= 2 && item.ItemId <= 19) itemIsACrystal = true;

            byte operation = 22;
            if (InventoryHandler.Inventory[0].Values.Count() + InventoryHandler.Inventory[1].Values.Count() + InventoryHandler.Inventory[2].Values.Count() + InventoryHandler.Inventory[3].Values.Count() > 99 && !itemIsACrystal)
            {
                // no remaining slots in inventory... how about the retainers inventory?
                if (InventoryHandler.Inventory[10000].Values.Count() + InventoryHandler.Inventory[10001].Values.Count() + InventoryHandler.Inventory[10002].Values.Count() + InventoryHandler.Inventory[10003].Values.Count() + InventoryHandler.Inventory[10004].Values.Count() + InventoryHandler.Inventory[10005].Values.Count() + InventoryHandler.Inventory[10006].Values.Count() > 174)
                {
                    Debug.WriteLine("There are no available slots in you or your retainer's inventory.");
                    return;
                }
                operation = 21;
            }

            if (InventoryHandler.IncrementingBagOpValueSet)
            {
                InventoryHandler.IncrementingBagOpValue += 2;
            }
            else
            {
                InventoryHandler.IncrementingBagOpValueSet = true;
            }

            BagOperation bagOperation = new BagOperation();
            BagOperation.Data bagOperationData = new BagOperation.Data();
            bagOperationData.incrementingValue = (ushort)InventoryHandler.IncrementingBagOpValue;
            bagOperationData.someSize = 4096;
            bagOperationData.operation = operation;
            bagOperationData.sourceBag = 12002;
            bagOperationData.sourceSlot = (byte)sourceSlot;
            bagOperationData.sourceCount = item.Quantity;
            bagOperation.PacketData = bagOperationData;

            SendPacket(new List<Subpacket>() { bagOperation });

        }

        private void Test()
        {
            PacketResponse callback = new PacketResponse(GameManager.TestCallback);

        }

        private static void TestCallback(Packet packet)
        {

            return;

        }


        


        private NktProcess GetProcess(string proccessName)
        {
            NktProcessesEnum enumProcess = _spyMgr.Processes();
            NktProcess tempProcess = enumProcess.First();
            while (tempProcess != null)
            {
                if (tempProcess.Name.Equals(proccessName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return tempProcess;
                }
                tempProcess = enumProcess.Next();
            }
            return null;
        }

        private byte[] GetBytes(Object str)
        {
            int size = Marshal.SizeOf(str);
            byte[] arr = new byte[size];

            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(str, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);
            return arr;
        }

        private unsafe long GenerateTimestamp()
        {
            Native.FILETIME v1, FileTime;
            Native.SYSTEMTIME SystemTime;
            Native.SYSTEMTIME v4 = new Native.SYSTEMTIME();

            Native.GetSystemTime(out SystemTime);
            Native.SystemTimeToFileTime(ref SystemTime, out FileTime);
            v4.wMilliseconds = 0;
            *(UInt32*)&v4.wYear = 67508;
            *(UInt32*)&v4.wDay = 1;
            *(UInt32*)&v4.wMinute = 0;
            v4.wDayOfWeek = 0;
            Native.SystemTimeToFileTime(ref v4, out v1);
            return *(Int64*)&FileTime - *(Int64*)&v1 / 1000 / 10;
        }

        private void GetSocketInfo(int socket)
        {
            //GetSocketInfo Socket p, ptr

            NktProcessMemory procMem;
            long[] callParams;
            IntPtr remoteBuffer;
            string dllName = Application.StartupPath + "\\FFXIV_dll.dll";

            procMem = GameProcess.Memory();
            IntPtr socketBuffer = procMem.AllocMem(new IntPtr(4), false);
            procMem.Write(socketBuffer, 1204);
            remoteBuffer = procMem.AllocMem(new IntPtr(4), false);
            _spyMgr.LoadCustomDll(GameProcess, dllName, true, true);
            callParams = new long[2] { 1204, 0 };
            //_spyMgr.CallCustomApi(proc, dllName, "fnCustom", callParams);
            uint test = (uint)_spyMgr.CallCustomApi(GameProcess, dllName, "GetSocketInfo", callParams);




            _spyMgr.UnloadCustomDll(GameProcess, dllName, true);
        }


    }
}
