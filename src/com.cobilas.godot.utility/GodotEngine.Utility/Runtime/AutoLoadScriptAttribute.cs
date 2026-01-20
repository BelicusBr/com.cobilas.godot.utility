using System;

namespace Cobilas.GodotEngine.Utility.Runtime;
/// <summary>Marks a class to be automatically generated as an autoload script for the Godot engine.</summary>
/// <remarks>
/// Types marked with this attribute will be automatically processed by the <see cref="AutoLoadScript"/>
/// editor plugin, which generates corresponding C# scripts and registers them as autoload singletons.
/// </remarks>
/// <param name="priority">The priority for autoload registration order. Lower values are registered first.</param>
/// <param name="autoLoadName">The name to use for the autoload singleton. If null, a default name will be generated.</param>
/// <seealso cref="AutoLoadScript"/>
[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class AutoLoadScriptAttribute(int priority = 0, string? autoLoadName = null) : Attribute {
	/// <summary>Gets or sets the priority for autoload registration order.</summary>
	/// <returns>The priority value for autoload ordering.</returns>
	/// <value>A value determining the order in which autoloads are registered.</value>
	/// <remarks>
	/// Lower priority values are processed and registered before higher values.
	/// This affects the initialization order of autoload singletons.
	/// </remarks>
	public int Priority { get; set; } = priority;
	/// <summary>Gets or sets the name to use for the autoload singleton.</summary>
	/// <returns>The custom name for the autoload singleton, or null to use a default name.</returns>
	/// <value>The name that will be used when registering the autoload singleton.</value>
	/// <remarks>
	/// If not set, the autoload will be registered with a default name based on the class name.
	/// This name is used in the Godot autoload settings and can be accessed via the singleton.
	/// </remarks>
	public string? AutoLoadName { get; set; } = autoLoadName;
}