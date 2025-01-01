using Godot;
using System;
using System.Reflection;
using Cobilas.Collections;
using System.Collections.Generic;

using GArray = Godot.Collections.Array;

namespace Cobilas.GodotEngine.Utility.Serialization {
    public class SerializedObject {
        private readonly string id;
        private readonly List<SerializedPropertyCustom> _serializedProperties;

        private static readonly Dictionary<Type, Type> cs_Properties = [];
        private static readonly Dictionary<string, SerializedObject> _serializedObject = [];
        private static readonly int _BindingFlags = (int)(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        public SerializedObject(string id, SerializedPropertyCustom[] customs) {
            this.id = id;
            _serializedProperties = new(customs);
        }

        public GArray GetPropertyList() {
            GArray array = [];
            foreach (SerializedPropertyCustom item in _serializedProperties)
                foreach (PropertyItem item2 in item.GetPropertyList())
                    array.Add(item2.ToDictionary());
            return array;
        }

        public object? Get(string? propertyPath) {
            if (!string.IsNullOrEmpty(propertyPath))
                if (TryGetValue(propertyPath, out SerializedPropertyCustom property)) {
                    if (SerializedPropertyCustom.LoadInCache(id, property, propertyPath, out object value)) return value;
                    return property.Get(propertyPath);
                }
            return null;
        }

        public bool Set(string? propertyPath, object value) {
            if (!string.IsNullOrEmpty(propertyPath))
                if (TryGetValue(propertyPath, out SerializedPropertyCustom property)) {
                    if (GDFeature.HasEditor)
                        if (SerializedPropertyCustom.UnloadInCache(id, property, propertyPath, value)) return true;
                    return property.Set(propertyPath, value);
                }
            return false;
        }

        public bool TryGetValue(string propertyPath, out SerializedPropertyCustom value) {
            for (int I = 0; I < _serializedProperties.Count; I++)
                if (_serializedProperties[I].IsPropertyPath(propertyPath)) {
                    value = _serializedProperties[I];
                    return true;                    
                }
            value = null;
            return false;
        }

        public static GArray GetPropertyList(ISerializedObject @object) {
            if (string.IsNullOrEmpty(@object.ObjectID)) return new GArray();
            if (_serializedObject.TryGetValue(@object.ObjectID, out SerializedObject sdo))
                return sdo.GetPropertyList();
            SerializedPropertyCustom[] customs = GetPropertyList(@object, @object.GetType(), string.Empty);
            SerializedObject serialized = new SerializedObject(@object.ObjectID, customs);
            _serializedObject.Add(@object.ObjectID, serialized);
            return serialized.GetPropertyList();
        }

        public static bool Set(ISerializedObject @object, string propertyPath, object Value) {
            if (string.IsNullOrEmpty(@object.ObjectID)) return false;
            if (_serializedObject.TryGetValue(@object.ObjectID, out SerializedObject sdo))
                return sdo.Set(propertyPath, Value);
            else {
                SerializedPropertyCustom[] customs = GetPropertyList(@object, @object.GetType(), string.Empty);
                SerializedObject serialized = new SerializedObject(@object.ObjectID, customs);
                _serializedObject.Add(@object.ObjectID, serialized);
                return serialized.Set(propertyPath, Value);
            }
        }

        public static object Get(ISerializedObject @object, string propertyPath) {
            if (string.IsNullOrEmpty(@object.ObjectID)) return null;
            if (_serializedObject.TryGetValue(@object.ObjectID, out SerializedObject sdo))
                return sdo.Get(propertyPath);
            else {
                SerializedPropertyCustom[] customs = GetPropertyList(@object, @object.GetType(), string.Empty);
                SerializedObject serialized = new SerializedObject(@object.ObjectID, customs);
                _serializedObject.Add(@object.ObjectID, serialized);
                return serialized.Get(propertyPath);
            }
        }

        public static string GetID(Node node) => node.GetPathTo(node).ToString().Hash().ToString();

        public static SerializedPropertyCustom[] GetPropertyList(object obj, Type type, string propertyPath) {
            SerializedPropertyCustom[] customs = Array.Empty<SerializedPropertyCustom>();
            if (obj is null) return customs;

            foreach (MemberInfo member in type.GetFields((BindingFlags)_BindingFlags))
                if (IsSerialized(member)) {
                    if (_IsPrimetiveType(member)) {
                        SerializedPropertyCustom custom = GetSerializedPropertyCustom(GetMemberType(member));
                        if (custom is null) continue;
                        custom.PropertyPath = $"{propertyPath}{member.Name}";
                        custom.Member.Parent = obj;
                        custom.Member.Menber = member;
                        custom.InitPropMark();
                        ArrayManipulation.Add(custom, ref customs);
                    } else {
                        SerializedPropertyCustom[] serializeds = GetPropertyList(GetobjectMember(obj, member), GetMemberType(member), $"{member.Name}/");
                        if (ArrayManipulation.EmpytArray(serializeds)) {
                            SerializedPropertyCustom custom = GetSerializedPropertyCustom(GetMemberType(member));
                            if (custom is null) continue;
                            custom.PropertyPath = $"{propertyPath}{member.Name}";
                            custom.Member.Parent = obj;
                            custom.Member.Menber = member;
                            custom.InitPropMark();
                            ArrayManipulation.Add(custom, ref customs);
                        } else ArrayManipulation.Add(serializeds, ref customs);
                    }
                }
            foreach (MemberInfo member in type.GetProperties((BindingFlags)_BindingFlags))
                if (member.Name == "ObjectID") continue;
                else if (IsSerialized(member)) {
                    if (_IsPrimetiveType(member)) {
                        SerializedPropertyCustom custom = GetSerializedPropertyCustom(GetMemberType(member));
                        if (custom is null) continue;
                        custom.PropertyPath = $"{propertyPath}{member.Name}";
                        custom.Member.Parent = obj;
                        custom.Member.Menber = member;
                        custom.InitPropMark();
                        ArrayManipulation.Add(custom, ref customs);
                    } else {
                        SerializedPropertyCustom[] serializeds = GetPropertyList(GetobjectMember(obj, member), GetMemberType(member), $"{member.Name}/");
                        if (ArrayManipulation.EmpytArray(serializeds)) {
                            SerializedPropertyCustom custom = GetSerializedPropertyCustom(GetMemberType(member));
                            if (custom is null) continue;
                            custom.PropertyPath = $"{propertyPath}{member.Name}";
                            custom.Member.Parent = obj;
                            custom.Member.Menber = member;
                            custom.InitPropMark();
                            ArrayManipulation.Add(custom, ref customs);
                        } else ArrayManipulation.Add(serializeds, ref customs);
                    }
                }
            return customs;
        }

        private static object GetobjectMember(object parent, MemberInfo property) 
            => property switch {
                FieldInfo fdi => fdi.GetValue(parent),
                PropertyInfo pti => pti.GetValue(parent),
                _ => false,
            };

        private static bool IsSerialized(MemberInfo property)
            => property switch {
                FieldInfo fdi => (fdi.GetCustomAttribute<SerializeFieldAttribute>() != null &&
                    fdi.FieldType.GetAttribute<SerializableAttribute>() != null) ||
                    (_IsPrimetiveType(fdi.FieldType) && fdi.GetCustomAttribute<SerializeFieldAttribute>() != null),
                PropertyInfo pti => (pti.GetCustomAttribute<SerializeFieldAttribute>() != null &&
                    pti.PropertyType.GetAttribute<SerializableAttribute>() != null) ||
                    (_IsPrimetiveType(pti.PropertyType) && pti.GetCustomAttribute<SerializeFieldAttribute>() != null),
                _ => false,
            };

        private static bool _IsPrimetiveType(Type type)
            => type.CompareType(typeof(byte), typeof(sbyte), typeof(short), typeof(ushort), typeof(int), typeof(uint),
                typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(string), typeof(bool));

        private static bool _IsPrimetiveType(MemberInfo member) => _IsPrimetiveType(GetMemberType(member));

        private static Type GetMemberType(MemberInfo member)
            => member switch {
                FieldInfo fdi => fdi.FieldType,
                PropertyInfo pti => pti.PropertyType,
                _ => NullObject.Null.GetType(),
            };

        private static SerializedPropertyCustom GetSerializedPropertyCustom(Type type) {
            if (cs_Properties.TryGetValue(type, out Type custom))
                return custom.Activator<SerializedPropertyCustom>();
            foreach (Type item in TypeUtilitarian.GetTypes())
                if (item.CompareTypeAndSubType<SerializedPropertyCustom>())
                    foreach (SerializedPropertyCustomAttribute item2 in item.GetAttributes<SerializedPropertyCustomAttribute>())
                        if (item2.IsTargetType(type)) {
                            cs_Properties.Add(type, item);
                            return item.Activator<SerializedPropertyCustom>();
                        }
            return null;
        }
    }
}
