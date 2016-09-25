using System;
using System.Runtime.InteropServices;

namespace FFXIVDeviare.Packets.Subpackets.Received
{

    public class MarketBoardHistoryForItem : Subpacket
    {

        public override int SubpacketId
        {
            get { return 265; }
        }

        public override Type PacketDataFormatType
        {
            get
            {
                return typeof(Data);
            }
        }

        public override SubpacketType Type => SubpacketType.MarketBoard;

        public unsafe struct Data
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public HistoryListing[] Listings;
            public byte unk1 { get; set; }
            public byte unk11 { get; set; }
            public byte unk111 { get; set; }
            public byte unk1111 { get; set; }
            public ushort unk2 { get; set; }
            
            public byte unk222 { get; set; }
            public byte unk2222 { get; set; }

        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi) ]
        public unsafe struct HistoryListing
        {

            public UInt32 itemIdOnce {
                get;set;   //0000
            }
            public UInt32 itemId
            {
                get; set;   //0000
            }       //0004
            public UInt32 price
            {
                get; set;   //0000
            }
            public UInt32 timestamp
            {
                get; set;   //0000
            }
            public UInt32 qty
            {
                get; set;   //0000
            }
            public UInt16 hq
            {
                get; set;   //0000
            }

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 30)]
            String _name;
            public String name => _name;

            

        };


    }
}
