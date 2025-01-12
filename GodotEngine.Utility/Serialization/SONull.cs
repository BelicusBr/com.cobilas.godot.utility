namespace Cobilas.GodotEngine.Utility.Serialization;
/// <summary>SerializedObjectNull; Represents a null <seealso cref="SerializedObject"/>.</summary>
public sealed class SONull : SerializedObject, INullObject {
    private static readonly SONull @null = new(string.Empty, null!, string.Empty);
    /// <inheritdoc/>
    public override string PropertyPath => throw new System.NotImplementedException();
    /// <inheritdoc/>
    public override MemberItem Member { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    /// <inheritdoc/>
    public override string Name { get => throw new System.NotImplementedException(); protected set => throw new System.NotImplementedException(); }
    /// <inheritdoc/>
    public override string RootNodeId { get => throw new System.NotImplementedException(); protected set => throw new System.NotImplementedException(); }
    /// <inheritdoc/>
    public override SerializedObject Parent { get => throw new System.NotImplementedException(); protected set => throw new System.NotImplementedException(); }
    /// <summary>Represents a null representation of <seealso cref="SerializedObject"/>.</summary>
    /// <returns>Returns a null representation of <seealso cref="SerializedObject"/>.</returns>
    public static SONull Null => @null;
    /// <summary>Creates a new instance of this object.</summary>
    public SONull(string name, SerializedObject parent, string rootNodeId) : base(name, parent, rootNodeId) {}
    /// <inheritdoc/>
    public override object? Get(string? propertyName) => throw new System.NotImplementedException();
    /// <inheritdoc/>
    public override PropertyItem[] GetPropertyList() => throw new System.NotImplementedException();
    /// <inheritdoc/>
    public override bool Set(string? propertyName, object? value) => throw new System.NotImplementedException();
}
