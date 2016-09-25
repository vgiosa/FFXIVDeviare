using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVDeviare.Packets.Subpackets.Subpackets.Received
{
    class UnkPacket413 : Subpacket
    {


        public override Int32 SubpacketId => 413;

        public override Type PacketDataFormatType => typeof(Data);

        unsafe struct Data
        {


#pragma warning disable 649

            public UInt32 Unk1 { get; set; }
            public UInt32 Unk2 { get; set; }
            public UInt32 Unk3 { get; set; }
            public UInt32 Unk4 { get; set; }
            public UInt32 Unk5 { get; set; }
            public UInt32 Unk6 { get; set; }
            public UInt32 Unk7 { get; set; }
            public UInt32 Unk8 { get; set; }
            public UInt32 Unk9 { get; set; }
            public UInt32 Unk10 { get; set; }
            public UInt32 Unk11 { get; set; }
            public UInt32 Unk12 { get; set; }
            public UInt32 Unk13 { get; set; }
            public UInt32 Unk14 { get; set; }
            public UInt32 Unk15 { get; set; }
            public UInt32 Unk16 { get; set; }
            

#pragma warning restore 649

        };
    }
}

