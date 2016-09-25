using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVDeviare.Packets.Subpackets.Subpackets.Sent.ExpandedPackets
{
    public class StartCraftInCrafterStance : Subpacket
    {


        public override Int32 SubpacketId => 472;

        public override Int32 ExpandedId => 4;

        public override Type PacketDataFormatType => typeof(Data);

        public unsafe struct Data
        {


#pragma warning disable 649

            public UInt32 Unk1 { get; set; }
            public UInt32 Unk2 { get; set; }
            public UInt32 ExpandedId { get; set; }
            UInt32 RecipeKey { get; set; }
            
            public string Recipe {
                get {

                    return ((SaintCoinach.Xiv.Item)Packet.RealmData.GameData.GetSheet("Recipe")[(int)RecipeKey].SourceRow["Item{Result}"]).Name;

                }
            }
            
            public UInt32 Unk5 { get; set; }
            public UInt32 Unk6 { get; set; }

#pragma warning restore 649

        };
    }
}

