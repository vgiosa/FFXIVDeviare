using System;
using System.Runtime.InteropServices;

namespace FFXIVDeviare.Packets.Subpackets.Subpackets.Chat.Received
{
    class GeneralMessageReceived : Subpacket
    {
        public override Int32 SubpacketId => 101;
        public override SubpacketType Type { get { return SubpacketType.Chat; } }
        public override Type PacketDataFormatType => typeof(Data);


        unsafe struct Data
        {
#pragma warning disable 649

            public UInt32 Unk1 { get; set; }
            public UInt32 Unk2 { get; set; }
            public UInt32 Unk3 { get; set; }
            public UInt32 Unk4 { get; set; }
            public UInt32 Unk5 { get; set; }
            public Byte Unk6 { get; set; }

            fixed byte _sender[32];
            public String Sender
            {
                get
                {
                    fixed (byte* psender = _sender)
                        return Marshal.PtrToStringAnsi((IntPtr)psender);
                }
            }

            fixed byte _message[1024];
            public String Message
            {
                get
                {
                    fixed (byte* pmessage = _message)
                        return Marshal.PtrToStringAnsi((IntPtr)pmessage);
                }
            }

            
            fixed byte _unknown[3];

#pragma warning restore 649

        };
    }
}

