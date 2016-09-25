using System;

namespace FFXIVDeviare.Packets.Subpackets.Received
{
    public class SelectedRetainerAtSummoningBell_RetainerMarketBoardData2 : Subpacket
    {
        public override Int32 SubpacketId => 428;

        public override Type PacketDataFormatType => typeof(Data);
        
        unsafe struct Data
        {
            
            // recv
            // 428 - part of the summoning retainer packet chain
            // 8 bytes
#pragma warning disable 649
            public UInt32 incrementingValue { get; set; }
            public UInt32 marketBoardItemCount { get; set; }
#pragma warning disable 649
        };
    }
}
