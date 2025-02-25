using Godot;
using System;
using System.Globalization;

namespace Cobilas.GodotEditor.Utility.Serialization.Properties;
/// <summary>Allows customization of a primitive type in the inspector.</summary>
public sealed class PrimitiveTypeCustom : PropertyCustom {
    /// <inheritdoc/>
    public override bool IsHide => Member.IsHide;
    /// <inheritdoc/>
    public override MemberItem Member { get; set; } = MemberItem.Null;
    /// <inheritdoc/>
    public override string PropertyPath { get; set; } = string.Empty;
    /// <inheritdoc/>
    public override object? Get(string? propertyName) {
        if (propertyName is null) return null;
        else if (propertyName == string.Empty) return null;
        else if (propertyName == PropertyPath) return Member.Value;
        return null;
    }
    /// <inheritdoc/>
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
            new(PropertyPath, type, Attribute.Hint.Hint, Attribute.Hint.HintString, flags)
        };
    }
    /// <inheritdoc/>
    public override bool Set(string? propertyName, object? value) {
        if (propertyName is null) return false;
        else if (propertyName == string.Empty) return false;
        else if (propertyName == PropertyPath) {
            Member.Value = value;
            return true;
        }
        return false;
    }
    /// <inheritdoc/>
    public static bool IsPrimitiveType(Type type) 
        => type.CompareType(typeof(string), typeof(bool), typeof(sbyte), typeof(byte), typeof(ushort),
            typeof(short), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float), typeof(double));
    /// <inheritdoc/>
    public override object? CacheValueToObject(string? propertyName, string? value) {
        if (value is null) return null;
        else if (string.IsNullOrEmpty(value)) return value;
        else if (Member.TypeMenber.CompareType<bool>()) return bool.Parse(value);
        else if (Member.TypeMenber.CompareType<sbyte>()) return sbyte.Parse(value);
        else if (Member.TypeMenber.CompareType<byte>()) return byte.Parse(value);
        else if (Member.TypeMenber.CompareType<ushort>()) return ushort.Parse(value);
        else if (Member.TypeMenber.CompareType<short>()) return short.Parse(value);
        else if (Member.TypeMenber.CompareType<uint>()) return uint.Parse(value);
        else if (Member.TypeMenber.CompareType<int>()) return int.Parse(value);
        else if (Member.TypeMenber.CompareType<ulong>()) return ulong.Parse(value);
        else if (Member.TypeMenber.CompareType<long>()) return long.Parse(value);
        else if (Member.TypeMenber.CompareType<float>()) return float.Parse(value, CultureInfo.InvariantCulture);
        else if (Member.TypeMenber.CompareType<double>()) return double.Parse(value, CultureInfo.InvariantCulture);
        return value;
    }
}
