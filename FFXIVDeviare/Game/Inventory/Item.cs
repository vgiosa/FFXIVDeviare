using FFXIVDeviare.Packets.Subpackets;
using FFXIVDeviare.Packets.Subpackets.Subpackets.Sent.ExpandedPackets;
using FFXIVDeviare.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVDeviare.Game.Inventory
{
    public class Item
    {
        InventoryManager InventoryHandler { get; }
        public UInt16 BagId { get; set; }
        public UInt16 Slot { get; set; }
        public UInt32 Quantity { get; set; }
        public UInt32 ItemId { get; set; }
        public UInt32 Unk3 { get; set; }
        public UInt32 CrafterIdMaybe { get; set; }
        public UInt32 ItemFlags { get; set; }
        public Byte IsHQ { get; set; }
        public UInt16 Durability { get; set; }
        public UInt16 Soulbind { get; set; }
        public UInt32 Glamour { get; set; }
        public UInt16 Materia1 { get; set; }
        public UInt16 Materia2 { get; set; }
        public UInt16 Materia3 { get; set; }
        public UInt16 Materia4 { get; set; }
        public UInt16 Materia5 { get; set; }
        public Byte MateriaLevel1 { get; set; }
        public Byte MateriaLevel2 { get; set; }
        public Byte MateriaLevel3 { get; set; }
        public Byte MateriaLevel4 { get; set; }
        public Byte MateriaLevel5 { get; set; }




        public Item (InventoryManager inventoryManager)
        {
            InventoryHandler = inventoryManager;
        }



        public void Repair()
        {
            if (this.Durability >= 30000) return;

            NPCRepair npcRepair = new NPCRepair();
            NPCRepair.Data npcRepairData = new NPCRepair.Data();
            npcRepairData.ExpandedId = 1600;
            npcRepairData.BagId = this.BagId;
            npcRepairData.Slot = this.Slot;
            npcRepairData.ItemId = this.IsHQ == 1 ? uint.Parse("10" + this.ItemId.ToString()) : this.ItemId; 
            npcRepair.PacketData = npcRepairData;

            InventoryHandler.GameHandler.SendPacket(new List<Subpacket>() { npcRepair });

            
        }

    }
}
