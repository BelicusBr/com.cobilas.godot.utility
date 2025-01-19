using Godot;
using System;

namespace Cobilas.GodotEngine.Utility.EditorSerialization; 
/// <summary>The attribute allows you to hide and save the value of a field or property in the editor.</summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public class HidePropertyAttribute : SerializeFieldAttribute {
    /// <inheritdoc/>
    public override bool SaveInCache { get; protected set; }
    /// <inheritdoc/>
    public override PropertyUsageFlags Flags { get; protected set; }
    public override CustomHint Hint { get; protected set; }
    /// <inheritdoc cref="SerializeFieldAttribute(PropertyUsageFlags, bool, CustomHint)"/>
    public HidePropertyAttribute(bool saveInCache, int min, int max) : this(saveInCache, new RangeHint(min, max)) {}
    /// <inheritdoc cref="SerializeFieldAttribute(PropertyUsageFlags, bool, CustomHint)"/>
    public HidePropertyAttribute(int min, int max) : this(false, new RangeHint(min, max)) {}
    /// <inheritdoc cref="SerializeFieldAttribute(PropertyUsageFlags, bool, CustomHint)"/>
    public HidePropertyAttribute(bool saveInCache, float min, float max) : this(saveInCache, new RangeHint(min, max)) {}
    /// <inheritdoc cref="SerializeFieldAttribute(PropertyUsageFlags, bool, CustomHint)"/>
    public HidePropertyAttribute(float min, float max) : this(false, new RangeHint(min, max)) {}
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
