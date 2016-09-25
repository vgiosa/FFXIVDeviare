using System;
using System.Runtime.InteropServices;

namespace FFXIVDeviare.Packets.Subpackets.Received
{
    public class MarketBoardItemSearchResult : Subpacket
    {
        public override Int32 SubpacketId => 268;

        public override SubpacketType Type => SubpacketType.MarketBoard;
        public override Type PacketDataFormatType => typeof(Data);











        public unsafe struct Data
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public SearchResultListing[] Listings;
            public UInt32 unk1 { get; set; }
            public UInt32 unk11 { get; set; }
            public UInt32 unk111 { get; set; }
            public UInt32 unk1111 { get; set; }

        }
        
        public unsafe struct SearchResultListing
        {

            public UInt32 ItemId { get; set; }
            public UInt16 Count { get; set; }
            public UInt16 Demand { get; set; }


        };







    }
}
