using System;
using Cobilas.GodotEditor.Utility.Serialization.Properties;
using Cobilas.GodotEngine.Utility;

namespace Cobilas.GodotEditor.Utility.Serialization.RenderObjects;
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
    public override SNInfo RootInfo { get; protected set; }
    /// <inheritdoc/>
    public override SerializedObject Parent { get; protected set; } = SONull.Null;

    /// <summary>Creates a new instance of this object.</summary>
    [Obsolete("Use SerializedProperty(string, SerializedObject, SOInfo) constructor!")]
    public SerializedProperty(string name, SerializedObject parent, string rootNodeId) : base(name, parent, rootNodeId) {}
    /// <summary>Creates a new instance of this object.</summary>
    public SerializedProperty(string name, SerializedObject parent, SNInfo info) : base(name, parent, info) {}
    /// <inheritdoc cref="PropertyCustom.Get(string?)"/>
    public override object? Get(string? propertyName) {
        if (Custom is null) return null;
        Custom.PropertyPath = PropertyPath;
        Custom.Member = Member;
        if (Member.IsSaveCache && GDFeature.HasEditor && propertyName == PropertyPath)
            if (SerializationCache.GetValueInCache(RootInfo, propertyName, out string value))
                return Custom.CacheValueToObject(propertyName, value);
        return Custom.Get(propertyName);
    }
    /// <inheritdoc cref="PropertyCustom.Set(string?, object?)"/>
    public override bool Set(string? propertyName, object? value) {
        if (Custom is null) return false;
        Custom.PropertyPath = PropertyPath;
        Custom.Member = Member;
        if (Member.IsSaveCache && GDFeature.HasEditor && propertyName == PropertyPath)
            _ = SerializationCache.SetValueInCache(RootInfo, propertyName, value);
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
