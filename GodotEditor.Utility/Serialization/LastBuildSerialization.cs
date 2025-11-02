using Godot;
using System;
using Cobilas.GodotEngine.Utility.Runtime;

namespace Cobilas.GodotEditor.Utility.Serialization;

[RunTimeInitializationClass(nameof(LastBuildSerialization), lastBoot:true)]
internal sealed class LastBuildSerialization :Node {

	internal static event Action? Ready = null;

	public override void _Ready() {
		try
		{

			Ready?.Invoke();
		}
		catch (Exception ex)
		{
			OS.Alert(ex.ToString());
		}
	}
}
