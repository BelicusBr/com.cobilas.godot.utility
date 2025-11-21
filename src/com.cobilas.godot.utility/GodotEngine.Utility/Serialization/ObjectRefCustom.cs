using Godot;
using System;
using Cobilas.GodotEditor.Utility.Serialization;
using Cobilas.GodotEditor.Utility.Serialization.Properties;
using Cobilas.GodotEditor.Utility.Serialization.Interfaces;

namespace Cobilas.GodotEngine.Utility.Serialization;
/// <summary>Provides custom property serialization for <see cref="ObjectRef"/> types in the Godot editor.</summary>
/// <remarks>
/// This class handles the serialization and deserialization of ObjectRef instances,
/// allowing them to be properly displayed and edited in the Godot editor's inspector.
/// </remarks>
[PropertyCustom(typeof(ObjectRef), true)]
public sealed class ObjectRefCustom : PropertyCustom {
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
		if (propertyName != $"{PropertyPath}_ref") return null;
		else if (value is null) return (NodePath?)value;
		return (NodePath)value;
	}
	/// <inheritdoc/>
	public override string? ObjectToCacheValue(string? propertyName, object? value) {
		if (propertyName != $"{PropertyPath}_ref") return string.Empty;
		else if (value is null) return string.Empty;
		return value.ToString();
	}
	/// <inheritdoc/>
	public override bool VerifyPropertyName(string? propertyName) => propertyName == $"{PropertyPath}_ref";
	/// <inheritdoc/>
	public override object? Get(string? propertyName) {
		if (propertyName != $"{PropertyPath}_ref") return null;
		else if (Value is null) return null;
		return (NodePath)(ObjectRef)Value;
	}
	/// <inheritdoc/>
	public override PropertyItem[] GetPropertyList() {
		PropertyUsageFlags flags = PropertyUsageFlags.Storage | PropertyUsageFlags.ScriptVariable;
		if (!IsHide) flags |= PropertyUsageFlags.Editor;

		return new PropertyItem[] {
			new($"{PropertyPath}_ref", type:Variant.Type.NodePath, usage:flags)
		};
	}
	/// <inheritdoc/>
	public override bool Set(string? propertyName, object? value) {
		if (propertyName != $"{PropertyPath}_ref") return false;
		else if (value is null) return false;
		(Value as ObjectRef)!.Set(value as NodePath);
		return true;
	}
}
