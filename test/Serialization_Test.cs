using Cobilas.GodotEditor.Utility.Serialization;
using Cobilas.GodotEngine.Utility;
using Cobilas.GodotEngine.Utility.Numerics;
using Godot;
using System;

[Tool]
public class Serialization_Test : Node2D
{
    [ShowProperty(true)] private ObjectRef Ref;
	[ShowProperty()] private ObjectRef Ref2;
	[ShowProperty(true)] private Rect2D Rect;
	[ShowProperty(true)] private Vector2D vec2;
	[ShowProperty(true)] private Vector2DInt vec2i;
	[ShowProperty(true)] private Vector3D vec3;
	[ShowProperty(true)] private Vector3DInt vec3i;
	[ShowProperty(true)] private Vector4D vec4;
	[Export] private NodePath label;
	private string refv;

    public override void _Ready() {
        Label _label = GetNode<Label>(label);
		try
		{
			Node node = Ref;
			_label.AppendLine(refv);
			_label.AppendLine($"=={nameof(Ref)}==");
			_label.Append("node path: ");
			_label.AppendLine(node.GetPath());

			node = Ref2;
			_label.AppendLine($"=={nameof(Ref2)}==");
			_label.Append("node path: ");
			_label.AppendLine(node.GetPath());

			_label.AppendLine($"=={nameof(Rect)}==");
			_label.AppendLine($"{Rect}");

			_label.AppendLine($"=={nameof(vec2)}==");
			_label.AppendLine($"{vec2}");

			_label.AppendLine($"=={nameof(vec2i)}==");
			_label.AppendLine($"{vec2i}");

			_label.AppendLine($"=={nameof(vec3)}==");
			_label.AppendLine($"{vec3}");

			_label.AppendLine($"=={nameof(vec3i)}==");
			_label.AppendLine($"{vec3i}");

			_label.AppendLine($"=={nameof(vec4)}==");
			_label.AppendLine($"{vec4}");
		}
		catch (Exception ex) {
			_label.AppendLine(ex.ToString());
		}
    }

	public override object _Get(string property)
		=> BuildSerialization.GetValue(this, property);

	public override bool _Set(string property, object value)
	{
		if (property == "Ref_ref")
			refv = $"Ref_ref : {value}";
		return BuildSerialization.SetValue(this, property, value);
	}

	public override Godot.Collections.Array _GetPropertyList()
		=> PropertyItem.PropertyItemArrayToGDArray(BuildSerialization.GetPropertyList(this));
}
