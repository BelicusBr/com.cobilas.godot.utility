using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Cobilas.IO.Serialization.Json;
using Cobilas.GodotEngine.Utility.Runtime;

namespace Cobilas.GodotEngine.Utility.EditorSerialization;
/// <summary>Class to handle property caching.</summary>
public static class SerializationCache {
    /// <summary>Gets the value of the property that is cached.</summary>
    /// <param name="id">The ID of the cache</param>
    /// <param name="propertyName">The name of the property.</param>
    /// <param name="value">The value of the property that is cached.</param>
    /// <returns>Returns <c>true</c> when the value is retrieved from the cache.</returns>
    public static bool GetValueInCache(string? id, string? propertyName, out string value) {
        if (id is null) throw new ArgumentNullException(nameof(id));
        else if (propertyName is null) throw new ArgumentNullException(nameof(propertyName));

        string res = $"res://cache/{(RunTime.ExecutionMode == ExecutionMode.PlayerMode ? "player" : "editor")}";

        value = string.Empty;
        using GDDirectory directory = GDDirectory.GetGDDirectory(res);
        GDFile file = directory.GetFile($"id_{id}.cache");
        if (file == GDIONull.FileNull) {
            SetValueInCache(id, propertyName, value = string.Empty);
            return false;
        }
        Dictionary<string, string>? cache = Json.Deserialize<Dictionary<string, string>>(file.Read());

        if (cache is not null && cache.TryGetValue(propertyName, out value)) return true;
        return false;
    }
    /// <summary>Caches the property value.</summary>
    /// <param name="id">The ID of the cache</param>
    /// <param name="propertyName">The name of the property.</param>
    /// <param name="value">The property value that will be cached.</param>
    /// <returns>Returns <c>true</c> when the property value is cached.</returns>
    public static bool SetValueInCache(string? id, string? propertyName, object? value) {
        if (id is null) throw new ArgumentNullException(nameof(id));
        else if (propertyName is null) throw new ArgumentNullException(nameof(propertyName));
        CreateFileCache($"id_{id}.cache");

        string res = $"res://cache/{(RunTime.ExecutionMode == ExecutionMode.PlayerMode ? "player" : "editor")}";

        using GDDirectory directory = GDDirectory.GetGDDirectory(res);
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
        dir = Path.Combine(dir, RunTime.ExecutionMode == ExecutionMode.PlayerMode ? "player" : "editor");
        fileName = Path.Combine(dir, fileName);

        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        if (!File.Exists(fileName)) {
            using FileStream stream = File.Create(fileName);
            stream.Write(Json.Serialize(new Dictionary<string, string>()));
        }
    }
}
