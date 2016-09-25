using ProtoBuf;

namespace FFXIVDeviare.GameData.Models
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class Ingredient
    {
        public int Id { get; set; }

        public int Amount { get; set; }

        public bool IsCrystal { get; set; }

        public int HqQualityGain { get; set; }
    }
}
