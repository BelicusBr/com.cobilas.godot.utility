using System;
using System.Reflection;
using Cobilas.Collections;

namespace Cobilas.GodotEngine.Utility.Serialization; 
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
    /// <summary>The member's secondary names.</summary>
    /// <remarks>
    /// Secondary names are used to identify fields.
    /// <para>Example:</para>
    /// a Vector2D type structure has fields x and y but they are not naturally serialized, 
    /// which makes secondary names necessary for them to be manipulated. For example, 
    /// we create secondary names "{0}/x" and "{0}/y" in this format that will be merged 
    /// with the PropertyPath property in the IsPropertyPath method.
    /// </remarks>
    /// <returns>Returns the secondary names of the member.</returns>
    public string[] PropertyNames { get; private set; }
    /// <summary>The name of the member.</summary>
    /// <returns>Returns the name of the member.</returns>
    public string Name => Menber is null ? string.Empty : Menber.Name;
    public bool IsStruct => TypeMenber.IsValueType;
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
    public static MemberItem Null => new(Array.Empty<string>()) {
        Parent = "null",
        Menber = null
    };
    /// <summary>Creates a new instance of this object.</summary>
    public MemberItem(params string[] propertyNames) 
        => this.PropertyNames = propertyNames;
    /// <summary>Creates a new instance of this object.</summary>
    public MemberItem() : this("{0}") { }
    /// <summary>Check if property name belongs to this member.</summary>
    /// <param name="propertyName">Name of the property to be checked.</param>
    /// <param name="index">The secondary name index.</param>
    /// <returns>returns <c>true</c> if name belongs to this member.</returns>
    public bool IsPropertyName(string propertyName, out int index) {
        for (int I = 0; I < ArrayManipulation.ArrayLength(PropertyNames); I++)
            if (string.Format(PropertyNames[I], Menber?.Name) == propertyName) {
                index = I;
                return true;
            }
        index = -1;
        return false;
    }
}
