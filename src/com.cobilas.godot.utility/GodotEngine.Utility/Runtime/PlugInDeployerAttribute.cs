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
/// <param name="internalPlugIn">Indicates whether the plugin will run inside the <see cref="PlugInDeployer"/> class.</param>
[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class PlugInDeployerAttribute(bool internalPlugIn = false) : Attribute {
	/// <summary>Gets or sets a value indicating whether the plugin runs within the <see cref="PlugInDeployer"/> class.</summary>
	/// <returns>True if the plugin runs inside <see cref="PlugInDeployer"/>; otherwise, false.</returns>
	/// <value>True to run the plugin inside <see cref="PlugInDeployer"/>; false to deploy as a standalone plugin.</value>
	/// <remarks>
	/// When set to true, the plugin will be executed within the context of the <see cref="PlugInDeployer"/> class.
	/// When false, it will be deployed as a regular standalone plugin in the Godot editor.
	/// </remarks>
	public bool InternalPlugIn { get; set; } = internalPlugIn;
}