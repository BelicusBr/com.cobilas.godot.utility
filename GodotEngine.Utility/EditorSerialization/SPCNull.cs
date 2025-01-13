using System;

namespace Cobilas.GodotEngine.Utility.EditorSerialization;
/// <summary>Null representation of <seealso cref="PropertyCustom"/>.</summary>
public sealed class SPCNull : PropertyCustom, INullObject {
    private static readonly SPCNull @null = new();
    /// <inheritdoc/>
    public override bool IsHide { get => throw new NotImplementedException(); }
    /// <inheritdoc/>
    public override string PropertyPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    /// <inheritdoc/>
    public override MemberItem Member { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    /// <summary>Null representation of <seealso cref="PropertyCustom"/>.</summary>
    /// <returns>Returns a null representation of <seealso cref="PropertyCustom"/>.</returns>
    public static SPCNull Null => @null;
    /// <inheritdoc/>
    public override object? Get(string? propertyName) => throw new NotImplementedException();
    /// <inheritdoc/>
    public override PropertyItem[] GetPropertyList() => throw new NotImplementedException();
    /// <inheritdoc/>
    public override bool Set(string? propertyName, object? value) => throw new NotImplementedException();
    /// <inheritdoc/>
    public override object? CacheValueToObject(string? propertyName, string? value) => throw new NotImplementedException();
}
