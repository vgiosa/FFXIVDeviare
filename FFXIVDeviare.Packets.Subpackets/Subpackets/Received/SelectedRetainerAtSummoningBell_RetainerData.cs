using System;

namespace FFXIVDeviare.Packets.Subpackets.Received
{
    public class SelectedRetainerAtSummoningBell_RetainerData : Subpacket
    {
        public override Int32 SubpacketId => 267; 

        public override Type PacketDataFormatType => typeof(Data);

        unsafe struct Data
        {

            // recv
            // 267 - part of the summoning retainer packet chain - could be part of a different chain considering it's in the 200s
            // it seems like 16777216 and 4 are entry codes and 2164260864 and 3 are exit codes
            // 56 bytes
#pragma warning disable 649
            public UInt32 retainerId{get;set;}
            public UInt32 magicNumber7864343{get;set;}
            public UInt32 unk3{get;set;}
            public UInt32 unk4{get;set;}
            public UInt32 unk5{get;set;}
            public Byte unk6{get;set;}
            public fixed Byte name[35];
#pragma warning restore 649
        };
    }
}
