using System;

namespace FFXIVDeviare.Packets.Subpackets.Received
{
    public class InventoryUpdate : Subpacket
    {
        public override Int32 SubpacketId => 438;
        public override SubpacketType Type { get { return SubpacketType.Inventory; } }
        public override Type PacketDataFormatType => typeof(Data);
        
        public unsafe struct Data
        {

            // recv
            // inventory update
#pragma warning disable 649
            public UInt16 UnkShort1 { get; set; }
            public UInt16 UnkShort2{get;set;}
            public UInt32 UnkInt1 { get; set; }
            public UInt16 BagId{get;set;}
            public UInt16 Slot{get;set;}
            public UInt32 Count{get;set;}
            public UInt32 ID{get;set;}



            public UInt16 UnkShort3{get;set;}
            public UInt16 UnkShort4 { get; set; }
            public UInt32 CrafterIdMaybe { get; set; }
            public UInt32 CraftedFlags{get;set;}
            public byte hq{get;set;}
            public Byte Unk4 { get; set; }
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
            public Byte UnkByte1 { get; set; }
            public Byte UnkByte2 { get; set; }
            public Byte UnkByte3 { get; set; }
            public Byte UnkByte4 { get; set; }
            public Byte UnkByte5 { get; set; }
#pragma warning restore 649
        }
    }
}
