using System;

namespace Cobilas.GodotEngine.Utility.Runtime;
/// <summary>Marks a method that returns a <see cref="PlugInManifest"/> containing plugin metadata.</summary>
/// <example>
///		<code>
///		using Godot;
///		using Cobilas.GodotEngine.Utility.Runtime;
///		
///		namespace Godot.PlugIn.Test;
///		[PlugInDeployer]
///		public class PlugInTest : EditorPlugin {
///			// Here you can put any code you want.
///			[PlugInDeployerDescription]
///			private static PluginManifest GetPluginManifest()
///				=> new(nameof(PlugInTest),// Plugin name
///					"Plugin de teste", // Description
///					"Author",
///					"1.0",
///					$"PlugIn_{nameof(PlugInTest)}.cs" // File name.
///				);
///		}
///		</code>
/// </example>
/// <seealso cref="PlugInDeployer"/>
/// <seealso cref="PlugInManifest"/>
[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public sealed class PlugInDeployerDescriptionAttribute : Attribute { }