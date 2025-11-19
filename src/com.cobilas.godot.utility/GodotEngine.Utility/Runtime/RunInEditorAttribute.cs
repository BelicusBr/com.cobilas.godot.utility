using Godot;
using System;

namespace Cobilas.Runtime {
    /// <summary>The attribute allows you to run a script in editor mode.</summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class RunInEditorAttribute : ToolAttribute {}
}