using Godot;
using System;
using System.Collections.Generic;

namespace Cobilas.GodotEngine.Utility.Runtime; 

/// <summary>Responsible for initializing other classes marked with the <seealso cref="RunTimeInitializationClassAttribute"/> attribute.</summary>
/// <example>
/// The <c>RunTimeInitialization</c> class allows you to automate the <kbd>Project&gt;Project Settings&gt;AutoLoad</kbd> option.
/// To use the <c>RunTimeInitialization</c> class, you must create a class and make it inherit <c>RunTimeInitialization</c>./// 
/// <code>
/// using Cobilas.GodotEngine.Utility.Runtime;
/// // The name of the class is up to you.
/// public class RunTimeProcess : RunTimeInitialization {}
/// </code>
/// And remember to add the class that inherits <c>RunTimeInitialization</c> in <kbd>Project&gt;Project Settings&gt;AutoLoad</kbd>.
/// Remembering that the <c>RunTimeInitialization</c> class uses the virtual method <c>_Ready()</c> to perform the initialization of other classes.
/// And to initialize other classes along with the <c>RunTimeInitialization</c> class, the class must inherit the <c>Godot.Node</c> class or some class that inherits <c>Godot.Node</c> and use the <c>RunTimeInitializationClassAttribute</c> attribute.
/// <code>
/// using Godot;
/// using Cobilas.GodotEngine.Utility.Runtime;
/// [RunTimeInitializationClass]
/// public class ClassTest : Node {}
/// </code>
/// </example>
public class RunTimeInitialization : Node {

    /// <summary>The interval in seconds from the last frame to the current one (Read Only)</summary>
    [Obsolete("Use RunTime.DeltaTime")]
    public const float DeltaTime = RunTime.DeltaTime;
    /// <summary>The interval in seconds of in-game time at which physics and other fixed frame rate updates  are performed.</summary>
    [Obsolete("Use RunTime.FixedDeltaTime")]
    public const float FixedDeltaTime = RunTime.FixedDeltaTime;

    /// <inheritdoc/>
    public override void _Ready() {
        Type[] components = TypeUtilitarian.GetTypes();
        Dictionary<Priority, PriorityList> pairs = new() {
            { Priority.StartBefore, new PriorityList() },
            { Priority.StartLater, new PriorityList() }
        };
        foreach (var item in components) {
            RunTimeInitializationClassAttribute attri = item.GetAttribute<RunTimeInitializationClassAttribute>();
            if (attri != null) {
                Node node = item.Activator<Node>();
                if (!string.IsNullOrEmpty(attri.ClassName))
                    node.Name = string.IsNullOrEmpty(attri.ClassName) ? item.Name : attri.ClassName;
                pairs[attri.BootPriority] = pairs[attri.BootPriority].Add(attri.SubPriority, node);
            }
        }

        using (PriorityList list = pairs[Priority.StartBefore]) {
            list.ReorderList();
            list.Run(this);
        }
        using (PriorityList list = pairs[Priority.StartLater]) {
            list.ReorderList();
            list.Run(this);
        }
    }
}