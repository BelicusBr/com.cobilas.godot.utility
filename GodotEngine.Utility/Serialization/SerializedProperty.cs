using System;

namespace Cobilas.GodotEngine.Utility.Serialization;

public class SerializedProperty : SerializedObject {
    public PropertyCustom Custom { get; set; }
    public override MemberItem Member { get; set; }
    public override string Name { get; protected set; }
    public override string PropertyPath => GetPath(this);
    public override string RootNodeId { get; protected set; }
    public override SerializedObject Parent { get; protected set; }

    public SerializedProperty(string name, SerializedObject parent, string rootNodeId) : base(name, parent, rootNodeId) {}

    public override object? Get(string? propertyName) {
        if (Custom is null) return null;
        Custom.PropertyPath = PropertyPath;
        Custom.Member = Member;
        Custom.IsHide = Member.IsHide;
        if (Member.IsSaveCache && GDFeature.HasEditor)
            if (SerializationCache.GetValueInCache(RootNodeId, propertyName, out string value))
                return Custom.CacheValueToObject(propertyName, value);
        return Custom.Get(propertyName);
    }

    public override bool Set(string? propertyName, object? value) {
        if (Custom is null) return false;
        Custom.PropertyPath = PropertyPath;
        Custom.Member = Member;
        Custom.IsHide = Member.IsHide;
        if (Member.IsSaveCache && GDFeature.HasEditor)
            _ = SerializationCache.SetValueInCache(RootNodeId, propertyName, value);
        return Custom.Set(propertyName, value);
    }

    public override PropertyItem[] GetPropertyList() {
        if (Custom is null) return Array.Empty<PropertyItem>();
        Custom.PropertyPath = PropertyPath;
        Custom.Member = Member;
        Custom.IsHide = Member.IsHide;
        return Custom.GetPropertyList();
    }
}
