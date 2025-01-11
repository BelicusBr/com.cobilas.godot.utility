using System;

namespace Cobilas.GodotEngine.Utility.Serialization;

public sealed class SPCNull : PropertyCustom, INullObject {

    private static readonly SPCNull @null = new();

    public override bool IsHide { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public override string PropertyPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public override MemberItem Member { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public static SPCNull Null => @null;

    public override object? Get(string? propertyName) => throw new NotImplementedException();
    public override PropertyItem[] GetPropertyList() => throw new NotImplementedException();
    public override bool Set(string? propertyName, object? value) => throw new NotImplementedException();
    public override object? CacheValueToObject(string? propertyName, string? value) => throw new NotImplementedException();
}
