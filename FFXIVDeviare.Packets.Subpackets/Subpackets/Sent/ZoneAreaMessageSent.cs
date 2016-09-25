using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVDeviare.Packets.Subpackets.Subpackets.Sent
{
    class ZoneAreaMessageSent : Subpacket
    {


        public override Int32 SubpacketId => 103;

        public override Type PacketDataFormatType => typeof(Data);

        unsafe struct Data
        {


#pragma warning disable 649

            public UInt32 Unk1 { get; set; }
            public UInt32 PlayerId { get; set; }
            public UInt32 Unk2 { get; set; }
            public UInt32 Unk3 { get; set; }
            public UInt32 Unk4 { get; set; }
            public UInt32 Unk5 { get; set; }
            public UInt16 Channel { get; set; }
            fixed byte _message[1030];
            public String Message
            {
                get
                {
                    fixed (byte* pmessage = _message)
                        return Marshal.PtrToStringAnsi((IntPtr)pmessage);
                }
            }


#pragma warning restore 649

        };
    }
}

