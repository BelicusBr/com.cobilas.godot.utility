using System;

namespace Cobilas.GodotEngine.Utility.Runtime; 

/// <summary>This attribute marks which classes will be called by <seealso cref="RunTimeInitialization"/>.</summary>
/// <example>
/// Simple example of class demarcation to be called by <seealso cref="RunTimeInitialization"/>.
/// <code>
/// using Godot;
/// using Cobilas.GodotEngine.Utility.Runtime;
/// [RunTimeInitializationClass]
/// public class ClassTest : Node {}
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class RunTimeInitializationClassAttribute : Attribute {
    /// <summary>The execution priority.</summary>
    /// <returns>Returns the priority execution level.</returns>
    public int SubPriority { get; private set; }
    /// <summary>The name of the priority.</summary>
    /// <returns>Returns the name of the priority that will be executed.</returns>
    public string ClassName { get; private set; }
    /// <summary>The type of priority.</summary>
    /// <returns>Returns the type of priority that will be executed.</returns>
    public Priority BootPriority { get; private set; }
    /// <summary>Allows the object to be called last in the root object hierarchy.</summary>
    /// <returns>Returns <c>true</c> when marked to be called last in the root object hierarchy.</returns>
    public bool LastBoot { get; private set; }
    /// <summary>Instance the RunTimeInitializationClassAttribute attribute.</summary>
    /// <param name="bootPriority">The type of priority.</param>
    /// <param name="name">The name of the priority.</param>
    /// <param name="subPriority">The execution priority.</param>
    /// <param name="lastBoot">Allows the object to be called last in the root object hierarchy.</param>
    public RunTimeInitializationClassAttribute(
        string? name,
        Priority bootPriority = Priority.StartBefore,
        int subPriority = 0, 
        bool lastBoot = false) {
        SubPriority = subPriority;
        ClassName = name is null ? string.Empty : name;
        BootPriority = bootPriority;
        LastBoot = lastBoot;
    }
    /// <summary>Instance the RunTimeInitializationClassAttribute attribute.</summary>
    public RunTimeInitializationClassAttribute() : this(string.Empty) {}
}