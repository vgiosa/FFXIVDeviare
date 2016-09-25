using System;
using System.Runtime.InteropServices;

namespace FFXIVDeviare.Packets.Subpackets.Received
{

    public class MarketBoardDataForItem : Subpacket
    {

        public override int SubpacketId
        {
            get { return 261; }
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
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public MarketListing[] Listings;
            public byte unk1 { get; set; }
            public byte unk11 { get; set; }
            public byte unk111 { get; set; }
            public byte unk1111 { get; set; }
            public byte unk2 { get; set; }
            public byte unk22 { get; set; }
            public byte unk222 { get; set; }
            public byte unk2222 { get; set; }

        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi) ]
        public unsafe struct MarketListing
        {

            // 112 bytes per
            // market list retainer [10]

            public UInt32 postId { get; set; }
            public UInt16 OtherToken { get; set; } 
            public UInt16 OtherToken2 { get; set; }                   

            public UInt32 retainerId { get; set; }
            public UInt32 unk4 { get; set; }                    //000C
            public UInt32 sellerId { get; set; }                    //0010
            public UInt32 unk6 { get; set; }                    //0014
            public UInt32 crafterId { get; set; }
            public UInt32 unk8 { get; set; }
            public UInt32 price { get; set; }
            public UInt32 qval_1 { get; set; }
            public UInt32 qty { get; set; }
            public UInt32 itemId { get; set; }
            public UInt32 timestamp { get; set; }
            public UInt16 unk14 { get; set; }
            public UInt16 Token { get; set; }
            public UInt16 unk17 { get; set; }
            public UInt16 unk18 { get; set; }
            public UInt16 materia1 { get; set; }
            public UInt16 materia2 { get; set; }
            public UInt16 materia3 { get; set; }
            public UInt16 materia4 { get; set; }
            public UInt16 materia5 { get; set; }
            public UInt16 unk19 { get; set; }
            public UInt32 unk20 { get; set; }

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            String _name;
            public String name => _name;

            public Byte hq { get; set; }
            public Byte materiaCount { get; set; }
            public Byte city { get; set; }
            public Byte dye { get; set; }

        };


    }
}
