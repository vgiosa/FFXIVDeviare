using System;
using System.Runtime.InteropServices;

namespace FFXIVDeviare.Packets.Subpackets.Subpackets.Chat.Received
{
    class WhisperMessageRecv : Subpacket
    {
        public override Int32 SubpacketId => 100;
        public override SubpacketType Type { get { return SubpacketType.Chat; } }
        public override Type PacketDataFormatType => typeof(Data);

        
        unsafe struct Data
        {
#pragma warning disable 649

            UInt32 Unk1 { get; set; }


            UInt32 Unk2 { get; set; }
            Byte Unk3 { get; set; }
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


            fixed byte _unknown[7];

#pragma warning restore 649

        };
    }
}

