using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace SaintCoinach.Xiv {
    public partial class InstanceContentData : IItemSource {
        #region Properties

        public InstanceContent InstanceContent { get; private set; }
        public IEnumerable<Fight> AllBosses { get; private set; }
        public Fight Boss { get; private set; }
        public IEnumerable<Fight> MidBosses { get; private set; }
        public IEnumerable<Treasure> MapTreasures { get; private set; }

        #endregion

        #region Constructor

        public InstanceContentData(InstanceContent instanceContent) {
            
        }
        #endregion

        #region Parse
       

        private void ReadBoss(JsonReader reader) {
            if (!reader.Read() || reader.TokenType != JsonToken.StartObject) throw new InvalidOperationException();

            this.Boss = new Fight(reader, InstanceContent.Sheet.Collection);
        }
        private void ReadMidBosses(JsonReader reader) {
            if (!reader.Read() || reader.TokenType != JsonToken.StartArray) throw new InvalidOperationException();

            var values = new List<Fight>();
            while (reader.Read() && reader.TokenType != JsonToken.EndArray) {
                if (reader.TokenType != JsonToken.StartObject) throw new InvalidOperationException();

                values.Add(new Fight(reader, InstanceContent.Sheet.Collection));
            }
            this.MidBosses = values;
        }
        private void ReadMapTreasures(JsonReader reader) {
            if (!reader.Read() || reader.TokenType != JsonToken.StartArray) throw new InvalidOperationException();

            var values = new List<Treasure>();
            while (reader.Read() && reader.TokenType != JsonToken.EndArray) {
                if (reader.TokenType != JsonToken.StartObject) throw new InvalidOperationException();

                values.Add(new Treasure(reader, InstanceContent.Sheet.Collection));
            }
            this.MapTreasures = values;
        }
        #endregion

        #region IItemSource Members

        private Item[] _ItemSourceItems;
        IEnumerable<Item> IItemSource.Items {
            get {
                if (_ItemSourceItems != null)
                    return _ItemSourceItems;

                IEnumerable<Item> v = new Item[0];

                if (Boss != null) {
                    v = v.Concat(Boss.RewardItems.Select(i => i.Item));
                    v = v.Concat(Boss.Treasures.SelectMany(i => i.Items));
                }
                if (MidBosses != null) {
                    v = v.Concat(MidBosses.SelectMany(f => f.RewardItems.Select(i => i.Item)));
                    v = v.Concat(MidBosses.SelectMany(f => f.Treasures.SelectMany(i => i.Items)));
                }
                if (MapTreasures != null)
                    v.Concat(MapTreasures.SelectMany(i => i.Items));

                return _ItemSourceItems = v.Distinct().ToArray();
            }
        }

        #endregion
    }
}
