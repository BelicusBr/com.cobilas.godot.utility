using System;
using Godot;

namespace Cobilas.GodotEngine.Utility.EditorSerialization;
/// <summary>Property serialization class in inspector.</summary>
public class SerializedProperty : SerializedObject {
    /// <summary>Property customization object.</summary>
    /// <value>Allows you to define a property customization object.</value>
    /// <returns>Returns the property customization object.</returns>
    public PropertyCustom Custom { get; set; } = SPCNull.Null;
    /// <inheritdoc/>
    public override MemberItem Member { get; set; } = MemberItem.Null;
    /// <inheritdoc/>
    public override string Name { get; protected set; } = string.Empty;
    /// <inheritdoc/>
    public override string PropertyPath => GetPath(this);
    /// <inheritdoc/>
    public override string RootNodeId { get; protected set; } = string.Empty;
    /// <inheritdoc/>
    public override SerializedObject Parent { get; protected set; } = SONull.Null;
    /// <summary>Creates a new instance of this object.</summary>
    public SerializedProperty(string name, SerializedObject parent, string rootNodeId) : base(name, parent, rootNodeId) {}
    /// <inheritdoc cref="PropertyCustom.Get(string?)"/>
    public override object? Get(string? propertyName) {
        if (Custom is null) return null;
        Custom.PropertyPath = PropertyPath;
        Custom.Member = Member;
        if (Member.IsSaveCache && GDFeature.HasEditor && propertyName == PropertyPath)
            if (SerializationCache.GetValueInCache(RootNodeId, propertyName, out string value))
                return Custom.CacheValueToObject(propertyName, value);
        return Custom.Get(propertyName);
    }
    /// <inheritdoc cref="PropertyCustom.Set(string?, object?)"/>
    public override bool Set(string? propertyName, object? value) {
        if (Custom is null) return false;
        Custom.PropertyPath = PropertyPath;
        Custom.Member = Member;
        if (Member.IsSaveCache && GDFeature.HasEditor && propertyName == PropertyPath)
            _ = SerializationCache.SetValueInCache(RootNodeId, propertyName, value);
        return Custom.Set(propertyName, value);
    }
    /// <inheritdoc cref="PropertyCustom.GetPropertyList"/>
    public override PropertyItem[] GetPropertyList() {
        if (Custom is null) return Array.Empty<PropertyItem>();
        Custom.PropertyPath = PropertyPath;
        Custom.Member = Member;
        return Custom.GetPropertyList();
    }
}
