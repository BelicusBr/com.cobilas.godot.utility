using Godot;
using System;
using Cobilas.GodotEditor.Utility.Serialization;
using Cobilas.GodotEditor.Utility.Serialization.Properties;

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
	public override object? CacheValueToObject(string? propertyName, string? value) {
		if (propertyName != $"{PropertyPath}/path") return null;
		else if (Member.Value is null) return null;
		else if (value is null) return (NodePath?)value;
		(Member.Value as ObjectRef)!.Set(value);
		return (NodePath)value;
	}
	/// <inheritdoc/>
	public override object? Get(string? propertyName) {
		if (propertyName != $"{PropertyPath}/path") return null;
		else if (Member.Value is null) return null;
		return (NodePath)(ObjectRef)Member.Value;
	}
	/// <inheritdoc/>
	public override PropertyItem[] GetPropertyList() {
		PropertyUsageFlags flags = PropertyUsageFlags.Storage | PropertyUsageFlags.ScriptVariable;
		if (!IsHide) flags |= PropertyUsageFlags.Editor;

		return new PropertyItem[] {
			new($"{PropertyPath}/path", type:Variant.Type.NodePath, usage:flags)
		};
	}
	/// <inheritdoc/>
	public override bool Set(string? propertyName, object? value) {
		if (propertyName != $"{PropertyPath}/path") return false;
		else if (value is null) return false;
		else if (Member.Value is null) Member.Value = Member.TypeMenber.Activator();
		(Member.Value as ObjectRef)!.Set(value as NodePath);
		return true;
	}
}
