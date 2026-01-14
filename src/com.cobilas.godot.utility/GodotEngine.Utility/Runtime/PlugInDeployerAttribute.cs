using System;

namespace Cobilas.GodotEngine.Utility.Runtime;
/// <summary>Marks a class to be automatically deployed as a Godot editor plugin.</summary>
/// <example>
///		<code>
///		using Godot;
///		using Cobilas.GodotEngine.Utility.Runtime;
///		
///		namespace Godot.PlugIn.Test;
///		[PlugInDeployer]
///		public class PlugInTest : EditorPlugin {
///			// Here you can put any code you want.
///		}
///		</code>
/// </example>
/// <seealso cref="PlugInDeployer"/>
[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class PlugInDeployerAttribute : Attribute { }