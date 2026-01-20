using Godot;
using System;
using Cobilas.Collections;
using Cobilas.GodotEngine.Utility.Runtime;

namespace Cobilas.GodotEngine.Utility;
/// <summary>Manages coroutine execution with priority 8 in the autoload order.</summary>
/// <remarks>
/// This class handles the execution of coroutines during both regular process and physics process frames.
/// It ensures that coroutines are properly started, stopped, and managed throughout the application lifecycle.
/// </remarks>
/// <seealso cref="AutoLoadScriptAttribute"/>
/// <seealso cref="CoroutineItem"/>
/// <seealso cref="Coroutine"/>
[AutoLoadScript(8)]
public class LastCoroutineManager : Node {
    private CoroutineItem[]? waits = System.Array.Empty<CoroutineItem>();

    private static event Action? delayAction = null;
    private static LastCoroutineManager? lastCoroutine = null;
    /// <inheritdoc/>
    public override void _Ready() {
        lastCoroutine ??= this;
        if (delayAction is not null) {
            delayAction();
            delayAction = null;
        }
    }
	/// <inheritdoc/>
	public override void _Process(float delta) {
        for (int I = 0; I < ArrayManipulation.ArrayLength(waits); I++) {
            CoroutineItem? coroutine = waits![I];
            if (!coroutine.IsPhysicsProcess)
                if (!coroutine.Run()) {
                    ArrayManipulation.Remove(I, ref waits);
                    --I;
                }
        }
    }
	/// <inheritdoc/>
	public override void _PhysicsProcess(float delta) {
        for (int I = 0; I < ArrayManipulation.ArrayLength(waits); I++) {
            CoroutineItem? coroutine = waits![I];
            if (coroutine.IsPhysicsProcess)
                if (!coroutine.Run()) {
                    ArrayManipulation.Remove(I, ref waits);
                    --I;
                }
        }
    }
    /// <summary>
    /// Allows you to call internal class commands.
    /// <para>start_c: StartCoroutine(Coroutine?)</para>
    /// <para>stop_c: StopCoroutine(Coroutine?)</para>
    /// <para>stop_all_c: StopAllCoroutines()</para>
    /// </summary>
    /// <param name="method">The command to use.</param>
    /// <param name="args">Argument to be passed to the command.</param>
    /// <exception cref="ArgumentException">Occurs when a command is not listed.</exception>
    internal static void CallMethod(string method, params object[] args) {
        switch (method) {
            case "start_c":
                if (lastCoroutine is null) delayAction += () => StartCoroutine((Coroutine)args[0]);
                else StartCoroutine((Coroutine)args[0]);
                break;
            case "stop_c":
                if (lastCoroutine is null) delayAction += () => StopCoroutine((Coroutine)args[0]);
                else StopCoroutine((Coroutine)args[0]);
                break;
            case "stop_all_c":
                if (lastCoroutine is null) delayAction += () => StopAllCoroutines();
                else StopAllCoroutines();
                break;
            default: throw new ArgumentException($"The command {method} is not listed.");
        }
    }
    
    private static void StartCoroutine(Coroutine? enumerator) {
        if (enumerator is null) throw new ArgumentNullException(nameof(Coroutine));
        else if (lastCoroutine is null) throw new ArgumentNullException("LastCoroutineManager is set to null.", (Exception?)null);
        ArrayManipulation.Add(new CoroutineItem(enumerator), ref lastCoroutine!.waits);
    }

    private static void StopCoroutine(Coroutine? Coroutine) {
        if (Coroutine is null) throw new ArgumentNullException(nameof(Coroutine));
        else if (lastCoroutine is null) throw new ArgumentNullException("LastCoroutineManager is set to null.", (Exception?)null);
        CoroutineItem[]? waits = lastCoroutine.waits;

        if (waits is not null)
            foreach (var item in waits)
                if (item.ID == Coroutine.ID) {
                    item.Cancel();
                    break;
                }
    }
    
    private static void StopAllCoroutines() {
        if (lastCoroutine is null) throw new ArgumentNullException("LastCoroutineManager is set to null.", (Exception?)null);
        CoroutineItem[]? waits = lastCoroutine.waits;

        if (waits is not null)
            foreach (var item in waits)
                item.Cancel();
    }
}
