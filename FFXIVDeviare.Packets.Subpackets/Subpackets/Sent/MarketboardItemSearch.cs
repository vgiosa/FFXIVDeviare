using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVDeviare.Packets.Subpackets.Subpackets.Sent
{
    class MarketboardItemSearch : Subpacket
    {


        public override Int32 SubpacketId => 265;
        public override SubpacketType Type { get { return SubpacketType.MarketBoard; } }
        public override Type PacketDataFormatType => typeof(Data);

        unsafe struct Data
        {


#pragma warning disable 649

            public UInt32 StartIndex { get; set; }
            public UInt32 Unk0 { get; set; }
            public UInt16 Level { get; set; }
            
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
            String _name;
            public String SearchTerm => _name;
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


#pragma warning restore 649

        };
    }
}

