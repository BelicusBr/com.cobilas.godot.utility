using Godot;
using System;

namespace Cobilas.GodotEngine.Utility.EditorSerialization;
/// <summary>The attribute allows you to show a field or property in the editor.</summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class ShowPropertyAttribute : SerializeFieldAttribute {
    /// <inheritdoc/>
    public override bool SaveInCache { get; protected set; }
    /// <inheritdoc/>
    public override PropertyUsageFlags Flags { get; protected set; }
    /// <inheritdoc/>
    public override CustomHint Hint { get; protected set; } = new NoneHint();
    /// <inheritdoc cref="SerializeFieldAttribute(PropertyUsageFlags, bool, CustomHint)"/>
    public ShowPropertyAttribute(bool saveInCache, CustomHint hint) : base(
        PropertyUsageFlags.ScriptVariable |
        PropertyUsageFlags.Storage, saveInCache, hint) {}
    /// <inheritdoc cref="SerializeFieldAttribute(PropertyUsageFlags, bool, CustomHint)"/>
    public ShowPropertyAttribute(CustomHint hint) : base(
        PropertyUsageFlags.ScriptVariable |
        PropertyUsageFlags.Storage, false, hint) {}
    /// <inheritdoc cref="SerializeFieldAttribute(PropertyUsageFlags, bool, CustomHint)"/>
    public ShowPropertyAttribute(bool saveInCache) : base(
        PropertyUsageFlags.ScriptVariable |
        PropertyUsageFlags.Storage |
        PropertyUsageFlags.Editor, saveInCache, new NoneHint()) {}
    /// <inheritdoc cref="SerializeFieldAttribute(PropertyUsageFlags, bool, CustomHint)"/>
    public ShowPropertyAttribute() : this(false) {}
}