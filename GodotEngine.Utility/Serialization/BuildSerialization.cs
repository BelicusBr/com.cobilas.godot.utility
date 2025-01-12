using Godot;
using System;
using System.Reflection;
using System.Collections.Generic;

namespace Cobilas.GodotEngine.Utility.Serialization;
/// <summary>Class allows to build a serialization list of properties of a node class.</summary>
public static class BuildSerialization {
    private const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
    private static readonly List<SerializedNode> serializeds = [];
    private static readonly Dictionary<Type, PropertyCustom> propertyCustom = [];
    /// <summary>The method constructs a serialization list of properties for the node.</summary>
    /// <param name="node">The node to use.</param>
    /// <returns>Returns a serialization representation of the node.</returns>
    public static SerializedNode? Build(Node node) {
        string id = GetID(node);
        if (TryGetSerializedObject(id, out SerializedNode? result)) return result;
        else serializeds.Add(result = new(id));
        result.Add(Build(node, node.GetType(), SONull.Null, id));
        return result;
    }

    private static List<SerializedObject> Build(object obj, Type type, SerializedObject root, string id) {
        List<SerializedObject> result = [];
        foreach (MemberInfo? item in type.GetMembers(flags))
            if (IsSerialized(item)) {
                if (PrimitiveTypeCustom.IsPrimitiveType(GetMemberType(item))) {
                    SerializedProperty property = new(item.Name, root, id) {
                        Member = new MemberItem() {
                            Parent = obj,
                            Menber = item
                        },
                        Custom = new PrimitiveTypeCustom()
                    };
                    result.Add(property);
                } else if (IsPropertyCustom(GetMemberType(item))) {
                    SerializedProperty property = new(item.Name, root, id) {
                        Member = new MemberItem() {
                            Parent = obj,
                            Menber = item
                        },
                        Custom = propertyCustom[GetMemberType(item)]
                    };
                    result.Add(property);
                } else {
                    NoSerializedProperty property = new(item.Name, root, id) {
                        Member = new MemberItem() {
                            Parent = obj,
                            Menber = item
                        }
                    };
                    result.Add(property);
                    KeyValuePair<object, Type> pair = GetValue(obj, item);
                    property.Add(Build(pair.Key, pair.Value, property, id));
                }
            }
        return result;
    }
    /// <summary>Checks if the type has a <seealso cref="PropertyCustom"/>.</summary>
    /// <param name="type">The type to check.</param>
    /// <returns>Returns <c>true</c> when the type has a <seealso cref="PropertyCustom"/>.</returns>
    public static bool IsPropertyCustom(Type type) {
        if (propertyCustom.ContainsKey(type)) return true;
        Type[] types = TypeUtilitarian.GetTypes();
        foreach (Type item in types) {
            PropertyCustomAttribute attribute = item.GetAttribute<PropertyCustomAttribute>();
            if (attribute is null) continue;
            if (attribute.Target != type) continue;
            propertyCustom.Add(type, item.Activator<PropertyCustom>());
            return true;
        }
        return false;
    }

    private static string GetID(Node node) => node.GetPathTo(node).StringHash();

    private static KeyValuePair<object, Type> GetValue(object parent, MemberInfo member)
        => member switch {
            FieldInfo fdi => new(fdi.GetValue(parent), fdi.FieldType),
            PropertyInfo pti => new(pti.GetValue(parent), pti.PropertyType),
            _ => new()
        };

    private static bool TryGetSerializedObject(string id, out SerializedNode? result) {
        for (int I = 0; I < serializeds.Count; I++)
            if (id == serializeds[I].Id) {
                result = serializeds[I];
                return true;
            }
        result = null;
        return false;
    }

    private static Type GetMemberType(MemberInfo member) 
        => member switch {
            FieldInfo fdi => fdi.FieldType,
            PropertyInfo pti => pti.PropertyType,
            _ => NullObject.Null.GetType()
        };

    private static bool IsSerialized(MemberInfo member) 
        => member switch {
                FieldInfo fdi => fdi.FieldType.GetAttribute<SerializableAttribute>() is not null &&
                                    (fdi.GetCustomAttribute<ShowPropertyAttribute>() is not null || fdi.GetCustomAttribute<HidePropertyAttribute>() is not null),
                PropertyInfo pti => pti.PropertyType.GetAttribute<SerializableAttribute>() is not null &&
                                    (pti.GetCustomAttribute<ShowPropertyAttribute>() is not null || pti.GetCustomAttribute<HidePropertyAttribute>() is not null),
                _ => false,
            };
}
