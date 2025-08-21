#if GODOT_EDITOR
using Godot;
using System;

namespace Cobilas.Runtime {
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class RunInEditorAttribute : ToolAttribute {}
}
#endif