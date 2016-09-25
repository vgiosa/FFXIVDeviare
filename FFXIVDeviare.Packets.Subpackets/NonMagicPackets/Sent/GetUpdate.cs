using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVDeviare.Packets.Subpackets.NonMagicPackets.Sent
{
    public unsafe struct GetUpdate
    {
        public fixed byte magic[16];
        public UInt32 Unk1 { get; set; }
        public UInt32 Unk2 { get; set; }
        public UInt32 FullSize { get; set; }
        public UInt16 Unk3 { get; set; }
        public byte Flag1 { get; set; }
        public byte Flag2 { get; set; }
        public byte Flag3 { get; set; }
        public byte Flag4 { get; set; }
        public UInt32 Unk4 { get; set; }
        public UInt16 Unk5 { get; set; }
        public UInt32 SubSize { get; set; }
        public UInt32 Unk6 { get; set; }
        public UInt32 Unk7 { get; set; }
        public UInt32 Unk8 { get; set; }
        public UInt32 CharacterId { get; set; }
        public UInt32 Timestamp { get; set; }

        public static GetUpdate ReadStruct(byte[] data)
        {
            fixed (byte* pb = &data[0])
            {
                return *(GetUpdate*)pb;
            }
        }
    }
}
