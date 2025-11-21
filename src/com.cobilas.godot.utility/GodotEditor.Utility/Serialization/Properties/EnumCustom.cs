using Godot;
using System;
using System.Text;
using System.Collections.Generic;
using Cobilas.GodotEditor.Utility.Serialization.Interfaces;

namespace Cobilas.GodotEditor.Utility.Serialization.Properties;
/// <summary>Provides a customizable class for <seealso cref="Enum"/>.</summary>
[PropertyCustom(typeof(Enum), true)]
public sealed class EnumCustom : PropertyCustom {
    /// <inheritdoc/>
    public override bool IsHide => Member.IsHide;
    /// <inheritdoc/>
    public override MemberItem Member { get; set; } = MemberItem.Null;
    /// <inheritdoc/>
    public override string PropertyPath { get; set; } = string.Empty;
	/// <inheritdoc/>
	public override IPropertyRender? PropertyRender { get; set; }
	/// <inheritdoc/>
	public override object? CacheValueToObject(string? propertyName, string? value) {
        if (string.IsNullOrEmpty(value)) return null;
        else if (Member.TypeMenber.CompareType<Enum>()) return Enum.Parse(Member.TypeMenber, value);
        return value;
    }
	/// <inheritdoc/>
	public override bool VerifyPropertyName(string? propertyName) => propertyName == PropertyPath;
	/// <inheritdoc/>
	public override string? ObjectToCacheValue(string? propertyName, object? value)
	{
		if (value is null) return string.Empty;
		return value.ToString();
	}
    /// <inheritdoc/>
    public override object? Get(string? propertyName) {
        if (propertyName is null) return null;
        else if (propertyName == string.Empty) return null;
        else if (propertyName == PropertyPath) return (int?)Value;
        return null;
    }
    /// <inheritdoc/>
    public override PropertyItem[] GetPropertyList() {
        Enum? @enum = (Enum?)Value;
        if (@enum is null) return Array.Empty<PropertyItem>();
        PropertyUsageFlags flags = PropertyUsageFlags.Storage | PropertyUsageFlags.ScriptVariable;
        if (!IsHide) flags |= PropertyUsageFlags.Editor;
        
        StringBuilder builder = new();
        foreach (KeyValuePair<string, int> item in @enum.GetEnumPairs())
            builder.AppendFormat("{0}:{1},", item.Key, item.Value);

        return new PropertyItem[] {
            new(PropertyPath, Godot.Variant.Type.Int, Godot.PropertyHint.Enum,
            builder.ToString().TrimEnd(','), flags)
        };
    }
    /// <inheritdoc/>
    public override bool Set(string? propertyName, object? value) {
        if (propertyName is null) return false;
        else if (propertyName == string.Empty) return false;
        else if (propertyName == PropertyPath) {
            if (value is null) return false;
            Value = Enum.Parse(Member.TypeMenber, value.ToString());
            return true;
        }
        return false;
    }
}
