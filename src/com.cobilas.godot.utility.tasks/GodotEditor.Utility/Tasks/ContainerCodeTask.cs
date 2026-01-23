namespace Cobilas.GodotEditor.Utility.Tasks;

public static class ContainerCodeTask {

	public const string PlugInDeployer_CFG =
@"[plugin]

name=""PlugInDeployer""
description=""Automatic plugin launcher.""
author=""Cobilas""
version=""1.0""
script=""GamePlugInDeployer.cs""
";

	public const string PlugInDeployerCode =
@"#if TOOLS
using Cobilas.GodotEngine.Utility.Runtime;
#pragma warning disable IDE0130
namespace Godot.PlugIn {
#pragma warning restore IDE0130
	/// <inheritdoc/>
	[Tool]
	public class GamePlugInDeployer : PlugInDeployer {}
}
#endif
";

	public const string PlugInDeployer_GD_CFG =
@"[plugin]

name=""PlugInDeployerStart""
description=""Automatic plugin launcher.""
author=""Cobilas""
version=""1.0""
script=""GamePlugInDeployer.gd""
";

	public const string PlugInDeployerGDCode =
@"tool
extends EditorPlugin

var initPlugInDeployer : bool = false

func _enter_tree():
	if plugInEnabled(""PlugInDeployer"") or initPlugInDeployer:
		return
	initPlugInDeployer = true
	dotnetBuild()
	pass
	
func _process(delta):
	if plugInEnabled(""PlugInDeployer""):
		return
	setPluginEnabled(""PlugInDeployer"", true, false)
	pass
	
func _exit_tree():
	if plugInEnabled(""PlugInDeployer""):
		return
	setPluginEnabled(""PlugInDeployer"", true, true)
	pass
	
func setPluginEnabled(pluginName:String, status:bool, build:bool = false):
	if build:
		dotnetBuild()
	get_editor_interface().set_plugin_enabled(pluginName, status)
	ProjectSettings.save()
	pass

func dotnetBuild():
	var num1 = OS.execute(""dotnet"", [""build""])
	match num1:
		0:
			print(""dotnet build executed successfully!"")
		_:
			print(""dotnet build failed!"", ""(Code error:"", num1, "")"")
	pass

func plugInEnabled(pluginName:String) -> bool:
	return get_editor_interface().is_plugin_enabled(pluginName)
";
}
