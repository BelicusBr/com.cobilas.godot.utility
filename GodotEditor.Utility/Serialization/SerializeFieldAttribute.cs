using Godot;
using System;
using Cobilas.GodotEditor.Utility.Serialization.Hints;

namespace Cobilas.GodotEditor.Utility.Serialization; 
/// <summary>Base attribute for field and property serialization attributes.</summary>
public abstract class SerializeFieldAttribute : Attribute {
    /// <summary>Custom property custom hint.</summary>
    /// <returns>Returns a custom hint for the custom property.</returns>
    public abstract CustomHint Hint { get; protected set; }
    /// <summary>Indicates whether fields and properties are cached.</summary>
    /// <returns><c>true</c> if fields and properties are cached.</returns>
    public abstract bool SaveInCache { get; protected set; }
    /// <summary>The flags that will be used to serialize fields and properties.</summary>
    /// <returns>Returns the flags that will be used to serialize fields and properties.</returns>
    public abstract PropertyUsageFlags Flags { get; protected set; }
    /// <summary>Creates a new instance of this object.</summary>
    /// <param name="flags">The flags that will be used to serialize fields and properties.</param>
    /// <param name="saveInCache">Allows fields and properties that are not normally serialized.</param>
    /// <param name="hint">You can receive a customized hint.</param>
    protected SerializeFieldAttribute(PropertyUsageFlags flags, bool saveInCache, CustomHint hint) {
        Flags = flags;
        SaveInCache = saveInCache;
        Hint = hint;
    }
}
