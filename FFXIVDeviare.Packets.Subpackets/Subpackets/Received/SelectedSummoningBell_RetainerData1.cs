using System;

namespace FFXIVDeviare.Packets.Subpackets.Received
{
    public class SelectedSummoningBell_RetainerData1 : Subpacket
    {
        public override Int32 SubpacketId => 427;
        
        public override Type PacketDataFormatType => typeof(Data);

        public unsafe struct Data
        {
            
            // recv
            // 427 - Select summoning bell - get a list of retainers
            // 72 bytes
#pragma warning disable 649
            public UInt32 incrementingValue1{get;set;}
            public UInt32 unk2{get;set;}
            public UInt32 retainerId{get;set;}
            public UInt32 unkMagicData{get;set;}
            public UInt32 incrementingValue2{get;set;}
            public UInt32 marketboardItemCount{get;set;}
            public Byte unk7{get;set;}
            public Byte unk8{get;set;}
            public Byte unk9{get;set;}
            public Byte unk10{get;set;}
            public UInt32 marketBoardSaleEndDate{get;set;}
            public Byte unk12{get;set;}
            public Byte unk13{get;set;}
            public fixed Byte name[34];
            public UInt32 unk15 { get; set; }
#pragma warning restore 649
        };
    }
}
