using System.Text;
using Godot.Collections;
using Cobilas.Collections;
using System.Collections.Generic;

namespace Cobilas.GodotEditor.Utility.Serialization.RenderObjects;
/// <summary>The class is a serialization representation of a node.</summary>
[System.Obsolete($"Use {nameof(NodeReneder)} or {nameof(ResourceRender)} class")]
public class SerializedNode : ISerializedPropertyManipulation {
    private readonly string id;
    private readonly List<SerializedObject> properties;
    /// <summary>The id of the node object.</summary>
    /// <returns>Returns the id of the node object.</returns>
    public string Id => id;
    /// <summary>Creates a new instance of this object.</summary>
    public SerializedNode(string id) {
        this.id = id;
        this.properties = [];
    }
    /// <inheritdoc cref="NoSerializedProperty.Add(SerializedObject)"/>
    public void Add(SerializedObject obj) => properties.Add(obj);
    /// <inheritdoc cref="NoSerializedProperty.Add(IEnumerable{SerializedObject})"/>
    public void Add(IEnumerable<SerializedObject> objs) => properties.AddRange(objs);
    /// <inheritdoc cref="Properties.PropertyCustom.Get(string?)"/>
    public object? Get(string? propertyName) {
        foreach (SerializedObject item in properties) {
            object? result = item.Get(propertyName);
            if (result is not null) return result;
        }
        return null;
    }
    /// <inheritdoc cref="Properties.PropertyCustom.Set(string?, object?)"/>
    public bool Set(string? propertyName, object? value) {
        foreach (SerializedObject item in properties) {
            if (item.Set(propertyName, value)) 
                return true;
        }
        return false;
    }
    /// <inheritdoc cref="Properties.PropertyCustom.GetPropertyList"/>
    public PropertyItem[] GetPropertyList() {
        PropertyItem[]? result = [];
        foreach (SerializedObject item in properties)
            ArrayManipulation.Add(item.GetPropertyList(), ref result);
        if (result is null) return [];
        return result;
    }
    /// <inheritdoc/>
    public override string ToString() {
        StringBuilder builder = new();
        builder.AppendLine($"ID: {id}");
        foreach (SerializedObject item in properties)
            switch (item) {
                case NoSerializedProperty nsp:
                    builder.Append(ToString(nsp));
                    break;
                case SerializedProperty sp:
                    builder.AppendLine(sp.PropertyPath);
                    break;
            }
        return builder.ToString();
    }
    /// <summary>The method converts a list of <seealso cref="PropertyItem"/> to an <see cref="Godot.Collections.Array"/></summary>
    /// <param name="list">The list to be converted.</param>
    /// <returns>Returns an <see cref="Godot.Collections.Array"/> containing the serialization information for the properties.</returns>
    public static Array GetPropertyList(PropertyItem[] list) {
        Array array = [];
        foreach (PropertyItem item in list)
            array.Add(item.ToDictionary());
        return array;
    }
    /// <inheritdoc cref="GetPropertyList(PropertyItem[])"/>
    /// <param name="node">The <seealso cref="SerializedNode"/> that will get the list of <seealso cref="PropertyItem"/> to be converted.</param>
    public static Array GetPropertyList(SerializedNode node)
        => GetPropertyList(node.GetPropertyList());

    private static string ToString(NoSerializedProperty property) {
        StringBuilder builder = new();
        builder.AppendLine(property.PropertyPath);
        foreach (SerializedObject item in property)
            switch (item) {
                case NoSerializedProperty nsp:
                    builder.Append(ToString(nsp));
                    break;
                case SerializedProperty sp:
                    builder.AppendLine(sp.PropertyPath);
                    break;
            }
        return builder.ToString();
    }
}
