using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace SaintCoinach.Libra {
    partial class ENpcResident {
        #region Fields
        private Tuple<int, System.Drawing.Point[]>[] _Coordinates;

        private Tuple<int, int[]>[] _Shops;
        #endregion

        #region Properties
        public IEnumerable<int> AsQuestClient { get { Parse(); return null; } }
        public IEnumerable<Tuple<int, System.Drawing.Point[]>> Coordinates { get { Parse(); return _Coordinates; } }
        public IEnumerable<int> Quests { get { Parse(); return null; } }
        public IEnumerable<Tuple<int, int[]>> Shops { get { Parse(); return _Shops; } }
        #endregion

        #region Parse
        public void Parse() {
        
        }

        private void ParseCoordinate(JsonTextReader reader) {
            if (!reader.Read() || reader.TokenType != JsonToken.StartObject)
                throw new InvalidOperationException();

            var allCoord = new List<Tuple<int, System.Drawing.Point[]>>();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject) {
                if (reader.TokenType != JsonToken.PropertyName) throw new InvalidOperationException();

                var key = Convert.ToInt32(reader.Value);

                if (!reader.Read() || reader.TokenType != JsonToken.StartArray) throw new InvalidOperationException();

                var coordinates = new List<System.Drawing.Point>();
                while (reader.Read() && reader.TokenType != JsonToken.EndArray) {
                    if (reader.TokenType != JsonToken.StartArray) throw new InvalidOperationException();

                    if (!reader.Read() || reader.TokenType != JsonToken.Integer) throw new InvalidOperationException();
                    var x = Convert.ToInt32(reader.Value);

                    if (!reader.Read() || reader.TokenType != JsonToken.Integer) throw new InvalidOperationException();
                    var y = Convert.ToInt32(reader.Value);

                    if (!reader.Read() || reader.TokenType != JsonToken.EndArray) throw new InvalidOperationException();

                    coordinates.Add(new System.Drawing.Point(x, y));
                }

                allCoord.Add(Tuple.Create(key, coordinates.ToArray()));
            }

            _Coordinates = allCoord.ToArray();
        }
        private void ParseShops(JsonTextReader reader) {
            if (!reader.Read() || reader.TokenType != JsonToken.StartArray) throw new InvalidOperationException();

            var shops = new List<Tuple<int, int[]>>();
            while (reader.Read() && reader.TokenType != JsonToken.EndArray) {
                if (reader.TokenType != JsonToken.StartObject) throw new InvalidOperationException();
                if (!reader.Read() || reader.TokenType != JsonToken.PropertyName) throw new InvalidOperationException();

                var key = Convert.ToInt32(reader.Value);

                var items = new List<int>();
                if (!reader.Read() || reader.TokenType != JsonToken.StartArray) throw new InvalidOperationException();
                while (reader.Read() && reader.TokenType != JsonToken.EndArray) {
                    if (reader.TokenType != JsonToken.StartObject) throw new InvalidOperationException();
                    if (!reader.Read() || reader.TokenType != JsonToken.PropertyName) throw new InvalidOperationException();

                    var itemKey = Convert.ToInt32(reader.Value);

                    if (!reader.Read() || reader.TokenType != JsonToken.StartArray) throw new InvalidOperationException();
                    while (reader.Read() && reader.TokenType != JsonToken.EndArray) { }

                    items.Add(itemKey);

                    if (!reader.Read() || reader.TokenType != JsonToken.EndObject) throw new InvalidOperationException();
                }

                shops.Add(Tuple.Create(key, items.ToArray()));

                if (!reader.Read() || reader.TokenType != JsonToken.EndObject) throw new InvalidOperationException();
            }

            _Shops = shops.ToArray();
        }
        #endregion

    }
}
