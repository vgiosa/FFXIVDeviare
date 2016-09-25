using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVDeviare.Packets.Subpackets
{
    class UnknownSubpacket : Subpacket
    {

        public struct UnknownBodyData
        {
            public bool IsSent { get; set; }
            public int BodySize { get; set; }
            public int Id { get; set; }
        }

        public override Type PacketDataFormatType
        {
            get
            {
                return null;
            }
        }


        

        public override int SubpacketId
        {
            get
            {
                return 0;
            }
        }
    }
}
