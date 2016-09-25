using System.Collections.Generic;
using FFXIVDeviare.GameData.Enums;
using ProtoBuf;

namespace FFXIVDeviare.GameData.Models
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class Recipe
    {
        public int Id { get; set; }

        public uint Item { get; set; }

        public int Level { get; set; }

        public int DisplayLevel { get; set; }

        public int RequiredCraftsmanship { get; set; }

        public int RequiredControl { get; set; }

        public int Yield { get; set; }

        public Job Job { get; set; }

        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        public int RequiredAura { get; set; }

        public uint RequiredEquipment { get; set; }

        public int Difficulty { get; set; }

        public int Quality { get; set; }

        public int Durability { get; set; }
    }
}
