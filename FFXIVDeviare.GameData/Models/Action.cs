using FFXIVDeviare.GameData.Enums;
using ProtoBuf;

namespace FFXIVDeviare.GameData.Models
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class Action
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Job Job { get; set; }

        public int Level { get; set; }

        public int Range { get; set; }

        public int EffectRange { get; set; }

        public int CastMs { get; set; }

        public int RecastMs { get; set; }

        public bool CanTargetSelf { get; set; }

        public bool CanTargetParty { get; set; }

        public bool CanTargetFriendly { get; set; }

        public bool CanTargetHostile { get; set; }

        public bool CanTargetArea { get; set; }

        public bool TargetArea { get; set; }
    }
}
