using Godot;
using System;

namespace Cobilas.GodotEngine.Utility.EditorSerialization;
/// <summary>The attribute allows you to show a field or property in the editor.</summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public class ShowPropertyAttribute : SerializeFieldAttribute {
    /// <inheritdoc/>
    public override bool SaveInCache { get; protected set; }
    /// <inheritdoc/>
    public override PropertyUsageFlags Flags { get; protected set; }
    /// <inheritdoc cref="SerializeFieldAttribute(PropertyUsageFlags, bool)"/>
    public ShowPropertyAttribute(bool saveInCache) : base(
        PropertyUsageFlags.ScriptVariable |
        PropertyUsageFlags.Storage |
        PropertyUsageFlags.Editor, saveInCache) {}
    /// <inheritdoc cref="SerializeFieldAttribute(PropertyUsageFlags, bool)"/>
    public ShowPropertyAttribute() : this(false) {}
}