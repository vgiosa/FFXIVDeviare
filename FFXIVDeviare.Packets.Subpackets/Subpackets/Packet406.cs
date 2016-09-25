using System;

namespace FFXIVDeviare.Packets.Subpackets
{
    public class Packet406 : Subpacket
    {
        public override Int32 SubpacketId => 406;
        
        public override Type PacketDataFormatType => typeof(packet_subpacket_content_406);

        unsafe struct packet_subpacket_content_406
        {
            
            // sent AND recv
            // action
#pragma warning disable 649
            public byte isDebuff;       //0000 (0 or 1)
            public byte unk2;           //0001 (normally always 1)
            public byte unk3;           //0002 (0 or... something, normally 0)
            public byte unk4;           //0003 (0 or... something, normally 0)
            public UInt32 actionId;     //0004
            public UInt32 actionCount;  //0008 (increments)
            public UInt16 heading;      //000C (6 and 7 are identical)
            public UInt16 facing;       //000E (6 and 7 are identical)
            public UInt32 actionTarget; //0010
            public UInt32 unk10;        //0014 (0)
            public UInt32 unk11;        //0018 (0)
            public UInt32 unk12;        //001C (0 or... not?)
#pragma warning restore 649
        };

    }
}
