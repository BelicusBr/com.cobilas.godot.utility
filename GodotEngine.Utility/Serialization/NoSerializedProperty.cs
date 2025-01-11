using System.Collections;
using Cobilas.Collections;
using System.Collections.Generic;

namespace Cobilas.GodotEngine.Utility.Serialization;

public class NoSerializedProperty : SerializedObject, IEnumerable<SerializedObject> {
    private readonly List<SerializedObject> properties;

    public override string RootNodeId { get; protected set; }
    public override string Name { get; protected set; }
    public override string PropertyPath => GetPath(this);
    public override SerializedObject Parent { get; protected set; }
    public override MemberItem Member { get; set; }

    public NoSerializedProperty(string name, SerializedObject parent, string rootNodeId) : base(name, parent, rootNodeId) {
        properties = [];
    }

    // public NoSerializedProperty(string name, SerializedObject parent) : base(name, parent) {
    // }

    public void Add(SerializedObject obj) => properties.Add(obj);
    public void Add(IEnumerable<SerializedObject> objs) => properties.AddRange(objs);

    public IEnumerator<SerializedObject> GetEnumerator() => ((IEnumerable<SerializedObject>)properties).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)properties).GetEnumerator();

    public override object? Get(string? propertyName) {
        foreach (SerializedObject item in properties) {
            object? result = item.Get(propertyName);
            if (result is not null) return result;
        }
        return null;
    }

    public override bool Set(string? propertyName, object? value) {
        foreach (SerializedObject item in properties)
            if (item.Set(propertyName, value)) {
                if (Member.IsStruct)
                    Member.Value = item.Member.Parent;
                return true;
            }
        return false;
    }

    public override PropertyItem[] GetPropertyList() {
        PropertyItem[] result = [];
        foreach (SerializedObject item in properties)
            ArrayManipulation.Add(item.GetPropertyList(), ref result);
        return result;
    }
}
