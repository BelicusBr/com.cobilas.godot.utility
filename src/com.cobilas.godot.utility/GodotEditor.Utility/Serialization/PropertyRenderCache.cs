using Godot;
using System;
using System.IO;
using System.Collections.Generic;
using Cobilas.IO.Serialization.Json;
using Cobilas.GodotEngine.Utility.IO;
using Cobilas.GodotEngine.Utility.IO.Interfaces;

namespace Cobilas.GodotEditor.Utility.Serialization;

/// <summary>Provides caching functionality for property render values.</summary>
public static class PropertyRenderCache {
	private const string cachePath = "res://cacheList.cache";
	/// <summary>Sets a value in the property cache.</summary>
	/// <param name="id">The identifier for the cached object.</param>
	/// <param name="key">The key for the cached value.</param>
	/// <param name="value">The value to cache.</param>
	/// <exception cref="ArgumentNullException">Thrown when id, key, or value is null.</exception>
	public static void SetValue(ulong id, string? key, string? value) {
		if (id == string.Empty.Hash()) return;
		else if (key is null) throw new ArgumentNullException(nameof(key));
		else if (value is null) throw new ArgumentNullException(nameof(value));
		Dictionary<ulong, Dictionary<string, string>>? cache = GetCache() ?? [];
		if (!cache.ContainsKey(id))
			cache.Add(id, []);
		if (!cache[id].ContainsKey(key))
			cache[id].Add(key, value);
		else cache[id][key] = value;

		SetCache(cache);
	}
	/// <summary>Gets a value from the property cache.</summary>
	/// <param name="id">The identifier for the cached object.</param>
	/// <param name="key">The key for the cached value.</param>
	/// <returns>Returns the cached value, or empty string if not found.</returns>
	/// <exception cref="ArgumentNullException">Thrown when id or key is null.</exception>
	public static string GetValue(ulong id, string? key) {
		if (id == string.Empty.Hash()) return string.Empty;
		else if (key is null) throw new ArgumentNullException(nameof(key));

		Dictionary<ulong, Dictionary<string, string>>? cache = GetCache() ?? [];

		if (!cache.ContainsKey(id)) return string.Empty;
		else if (!cache[id].ContainsKey(key)) return string.Empty;
		return cache[id][key];
	}

	private static Dictionary<ulong, Dictionary<string, string>>? GetCache() {
		if (Archive.Exists(cachePath)) {
			using IStream stream = Archive.Open(cachePath, FileAccess.Read, StreamType.IOStream);
			stream.Read(out string stg);
			return Json.Deserialize<Dictionary<ulong, Dictionary<string, string>>>(stg);
		}
		return [];
	}

	private static void SetCache(Dictionary<ulong, Dictionary<string, string>> cache) {
		if (!Archive.Exists(cachePath))
			Archive.Create(cachePath);
		using IStream stream = Archive.Open(cachePath, FileAccess.Write, StreamType.IOStream);
		stream.ReplaceBuffer(Json.Serialize(cache));
	}
}