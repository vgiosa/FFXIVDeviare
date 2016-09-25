using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVDeviare.Packets.Subpackets.Subpackets.Received
{
    class FCActivityList : Subpacket
    {


        public override Int32 SubpacketId => 277;

        public override Type PacketDataFormatType => typeof(Data);

        unsafe struct Data
        {


#pragma warning disable 649
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
            public UInt32 Unk51 { get; set; }
            public UInt32 Unk52 { get; set; }
            public UInt32 Unk53 { get; set; }
            public UInt32 Unk54 { get; set; }
            public UInt32 Unk55 { get; set; }
            public UInt32 Unk56 { get; set; }
            public UInt32 Unk57 { get; set; }
            public UInt32 Unk58 { get; set; }
            public UInt32 Unk59 { get; set; }
            public UInt32 Unk60 { get; set; }
            public UInt32 Unk61 { get; set; }
            public UInt32 Unk62 { get; set; }
            public UInt32 Unk63 { get; set; }
            public UInt32 Unk64 { get; set; }
            public UInt32 Unk65 { get; set; }
            public UInt32 Unk66 { get; set; }
            public UInt32 Unk67 { get; set; }
            public UInt32 Unk68 { get; set; }
            public UInt32 Unk69 { get; set; }
            public UInt32 Unk70 { get; set; }
            public UInt32 Unk71 { get; set; }
            public UInt32 Unk72 { get; set; }
            public UInt32 Unk73 { get; set; }
            public UInt32 Unk74 { get; set; }
            public UInt32 Unk75 { get; set; }
            public UInt32 Unk76 { get; set; }
            public UInt32 Unk77 { get; set; }
            public UInt32 Unk78 { get; set; }
            public UInt32 Unk79 { get; set; }
            public UInt32 Unk80 { get; set; }
            public UInt32 Unk81 { get; set; }
            public UInt32 Unk82 { get; set; }
            public UInt32 Unk83 { get; set; }
            public UInt32 Unk84 { get; set; }
            public UInt32 Unk85 { get; set; }
            public UInt32 Unk86 { get; set; }
            public UInt32 Unk87 { get; set; }
            public UInt32 Unk88 { get; set; }
            public UInt32 Unk89 { get; set; }
            public UInt32 Unk90 { get; set; }
            public UInt32 Unk91 { get; set; }
            public UInt32 Unk92 { get; set; }
            public UInt32 Unk93 { get; set; }
            public UInt32 Unk94 { get; set; }
            public UInt32 Unk95 { get; set; }
            public UInt32 Unk96 { get; set; }
            public UInt32 Unk97 { get; set; }
            public UInt32 Unk98 { get; set; }
            public UInt32 Unk99 { get; set; }
            public UInt32 Unk100 { get; set; }
            public UInt32 Unk101 { get; set; }
            public UInt32 Unk102 { get; set; }
            public UInt32 Unk103 { get; set; }
            public UInt32 Unk104 { get; set; }
            public UInt32 Unk105 { get; set; }
            public UInt32 Unk106 { get; set; }
            public UInt32 Unk107 { get; set; }
            public UInt32 Unk108 { get; set; }
            public UInt32 Unk109 { get; set; }
            public UInt32 Unk110 { get; set; }
            public UInt32 Unk111 { get; set; }
            public UInt32 Unk112 { get; set; }
            public UInt32 Unk113 { get; set; }
            public UInt32 Unk114 { get; set; }
            public UInt32 Unk115 { get; set; }
            public UInt32 Unk116 { get; set; }
            public UInt32 Unk117 { get; set; }
            public UInt32 Unk118 { get; set; }
            public UInt32 Unk119 { get; set; }
            public UInt32 Unk120 { get; set; }
            public UInt32 Unk121 { get; set; }
            public UInt32 Unk122 { get; set; }
            public UInt32 Unk123 { get; set; }
            public UInt32 Unk124 { get; set; }
            public UInt32 Unk125 { get; set; }
            public UInt32 Unk126 { get; set; }
            public UInt32 Unk127 { get; set; }
            public UInt32 Unk128 { get; set; }
            public UInt32 Unk129 { get; set; }
            public UInt32 Unk130 { get; set; }
            public UInt32 Unk131 { get; set; }
            public UInt32 Unk132 { get; set; }




#pragma warning restore 649

        };
    }
}

