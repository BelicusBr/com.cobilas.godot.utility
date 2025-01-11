namespace Cobilas.GodotEngine.Utility.Serialization;
/// <summary>SerializedObjectNull; Representa um SerializedObject nulo.</summary>
public sealed class SONull : SerializedObject, INullObject {
    public override string RootNodeId { get; protected set; }
    private static readonly SONull @null = new(string.Empty, null!, string.Empty);

    public SONull(string name, SerializedObject parent, string rootNodeId) : base(name, parent, rootNodeId) {}

    /// <summary>Representa nula de SerializedObject.</summary>
    /// <returns>Retorna uma representa nula de SerializedObject.</returns>
    public static SONull Null => @null;

    public override string PropertyPath => throw new System.NotImplementedException();
    public override MemberItem Member { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override string Name { get => throw new System.NotImplementedException(); protected set => throw new System.NotImplementedException(); }
    public override SerializedObject Parent { get => throw new System.NotImplementedException(); protected set => throw new System.NotImplementedException(); }

    public override object? Get(string? propertyName) => throw new System.NotImplementedException();
    public override PropertyItem[] GetPropertyList() => throw new System.NotImplementedException();
    public override bool Set(string? propertyName, object? value) => throw new System.NotImplementedException();
}
