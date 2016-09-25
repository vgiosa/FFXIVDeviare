using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVDeviare.Packets.Subpackets.Subpackets.Received
{
    class UnkPacket100 : Subpacket
    {


        public override Int32 SubpacketId => 100;

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
            public UInt32 Unk133 { get; set; }
            public UInt32 Unk134 { get; set; }
            public UInt32 Unk135 { get; set; }
            public UInt32 Unk136 { get; set; }
            public UInt32 Unk137 { get; set; }
            public UInt32 Unk138 { get; set; }
            public UInt32 Unk139 { get; set; }
            public UInt32 Unk140 { get; set; }
            public UInt32 Unk141 { get; set; }
            public UInt32 Unk142 { get; set; }
            public UInt32 Unk143 { get; set; }
            public UInt32 Unk144 { get; set; }
            public UInt32 Unk145 { get; set; }
            public UInt32 Unk146 { get; set; }
            public UInt32 Unk147 { get; set; }
            public UInt32 Unk148 { get; set; }
            public UInt32 Unk149 { get; set; }
            public UInt32 Unk150 { get; set; }
            public UInt32 Unk151 { get; set; }
            public UInt32 Unk152 { get; set; }
            public UInt32 Unk153 { get; set; }
            public UInt32 Unk154 { get; set; }
            public UInt32 Unk155 { get; set; }
            public UInt32 Unk156 { get; set; }
            public UInt32 Unk157 { get; set; }
            public UInt32 Unk158 { get; set; }
            public UInt32 Unk159 { get; set; }
            public UInt32 Unk160 { get; set; }
            public UInt32 Unk161 { get; set; }
            public UInt32 Unk162 { get; set; }
            public UInt32 Unk163 { get; set; }
            public UInt32 Unk164 { get; set; }
            public UInt32 Unk165 { get; set; }
            public UInt32 Unk166 { get; set; }
            public UInt32 Unk167 { get; set; }
            public UInt32 Unk168 { get; set; }
            public UInt32 Unk169 { get; set; }
            public UInt32 Unk170 { get; set; }
            public UInt32 Unk171 { get; set; }
            public UInt32 Unk172 { get; set; }
            public UInt32 Unk173 { get; set; }
            public UInt32 Unk174 { get; set; }
            public UInt32 Unk175 { get; set; }
            public UInt32 Unk176 { get; set; }
            public UInt32 Unk177 { get; set; }
            public UInt32 Unk178 { get; set; }
            public UInt32 Unk179 { get; set; }
            public UInt32 Unk180 { get; set; }
            public UInt32 Unk181 { get; set; }
            public UInt32 Unk182 { get; set; }
            public UInt32 Unk183 { get; set; }
            public UInt32 Unk184 { get; set; }
            public UInt32 Unk185 { get; set; }
            public UInt32 Unk186 { get; set; }
            public UInt32 Unk187 { get; set; }
            public UInt32 Unk188 { get; set; }
            public UInt32 Unk189 { get; set; }
            public UInt32 Unk190 { get; set; }
            public UInt32 Unk191 { get; set; }
            public UInt32 Unk192 { get; set; }
            public UInt32 Unk193 { get; set; }
            public UInt32 Unk194 { get; set; }
            public UInt32 Unk195 { get; set; }
            public UInt32 Unk196 { get; set; }
            public UInt32 Unk197 { get; set; }
            public UInt32 Unk198 { get; set; }
            public UInt32 Unk199 { get; set; }
            public UInt32 Unk200 { get; set; }
            public UInt32 Unk201 { get; set; }
            public UInt32 Unk202 { get; set; }
            public UInt32 Unk203 { get; set; }
            public UInt32 Unk204 { get; set; }
            public UInt32 Unk205 { get; set; }
            public UInt32 Unk206 { get; set; }
            public UInt32 Unk207 { get; set; }
            public UInt32 Unk208 { get; set; }
            public UInt32 Unk209 { get; set; }
            public UInt32 Unk210 { get; set; }
            public UInt32 Unk211 { get; set; }
            public UInt32 Unk212 { get; set; }
            public UInt32 Unk213 { get; set; }
            public UInt32 Unk214 { get; set; }
            public UInt32 Unk215 { get; set; }
            public UInt32 Unk216 { get; set; }
            public UInt32 Unk217 { get; set; }
            public UInt32 Unk218 { get; set; }
            public UInt32 Unk219 { get; set; }
            public UInt32 Unk220 { get; set; }
            public UInt32 Unk221 { get; set; }
            public UInt32 Unk222 { get; set; }
            public UInt32 Unk223 { get; set; }
            public UInt32 Unk224 { get; set; }
            public UInt32 Unk225 { get; set; }
            public UInt32 Unk226 { get; set; }
            public UInt32 Unk227 { get; set; }
            public UInt32 Unk228 { get; set; }
            public UInt32 Unk229 { get; set; }
            public UInt32 Unk230 { get; set; }
            public UInt32 Unk231 { get; set; }
            public UInt32 Unk232 { get; set; }
            public UInt32 Unk233 { get; set; }
            public UInt32 Unk234 { get; set; }
            public UInt32 Unk235 { get; set; }
            public UInt32 Unk236 { get; set; }
            public UInt32 Unk237 { get; set; }
            public UInt32 Unk238 { get; set; }
            public UInt32 Unk239 { get; set; }
            public UInt32 Unk240 { get; set; }
            public UInt32 Unk241 { get; set; }
            public UInt32 Unk242 { get; set; }
            public UInt32 Unk243 { get; set; }
            public UInt32 Unk244 { get; set; }
            public UInt32 Unk245 { get; set; }
            public UInt32 Unk246 { get; set; }
            public UInt32 Unk247 { get; set; }
            public UInt32 Unk248 { get; set; }
            public UInt32 Unk249 { get; set; }
            public UInt32 Unk250 { get; set; }
            public UInt32 Unk251 { get; set; }
            public UInt32 Unk252 { get; set; }
            public UInt32 Unk253 { get; set; }
            public UInt32 Unk254 { get; set; }
            public UInt32 Unk255 { get; set; }
            public UInt32 Unk256 { get; set; }
            public UInt32 Unk257 { get; set; }
            public UInt32 Unk258 { get; set; }
            public UInt32 Unk259 { get; set; }
            public UInt32 Unk260 { get; set; }
            public UInt32 Unk261 { get; set; }
            public UInt32 Unk262 { get; set; }
            public UInt32 Unk263 { get; set; }
            public UInt32 Unk264 { get; set; }
            public UInt32 Unk265 { get; set; }
            public UInt32 Unk266 { get; set; }
            public UInt32 Unk267 { get; set; }
            public UInt32 Unk268 { get; set; }

#pragma warning restore 649

        };
    }
}

