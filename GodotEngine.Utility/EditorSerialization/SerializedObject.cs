using System;

namespace Cobilas.GodotEngine.Utility.EditorSerialization;
/// <summary>Base class for serialization properties.</summary>
public abstract class SerializedObject : ISerializedPropertyManipulation {
    /// <summary>Contains information about the root object.</summary>
    /// <returns>Returns information about the root object.</returns>
    public abstract SNInfo RootInfo { get; protected set; }
    /// <summary>The name of the object.</summary>
    /// <returns>Returns the name of the object.</returns>
    public abstract string Name { get; protected set; }
    /// <summary>The id of the node object.</summary>
    /// <returns>Returns a <seealso cref="string"/> with the id of the node object.</returns>
    public string RootNodeId { get => (string)RootInfo["id"]; }
    /// <summary>The parent object of the SerializedObject.</summary>
    /// <returns>Returns the parent object of the SerializedObject.</returns>
    public abstract SerializedObject Parent { get; protected set; }
    /// <summary>The custom class member.</summary>
    /// <returns>Returns the custom class member.</returns>
    /// <value>Receives the custom class member.</value>
    public abstract MemberItem Member { get; set; }
    /// <summary>The custom property path.</summary>
    /// <returns>Returns the path of the custom property.</returns>
    /// <value>Receives the custom property path.</value>
    public abstract string PropertyPath { get; }
    /// <summary>Creates a new instance of this object.</summary>
    [Obsolete("Use SerializedObject(string, SerializedObject, SOInfo) constructor!")]
    protected SerializedObject(string name, SerializedObject parent, string rootNodeId) {
        Name = name;
        Parent = parent;
    }
    /// <summary>Creates a new instance of this object.</summary>
    protected SerializedObject(string name, SerializedObject parent, SNInfo info) {
        Name = name;
        Parent = parent;
        RootInfo = info;
    }
    /// <inheritdoc/>
    public abstract object? Get(string? propertyName);
    /// <inheritdoc/>
    public abstract bool Set(string? propertyName, object? value);
    /// <inheritdoc/>
    public abstract PropertyItem[] GetPropertyList();
    /// <summary>Allows you to get the path of the <seealso cref="SerializedObject"/>.</summary>
    /// <param name="obj">The <seealso cref="SerializedObject"/> that will be used to generate the path.</param>
    /// <returns>Returns a <seealso cref="string"/> containing the path of the <seealso cref="SerializedObject"/>.</returns>
    public static string GetPath(SerializedObject obj) {
        if (obj is null) return string.Empty;
        else if (obj == SONull.Null) return string.Empty;
        return $"{GetPath(obj.Parent)}/{obj.Name}".TrimStart('/');
    }
}
