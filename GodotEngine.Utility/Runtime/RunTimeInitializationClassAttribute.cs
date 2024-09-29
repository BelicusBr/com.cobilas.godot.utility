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
    /// <value>Returns the priority execution level.</value>
    public int SubPriority { get; private set; }
    /// <summary>The name of the priority.</summary>
    /// <value>Returns the name of the priority that will be executed.</value>
    public string ClassName { get; private set; }
    /// <summary>The type of priority.</summary>
    /// <value>Returns the type of priority that will be executed.</value>
    public Priority BootPriority { get; private set; }

    /// <summary>Instance the RunTimeInitializationClassAttribute attribute.</summary>
    /// <param name="bootPriority">The type of priority.</param>
    /// <param name="name">The name of the priority.</param>
    /// <param name="subPriority">The execution priority.</param>
    public RunTimeInitializationClassAttribute(Priority bootPriority, string? name, int subPriority) {
        SubPriority = subPriority;
        ClassName = name!;
        BootPriority = bootPriority;
    }

    /// <summary>Instance the RunTimeInitializationClassAttribute attribute.
    /// <para>By default the execution priority is 0.</para>
    /// </summary>
    /// <param name="bootPriority">The type of priority.</param>
    /// <param name="name">The name of the priority.</param>
    public RunTimeInitializationClassAttribute(Priority bootPriority, string? name) : this(bootPriority, name, 0) {}

    /// <summary>Instance the RunTimeInitializationClassAttribute attribute.
    /// <para>By default the execution priority is 0.</para>
    /// <para>By default the priority name and the class name that the attribute is associated with.</para>
    /// </summary>
    /// <param name="bootPriority">The type of priority.</param>
    public RunTimeInitializationClassAttribute(Priority bootPriority) : this(bootPriority, null) {}

    /// <summary>Instance the RunTimeInitializationClassAttribute attribute.
    /// <para>By default the priority type is <seealso cref="Priority.StartBefore"/>.</para>
    /// </summary>
    /// <param name="name">The name of the priority.</param>
    /// <param name="subPriority">The execution priority.</param>
    public RunTimeInitializationClassAttribute(string name, int subPriority) : this(Priority.StartBefore, name, subPriority) {}

    /// <summary>Instance the RunTimeInitializationClassAttribute attribute.
    /// <para>By default the priority type is <seealso cref="Priority.StartBefore"/>.</para>
    /// <para>By default the execution priority is 0.</para>
    /// </summary>
    /// <param name="name">The name of the priority.</param>
    public RunTimeInitializationClassAttribute(string name) : this(Priority.StartBefore, name) {}

    /// <summary>Instance the RunTimeInitializationClassAttribute attribute.
    /// <para>By default the priority type is <seealso cref="Priority.StartBefore"/>.</para>
    /// <para>By default the execution priority is 0.</para>
    /// <para>By default the priority name and the class name that the attribute is associated with.</para>
    /// </summary>
    public RunTimeInitializationClassAttribute() : this(Priority.StartBefore) {}
}