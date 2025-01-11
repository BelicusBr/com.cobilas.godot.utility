using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Cobilas.IO.Serialization.Json;

namespace Cobilas.GodotEngine.Utility.Serialization;

public static class SerializationCache {
    public static bool GetValueInCache(string? id, string? propertyName, out string value) {
        if (id is null) throw new ArgumentNullException(nameof(id));
        else if (propertyName is null) throw new ArgumentNullException(nameof(propertyName));

        value = string.Empty;
        using GDDirectory directory = GDDirectory.GetGDDirectory("res://cache");
        GDFile file = directory.GetFile($"id_{id}.cache");
        if (file == GDIONull.FileNull) {

        }
        Dictionary<string, string>? cache = Json.Deserialize<Dictionary<string, string>>(file.Read());

        if (cache is not null && cache.TryGetValue(propertyName, out value)) return true;
        return false;
    }

    public static bool SetValueInCache(string? id, string? propertyName, object? value) {
        if (id is null) throw new ArgumentNullException(nameof(id));
        else if (propertyName is null) throw new ArgumentNullException(nameof(propertyName));
        CreateFileCache($"id_{id}.cache");

        using GDDirectory directory = GDDirectory.GetGDDirectory("res://cache");
        GDFile file = directory.GetFile($"id_{id}.cache");
        Dictionary<string, string>? cache = Json.Deserialize<Dictionary<string, string>>(file.Read());
        if (cache is null) return false;
        else if (!cache.ContainsKey(propertyName))
            cache.Add(propertyName, value is null ? string.Empty : value.ToString());
        else cache[propertyName] = value is null ? string.Empty : value.ToString();
        file.Write(Encoding.UTF8.GetBytes(Json.Serialize(cache)));
        return true;
    }

    private static void CreateFileCache(string fileName) {
        string dir = Path.Combine(Environment.CurrentDirectory, "cache");
        fileName = Path.Combine(dir, fileName);

        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        if (!File.Exists(fileName)) {
            using FileStream stream = File.Create(fileName);
            stream.Write(Json.Serialize(new Dictionary<string, string>()));
        }
    }
}
