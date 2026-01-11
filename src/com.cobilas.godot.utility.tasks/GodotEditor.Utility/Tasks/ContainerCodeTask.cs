namespace Cobilas.GodotEditor.Utility.Tasks;

public static class ContainerCodeTask {
	public const string GodotUtilityTaskCode =
@"using Cobilas.GodotEngine.Utility.Runtime;
#pragma warning disable IDE0130
namespace Godot.Runtime {
#pragma warning restore IDE0130
	public class GameRuntime : RunTimeInitialization { }
}";

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
	[Tool]
	public class GamePlugInDeployer : PlugInDeployer {}
}
#endif
";
}
