using System;
using System.Reflection;

namespace Cobilas.GodotEditor.Utility.Serialization; 
/// <summary>Represents a property or field.</summary>
public sealed class MemberItem : INullObject {
    /// <summary>The parent object of the member.</summary>
    /// <value>Receives the parent object of the member.</value>
    /// <returns>Returns the parent object of the member.</returns>
    public object? Parent { get; set; }
    /// <summary>The field or property.</summary>
    /// <value>Receives the field or property.</value>
    /// <returns>Returns the field or property.</returns>
    public MemberInfo? Menber { get; set; }
    /// <summary>The name of the member.</summary>
    /// <returns>Returns the name of the member.</returns>
    public string Name => Menber is null ? string.Empty : Menber.Name;
    /// <summary>Checks if the member is a <c>struct</c>.</summary>
    /// <returns>Returns <c>true</c> when the member is a <c>struct</c>.</returns>
    public bool IsStruct => TypeMenber.IsValueType;
    /// <summary>Check if the member is hidden from the editor.</summary>
    /// <returns>returns <c>true</c> when the member is hidden from the editor.</returns>
    public bool IsHide => Menber.GetCustomAttribute<HidePropertyAttribute>() is not null;
    /// <summary>Checks if the member value is cacheable.</summary>
    /// <returns>Returns <c>true</c> when the member value is cacheable.</returns>
    public bool IsSaveCache {
        get {
            SerializeFieldAttribute attribute = Menber.GetCustomAttribute<SerializeFieldAttribute>();
            return attribute is not null && attribute.SaveInCache;
        }
    }
    /// <summary>Check if it is written.</summary>
    /// <returns>Returns <c>true</c> if written.</returns>
    public bool IsWrite => Menber switch {
            FieldInfo fdi => !fdi.IsInitOnly,
            PropertyInfo pti => pti.CanWrite,
            _ => false,
        };
    /// <summary>Check if it is reading.</summary>
    /// <returns>Returns <c>true</c>if it is read.</returns>
    public bool IsRead => Menber switch {
            PropertyInfo pti => pti.CanRead,
            _ => true,
        };
    /// <summary>The value of the member.</summary>
    /// <returns>Returns the member value.</returns>
    /// <value>Receives the value from the member.</value>
    public object? Value { 
        get => Menber switch {
                FieldInfo fdi => fdi.GetValue(Parent),
                PropertyInfo pti => pti.GetValue(Parent),
                _ => null
            };
        set {
            switch (Menber) {
                case FieldInfo fdi: fdi.SetValue(Parent, value); break;
                case PropertyInfo pti: pti.SetValue(Parent, value); break;
            }
        }
    }
    /// <summary>The type of member.</summary>
    /// <returns>Returns the type of the member.</returns>
    public Type TypeMenber => Menber switch {
            FieldInfo fdi => fdi.FieldType,
            PropertyInfo pti => pti.PropertyType,
            _ => NullObject.Null.GetType(),
        };
    /// <summary>Null representation of <seealso cref="MemberItem"/>.</summary>
    /// <returns>Returns a null representation of <seealso cref="MemberItem"/>.</returns>
    public static MemberItem Null => new() {
        Parent = "null",
        Menber = null
    };
}
