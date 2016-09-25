using System;

namespace FFXIVDeviare.Packets.Subpackets.Received
{
    public class SelectedRetainerAtSummoningBell_RetainerMarketBoardData1 : Subpacket
    {
        public override Int32 SubpacketId => 429;

        public override Type PacketDataFormatType => typeof(Data);
        
        unsafe struct Data
        {
            
            // recv
            // 429 - part of the summoning retainer packet chain
            // retainer market item metadata
            // 24 bytes
#pragma warning disable 649
            public UInt32 incrementingValue{get;set;}
            public UInt32 retainerMarketCode{get;set;}
            public UInt16 currentIteration{get;set;}
            public UInt16 unk4{get;set;}
            public UInt32 unk5{get;set;}
            public UInt32 price{get;set;}
            public UInt32 unk7{get;set;}
#pragma warning restore 649
        };
    }
}
