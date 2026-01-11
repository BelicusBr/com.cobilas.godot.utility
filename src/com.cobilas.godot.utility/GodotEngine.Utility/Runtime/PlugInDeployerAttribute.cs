using Godot;
using System;

namespace Cobilas.GodotEngine.Utility.Runtime;

[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public sealed class PlugInDeployerAttribute : ToolAttribute { }