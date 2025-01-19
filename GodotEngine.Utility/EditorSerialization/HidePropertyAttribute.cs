using Godot;
using System;

namespace Cobilas.GodotEngine.Utility.EditorSerialization; 
/// <summary>The attribute allows you to hide and save the value of a field or property in the editor.</summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class HidePropertyAttribute : SerializeFieldAttribute {
    /// <inheritdoc/>
    public override bool SaveInCache { get; protected set; }
    /// <inheritdoc/>
    public override PropertyUsageFlags Flags { get; protected set; }
    /// <inheritdoc/>
    public override CustomHint Hint { get; protected set; } = new NoneHint();
    /// <inheritdoc cref="SerializeFieldAttribute(PropertyUsageFlags, bool, CustomHint)"/>
    public HidePropertyAttribute(bool saveInCache, CustomHint hint) : base(
        PropertyUsageFlags.ScriptVariable |
        PropertyUsageFlags.Storage, saveInCache, hint) {}
    /// <inheritdoc cref="SerializeFieldAttribute(PropertyUsageFlags, bool, CustomHint)"/>
    public HidePropertyAttribute(CustomHint hint) : base(
        PropertyUsageFlags.ScriptVariable |
        PropertyUsageFlags.Storage, false, hint) {}
    /// <inheritdoc cref="SerializeFieldAttribute(PropertyUsageFlags, bool, CustomHint)"/>
    public HidePropertyAttribute(bool saveInCache) : base(
        PropertyUsageFlags.ScriptVariable |
        PropertyUsageFlags.Storage, saveInCache, new NoneHint()) {}
    /// <inheritdoc cref="SerializeFieldAttribute(PropertyUsageFlags, bool, CustomHint)"/>
    public HidePropertyAttribute() : this(false) {}
}
