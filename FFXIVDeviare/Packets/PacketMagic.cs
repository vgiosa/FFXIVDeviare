using FFXIVDeviare.Packets;
using Nektra.Deviare2;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FFXIVDeviare.Packets.Subpackets;
using FFXIVDeviare.Packets.Subpackets.Sent;
using System.Diagnostics;
using System.Collections.Generic;

using FFXIVDeviare.Utility;

namespace FFXIVDeviare.Packets
{

    class PacketMagic
    {
        //TODO: Can we see if theres any instance of a sent packet that has a different sourceId and targetId?

        /*
    private packet_subpacket_header_t formSubheader(UInt32 size, UInt16 unk33, UInt16 id)
    {

        packet_subpacket_header_t subheader = new packet_subpacket_header_t();

        subheader.s_size = size; // uint32
        subheader.s_sourceId = (uint)form1.characterId; // uin32
        subheader.s_targetId = (uint)form1.characterId; // uint32
        subheader.s_unk3 = 3; // uint16
        subheader.s_unk33 = unk33; // uint16
        subheader.s_magic = 20; // uint16
        subheader.s_id = id; // uint16
        subheader.s_unk4 = 0; // uint16
        subheader.s_zone = (ushort)form1.currentZone; // uint16
        subheader.s_time = (UInt32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds; // uint32
        subheader.s_unk5 = 0; // uint32

        return subheader;

    }

    private void sendPacket(List<byte> nonHeaderBytes, UInt16 subpacketCount)
    {

        NktProcessMemory procMem;
        long[] callParams;
        IntPtr remoteBuffer;
        string dllName = Application.StartupPath + "\\FFXIV_dll.dll";
        NktProcess proc = GetProcess("ffxiv_dx11.exe");
        if (proc == null) proc = GetProcess("ffxiv.exe");
        procMem = proc.Memory();
        remoteBuffer = procMem.AllocMem(new IntPtr(8), false);
        form1._spyMgr.LoadCustomDll(proc, dllName, true, true);
        callParams = new long[1] { remoteBuffer.ToInt64() };
        long timestampSuccess = form1._spyMgr.CallCustomApi(proc, dllName, "GenerateUniqueTimestamp", callParams);

        dynamic timestamp = procMem.Read((IntPtr)callParams[0], eNktDboFundamentalType.ftUnsignedDoubleWord);

        packet_header_t header = new packet_header_t();

        int size = nonHeaderBytes.Count() + 40;

        unsafe
        {
            Marshal.Copy(Packet.ARR_HEADER_MAGIC, 0, (IntPtr)header.h_magic, 16);

            header.h_timestampOffset = (uint)timestamp; // uint32
            header.h_unk3 = 0x153; // uint32
            header.h_size = (UInt32)size; // uint32
            header.h_unk5 = 0; // uint16
            header.h_subpacketCount = subpacketCount; // uint16
            header.h_unk6 = 1; // byte
            header.h_unk7 = 0; // byte
            header.h_deflated = 0; // byte
            header.h_unk9 = 0; // byte
            header.h_unk10 = 0; // uint32
        }

        byte[] headerBytes = getBytes(header);
        byte[] fullPacket = new byte[size];

        headerBytes.CopyTo(fullPacket, 0);
        nonHeaderBytes.CopyTo(fullPacket, headerBytes.Length);

        IntPtr socket = procMem.AllocMem(new IntPtr(4), false);
        IntPtr buf = procMem.AllocMem(new IntPtr(size), false);
        IntPtr len = procMem.AllocMem(new IntPtr(4), false);
        IntPtr flags = procMem.AllocMem(new IntPtr(4), false);
        NktParam param = procMem.BuildParam(socket, "int");


        IntPtr socketPointer = Marshal.AllocHGlobal(8);
        IntPtr bufPointer = Marshal.AllocHGlobal(size);
        IntPtr lenPointer = Marshal.AllocHGlobal(8);
        IntPtr flagsPointer = Marshal.AllocHGlobal(8);

        Marshal.Copy(BitConverter.GetBytes(form1.gameSocket), 0, socketPointer, 4);
        Marshal.Copy(fullPacket, 0, bufPointer, size);
        Marshal.Copy(BitConverter.GetBytes(size), 0, lenPointer, 4);
        procMem.WriteMem(socket, socketPointer, new IntPtr(8));
        procMem.WriteMem(buf, bufPointer, new IntPtr(size));
        procMem.WriteMem(len, lenPointer, new IntPtr(8));

        long[] cparams = { form1.gameSocket, buf.ToInt64(), size, 0 };

        long socketSendResponse = form1._spyMgr.CallCustomApi(proc, dllName, "SocketSend", cparams.ToArray(), true);

        form1._spyMgr.UnloadCustomDll(proc, dllName, true);

    }

    public void send_263_buyAllUnderpricedItems(byte[] subbody261, UInt32 minimumPrice)
    {

        if (form1.unk33_105 == 0 || form1.unk33_209 == 0 || form1.unk33_286 == 0)
        {
            return;
        }

        while (subbody261.Length > 8)
        {

            BuyItemFromMarketBoard.buyItemFromMarketBoard subbody263 = new BuyItemFromMarketBoard.buyItemFromMarketBoard();

            subbody263.price = BitConverter.ToUInt32(subbody261.ToList().GetRange(32, 4).ToArray(), 0);

            if (subbody263.price > minimumPrice)
            {
                return;
            }

            subbody263.retainerId = BitConverter.ToUInt32(subbody261.ToList().GetRange(8, 4).ToArray(), 0);
            subbody263.magic_7864343 = 7864343;
            subbody263.postId = BitConverter.ToUInt32(subbody261.ToList().GetRange(0, 4).ToArray(), 0);
            subbody263.unk4 = BitConverter.ToUInt32(subbody261.ToList().GetRange(4, 4).ToArray(), 0);
            subbody263.itemId = BitConverter.ToUInt32(subbody261.ToList().GetRange(44, 4).ToArray(), 0);
            subbody263.quantity = BitConverter.ToUInt32(subbody261.ToList().GetRange(40, 4).ToArray(), 0);
            subbody263.unk8 = BitConverter.ToUInt32(subbody261.ToList().GetRange(36, 4).ToArray(), 0);
            subbody263.unk9 = BitConverter.ToUInt16(subbody261.ToList().GetRange(54, 2).ToArray(), 0);
            subbody263.hq = subbody261[108];
            subbody263.unk11 = subbody261[110];
            subbody263.unk12 = 0;

            packet_subpacket_header_t subhead263 = formSubheader(72, (UInt16)form1.unk33_105, 263);

            byte[] subheadBytes = getBytes(subhead263);
            byte[] subbodyBytes = getBytes(subbody263);

            List<byte> nonHeaderBytes = new List<byte>();

            foreach (byte b in subheadBytes)
            {
                nonHeaderBytes.Add(b);
            }

            foreach (byte b in subbodyBytes)
            {
                nonHeaderBytes.Add(b);
            }

            sendPacket(nonHeaderBytes, 1);

            subbody261 = subbody261.Skip(112).ToArray();

        }


    }

    public void send_450_401_establishJunkmongerDialog()
    {

        if (form1.unk33_105 == 0 || form1.unk33_209 == 0 || form1.unk33_286 == 0)
        {
            return;
        }

        packet_subpacket_header_t subhead450 = formSubheader(48, (UInt16)form1.unk33_209, 450);
        packet_subpacket_header_t subhead401 = formSubheader(64, (UInt16)form1.unk33_286, 401);

        string body450 = "98 C3 44 00 01 00 00 00 5C 01 04 00 08 00 00 00";
        string body401 = "03 00 00 00 98 C3 44 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00";

        byte[] subheadBytes450 = getBytes(subhead450);
        byte[] subbodyBytes450 = Form1.StringToByteArray(body450.Replace(" ", ""));
        byte[] subheadBytes401 = getBytes(subhead401);
        byte[] subbodyBytes401 = Form1.StringToByteArray(body401.Replace(" ", ""));

        List<byte> nonHeaderBytes = new List<byte>();
        foreach (byte b in subheadBytes450)
        {
            nonHeaderBytes.Add(b);
        }
        foreach (byte b in subbodyBytes450)
        {
            nonHeaderBytes.Add(b);
        }
        foreach (byte b in subheadBytes401)
        {
            nonHeaderBytes.Add(b);
        }

        foreach (byte b in subbodyBytes401)
        {
            nonHeaderBytes.Add(b);
        }

        sendPacket(nonHeaderBytes, 2);

    }

    public void send_260_GetMarketboardMetadataForItem(UInt32 itemId, UInt32 unk2)
    {

        if (form1.unk33_105 == 0 || form1.unk33_209 == 0 || form1.unk33_286 == 0)
        {
            return;
        }

        packet_subpacket_header_t subpacket_header_260 = formSubheader(40, (UInt16)form1.unk33_105, 260);

        //Packet260.packet_subpacket_content_260 subpacket_body_260 = new Packet260.packet_subpacket_content_260();
        RequestMarketBoardDataForItem.requestMarketBoardDataForItem subpacket_body_260 = new RequestMarketBoardDataForItem.requestMarketBoardDataForItem();
        //subpacket_body_260.itemId = 4761;
        //subpacket_body_260.unk2 = 2306;
        subpacket_body_260.itemId = itemId;
        subpacket_body_260.unk2 = unk2;

        byte[] subheadBytes260 = getBytes(subpacket_header_260);
        byte[] subbodyBytes260 = getBytes(subpacket_body_260);

        List<byte> nonHeaderBytes = new List<byte>();

        foreach (byte b in subheadBytes260)
        {
            nonHeaderBytes.Add(b);
        }
        foreach (byte b in subbodyBytes260)
        {
            nonHeaderBytes.Add(b);
        }

        sendPacket(nonHeaderBytes, 1);

    }

    // eventually convert this to sell any item...
    public void send_478_SellMoleMeat()
    {

        if (form1.unk33_105 == 0 || form1.unk33_209 == 0 || form1.unk33_286 == 0)
        {
            return;
        }

        packet_subpacket_header_t subhead = formSubheader(1064, (UInt16)form1.unk33_105, 478);
        string body478 = "5C 01 04 00 28 00 00 FF 00 00 00 00 02 00 00 00 00 00 00 00 08 00 00 00 01 00 00 00 01 00 00 00 99 12 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00";

        byte[] subheadBytes478 = getBytes(subhead);
        byte[] subbodyBytes478 = Form1.StringToByteArray(body478.Replace(" ", ""));

        List<byte> nonHeaderBytes = new List<byte>();

        foreach (byte b in subheadBytes478)
        {
            nonHeaderBytes.Add(b);
        }
        foreach (byte b in subbodyBytes478)
        {
            nonHeaderBytes.Add(b);
        }

        sendPacket(nonHeaderBytes, 1);

    }

    private BagOperation.bagOperation create_subpacket_body_430(UInt16 bagOperationIncrementingValue, UInt32 quantity, UInt32 sourceBag, byte sourceSlot, UInt32 sourceCount, UInt32 destinationBag, UInt32 destinationSlot)
    {

        BagOperation.bagOperation subpacket_body_430 = new BagOperation.bagOperation();
        subpacket_body_430.incrementingValue = bagOperationIncrementingValue; // we'll have to keep track of this and adjust it as we go... we can grab it from RECV 437 0x0000
        subpacket_body_430.someSize = 4096;
        if (quantity == 99)
        {
            subpacket_body_430.operation = 8;
        }
        else
        {
            subpacket_body_430.operation = 10;
        }
        subpacket_body_430.always0_1 = 0;
        subpacket_body_430.sourceBag = sourceBag;
        subpacket_body_430.sourceSlot = sourceSlot;
        subpacket_body_430.always0_2 = 0;
        subpacket_body_430.always0_3 = 0;
        subpacket_body_430.sourceCount = sourceCount;
        subpacket_body_430.always0_4 = 0;
        subpacket_body_430.always0_5 = 0;
        subpacket_body_430.destinationBag = destinationBag;
        subpacket_body_430.destinationSlot = destinationSlot;
        subpacket_body_430.quantity = quantity;
        subpacket_body_430.always0_7 = 0;

        return subpacket_body_430;

    }

    private Packet401.packet_subpacket_content_401 create_subpacket_body_401(UInt32 operation, UInt32 target, UInt32 value, UInt32 unk4, UInt32 unk5, UInt32 unk6, UInt32 id, UInt32 unk8)
    {

        Packet401.packet_subpacket_content_401 subpacket_body_401 = new Packet401.packet_subpacket_content_401();

        subpacket_body_401.operation = operation;
        subpacket_body_401.target = target;
        subpacket_body_401.value = value;
        subpacket_body_401.unk4 = unk4;
        subpacket_body_401.unk5 = unk5;
        subpacket_body_401.unk6 = unk6;
        subpacket_body_401.id = id;
        subpacket_body_401.unk8 = unk8;

        return subpacket_body_401;

    }

    public void send_430_401_PostItemToMarketboard(UInt16 bagOperationIncrementingValue, UInt32 quantity, UInt32 sourceBag, byte sourceSlot, UInt32 sourceCount)
    {

        UInt32 unitPrice = 789; // this is just an arbitrary number...
        UInt32 destinationSlot = 10;

        BagOperation.bagOperation subpacket_body_430 = create_subpacket_body_430(bagOperationIncrementingValue, quantity, sourceBag, sourceSlot, sourceCount, 12002, destinationSlot);
        Packet401.packet_subpacket_content_401 subpakcet_body_401 = create_subpacket_body_401(400, destinationSlot, unitPrice, 0, 0, 0, 0, 0);

        packet_subpacket_header_t subhead430 = formSubheader(80, (UInt16)form1.unk33_105, 430);
        packet_subpacket_header_t subhead401 = formSubheader(64, (UInt16)form1.unk33_105, 401);

        byte[] subheadBytes430 = getBytes(subhead430);
        byte[] subbodyBytes430 = getBytes(subpacket_body_430);
        byte[] subheadBytes401 = getBytes(subhead401);
        byte[] subbodyBytes401 = getBytes(subpakcet_body_401);

        List<byte> nonHeaderBytes = new List<byte>();
        foreach (byte b in subheadBytes430)
        {
            nonHeaderBytes.Add(b);
        }
        foreach (byte b in subbodyBytes430)
        {
            nonHeaderBytes.Add(b);
        }
        foreach (byte b in subheadBytes401)
        {
            nonHeaderBytes.Add(b);
        }
        foreach (byte b in subbodyBytes401)
        {
            nonHeaderBytes.Add(b);
        }

        sendPacket(nonHeaderBytes, 2);

    }


    private NktProcess GetProcess(string proccessName)
    {
        NktProcessesEnum enumProcess = form1._spyMgr.Processes();
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

    private BagManager getBagManager()
    {

        NktProcess process = GetProcess("ffxiv_dx11.exe");
        if (process == null) process = GetProcess("ffxiv.exe");

        Object p = TcpHelper.GetAllTcpConnections().Where(t => t.ProcessId == process.Id);
        BagManager bagManager = new BagManager(process, "english");

        return bagManager;

    }

    public byte[] getBytes(Object str)
    {
        int size = Marshal.SizeOf(str);
        byte[] arr = new byte[size];

        IntPtr ptr = Marshal.AllocHGlobal(size);
        Marshal.StructureToPtr(str, ptr, true);
        Marshal.Copy(ptr, arr, 0, size);
        Marshal.FreeHGlobal(ptr);
        return arr;
    }

*/
    }


}
