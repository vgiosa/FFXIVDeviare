using FFXIVDeviare.Game.Events;
using FFXIVDeviare.Packets;
using FFXIVDeviare.Packets.Subpackets;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVDeviare.Game.Inventory
{



    public enum BagType
    {
        Bag1 = 0,
        Bag2 = 1,
        Bag3 = 2,
        Bag4 = 3,
        EquippedItems = 1000,
        Currency = 2000,
        Crystals = 2001,
        KeyItems = 2004,
        Armory_MainHand = 3500,
        Armory_OffHand = 3200,
        Armory_Helmet = 3201,
        Armory_Chest = 3202,
        Armory_Glove = 3203,
        Armory_Belt = 3204,
        Armory_Pants = 3205,
        Armory_Boots = 3206,
        Armory_Earrings = 3207,
        Armory_Necklace = 3208,
        Armory_Writs = 3209,
        Armory_Rings = 3300,
        Armory_Souls = 3400,
        Retainer_Page1 = 10000,
        Retainer_Page2 = 10001,
        Retainer_Page3 = 10002,
        Retainer_Page4 = 10003,
        Retainer_Page5 = 10004,
        Retainer_Page6 = 10005,
        Retainer_Page7 = 10006,
        Retainer_EquippedItems = 11000,
        Retainer_Gil = 12000,
        Retainer_Crystals = 12001,
        Retainer_Market = 12002,
        GrandCompany_Page1 = 20000,
        GrandCompany_Page2 = 20001,
        GrandCompany_Page3 = 20002,
        GrandCompany_Gil = 22000,
        GrandCompany_Crystals = 22001
    }

    public class InventoryManager
    {
        public uint IncrementingBagOpValue = 0;
        public bool IncrementingBagOpValueSet = false;
        public GameManager GameHandler { get; }
        public InventoryManager(GameManager g)
        {
            GameHandler =  g;
            GameHandler.InventoryPacketCaptured += HandleInventoryPacket;
        }
        public ConcurrentDictionary<int, ConcurrentDictionary<int, Item>> Inventory = new ConcurrentDictionary<int, ConcurrentDictionary<int, Item>>();



        public int GetFirstAvailableRetainerSaleSlot()
        {
            if(Inventory.ContainsKey((int)BagType.Retainer_Market) && Inventory[(int)BagType.Retainer_Market] != null)
            {

                var retainerMarket = Inventory[(int)BagType.Retainer_Market];
                
                for (int x = 0; x < 20; x++)
                {
                    if (retainerMarket.ContainsKey(x)) continue;
                    else return x;
                }
            }
            return -1;
        }

        private void HandleInventoryPacket(object e, GameEventArgs args)
        {
            
            Packet p = args.Packet;
            var bags = p.Subpackets.OfType<Packets.Subpackets.Received.BagContents>().GroupBy(s => ((Packets.Subpackets.Received.BagContents.Data)s.PacketData).BagId, s => s.PacketData, (key, g) => new { BagId = key, BagContents = g.OfType<Packets.Subpackets.Received.BagContents.Data>() });
            foreach(var result in bags)
            {
                if(!Inventory.ContainsKey(result.BagId)) Inventory.TryAdd(result.BagId, null);
                ConcurrentDictionary<int, Item> newBag = new ConcurrentDictionary<int, Item>();
                foreach(var slotItem in result.BagContents)
                {

                    newBag.TryAdd(slotItem.Slot, new Item(this)
                    {
                        BagId = slotItem.BagId,
                        CrafterIdMaybe = slotItem.CrafterIdMaybe,
                        Durability = slotItem.Durability,
                        Glamour = slotItem.Glamour,
                        IsHQ = slotItem.IsHQ,
                        ItemFlags = slotItem.ItemFlags,
                        ItemId = slotItem.ItemId,
                        Materia1 = slotItem.Materia1,
                        Materia2 = slotItem.Materia2,
                        Materia3 = slotItem.Materia3,
                        Materia4 = slotItem.Materia4,
                        Materia5 = slotItem.Materia5,
                        MateriaLevel1 = slotItem.MateriaLevel1,
                        MateriaLevel2 = slotItem.MateriaLevel2,
                        MateriaLevel3 = slotItem.MateriaLevel3,
                        MateriaLevel4 = slotItem.MateriaLevel4,
                        MateriaLevel5 = slotItem.MateriaLevel5,
                        Quantity = slotItem.Quantity,
                        Slot = slotItem.Slot,
                        Soulbind = slotItem.Soulbind
                    });
                }
                Inventory[result.BagId] = newBag;
            }


            var slotUpdates = p.Subpackets.OfType<Packets.Subpackets.Received.InventoryUpdate>();
            foreach (var subPacketResult in slotUpdates)
            {
                var result = (Packets.Subpackets.Received.InventoryUpdate.Data) subPacketResult.PacketData;
                if (!Inventory.ContainsKey(result.BagId))
                {
                    Inventory.TryAdd(result.BagId, new ConcurrentDictionary<int, Item>());
                }
                

                

                Item updatedSlot = new Item(this)
                {

                    BagId = result.BagId,
                    CrafterIdMaybe = result.CrafterIdMaybe,
                    Durability = result.Durability,
                    Glamour = result.Glamour,
                    ItemFlags = result.CraftedFlags,
                    IsHQ = result.hq,
                    ItemId = result.ID,
                    Materia1 = result.Materia1,
                    Materia2 = result.Materia2,
                    Materia3 = result.Materia3,
                    Materia4 = result.Materia4,
                    Materia5 = result.Materia5,
                    MateriaLevel1 = result.MateriaLevel1,
                    MateriaLevel2 = result.MateriaLevel2,
                    MateriaLevel3 = result.MateriaLevel3,
                    MateriaLevel4 = result.MateriaLevel4,
                    MateriaLevel5 = result.MateriaLevel5,
                    Slot = result.Slot,
                    Soulbind = result.Soulbind,
                    Quantity = result.Count,
                };




                if (!Inventory[result.BagId].ContainsKey(updatedSlot.Slot)) Inventory[result.BagId].TryAdd(updatedSlot.Slot, null);
                Inventory[result.BagId][updatedSlot.Slot] = updatedSlot;

            }



            var bagOps = p.Subpackets.OfType<Packets.Subpackets.Sent.BagOperation>();
            foreach(var sp in bagOps)
            {
                UInt32 incValue = ((Packets.Subpackets.Sent.BagOperation.Data)(sp.PacketData)).incrementingValue;
                if (incValue > IncrementingBagOpValue)
                {
                    IncrementingBagOpValue = incValue;
                    IncrementingBagOpValueSet = true;
                }
            }



        }

  
    }
}
