using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVDeviare.Packets.Subpackets.Sent
{

    public class BuyItemFromMarketBoard : Subpacket
    {

        public override int SubpacketId
        {
            get { return 263; }
        }

        public override SubpacketType Type => SubpacketType.MarketBoard;
        public override Type PacketDataFormatType
        {
            get
            {
                return typeof(Data);
            }
        }

        public unsafe struct Data
        {

            // buy item

            public UInt32 retainerId { get; set; } // grab from 261 0008
            public UInt32 magic_7864343 { get; set; } // magic 7864343
            public UInt32 postId { get; set; } // grab from 261 0004
            public UInt32 OtherToken { get; set; } // grab from 261 0004  
            public UInt32 itemId { get; set; } // item id
            public UInt32 quantity { get; set; } // grab from 261 0028
            public UInt32 price { get; set; } // grab from 261 0020
            public UInt32 QVal_1 { get; set; } // grab from 261 0024
            public UInt16 Token { get; set; } // grab from 261 0036
            public byte hq { get; set; } // grab from 261 006C
            public byte City { get; set; } // grab from 261 006E
            public UInt32 unk12 { get; set; } // always 0

        };

    }

}
