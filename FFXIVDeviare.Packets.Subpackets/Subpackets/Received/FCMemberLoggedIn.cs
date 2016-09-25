using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVDeviare.Packets.Subpackets.Subpackets.Received
{
    class FCMemberLoggedIn : Subpacket
    {


        public override Int32 SubpacketId => 270;

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
            public UInt16 Unk7 { get; set; }

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 46)]
            String _FCname;
            public String FCname => _FCname;


            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            String playerName;
            public String PlayerName => playerName;

#pragma warning restore 649

        };
    }
}

