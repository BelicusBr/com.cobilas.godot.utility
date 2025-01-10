using System;
using System.Reflection;

namespace Cobilas.GodotEngine.Utility.Serialization;

public class SerializedProperty : SerializedObject {
    public override string Name { get; protected set; }
    public override string PropertyPath => GetPath(this);
    public override SerializedObject Parent { get; protected set; }
    public override MemberItem Member { get; set; }

    public PropertyCustom Custom { get; set; }

    public SerializedProperty(string name, SerializedObject parent) : base(name, parent)
    {
    }

    public override object? Get(string? propertyName) {
        if (Custom is null) return null;
        Custom.PropertyPath = PropertyPath;
        Custom.Member = Member;
        Custom.IsHide = Member.Menber.GetCustomAttribute<HidePropertyAttribute>() is not null;
        return Custom.Get(propertyName);
    }

    public override bool Set(string? propertyName, object? value) {
        if (Custom is null) return false;
        Custom.PropertyPath = PropertyPath;
        Custom.Member = Member;
        Custom.IsHide = Member.Menber.GetCustomAttribute<HidePropertyAttribute>() is not null;
        return Custom.Set(propertyName, value);
    }

    public override PropertyItem[] GetPropertyList() {
        if (Custom is null) return Array.Empty<PropertyItem>();
        Custom.PropertyPath = PropertyPath;
        Custom.Member = Member;
        Custom.IsHide = Member.Menber.GetCustomAttribute<HidePropertyAttribute>() is not null;
        return Custom.GetPropertyList();
    }
}
