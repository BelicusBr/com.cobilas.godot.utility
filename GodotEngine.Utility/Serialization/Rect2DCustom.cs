using Godot;
using System;
using Cobilas.Collections;
using System.Globalization;
using Cobilas.GodotEditor.Utility.Serialization;
using Cobilas.GodotEditor.Utility.Serialization.Interfaces;
using Cobilas.GodotEditor.Utility.Serialization.Properties;

namespace Cobilas.GodotEngine.Utility.Serialization;
/// <summary>Custom property handler for Rect2D type serialization.</summary>
[PropertyCustom(typeof(Rect2D))]
public sealed class Rect2DCustom : PropertyCustom {
	private static readonly char[] separator = { ';' };
	/// <inheritdoc/>
	public override bool IsHide => Member.IsHide;
	/// <inheritdoc/>
	public override string PropertyPath { get; set; } = String.Empty;
	/// <inheritdoc/>
	public override MemberItem Member { get; set; } = MemberItem.Null;
	/// <inheritdoc/>
	public override IPropertyRender? PropertyRender { get; set; }
	/// <inheritdoc/>
	public override object? CacheValueToObject(string? propertyName, string? value) {
		if (string.IsNullOrEmpty(propertyName)) return null;
		else if (string.IsNullOrEmpty(value)) return null;
		if (propertyName == $"{PropertyPath}/rect_rotation")
			return float.Parse(value, NumberStyles.Float, CultureInfo.InvariantCulture);
		string[] split = value!.Split(separator, StringSplitOptions.RemoveEmptyEntries);
		if (ArrayManipulation.EmpytArray(split))
			return Vector2.Zero;
		return new Vector2(
				float.Parse(split[0], NumberStyles.Float, CultureInfo.InvariantCulture),
				float.Parse(split[1], NumberStyles.Float, CultureInfo.InvariantCulture)
			);
	}
	/// <inheritdoc/>
	public override object? Get(string? propertyName) {
		if (string.IsNullOrEmpty(propertyName)) return null;
		Rect2D result = Value is null ? new() : (Rect2D)Value;
		if (propertyName == $"{PropertyPath}/rect_position")
			return (Vector2)result.Position;
		else if (propertyName == $"{PropertyPath}/rect_size")
			return (Vector2)result.Size;
		else if (propertyName == $"{PropertyPath}/rect_min_size")
			return (Vector2)result.MinSizeScale;
		else if (propertyName == $"{PropertyPath}/rect_rotation")
			return result.Rotation;
		else if (propertyName == $"{PropertyPath}/rect_scale")
			return (Vector2)result.Scale;
		else if (propertyName == $"{PropertyPath}/rect_pivot")
			return (Vector2)result.Pivot;
		return null;
	}
	/// <inheritdoc/>
	public override PropertyItem[] GetPropertyList() {
		PropertyUsageFlags flags = PropertyUsageFlags.Storage | PropertyUsageFlags.ScriptVariable;
		if (!IsHide) flags |= PropertyUsageFlags.Editor;

		return new PropertyItem[] {
			new($"{PropertyPath}/rect_position", Variant.Type.Vector2, usage:flags),
			new($"{PropertyPath}/rect_size", Variant.Type.Vector2, usage:flags),
			new($"{PropertyPath}/rect_min_size", Variant.Type.Vector2, usage:flags),
			new($"{PropertyPath}/rect_rotation", Variant.Type.Real, usage:flags),
			new($"{PropertyPath}/rect_scale", Variant.Type.Vector2, usage:flags),
			new($"{PropertyPath}/rect_pivot", Variant.Type.Vector2, usage:flags)
		};
	}
	/// <inheritdoc/>
	public override string? ObjectToCacheValue(string? propertyName, object? value) {
		if (string.IsNullOrEmpty(propertyName)) return string.Empty;
		else if (value is null) return string.Empty;
		if (propertyName == $"{PropertyPath}/rect_rotation")
			return Convert.ToString(value, CultureInfo.InvariantCulture);
		Vector2 result = value is null ? Vector2.Zero : (Vector2)value;
		return $"{result.x};{result.y}";
	}
	/// <inheritdoc/>
	public override bool Set(string? propertyName, object? value) {
		if (string.IsNullOrEmpty(propertyName)) return false;
		else if (value is null) return false;
		Rect2D result = Value is null ? new() : (Rect2D)Value;
		if (propertyName == $"{PropertyPath}/rect_position")
			result.SetPosition((Vector2)value);
		else if (propertyName == $"{PropertyPath}/rect_size")
			result.SetSize((Vector2)value);
		else if (propertyName == $"{PropertyPath}/rect_min_size")
			result.SetMinSize((Vector2)value);
		else if (propertyName == $"{PropertyPath}/rect_rotation")
			result.SetRotation(Convert.ToSingle(value, CultureInfo.InvariantCulture));
		else if (propertyName == $"{PropertyPath}/rect_scale")
			result.SetScale((Vector2)value);
		else if (propertyName == $"{PropertyPath}/rect_pivot")
			result.SetPivot((Vector2)value);
		Value = result;
		return true;
	}
	/// <inheritdoc/>
	public override bool VerifyPropertyName(string? propertyName) {
		if (string.IsNullOrEmpty(propertyName)) return false;
		return propertyName == $"{PropertyPath}/rect_position" ||
			propertyName == $"{PropertyPath}/rect_size" ||
			propertyName == $"{PropertyPath}/rect_min_size" ||
			propertyName == $"{PropertyPath}/rect_rotation" ||
			propertyName == $"{PropertyPath}/rect_scale" ||
			propertyName == $"{PropertyPath}/rect_pivot";
	}
}
