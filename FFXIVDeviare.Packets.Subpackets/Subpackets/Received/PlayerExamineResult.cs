using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVDeviare.Packets.Subpackets.Subpackets.Received
{
    class PlayerExamineResult : Subpacket
    {


        public override Int32 SubpacketId => 422;

        public override Type PacketDataFormatType => typeof(Data);

        unsafe struct Data
        {


#pragma warning disable 649



            public Byte Unk1 { get; set; }
            public Byte Unk2 { get; set; }
            public Byte JobCode { get; set; }
            public UInt16 Level { get; set; }
            public UInt32 Unk3 { get; set; }
            public UInt32 Unk4 { get; set; }
            public UInt16 Unk5 { get; set; }
            public UInt16 Unk6 { get; set; }
            public UInt16 Unk7 { get; set; }
            public UInt16 Unk8 { get; set; }
            public UInt32 Unk9 { get; set; }
            public UInt32 Unk10 { get; set; }
            public UInt16 Unk11 { get; set; }
            public Byte Unk20 { get; set; }
            public UInt16 Unk21 { get; set; }
            public UInt16 Unk22 { get; set; }
            public UInt32 Unk23 { get; set; }
            public UInt16 Unk24 { get; set; }
            public UInt16 Unk25 { get; set; }
            public UInt32 Unk26 { get; set; }
            public UInt32 Unk27 { get; set; }
            public UInt32 Unk28 { get; set; }
            public UInt32 Unk29 { get; set; }

            public UInt32 MainHand { get; set; }
            public UInt32 MainHandGlamour { get; set; }
            public UInt32 MainHandWeirdCrafterId { get; set; }
            public UInt32 MainHandCrafterFlag { get; set; }
            public byte MainHandIsHQ { get; set; }
            public byte MainHandDyeColor { get; set; }
            public UInt16 MainHandMateriaType1 { get; set; }
            public UInt16 MainHandMateriaLevel1 { get; set; }
            public UInt16 MainHandMateriaType2 { get; set; }
            public UInt16 MainHandMateriaLevel2 { get; set; }
            public UInt16 MainHandMateriaType3 { get; set; }
            public UInt16 MainHandMateriaLevel3 { get; set; }
            public UInt16 MainHandMateriaType4 { get; set; }
            public UInt16 MainHandMateriaLevel4 { get; set; }
            public UInt16 MainHandMateriaType5 { get; set; }
            public UInt16 MainHandMateriaLevel5 { get; set; }
            public UInt16 MainHandUnk { get; set; }


            public UInt32 OffHand { get; set; }
            public UInt32 OffHandGlamour { get; set; }
            public UInt32 OffHandWeirdCrafterId { get; set; }
            public UInt32 OffHandCrafterFlag { get; set; }
            public byte OffHandIsHQ { get; set; }
            public byte OffHandDyeColor { get; set; }
            public UInt16 OffHandMateriaType1 { get; set; }
            public UInt16 OffHandMateriaLevel1 { get; set; }
            public UInt16 OffHandMateriaType2 { get; set; }
            public UInt16 OffHandMateriaLevel2 { get; set; }
            public UInt16 OffHandMateriaType3 { get; set; }
            public UInt16 OffHandMateriaLevel3 { get; set; }
            public UInt16 OffHandMateriaType4 { get; set; }
            public UInt16 OffHandMateriaLevel4 { get; set; }
            public UInt16 OffHandMateriaType5 { get; set; }
            public UInt16 OffHandMateriaLevel5 { get; set; }
            public UInt16 OffHandUnk { get; set; }

            public UInt32 Head { get; set; }
            public UInt32 HeadGlamour { get; set; }
            public UInt32 HeadWeirdCrafterId { get; set; }
            public UInt32 HeadCrafterFlag { get; set; }
            public byte HeadIsHQ { get; set; }
            public byte HeadDyeColor { get; set; }
            public UInt16 HeadMateriaType1 { get; set; }
            public UInt16 HeadMateriaLevel1 { get; set; }
            public UInt16 HeadMateriaType2 { get; set; }
            public UInt16 HeadMateriaLevel2 { get; set; }
            public UInt16 HeadMateriaType3 { get; set; }
            public UInt16 HeadMateriaLevel3 { get; set; }
            public UInt16 HeadMateriaType4 { get; set; }
            public UInt16 HeadMateriaLevel4 { get; set; }
            public UInt16 HeadMateriaType5 { get; set; }
            public UInt16 HeadMateriaLevel5 { get; set; }
            public UInt16 HeadUnk { get; set; }

            public UInt32 Body { get; set; }
            public UInt32 BodyGlamour { get; set; }
            public UInt32 BodyWeirdCrafterId { get; set; }
            public UInt32 BodyCrafterFlag { get; set; }
            public byte BodyIsHQ { get; set; }
            public byte BodyDyeColor { get; set; }
            public UInt16 BodyMateriaType1 { get; set; }
            public UInt16 BodyMateriaLevel1 { get; set; }
            public UInt16 BodyMateriaType2 { get; set; }
            public UInt16 BodyMateriaLevel2 { get; set; }
            public UInt16 BodyMateriaType3 { get; set; }
            public UInt16 BodyMateriaLevel3 { get; set; }
            public UInt16 BodyMateriaType4 { get; set; }
            public UInt16 BodyMateriaLevel4 { get; set; }
            public UInt16 BodyMateriaType5 { get; set; }
            public UInt16 BodyMateriaLevel5 { get; set; }
            public UInt16 BodyUnk { get; set; }

            public UInt32 Hand { get; set; }
            public UInt32 HandGlamour { get; set; }
            public UInt32 HandWeirdCrafterId { get; set; }
            public UInt32 HandCrafterFlag { get; set; }
            public byte HandIsHQ { get; set; }
            public byte HandDyeColor { get; set; }
            public UInt16 HandMateriaType1 { get; set; }
            public UInt16 HandMateriaLevel1 { get; set; }
            public UInt16 HandMateriaType2 { get; set; }
            public UInt16 HandMateriaLevel2 { get; set; }
            public UInt16 HandMateriaType3 { get; set; }
            public UInt16 HandMateriaLevel3 { get; set; }
            public UInt16 HandMateriaType4 { get; set; }
            public UInt16 HandMateriaLevel4 { get; set; }
            public UInt16 HandMateriaType5 { get; set; }
            public UInt16 HandMateriaLevel5 { get; set; }
            public UInt16 HandUnk { get; set; }

            public UInt32 Waist { get; set; }
            public UInt32 WaistGlamour { get; set; }
            public UInt32 WaistWeirdCrafterId { get; set; }
            public UInt32 WaistCrafterFlag { get; set; }
            public byte WaistIsHQ { get; set; }
            public byte WaistDyeColor { get; set; }
            public UInt16 WaistMateriaType1 { get; set; }
            public UInt16 WaistMateriaLevel1 { get; set; }
            public UInt16 WaistMateriaType2 { get; set; }
            public UInt16 WaistMateriaLevel2 { get; set; }
            public UInt16 WaistMateriaType3 { get; set; }
            public UInt16 WaistMateriaLevel3 { get; set; }
            public UInt16 WaistMateriaType4 { get; set; }
            public UInt16 WaistMateriaLevel4 { get; set; }
            public UInt16 WaistMateriaType5 { get; set; }
            public UInt16 WaistMateriaLevel5 { get; set; }
            public UInt16 WaistUnk { get; set; }


            public UInt32 Leg { get; set; }
            public UInt32 LegGlamour { get; set; }
            public UInt32 LegWeirdCrafterId { get; set; }
            public UInt32 LegCrafterFlag { get; set; }
            public byte LegIsHQ { get; set; }
            public byte LegDyeColor { get; set; }
            public UInt16 LegMateriaType1 { get; set; }
            public UInt16 LegMateriaLevel1 { get; set; }
            public UInt16 LegMateriaType2 { get; set; }
            public UInt16 LegMateriaLevel2 { get; set; }
            public UInt16 LegMateriaType3 { get; set; }
            public UInt16 LegMateriaLevel3 { get; set; }
            public UInt16 LegMateriaType4 { get; set; }
            public UInt16 LegMateriaLevel4 { get; set; }
            public UInt16 LegMateriaType5 { get; set; }
            public UInt16 LegMateriaLevel5 { get; set; }
            public UInt16 LegUnk { get; set; }

            public UInt32 Feet { get; set; }
            public UInt32 FeetGlamour { get; set; }
            public UInt32 FeetWeirdCrafterId { get; set; }
            public UInt32 FeetCrafterFlag { get; set; }
            public byte FeetIsHQ { get; set; }
            public byte FeetDyeColor { get; set; }
            public UInt16 FeetMateriaType1 { get; set; }
            public UInt16 FeetMateriaLevel1 { get; set; }
            public UInt16 FeetMateriaType2 { get; set; }
            public UInt16 FeetMateriaLevel2 { get; set; }
            public UInt16 FeetMateriaType3 { get; set; }
            public UInt16 FeetMateriaLevel3 { get; set; }
            public UInt16 FeetMateriaType4 { get; set; }
            public UInt16 FeetMateriaLevel4 { get; set; }
            public UInt16 FeetMateriaType5 { get; set; }
            public UInt16 FeetMateriaLevel5 { get; set; }
            public UInt16 FeetUnk { get; set; }


            public UInt32 Earring { get; set; }
            public UInt32 EarringGlamour { get; set; }
            public UInt32 EarringWeirdCrafterId { get; set; }
            public UInt32 EarringCrafterFlag { get; set; }
            public byte EarringIsHQ { get; set; }
            public byte EarringDyeColor { get; set; }
            public UInt16 EarringMateriaType1 { get; set; }
            public UInt16 EarringMateriaLevel1 { get; set; }
            public UInt16 EarringMateriaType2 { get; set; }
            public UInt16 EarringMateriaLevel2 { get; set; }
            public UInt16 EarringMateriaType3 { get; set; }
            public UInt16 EarringMateriaLevel3 { get; set; }
            public UInt16 EarringMateriaType4 { get; set; }
            public UInt16 EarringMateriaLevel4 { get; set; }
            public UInt16 EarringMateriaType5 { get; set; }
            public UInt16 EarringMateriaLevel5 { get; set; }
            public UInt16 EarringUnk { get; set; }


            public UInt32 Necklace { get; set; }
            public UInt32 NecklaceGlamour { get; set; }
            public UInt32 NecklaceWeirdCrafterId { get; set; }
            public UInt32 NecklaceCrafterFlag { get; set; }
            public byte NecklaceIsHQ { get; set; }
            public byte NecklaceDyeColor { get; set; }
            public UInt16 NecklaceMateriaType1 { get; set; }
            public UInt16 NecklaceMateriaLevel1 { get; set; }
            public UInt16 NecklaceMateriaType2 { get; set; }
            public UInt16 NecklaceMateriaLevel2 { get; set; }
            public UInt16 NecklaceMateriaType3 { get; set; }
            public UInt16 NecklaceMateriaLevel3 { get; set; }
            public UInt16 NecklaceMateriaType4 { get; set; }
            public UInt16 NecklaceMateriaLevel4 { get; set; }
            public UInt16 NecklaceMateriaType5 { get; set; }
            public UInt16 NecklaceMateriaLevel5 { get; set; }
            public UInt16 NecklaceUnk { get; set; }


            public UInt32 Bracelet { get; set; }
            public UInt32 BraceletGlamour { get; set; }
            public UInt32 BraceletWeirdCrafterId { get; set; }
            public UInt32 BraceletCrafterFlag { get; set; }
            public byte BraceletIsHQ { get; set; }
            public byte BraceletDyeColor { get; set; }
            public UInt16 BraceletMateriaType1 { get; set; }
            public UInt16 BraceletMateriaLevel1 { get; set; }
            public UInt16 BraceletMateriaType2 { get; set; }
            public UInt16 BraceletMateriaLevel2 { get; set; }
            public UInt16 BraceletMateriaType3 { get; set; }
            public UInt16 BraceletMateriaLevel3 { get; set; }
            public UInt16 BraceletMateriaType4 { get; set; }
            public UInt16 BraceletMateriaLevel4 { get; set; }
            public UInt16 BraceletMateriaType5 { get; set; }
            public UInt16 BraceletMateriaLevel5 { get; set; }
            public UInt16 BraceletUnk { get; set; }


            public UInt32 Ring1 { get; set; }
            public UInt32 Ring1Glamour { get; set; }
            public UInt32 Ring1WeirdCrafterId { get; set; }
            public UInt32 Ring1CrafterFlag { get; set; }
            public byte Ring1IsHQ { get; set; }
            public byte Ring1DyeColor { get; set; }
            public UInt16 Ring1MateriaType1 { get; set; }
            public UInt16 Ring1MateriaLevel1 { get; set; }
            public UInt16 Ring1MateriaType2 { get; set; }
            public UInt16 Ring1MateriaLevel2 { get; set; }
            public UInt16 Ring1MateriaType3 { get; set; }
            public UInt16 Ring1MateriaLevel3 { get; set; }
            public UInt16 Ring1MateriaType4 { get; set; }
            public UInt16 Ring1MateriaLevel4 { get; set; }
            public UInt16 Ring1MateriaType5 { get; set; }
            public UInt16 Ring1MateriaLevel5 { get; set; }
            public UInt16 Ring1Unk { get; set; }


            public UInt32 Ring2 { get; set; }
            public UInt32 Ring2Glamour { get; set; }
            public UInt32 Ring2WeirdCrafterId { get; set; }
            public UInt32 Ring2CrafterFlag { get; set; }
            public byte Ring2IsHQ { get; set; }
            public byte Ring2DyeColor { get; set; }
            public UInt16 Ring2MateriaType1 { get; set; }
            public UInt16 Ring2MateriaLevel1 { get; set; }
            public UInt16 Ring2MateriaType2 { get; set; }
            public UInt16 Ring2MateriaLevel2 { get; set; }
            public UInt16 Ring2MateriaType3 { get; set; }
            public UInt16 Ring2MateriaLevel3 { get; set; }
            public UInt16 Ring2MateriaType4 { get; set; }
            public UInt16 Ring2MateriaLevel4 { get; set; }
            public UInt16 Ring2MateriaType5 { get; set; }
            public UInt16 Ring2MateriaLevel5 { get; set; }
            public UInt16 Ring2Unk { get; set; }

            public UInt32 SoulCrystal { get; set; }
            public UInt32 SoulCrystalGlamour { get; set; }
            public UInt32 SoulCrystalWeirdCrafterId { get; set; }
            public UInt32 SoulCrystalCrafterFlag { get; set; }
            public byte SoulCrystalIsHQ { get; set; }
            public byte SoulCrystalDyeColor { get; set; }
            public UInt16 SoulCrystalMateriaType1 { get; set; }
            public UInt16 SoulCrystalMateriaLevel1 { get; set; }
            public UInt16 SoulCrystalMateriaType2 { get; set; }
            public UInt16 SoulCrystalMateriaLevel2 { get; set; }
            public UInt16 SoulCrystalMateriaType3 { get; set; }
            public UInt16 SoulCrystalMateriaLevel3 { get; set; }
            public UInt16 SoulCrystalMateriaType4 { get; set; }
            public UInt16 SoulCrystalMateriaLevel4 { get; set; }
            public UInt16 SoulCrystalMateriaType5 { get; set; }
            public UInt16 SoulCrystalMateriaLevel5 { get; set; }
            public UInt16 SoulCrystalUnk { get; set; }


            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            String name;
            public String PlayerName => name;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            String psnGamerTag;
            public String PSNGamerTag => psnGamerTag;

            public fixed byte BucketOfFuckIt[312];

#pragma warning restore 649

        };
    }
}

