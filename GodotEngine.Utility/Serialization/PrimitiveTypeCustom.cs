using Godot;
using System;

namespace Cobilas.GodotEngine.Utility.Serialization;
public class PrimitiveTypeCustom : PropertyCustom {
    public override bool IsHide { get; set; }
    public override string PropertyPath { get; set; }
    public override MemberItem Member { get; set; }

    public override object? Get(string? propertyName) {
        if (propertyName is null) return null;
        else if (propertyName == string.Empty) return null;
        else if (propertyName == PropertyPath) return Member.Value;
        return null;
    }

    public override PropertyItem[] GetPropertyList() {
        PropertyUsageFlags flags = PropertyUsageFlags.Storage | PropertyUsageFlags.ScriptVariable;
        if (!IsHide) flags |= PropertyUsageFlags.Editor;
        Variant.Type type = Variant.Type.Nil;

        if (Member.TypeMenber.CompareType<string>()) type = Variant.Type.String;
        else if (Member.TypeMenber.CompareType<bool>()) type = Variant.Type.Bool;
        else if (Member.TypeMenber.CompareType(typeof(sbyte), typeof(byte), typeof(ushort),
            typeof(short), typeof(int), typeof(uint), typeof(long), typeof(ulong))) type = Variant.Type.Int;
        else if (Member.TypeMenber.CompareType(typeof(float), typeof(double))) type = Variant.Type.Real;

        return new PropertyItem[] {
            new(PropertyPath, type, usage:flags)
        };
    }

    public override bool Set(string? propertyName, object? value) {
        if (propertyName is null) return false;
        else if (propertyName == string.Empty) return false;
        else if (propertyName == PropertyPath) {
            Member.Value = value;
            return true;
        }
        return false;
    }

    public static bool IsPrimitiveType(Type type) 
        => type.CompareType(typeof(string), typeof(bool), typeof(sbyte), typeof(byte), typeof(ushort),
            typeof(short), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float), typeof(double));
}
