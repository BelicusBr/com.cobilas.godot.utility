using System.Text;
using Godot.Collections;
using Cobilas.Collections;
using System.Collections.Generic;

namespace Cobilas.GodotEngine.Utility.Serialization;

public class SerializedNode : ISerializedPropertyManipulation {
    private readonly string id;
    private readonly List<SerializedObject> properties;

    public string Id => id;

    public SerializedNode(string id) {
        this.id = id;
        this.properties = [];
    }

    public void Add(SerializedObject obj) => properties.Add(obj);
    public void Add(IEnumerable<SerializedObject> objs) => properties.AddRange(objs);

    public object? Get(string? propertyName) {
        foreach (SerializedObject item in properties) {
            object? result = item.Get(propertyName);
            if (result is not null) return result;
        }
        return null;
    }

    public bool Set(string? propertyName, object? value) {
        foreach (SerializedObject item in properties) {
            if (item.Set(propertyName, value)) 
                return true;
        }
        return false;
    }

    public PropertyItem[] GetPropertyList() {
        PropertyItem[] result = [];
        foreach (SerializedObject item in properties)
            ArrayManipulation.Add(item.GetPropertyList(), ref result);
        return result;
    }

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

    public static Array GetPropertyList(PropertyItem[] list) {
        Array array = [];
        foreach (PropertyItem item in list)
            array.Add(item.ToDictionary());
        return array;
    }

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
