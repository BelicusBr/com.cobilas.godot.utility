using System;

namespace Cobilas.GodotEngine.Utility.Runtime; 

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class RunTimeInitializationClassAttribute : Attribute {
    public int SubPriority { get; private set; }
    public string ClassName { get; private set; }
    public Priority BootPriority { get; private set; }

    public RunTimeInitializationClassAttribute(Priority bootPriority, string? name, int subPriority) {
        SubPriority = subPriority;
        ClassName = name!;
        BootPriority = bootPriority;
    }

    public RunTimeInitializationClassAttribute(Priority bootPriority, string? name) : this(bootPriority, name, 0) {}

    public RunTimeInitializationClassAttribute(Priority bootPriority) : this(bootPriority, null) {}

    public RunTimeInitializationClassAttribute(string name, int subPriority) : this(Priority.StartBefore, name, subPriority) {}

    public RunTimeInitializationClassAttribute(string name) : this(Priority.StartBefore, name) {}

    public RunTimeInitializationClassAttribute() : this(Priority.StartBefore) {}
}