using System;

namespace FFXIVDeviare.Packets.Subpackets.Received
{
    public class BagContents : Subpacket
    {
        public override Int32 SubpacketId => 431;
        public override SubpacketType Type { get { return SubpacketType.Inventory; } }
        public override Type PacketDataFormatType => typeof(Data);
        
        public unsafe struct Data
        {
            
            // sent AND recv
            // 431 - Selected retainer at summoning bell
            // 64 bytes
#pragma warning disable 649
            public UInt32 unkIncrementingNumber{get;set;}
            public UInt32 unk2{get;set;}
            public UInt16 BagId{get;set;}
            public UInt16 Slot{get;set;}
            public UInt32 Quantity{get;set;}
            public UInt32 ItemId{get;set;}
            public UInt32 Unk3{get;set;}
            public UInt32 CrafterIdMaybe{get;set;}
            public UInt32 ItemFlags{get;set;}
            public Byte IsHQ{get;set;}
            public Byte Unk4{get;set;}
            public UInt16 Durability { get; set; }
            public UInt16 Soulbind { get; set; }
            public UInt32 Glamour { get; set; }
            public UInt16 Unk5 { get; set; }
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

#pragma warning restore 649
        };
    }
}
