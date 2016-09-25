using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVDeviare.Packets.Subpackets.Subpackets.Sent.ExpandedPackets
{
    public class AssignClassToRetainer : Subpacket
    {


        public override Int32 SubpacketId => 480;
        public override Int32 ExpandedId => 286;

        public override Type PacketDataFormatType => typeof(Data);

        public unsafe struct Data
        {

            // this is a sent packet
            // it's triggered via interactions with objects: market boards / summoning bells / npcs / aetherytes / probably other things
            // 16 bytes

#pragma warning disable 649

            public UInt32 Unk1 { get; set; }
            public UInt16 Unk2 { get; set; }
            public UInt16 ExpandedId { get; set; }
            public UInt32 JobCode { get; set; }
            //400 = set, 368 = reset
            public UInt32 ActionCode { get; set; }



#pragma warning restore 649

        };
    }
}

