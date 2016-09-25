using System;
using System.Runtime.InteropServices;

namespace FFXIVDeviare.Packets.Subpackets.Subpackets.Chat.Sent
{
    class WhisperMessageSent : Subpacket
    {
        public override Int32 SubpacketId => 100;
        public override SubpacketType Type { get { return SubpacketType.Chat; } }
        public override Type PacketDataFormatType => typeof(Data);


        unsafe struct Data
        {
#pragma warning disable 649

            public Byte Unk1 { get; set; }

            fixed byte _sender[32];
            public String Sender
            {
                get
                {
                    fixed (byte* psender = _sender)
                        return Marshal.PtrToStringAnsi((IntPtr)psender);        
                }
            }
            fixed byte _message[1023];
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

