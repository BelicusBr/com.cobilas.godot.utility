using Godot;
using System;
using System.Text;
using System.Collections;
using Cobilas.Collections;
using Cobilas.GodotEngine.Utility.Runtime;

namespace Cobilas.GodotEngine.Utility; 
/// <summary>This class is responsible for managing all coroutines.</summary>
[RunTimeInitializationClass(nameof(CoroutineManager))]
public class CoroutineManager : Node {
    private CoroutineItem[] waits = System.Array.Empty<CoroutineItem>();

    private static CoroutineManager? _Coroutine = null;
    private static readonly char[] chars = { 'a', 'b', 'c', 'd', 'e', 'f', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    /// <inheritdoc/>
    public override void _Ready() {
        _Coroutine ??= this;
    }
    /// <inheritdoc/>
    public override void _Process(float delta) {
        for (int I = 0; I < ArrayManipulation.ArrayLength(waits); I++)
            if (!waits[I].IsPhysicsProcess)
                if (!waits[I].Run()) {
                    ArrayManipulation.Remove(I, ref waits);
                    --I;
                }
    }
    /// <inheritdoc/>
    public override void _PhysicsProcess(float delta) {
        for (int I = 0; I < ArrayManipulation.ArrayLength(waits); I++)
            if (waits[I].IsPhysicsProcess)
                if (!waits[I].Run()) {
                    ArrayManipulation.Remove(I, ref waits);
                    --I;
                }
    }
    /// <summary>Starts a collating process from an <seealso cref="IEnumerator"/>.</summary>
    /// <param name="enumerator">The <seealso cref="IEnumerator"/> that will be used to start the <seealso cref="Coroutine"/>.</param>
    /// <exception cref="ArgumentNullException">When the object is null.</exception>
    /// <returns>Returns the <seealso cref="Coroutine"/> process that was started.</returns>
    public static Coroutine StartCoroutine(IEnumerator? enumerator) {
        if (enumerator is null) throw new ArgumentNullException(nameof(enumerator));
        Coroutine Coroutine = new(enumerator, GenID());

        if (enumerator.Current is IYieldCoroutine coroutine && coroutine.IsLastCoroutine)
            LastCoroutineManager.StartCoroutine(Coroutine);
        else ArrayManipulation.Add(new CoroutineItem(Coroutine), ref _Coroutine!.waits);

        return Coroutine;
    }
    /// <summary>Ends all open Coroutines.</summary>
    /// <param name="Coroutine">The <seealso cref="Coroutine"/> that will be closed.</param>
    public static void StopCoroutine(Coroutine? Coroutine) {
        if (Coroutine is null) throw new ArgumentNullException(nameof(Coroutine));

        foreach (var item in _Coroutine!.waits)
            if (item.ID == Coroutine.ID) {
                item.Cancel();
                break;
            }
    }
    /// <summary>Ends all open Coroutines.</summary>
    public static void StopAllCoroutines() {
        foreach (var item in _Coroutine!.waits)
            item.Cancel();
        LastCoroutineManager.StopAllCoroutines();
    }
    /// <summary>Generates an ID to be used in a <seealso cref="Coroutine"/>.</summary>
    /// <returns>Returns in <seealso cref="string"/> form the ID generated.</returns>
    public static string GenID() {
        StringBuilder builder = new();
        Random random = new();
        for (int I = 0; I < 64; I++)
            builder.Append(random.Next(0, 50) > 25 ? char.ToUpper(chars[random.Next(0, 15)]) : chars[random.Next(0, 15)]);
        return builder.ToString();
    }
}