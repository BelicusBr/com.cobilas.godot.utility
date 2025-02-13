using Godot;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Cobilas.IO.Serialization.Json;

using IOPath = System.IO.Path;
using IOFile = System.IO.File;
using IODirectory = System.IO.Directory;
using SYSEnvironment = System.Environment;
using Cobilas.GodotEngine.Utility.Runtime;

namespace Cobilas.GodotEngine.Utility.EditorSerialization;
/// <summary>Class to handle property caching.</summary>
public static class SerializationCache {
    private static readonly char[] separator = { '/' };
    private static readonly List<string> _cache = [];
    /// <summary>Gets the value of the property that is cached.</summary>
    /// <param name="info">Contains information about the SerializedObject.</param>
    /// <param name="propertyName">The name of the property.</param>
    /// <param name="value">The value of the property that is cached.</param>
    /// <returns>Returns <c>true</c> when the value is retrieved from the cache.</returns>
    public static bool GetValueInCache(SNInfo info, string? propertyName, out string value) {
        if (propertyName is null) throw new ArgumentNullException(nameof(propertyName));
        //string res = $"res://cache/{(RunTime.ExecutionMode == ExecutionMode.PlayerMode ? "player" : "editor")}";

        value = string.Empty;
        using GDDirectory directory = GDDirectory.GetGDDirectory("res://cache");
        GDFile file = directory.GetFile($"id_{GetID(info)}.cache");
        if (file == GDIONull.FileNull) {
            SetValueInCache(info, propertyName, value = string.Empty);
            return false;
        }
        Dictionary<string, string>? cache = Json.Deserialize<Dictionary<string, string>>(file.Read());

        if (cache is not null && cache.TryGetValue(propertyName, out value)) return true;
        return false;
    }
    /// <summary>Caches the property value.</summary>
    /// <param name="info">Contains information about the SerializedObject.</param>
    /// <param name="propertyName">The name of the property.</param>
    /// <param name="value">The property value that will be cached.</param>
    /// <returns>Returns <c>true</c> when the property value is cached.</returns>
    public static bool SetValueInCache(SNInfo info, string? propertyName, object? value) {
        if (propertyName is null) throw new ArgumentNullException(nameof(propertyName));
        if (string.IsNullOrEmpty((string)info[1])) return false;
        CreateFileCache($"id_{info["id"]}.cache");

        using GDDirectory directory = GDDirectory.GetGDDirectory("res://cache");
        GDFile file = directory.GetFile($"id_{info["id"]}.cache");
        Dictionary<string, string>? cache = Json.Deserialize<Dictionary<string, string>>(file.Read());
        if (cache is null) return false;
        if (!cache.ContainsKey("nodePath"))
            cache.Add("nodePath", (string)info[1]);
        if (!cache.ContainsKey(propertyName))
            cache.Add(propertyName, value is null ? string.Empty : value.ToString());
        else cache[propertyName] = value is null ? string.Empty : value.ToString();
        file.Write(Encoding.UTF8.GetBytes(Json.Serialize(cache)));
        return true;
    }

    private static string GetID(SNInfo info) {
        if (RunTime.ExecutionMode == ExecutionMode.EditorMode) return (string)info[0];
        else if (_cache.Contains((string)info[0])) return (string)info[0];
        _cache.Add((string)info[0]);
        using GDDirectory directory = GDDirectory.GetGDDirectory("res://cache");
        List<Dictionary<string, string>> list = [];
        foreach (GDFile file in directory.GetFiles()) {
            Dictionary<string, string>? cache = Json.Deserialize<Dictionary<string, string>>(file.Read());
            if (cache is null) continue;
            foreach (string? item in cache["nodePath"].Split(separator, StringSplitOptions.RemoveEmptyEntries))
                if (item == "EditorNode") {
                    list.Add(cache);
                    break;
                }
        }
        string[] path = ((string)info[1]).Split(separator, StringSplitOptions.RemoveEmptyEntries);
        foreach (Dictionary<string, string> item in list) {
            string cop = item["nodePath"];
            int num1 = 0;
            foreach (string item2 in path)
                if (cop.Contains(item2))
                    num1++;
            if (num1 == path.Length) {
                if (GDFeature.HasEditor) {
                    Dictionary<string, string> clone = new(item) {
                        ["nodePath"] = (string)info[1]
                    };
                    RefreshFileCache($"id_{info[0]}.cache", clone);
                }
                return cop.StringHash();
            }
        }
        return string.Empty;
    }

    private static void RefreshFileCache(string fileName, Dictionary<string, string> json) {
        string dir = IOPath.Combine(SYSEnvironment.CurrentDirectory, "cache", fileName);
        if (IOFile.Exists(dir)) {
            using FileStream stream = IOFile.Create(dir);
            stream.Write(Json.Serialize(json));
        } else CreateFileCache(fileName, json);
    }

    private static void CreateFileCache(string fileName) => CreateFileCache(fileName, []);

    private static void CreateFileCache(string fileName, Dictionary<string, string> json) {
        string dir = IOPath.Combine(SYSEnvironment.CurrentDirectory, "cache");
        
        fileName = IOPath.Combine(dir, fileName);

        if (!IODirectory.Exists(dir))
            IODirectory.CreateDirectory(dir);

        if (!IOFile.Exists(fileName)) {
            using FileStream stream = IOFile.Create(fileName);
            stream.Write(Json.Serialize(json));
        }
    }
}
