using Godot;
using Cobilas.GodotEditor.Utility.Serialization.Interfaces;

namespace Cobilas.GodotEditor.Utility.Serialization.Properties;
/// <summary>Custom property handler for NodePath type serialization.</summary>
public sealed class NodePathCustom : PropertyCustom {
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
		if (string.IsNullOrEmpty(value)) return (NodePath?)null;
		return (NodePath?)value;
	}
	/// <inheritdoc/>
	public override object? Get(string? propertyName) {
		if (propertyName is null) return (NodePath?)null;
		return Value;
	}
	/// <inheritdoc/>
	public override PropertyItem[] GetPropertyList() {
		PropertyUsageFlags flags = PropertyUsageFlags.Storage | PropertyUsageFlags.ScriptVariable;
		if (!IsHide) flags |= PropertyUsageFlags.Editor;
		return new PropertyItem[] {
			new(PropertyPath, Variant.Type.NodePath, Attribute.Hint.Hint, Attribute.Hint.HintString, flags)
		};
	}
	/// <inheritdoc/>
	public override string? ObjectToCacheValue(string? propertyName, object? value) {
		if (value is null) return (NodePath?)null;
		else if (propertyName is null) return (NodePath?)null;
		return (string?)(NodePath?)value;
	}
	/// <inheritdoc/>
	public override bool Set(string? propertyName, object? value) {
		if (propertyName is null) return false;
		else if (propertyName == string.Empty) return false;
		Value = (NodePath?)value;
		return true;
	}
	/// <inheritdoc/>
	public override bool VerifyPropertyName(string? propertyName) => propertyName == PropertyPath;
}
