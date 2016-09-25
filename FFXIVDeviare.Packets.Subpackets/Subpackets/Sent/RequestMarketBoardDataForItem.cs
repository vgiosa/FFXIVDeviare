using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVDeviare.Packets.Subpackets.Sent
{

    public  class RequestMarketBoardDataForItem : Subpacket
    {

        public override SubpacketType Type => SubpacketType.MarketBoard;
        public override int SubpacketId
        {
            get { return 260; }
        }

        public override Type PacketDataFormatType
        {
            get
            {
                return typeof(Data);
            }
        }

        public unsafe struct Data
        {

            // item mark data request
            #pragma warning disable 649

            public UInt32 itemId{get;set;}          //
            public UInt32 unk2{get;set;}            //uhh, 2306 for 1622 somehow? maybe group? who knows
            
            #pragma warning restore 649
        };

    }

}
