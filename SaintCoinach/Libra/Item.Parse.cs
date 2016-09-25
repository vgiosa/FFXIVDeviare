using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace SaintCoinach.Libra {
    partial class Item {
        #region Fields

        private long[] _BNpcs = new long[0];
        private int[] _ShopENpcs = new int[0];
        private int[] _InstanceContents = new int[0];
        private int[] _Recipes = new int[0];
        private int[] _Quests = new int[0];
        private int[] _ClassJobs = new int[0];
        private int[] _Achievements = new int[0];
      
        private BasicParam[] _BasicParams = new BasicParam[0];
        private BasicParam[] _BasicParamsHq = new BasicParam[0];
        private Action[] _Actions = new Action[0];
        private Action[] _ActionsHq = new Action[0];
        private Bonus[] _Bonuses = new Bonus[0];
        private Bonus[] _BonusesHq = new Bonus[0];

        #endregion

        #region Helper structs
        public class Bonus {
            public int BaseParam;
            public int Value;
        }
        public class BasicParam {
            public string Param;
            public float Value;
        }
        public abstract class Action {
            public int BaseParam;
        }
        public class FixedAction : Action {
            public int Value;
        }
        public class RelativeAction : Action {
            public int Rate;
            public int Limit;
        }
        public class SeriesBonus {
            public string Series;
            public string SpecialBonus;

            public List<KeyValuePair<string, Bonus>> Bonuses = new List<KeyValuePair<string, Bonus>>();
        }
        #endregion

        #region Properties
        #endregion

        #region Parse
        public void Parse() {
            
        }
        private System.Drawing.Color ParseColor(JsonReader reader) {
            if (!reader.Read() || reader.TokenType != JsonToken.StartArray) throw new InvalidOperationException();

            var r = reader.ReadInt32();
            var g = reader.ReadInt32();
            var b = reader.ReadInt32();

            if (!reader.Read() || reader.TokenType != JsonToken.EndArray) throw new InvalidOperationException();

            return System.Drawing.Color.FromArgb(r, g, b);
        }
        private Action[] ParseActions(JsonReader r) {
            if (!r.Read() || r.TokenType != JsonToken.StartArray) throw new InvalidOperationException();

            var values = new List<Action>();
            while (r.Read() && r.TokenType != JsonToken.EndArray) {
                values.Add(ParseAction(r));
            }
            return values.ToArray();
        }
        private Action ParseAction(JsonReader r) {
            if (r.TokenType != JsonToken.StartObject) throw new InvalidOperationException();
            if (!r.Read() || r.TokenType != JsonToken.PropertyName) throw new InvalidOperationException();

            var paramKey = Convert.ToInt32(r.Value);

            if (!r.Read()) throw new InvalidOperationException();

            Action value;
            if (r.TokenType == JsonToken.StartObject) {
                var act = new RelativeAction { BaseParam = paramKey };

                while (r.Read() && r.TokenType != JsonToken.EndObject) {
                    if (r.TokenType != JsonToken.PropertyName) throw new InvalidOperationException();

                    switch (r.Value.ToString()) {
                        case "rate":
                            act.Rate = r.ReadInt32();
                            break;
                        case "limit":
                            act.Limit = r.ReadInt32();
                            break;
                        default:
                            Console.Error.WriteLine("Unknown 'Item'.'action' data key: {0}", r.Value);
                            throw new NotSupportedException();
                    }
                }

                value = act;
            } else if (r.TokenType == JsonToken.Integer || r.TokenType == JsonToken.String) {
                value = new FixedAction { BaseParam = paramKey, Value = Convert.ToInt32(r.Value) };
            } else
                throw new InvalidOperationException();


            if (!r.Read() || r.TokenType != JsonToken.EndObject) throw new InvalidOperationException();

            return value;
        }
        private Bonus[] ParseBonuses(JsonReader r) {
            if (!r.Read() || r.TokenType != JsonToken.StartArray) throw new InvalidOperationException();

            var values = new List<Bonus>();
            while (r.Read() && r.TokenType != JsonToken.EndArray) {
                values.Add(ParseBonus(r));
            }
            return values.ToArray();
        }
        private Bonus ParseBonus(JsonReader r) {
            if (r.TokenType != JsonToken.StartObject) throw new InvalidOperationException();
            if (!r.Read() || r.TokenType != JsonToken.PropertyName) throw new InvalidOperationException();

            var key = Convert.ToInt32(r.Value);
            var value = r.ReadInt32();

            if (!r.Read() || r.TokenType != JsonToken.EndObject) throw new InvalidOperationException();

            return new Bonus { BaseParam = key, Value = value };
        }
        private BasicParam[] ParseBasicParam(JsonReader r) {
            if (!r.Read() || r.TokenType != JsonToken.StartArray) throw new InvalidOperationException();

            var values = new List<BasicParam>();
            while (r.Read() && r.TokenType != JsonToken.EndArray) {
                if (r.TokenType != JsonToken.StartObject) throw new InvalidOperationException();
                if (!r.Read() || r.TokenType != JsonToken.PropertyName) throw new InvalidOperationException();

                var key = r.Value.ToString();
                var value = r.ReadSingle();

                values.Add(new BasicParam { Param = key, Value = value });

                if (!r.Read() || r.TokenType != JsonToken.EndObject) throw new InvalidOperationException();
            }
            return values.ToArray();
        }
        private SeriesBonus ParseSeriesBonuses(JsonReader r) {
            if (!r.Read() || r.TokenType != JsonToken.StartObject) throw new InvalidOperationException();

            var bonus = new SeriesBonus();
            while (r.Read() && r.TokenType != JsonToken.EndObject) {
                if (r.TokenType != JsonToken.PropertyName) throw new InvalidOperationException();

                switch (r.Value.ToString()) {
                    case "SpecialBonus":
                        bonus.SpecialBonus = r.ReadAsString();
                        break;
                    case "Series":
                        bonus.Series = r.ReadAsString();
                        break;
                    case "bonus":
                        ParseSeriesBonus(r, bonus);
                        break;
                    default:
                        Console.Error.WriteLine("Unknown 'Item'.'series_bonus' data key: {0}", r.Value);
                        throw new NotSupportedException();
                }
            }
            return bonus;
        }
        private void ParseSeriesBonus(JsonReader r, SeriesBonus bonus) {
            if (!r.Read() || r.TokenType != JsonToken.StartArray) throw new InvalidOperationException();

            while (r.Read() && r.TokenType != JsonToken.EndArray) {
                if (r.TokenType != JsonToken.StartObject) throw new InvalidOperationException();

                if (!r.Read() || r.TokenType != JsonToken.PropertyName) throw new InvalidOperationException();

                var bonusKey = r.Value.ToString();

                if (!r.Read() || r.TokenType != JsonToken.StartObject) throw new InvalidOperationException();
                if (!r.Read() || r.TokenType != JsonToken.PropertyName) throw new InvalidOperationException();

                var paramKey = Convert.ToInt32(r.Value);
                var value = r.ReadInt32();

                bonus.Bonuses.Add(new KeyValuePair<string, Bonus>(bonusKey, new Bonus { BaseParam = paramKey, Value = value }));

                if (!r.Read() || r.TokenType != JsonToken.EndObject) throw new InvalidOperationException();
                if (!r.Read() || r.TokenType != JsonToken.EndObject) throw new InvalidOperationException();
            }
        }
        #endregion
    }
}
