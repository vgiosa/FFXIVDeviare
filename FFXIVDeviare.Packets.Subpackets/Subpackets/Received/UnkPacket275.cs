using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVDeviare.Packets.Subpackets.Subpackets.Received
{
    class UnkPacket275 : Subpacket
    {


        public override Int32 SubpacketId => 275;

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
            public UInt32 Unk17 { get; set; }
            public UInt32 Unk18 { get; set; }
            public UInt32 Unk19 { get; set; }
            public UInt32 Unk20 { get; set; }
            public UInt32 Unk21 { get; set; }
            public UInt32 Unk22 { get; set; }
            public UInt32 Unk23 { get; set; }
            public UInt32 Unk24 { get; set; }
            public UInt32 Unk25 { get; set; }
            public UInt32 Unk26 { get; set; }
            public UInt32 Unk27 { get; set; }
            public UInt32 Unk28 { get; set; }
            public UInt32 Unk29 { get; set; }
            public UInt32 Unk30 { get; set; }
            public UInt32 Unk31 { get; set; }
            public UInt32 Unk32 { get; set; }
            public UInt32 Unk33 { get; set; }
            public UInt32 Unk34 { get; set; }
            public UInt32 Unk35 { get; set; }
            public UInt32 Unk36 { get; set; }
            public UInt32 Unk37 { get; set; }
            public UInt32 Unk38 { get; set; }
            public UInt32 Unk39 { get; set; }
            public UInt32 Unk40 { get; set; }
            public UInt32 Unk41 { get; set; }
            public UInt32 Unk42 { get; set; }
            public UInt32 Unk43 { get; set; }
            public UInt32 Unk44 { get; set; }
            public UInt32 Unk45 { get; set; }
            public UInt32 Unk46 { get; set; }
            public UInt32 Unk47 { get; set; }
            public UInt32 Unk48 { get; set; }
            public UInt32 Unk49 { get; set; }
            public UInt32 Unk50 { get; set; }



#pragma warning restore 649

        };
    }
}

