using System;

namespace FFXIVDeviare.Packets.Subpackets.Received
{
    public class SelectedSummoningBell_RetainerData2 : Subpacket
    {
        public override Int32 SubpacketId => 426;
        
        public override Type PacketDataFormatType => typeof(Data);
        
        unsafe struct Data
        {
            
            // recv
            // 426 - part of the summoning retainer packet chain
            // retainer list metadata
            // 8 bytes
#pragma warning disable 649
            public UInt32 incrementingValue{get;set;}
            public Byte maxRetainerCount{get;set;} // always 8?
            public Byte currentRetainerCount{get;set;}
            public Byte unk4{get;set;}
            public Byte unk5{get;set;}
#pragma warning restore 649
        };
    }
}
