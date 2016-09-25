using ProtoBuf;

namespace FFXIVDeviare.GameData.Models
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
