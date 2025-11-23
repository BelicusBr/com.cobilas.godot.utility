using Godot;
using System;
using Godot.Collections;

using GDArray = Godot.Collections.Array;

namespace Cobilas.GodotEditor.Utility.Serialization; 
/// <summary>The class stores the information for drawing in the editor.</summary>
public sealed class PropertyItem : IDisposable {
    private readonly Dictionary dictionary;
    /// <summary>Creates a new instance of this object.</summary>
    public PropertyItem(
        string name,
        Variant.Type type,
        PropertyHint hint = PropertyHint.None,
        string hintString = "",
        PropertyUsageFlags usage = PropertyUsageFlags.ScriptVariable | PropertyUsageFlags.Editor | PropertyUsageFlags.Storage) {
        dictionary = new Dictionary {
            { "name", name },
            { "type", (int)type },
            { "hint", (int)hint },
            { "hint_string", hintString },
            { "usage", (int)usage },
        };
    }
    /// <inheritdoc/>
    public void Dispose() => ((IDisposable)dictionary).Dispose();
    /// <summary>Converts <seealso cref="PropertyItem"/> to <see cref="Godot.Collections.Dictionary"/>.</summary>
    /// <returns>Returns <seealso cref="PropertyItem"/> converted to <see cref="Godot.Collections.Dictionary"/>.</returns>
    public Dictionary ToDictionary() => dictionary;
	/// <summary>The method converts a list of <seealso cref="PropertyItem"/> to an <see cref="Godot.Collections.Array"/></summary>
	/// <param name="array">The list to be converted.</param>
	/// <returns>Returns an <see cref="Godot.Collections.Array"/> containing the serialization information for the properties.</returns>
	public static GDArray PropertyItemArrayToGDArray(PropertyItem[]? array) {
        if (array is null) throw new ArgumentNullException(nameof(array));
        GDArray result = [];
		foreach (PropertyItem item in array)
		    result.Add(item.ToDictionary());
        return result;
	}
}
