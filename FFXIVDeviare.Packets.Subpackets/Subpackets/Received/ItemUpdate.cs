using System;

namespace FFXIVDeviare.Packets.Subpackets.Received
{
    public class EntrustOrWithdrawGilFromRetainer_PlayerWalletData : Subpacket
    {
        public override Int32 SubpacketId => 434;
        public override SubpacketType Type { get { return SubpacketType.Inventory; } }
        public override Type PacketDataFormatType => typeof(Data);
        
        unsafe struct Data
        {
            
            // recv
            // 434 - Entrust or withdraw gil packet chain
            // 48 bytes
#pragma warning disable 649
            public UInt32 incrementingNumber{get;set;}
            public UInt32 unk2{get;set;}
            public UInt32 PlayerId{get;set;}
            public UInt32 BagId{get;set;}
            public UInt16 Slot { get; set; }
            public UInt16 unk5 { get; set; }
            public UInt32 Count{get;set;}
            public UInt32 unk7{get;set;}
            public UInt32 unk8{get;set;}
            public UInt32 unk9{get;set;}
            public UInt32 unk10{get;set;}
            public UInt32 unk11{get;set;}
            public UInt32 unk12{get;set;}
#pragma warning restore 649
        }
    }
}
