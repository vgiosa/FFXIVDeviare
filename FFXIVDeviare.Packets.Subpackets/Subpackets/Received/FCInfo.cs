using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVDeviare.Packets.Subpackets.Subpackets.Received
{
    class FCInfo : Subpacket
    {


        public override Int32 SubpacketId => 273;

        public override Type PacketDataFormatType => typeof(Data);

        unsafe struct Data
        {


#pragma warning disable 649

            public UInt32 Unk1 { get; set; }
            public UInt32 Unk2 { get; set; }
            public UInt64 FC_ID { get; set; }
            
            public UInt32 Unk5 { get; set; }
            public UInt32 Unk6 { get; set; }
            public UInt32 Unk7 { get; set; }
            public UInt32 Unk8 { get; set; }
            public UInt32 Unk9 { get; set; }
            public UInt32 CreatedTimestamp { get; set; }
            public UInt32 Unk10 { get; set; }
            public UInt16 TotalMembers { get; set; }
            public UInt16 MembersOnline { get; set; }
            public UInt32 Unk11 { get; set; }
            public UInt32 Unk12 { get; set; }
            public UInt16 Rank { get; set; }

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 22)]
            String fcName;
            public String FCName => fcName;



            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
            String fcTag;
            public String FCTag => fcTag;


            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            String fcLeader;
            public String FCLeader => fcLeader;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 193)]
            String fcSlogan;
            public String FCSlogan => fcSlogan;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 24)]
            String fcHouseName;
            public String FCHouseName => fcHouseName;

#pragma warning restore 649

        };
    }
}

