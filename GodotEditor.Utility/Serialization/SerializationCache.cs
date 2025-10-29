using Godot;
using System;
using System.IO;
using Cobilas.Collections;
using System.Collections.Generic;
using Cobilas.GodotEngine.Utility;
using Cobilas.IO.Serialization.Json;
using Cobilas.GodotEngine.Utility.IO;
using Cobilas.GodotEngine.Utility.Runtime;

using Cobilas.GodotEngine.Utility.IO.Interfaces;

namespace Cobilas.GodotEditor.Utility.Serialization;
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

        value = string.Empty;
        string id = GetID(info),
               path = GodotPath.Combine(GodotPath.ResPath, "cache", $"id_{id}.cache");

        if (Archive.Exists(path)) {
            using IGodotArchiveStream stream = (IGodotArchiveStream)Archive.Open(path, FileAccess.Read);

			stream.Read(out string stg);
			Dictionary<string, string>? cache = Json.Deserialize<Dictionary<string, string>>(stg);

			if (cache is not null && cache.TryGetValue(propertyName, out value)) return true;
		} else _ = SetValueInCache(info, propertyName, value = string.Empty);

        return false;
    }
    /// <summary>Caches the property value.</summary>
    /// <param name="info">Contains information about the SerializedObject.</param>
    /// <param name="propertyName">The name of the property.</param>
    /// <param name="value">The property value that will be cached.</param>
    /// <returns>Returns <c>true</c> when the property value is cached.</returns>
    public static bool SetValueInCache(SNInfo info, string? propertyName, object? value) {
        if (propertyName is null) throw new ArgumentNullException(nameof(propertyName));
        else if (string.IsNullOrEmpty((string)info[1])) return false;

        string id = (string)info["id"],
               path = GodotPath.Combine(GodotPath.ResPath, "cache", $"id_{id}.cache");
        CreateFileCache($"id_{id}.cache");

        if (Archive.Exists(path)) {
            using IStream stream = Archive.Open(path, FileAccess.ReadWrite);
            stream.AutoFlush = true;
            stream.Read(out string stg);
            Dictionary<string, string>? cache = Json.Deserialize<Dictionary<string, string>>(stg) ?? [];
            if (!cache.ContainsKey("nodePath"))
                cache.Add("nodePath", (string)info[1]);
            if (!cache.ContainsKey(propertyName))
                cache.Add(propertyName, value is null ? string.Empty : value.ToString());
            else cache[propertyName] = value is null ? string.Empty : value.ToString();
			stream.ReplaceBuffer(Json.Serialize(cache));
            return true;
		}
        return false;
    }

    private static string GetID(SNInfo info) {
        if (RunTime.ExecutionMode == ExecutionMode.EditorMode) return (string)info[0];
        else if (_cache.Contains((string)info[0])) return (string)info[0];
        _cache.Add((string)info[0]);

        using IFolderInfo datas = Folder.Open(GodotPath.Combine(GodotPath.ResPath, "cache"));
        List<Dictionary<string, string>> list = [];

        IArchiveInfo[] archives = datas.GetArchives();

        if (ArrayManipulation.EmpytArray(archives)) return string.Empty;
        
        foreach (IArchiveInfo file in archives) {
            using IStream stream = file.Open(FileAccess.Read);
			stream.Read(out string stg);
            Dictionary<string, string>? cache = Json.Deserialize<Dictionary<string, string>>(stg);
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
        string path = GodotPath.Combine(GodotPath.ResPath, "cache", fileName);

        if (Archive.Exists(path)) {
			using IStream stream = Archive.Open(path, FileAccess.Write);
			stream.Write(Json.Serialize(json));
		} else CreateFileCache(fileName, json);
	}

    private static void CreateFileCache(string fileName) => CreateFileCache(fileName, []);

    private static void CreateFileCache(string fileName, Dictionary<string, string> json) {
        string path = GodotPath.Combine(GodotPath.ResPath, "cache");

        if (!Folder.Exists(path))
            Folder.Create(path).Dispose();

        path = GodotPath.Combine(path, fileName);

        if (!Archive.Exists(path))
            if (Archive.Create(path)) {
                using IStream stream = Archive.Open(path, FileAccess.Write);
                stream.Write(Json.Serialize(json));
            }
    }
}
