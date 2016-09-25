using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVDeviare.Packets.Subpackets.Subpackets.Sent.ExpandedPackets
{
    public class PlayerSetTarget : Subpacket
    {


        public override Int32 SubpacketId => 401;

        public override Int32 ExpandedId => 3;

        public override Type PacketDataFormatType => typeof(Data);

        public unsafe struct Data
        {


#pragma warning disable 649

            public UInt32 ExpandedId { get; set; }
            public UInt32 TargetId { get; set; }
            public UInt32 Unk3 { get; set; }
            public UInt32 Unk4 { get; set; } 
            public UInt32 Unk5 { get; set; }
            public UInt32 Unk6 { get; set; }
            public UInt32 Unk7 { get; set; }
            public UInt32 Unk8 { get; set; }

#pragma warning restore 649

        };
    }
}

