using Godot;
using Cobilas.GodotEditor.Utility.Serialization.Interfaces;

namespace Cobilas.GodotEditor.Utility.Serialization.Properties;
/// <summary>Provides a custom property handler for <see cref="Godot.Resource"/> type serialization.</summary>
/// <remarks>
/// This class handles the serialization and deserialization of Godot resources,
/// converting between resource instances and their file paths for caching purposes.
/// </remarks>
public sealed class ResourceCustom : PropertyCustom {
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
		if (string.IsNullOrEmpty(value)) return (Resource?)null;
		return GD.Load(value);
	}
	/// <inheritdoc/>
	public override object? Get(string? propertyName) {
		if (propertyName is null) return (Resource?)null;
		return Value;
	}
	/// <inheritdoc/>
	public override PropertyItem[] GetPropertyList() {
		PropertyUsageFlags flags = PropertyUsageFlags.Storage | PropertyUsageFlags.ScriptVariable;
		if (!IsHide) flags |= PropertyUsageFlags.Editor;
		return new PropertyItem[] {
			new(PropertyPath, Variant.Type.Object, Attribute.Hint.Hint, Attribute.Hint.HintString, flags)
		};
	}
	/// <inheritdoc/>
	public override string? ObjectToCacheValue(string? propertyName, object? value) {
		if (value is null) return (string?)null;
		else if (propertyName is null) return (string?)null;
		return ((Resource)value).ResourcePath;
	}
	/// <inheritdoc/>
	public override bool Set(string? propertyName, object? value) {
		if (propertyName is null) return false;
		else if (propertyName == string.Empty) return false;
		Value = (Resource?)value;
		return true;
	}
	/// <inheritdoc/>
	public override bool VerifyPropertyName(string? propertyName) => propertyName == PropertyPath;
}
