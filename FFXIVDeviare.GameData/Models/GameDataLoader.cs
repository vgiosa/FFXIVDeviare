using System;
using System.Collections.Generic;
using FFXIVDeviare.GameData.Utility;
using ProtoBuf;

namespace FFXIVDeviare.GameData.Models
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class GameDataLoader
    {
        private static readonly string path = Environment.CurrentDirectory + "/gamedata.bin";

        private static readonly Lazy<GameDataLoader> instance = new Lazy<GameDataLoader>(() => Loader.ProtoLoadClass<GameDataLoader>(path));

        public static GameDataLoader Instance => instance.Value;

        public Dictionary<int, Item> Items { get; set; } = new Dictionary<int, Item>();

        public Dictionary<int, Recipe> Recipes { get; set; } = new Dictionary<int, Recipe>();

        public Dictionary<int, Action> Actions { get; set; } = new Dictionary<int, Action>();
    }
}
