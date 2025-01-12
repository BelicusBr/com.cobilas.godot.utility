using System.Collections;
using Cobilas.Collections;
using System.Collections.Generic;

namespace Cobilas.GodotEngine.Utility.Serialization;
/// <summary>Represents a property that does not have a <seealso cref="PropertyCustom"/>.</summary>
public class NoSerializedProperty : SerializedObject, IEnumerable<SerializedObject> {
    private readonly List<SerializedObject> properties;
    /// <inheritdoc/>
    public override MemberItem Member { get; set; } = MemberItem.Null;
    /// <inheritdoc/>
    public override string Name { get; protected set; } = string.Empty;
    /// <inheritdoc/>
    public override string PropertyPath => GetPath(this);
    /// <inheritdoc/>
    public override string RootNodeId { get; protected set; } = string.Empty;
    /// <inheritdoc/>
    public override SerializedObject Parent { get; protected set; } = SONull.Null;
    /// <summary>Creates a new instance of this object.</summary>
    public NoSerializedProperty(string name, SerializedObject parent, string rootNodeId) : base(name, parent, rootNodeId) {
        properties = [];
    }
    /// <summary>Allows you to add a new <seealso cref="SerializedObject"/> object.</summary>
    /// <param name="obj">The <seealso cref="SerializedObject"/> object to be added.</param>
    public void Add(SerializedObject obj) => properties.Add(obj);
    /// <inheritdoc cref="Add(SerializedObject)"/>
    /// <param name="objs">The list of <seealso cref="SerializedObject"/> objects to add.</param>
    public void Add(IEnumerable<SerializedObject> objs) => properties.AddRange(objs);
    /// <inheritdoc/>
    public IEnumerator<SerializedObject> GetEnumerator() => ((IEnumerable<SerializedObject>)properties).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)properties).GetEnumerator();
    /// <inheritdoc cref="PropertyCustom.Get(string?)"/>
    public override object? Get(string? propertyName) {
        foreach (SerializedObject item in properties) {
            object? result = item.Get(propertyName);
            if (result is not null) return result;
        }
        return null;
    }
    /// <inheritdoc cref="PropertyCustom.Set(string?, object?)"/>
    public override bool Set(string? propertyName, object? value) {
        foreach (SerializedObject item in properties)
            if (item.Set(propertyName, value)) {
                if (Member.IsStruct)
                    Member.Value = item.Member.Parent;
                return true;
            }
        return false;
    }
    /// <inheritdoc cref="PropertyCustom.GetPropertyList"/>
    public override PropertyItem[] GetPropertyList() {
        PropertyItem[] result = [];
        foreach (SerializedObject item in properties)
            ArrayManipulation.Add(item.GetPropertyList(), ref result);
        return result;
    }
}
