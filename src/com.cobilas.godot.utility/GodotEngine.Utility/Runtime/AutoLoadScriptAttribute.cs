using System;

namespace Cobilas.GodotEngine.Utility.Runtime;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class AutoLoadScriptAttribute(int priority) : Attribute {
	public int Priority { get; set; } = priority;

	public AutoLoadScriptAttribute() : this(0) { }
}