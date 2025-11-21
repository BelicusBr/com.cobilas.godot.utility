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
    internal static bool _closePlayModeStateChanged;
    private static event Action<PlayModeStateChange>? _playModeStateChanged;
    /// <summary>Event that is raised whenever the Editor's play mode state changes.</summary>
    public static event Action<PlayModeStateChange>? PlayModeStateChanged {
        add {
            if (!_closePlayModeStateChanged) {
                if (GDFeature.HasEditor) value?.Invoke(PlayModeStateChange.ExitingEditMode);
                value?.Invoke(PlayModeStateChange.EnteredPlayMode);
            }
            _playModeStateChanged += value;
        }
        remove => _playModeStateChanged -= value;
    }
    /// <inheritdoc/>
    public override void _Ready() {
        RunTime.ExecutionMode = ExecutionMode.PlayerMode;
        LastRunTimeInitialization lastRunTime = new() {
            Name = nameof(LastRunTimeInitialization)
        };
        GetTree().Root.CallDeferred("add_child", lastRunTime);

        Type[] components = TypeUtilitarian.GetTypes();
        StartRunTimeInitializationClass(this, components, false);
    }
    /// <inheritdoc/>
    public override void _EnterTree() => _closePlayModeStateChanged = false;
    /// <inheritdoc/>
    public override void _ExitTree() {
        _playModeStateChanged?.Invoke(PlayModeStateChange.ExitingPlayMode);
        if (GDFeature.HasEditor) _playModeStateChanged?.Invoke(PlayModeStateChange.EnteredEditMode);
    }
    /// <summary>Initializes all classes that are marked with the <seealso cref="RunTimeInitializationClassAttribute"/> attribute.</summary>
    /// <param name="taget">The node that will receive the components.</param>
    /// <param name="components">The list of all types that will be checked to see if they contain the <seealso cref="RunTimeInitializationClassAttribute"/> attribute.</param>
    /// <param name="lastBoot"><c>true</c> for classes that will be started last in the root object hierarchy.
    /// <para><c>false</c> for classes that will be started first in the root object hierarchy.</para>
    /// </param>
    internal static void StartRunTimeInitializationClass(Node taget, Type[] components, bool lastBoot) {
        Dictionary<Priority, PriorityList> pairs = new() {
            { Priority.StartBefore, new PriorityList() },
            { Priority.StartLater, new PriorityList() }
        };
        foreach (var item in components) {
            RunTimeInitializationClassAttribute attri = item.GetAttribute<RunTimeInitializationClassAttribute>();
            if (attri is not null && attri.LastBoot == lastBoot) {
                Node node = item.Activator<Node>();
                if (!string.IsNullOrEmpty(attri.ClassName))
                    node.Name = string.IsNullOrEmpty(attri.ClassName) ? item.Name : attri.ClassName;
                pairs[attri.BootPriority] = pairs[attri.BootPriority].Add(attri.SubPriority, node);
            }
        }

        using (PriorityList list = pairs[Priority.StartBefore]) {
            list.ReorderList();
            list.Run(taget);
        }
        using (PriorityList list = pairs[Priority.StartLater]) {
            list.ReorderList();
            list.Run(taget);
        }
    }
}