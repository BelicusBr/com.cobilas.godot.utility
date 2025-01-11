namespace Cobilas.GodotEngine.Utility.Serialization;

public abstract class SerializedObject : ISerializedPropertyManipulation {
    public abstract string RootNodeId { get; protected set; }
    public abstract string Name { get; protected set; }
    public abstract SerializedObject Parent { get; protected set; }
    /// <summary>The custom class member.</summary>
    /// <returns>Returns the custom class member.</returns>
    /// <value>Receives the custom class member.</value>
    public abstract MemberItem Member { get; set; }
    /// <summary>The custom property path.</summary>
    /// <returns>Returns the path of the custom property.</returns>
    /// <value>Receives the custom property path.</value>
    public abstract string PropertyPath { get; }

    protected SerializedObject(string name, SerializedObject parent, string rootNodeId) {
        Name = name;
        Parent = parent;
        RootNodeId = rootNodeId;
    }

    public abstract object? Get(string? propertyName);
    public abstract bool Set(string? propertyName, object? value);
    public abstract PropertyItem[] GetPropertyList();

    public static string GetPath(SerializedObject obj) {
        if (obj is null) return string.Empty;
        return $"{GetPath(obj.Parent)}/{obj.Name}".TrimStart('/');
    }
}
