using Godot;
using System;

namespace Cobilas.GodotEngine.Utility.Serialization; 
/// <summary>The attribute allows you to hide and save the value of a field or property in the editor.</summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public class HidePropertyAttribute : SerializeFieldAttribute {
    /// <inheritdoc/>
    public override bool SaveInCache { get; protected set; }
    /// <inheritdoc/>
    public override PropertyUsageFlags Flags { get; protected set; }
    /// <inheritdoc cref="SerializeFieldAttribute(PropertyUsageFlags, bool)"/>
    public HidePropertyAttribute(bool saveInCache) : base(
        PropertyUsageFlags.ScriptVariable |
        PropertyUsageFlags.Storage, saveInCache) {}
    /// <inheritdoc cref="SerializeFieldAttribute(PropertyUsageFlags, bool)"/>
    public HidePropertyAttribute() : this(false) {}
}
