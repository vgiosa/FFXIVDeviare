using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVDeviare.Packets.Subpackets.Subpackets.Sent.ExpandedPackets
{
    public class RetainerSetItemPrice : Subpacket
    {


        public override Int32 SubpacketId => 401;

        public override Int32 ExpandedId => 400;

        public override Type PacketDataFormatType => typeof(Data);

        public unsafe struct Data
        {


#pragma warning disable 649

            public UInt32 ExpandedId { get; set; }
            public UInt32 SlotId { get; set; }
            public UInt32 Price { get; set; }
            public UInt32 Always0_1 { get; set; } 
            public UInt32 Always0_2 { get; set; }
            public UInt32 Unk1 { get; set; }
            public UInt32 Always0_3 { get; set; }
            public UInt32 Always0_4 { get; set; }

#pragma warning restore 649

        };
    }
}

