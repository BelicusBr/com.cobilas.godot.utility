using System;

namespace Cobilas.GodotEngine.Utility.EditorSerialization;
/// <summary>Null representation of <seealso cref="PropertyCustom"/>.</summary>
public sealed class SPCNull : PropertyCustom, INullObject {
    private static readonly SPCNull @null = new();
    /// <inheritdoc/>
    public override bool IsHide => false;
    /// <inheritdoc/>
    public override string PropertyPath { get; set; } = string.Empty;
    /// <inheritdoc/>
    public override MemberItem Member { get; set; } = MemberItem.Null;
    /// <summary>Null representation of <seealso cref="PropertyCustom"/>.</summary>
    /// <returns>Returns a null representation of <seealso cref="PropertyCustom"/>.</returns>
    public static SPCNull Null => @null;
    /// <inheritdoc/>
    public override object? Get(string? propertyName) => null;
    /// <inheritdoc/>
    public override PropertyItem[] GetPropertyList() => Array.Empty<PropertyItem>();
    /// <inheritdoc/>
    public override bool Set(string? propertyName, object? value) => false;
    /// <inheritdoc/>
    public override object? CacheValueToObject(string? propertyName, string? value) => null;
}
