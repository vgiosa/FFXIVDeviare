using System;

namespace FFXIVDeviare.Packets.Subpackets.Sent
{
    public class RetainerMenuOption_ViewSaleHistory : Subpacket
    {
        public override Int32 SubpacketId => 264;

        public override Type PacketDataFormatType => typeof(Data);

        unsafe struct Data
        {

            // sent or recv?
            // buy item
            #pragma warning disable 649
            public UInt32 itemId{get;set;}   //0000 (Some Id which is in the item list)
            public UInt32 qty{get;set;}      //0004 (Unknown static number, maybe area, or retainer, something - 7864343 (0x780017), also seems to be in item list (pId 261))
            public UInt32 price{get;set;}    //0008 (Honestly no Idea, could be some kind of timestamp thing for all I know)
            public UInt32 unk4{get;set;}     //000C (Another unknown static, unknown 1277952 (0x138000))
            public UInt32 unk5{get;set;}     //0010
            public UInt32 unk6{get;set;}     //0014
            public UInt32 unk7{get;set;}     //0018
            public UInt32 fee{get;set;}      //001C
            public UInt16 unk9{get;set;}     //0020
            public byte unk10{get;set;}      //0022 (0 or 1)
            public byte city{get;set;}       //0023 (Limsa = 1, GrIdania = 2, Ul'dah = 3)
            public UInt32 always0{get;set;}  //0024 (Seems to always be zero)
            #pragma warning restore 649
        };
    }
}
