using System;

namespace FFXIVDeviare.Packets.Subpackets.Received
{
    public class SelectedRetainerAtSummoningBell_RetainerWalletData : Subpacket
    {
        public override Int32 SubpacketId => 435;

        public override Type PacketDataFormatType => typeof(Data);

        unsafe struct Data
        {
            
            // recv
            // 435 - part of the summoning retainer packet chain
            // retainer wallet metadata
            // 32 bytes
#pragma warning disable 649
            public UInt32 incrementingValue{get;set;}
            public UInt32 retainerWalletCode{get;set;}
            public UInt32 retainerGil{get;set;}
            public UInt32 unk4{get;set;}
            public UInt32 unk5{get;set;}
            public UInt32 unk6{get;set;}
            public UInt32 unk7{get;set;}
            public UInt32 unk8{get;set;}
#pragma warning restore 649
        };
    }
}
