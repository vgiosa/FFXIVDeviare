using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVDeviare.Packets.Subpackets.Subpackets.Received
{
    class CraftActionResponse : Subpacket
    {


        public override Int32 SubpacketId => 455;
        public override SubpacketType Type { get { return SubpacketType.Crafting; } }
        public override Type PacketDataFormatType => typeof(Data);

        unsafe struct Data
        {


#pragma warning disable 649
            public UInt32 PlayerID { get; set; }
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
            public UInt32 ActionUsed { get; set; }
            public UInt32 CPAdded { get; set; }
            public UInt32 Step { get; set; }
            public UInt32 Progress { get; set; }
            public UInt32 ProgressAdded { get; set; }
            public UInt32 Quality { get; set; }
            public UInt32 QualityAdded { get; set; }
            public UInt32 HQPercent { get; set; }
            public UInt32 Durability { get; set; }
            public UInt16 Unk21 { get; set; }
            public UInt16 Unk21_2 { get; set; }
            public UInt32 State { get; set; }
            public UInt32 PreviousState { get; set; }
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




#pragma warning restore 649

        };
    }
}

