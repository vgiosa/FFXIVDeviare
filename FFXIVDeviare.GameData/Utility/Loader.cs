using System;
using System.IO;
using Newtonsoft.Json;
using ProtoBuf;

namespace FFXIVDeviare.GameData.Utility
{
    public static class Loader
    {
        public static T ProtoLoad<T>(string path)
        {
            if (path == null || !File.Exists(path)) { return default(T); }

            var obj = default(T);
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                try { obj = Serializer.Deserialize<T>(stream); }
                catch (Exception e) { Console.WriteLine(e); }
            }

            return obj;
        }

        public static T ProtoLoadClass<T>(string path) where T : new()
        {
            var obj = new T();
            if (!File.Exists(path)) { return obj; }

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                try { obj = Serializer.Deserialize<T>(stream); }
                catch (Exception e) { Console.WriteLine(e); }
            }

            return obj;
        }

        public static void ProtoSave<T>(string path, T data)
        {
            string directory = Path.GetDirectoryName(path);
            if (directory == null) { return; }
            if (!Directory.Exists(directory)) { Directory.CreateDirectory(directory); }

            if (File.Exists(path))
            {
                try { File.Delete(path); }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return;
                }
            }

            using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                try { Serializer.Serialize(stream, data); }
                catch (Exception e) { Console.WriteLine(e); }
            }
        }

        public static T FromBytes<T>(byte[] data)
        {
            var obj = default(T);
            using (var stream = new MemoryStream(data))
            {
                try { obj = Serializer.Deserialize<T>(stream); }
                catch (Exception e) { Console.WriteLine(e); }
            }

            return obj;
        }

        public static byte[] ToBytes<T>(T obj)
        {
            byte[] data;
            using (var stream = new MemoryStream())
            {
                try { Serializer.Serialize(stream, obj); }
                catch (Exception e) { Console.WriteLine(e); }

                data = stream.ToArray();
            }

            return data;
        }

        public static T Clone<T>(T obj)
        {
            var data = ToBytes(obj);
            return FromBytes<T>(data);
        }

        public static void JsonSave<T>(string path, T data)
        {
            string directory = Path.GetDirectoryName(path);
            if (directory == null) { return; }
            if (!Directory.Exists(directory)) { Directory.CreateDirectory(directory); }

            if (File.Exists(path))
            {
                try { File.Delete(path); }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return;
                }
            }

            try
            {
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(path, json);
            }
            catch (Exception e) { Console.WriteLine(e); }
        }

        public static T JsonLoadClass<T>(string path) where T : new()
        {
            var obj = JsonLoad<T>(path);
            if (obj == null) { return new T(); }
            return obj.Equals(default(T)) ? new T() : obj;
        }

        public static T JsonLoad<T>(string path)
        {
            var obj = default(T);
            if (!File.Exists(path)) { return obj; }

            try { obj = JsonConvert.DeserializeObject<T>(File.ReadAllText(path)); }
            catch (Exception e) { Console.WriteLine(e); }
            return obj;
        }
    }
}
