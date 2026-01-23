# [PlugInDeployer](README.md)

Now with `PlugInDeployer` it is possible to create plugins and load them via code, \
allowing the creation of plugins for `NuGet` packages.

## PlugInDeployerAttribute
To allow the plugin to be loaded via code, it is necessary to use \
the `PlugInDeployerAttribute` attribute in the target class and load it in the Godot project.
The plugin must inherit `EditorPlugin` to be loaded in the Godot project.

```csharp
using Godot;
using Cobilas.GodotEngine.Utility.Runtime;

namespace Godot.PlugIn.Test;
[PlugInDeployer]
public class PlugInTest : EditorPlugin {
	// Here you can put any code you want.
}
```

## PlugInDeployerDescriptionAttribute
To create a description for the custom plugin, it is necessary to create a static \
method with a return type of `PluginManifest` and that has the attribute `PlugInDeployerDescriptionAttribute`. \
The method does not need to have parameters and must be a public method.
This description method is optional, and if a description is not created, it will be created automatically.
This method must be in a class marked with the attribute `PlugInDeployerAttribute`.
The file name can be to your liking, but the class that will be inserted will have the \
same name as the original class preceded by the prefix `PlugIn_`.

```csharp
using Godot;
using Cobilas.GodotEngine.Utility.Runtime;

namespace Godot.PlugIn.Test;
[PlugInDeployer]
public class PlugInTest : EditorPlugin {
	// Here you can put any code you want.

	[PlugInDeployerDescription]
	private static PluginManifest GetPluginManifest()
		=> new(nameof(PlugInTest),// Plugin name
			"Plugin de teste", // Description
			"Author", 
			"1.0",
			$"PlugIn_{nameof(PlugInTest)}.cs" // File name.
		);
}
```

## Internal Plugin

Internal plugins are plugins that are loaded and executed through the `PlugInDeployer` class
without the need to create a separate directory for the specific plugin. \
This can be done using the `internalPlugIn` parameter.

```csharp
using Godot;
using Cobilas.GodotEngine.Utility.Runtime;

namespace Godot.PlugIn.Test;
[PlugInDeployer(internalPlugIn:true)]
public class PlugInTest : EditorPlugin {
	// Here you can put any code you want.
}
```