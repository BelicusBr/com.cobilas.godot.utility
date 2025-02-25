using System;

namespace Cobilas.GodotEditor.Utility.Serialization.RenderObjects;
/// <summary>SerializedObjectNull; Represents a null <seealso cref="SerializedObject"/>.</summary>
public sealed class SONull : SerializedObject, INullObject {
    private static readonly SONull @null = new(string.Empty, null!, SNInfo.Empty);
    /// <inheritdoc/>
    public override string PropertyPath => string.Empty;
    /// <inheritdoc/>
    public override MemberItem Member { get; set; } = MemberItem.Null;
    /// <inheritdoc/>
    public override string Name { get; protected set; } = string.Empty;
    /// <inheritdoc/>
    public override SNInfo RootInfo { get; protected set; }
    /// <inheritdoc/>
    public override SerializedObject Parent { get; protected set; } = null!;
    /// <summary>Represents a null representation of <seealso cref="SerializedObject"/>.</summary>
    /// <returns>Returns a null representation of <seealso cref="SerializedObject"/>.</returns>
    public static SONull Null => @null;

    /// <summary>Creates a new instance of this object.</summary>
    [Obsolete("Use SONull(string, SerializedObject, SOInfo) constructor!")]
    public SONull(string name, SerializedObject parent, string rootNodeId) : base(name, parent, rootNodeId) {}
    /// <summary>Creates a new instance of this object.</summary>
    public SONull(string name, SerializedObject parent, SNInfo info) : base(name, parent, info) {}
    /// <inheritdoc/>
    public override object? Get(string? propertyName) => null;
    /// <inheritdoc/>
    public override PropertyItem[] GetPropertyList() => Array.Empty<PropertyItem>();
    /// <inheritdoc/>
    public override bool Set(string? propertyName, object? value) => false;
}
