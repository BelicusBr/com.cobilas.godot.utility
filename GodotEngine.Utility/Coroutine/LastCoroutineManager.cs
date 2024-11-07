using Godot;
using System;
using Cobilas.Collections;
using Cobilas.GodotEngine.Utility.Runtime;

namespace Cobilas.GodotEngine.Utility;
[RunTimeInitializationClass(nameof(LastCoroutineManager), lastBoot:true)]
internal class LastCoroutineManager : Node {
    private CoroutineItem[] waits = System.Array.Empty<CoroutineItem>();

    private static LastCoroutineManager? lastCoroutine = null;

    public override void _Ready() {
        lastCoroutine ??= this;
    }

    public override void _Process(float delta) {
        for (int I = 0; I < ArrayManipulation.ArrayLength(waits); I++)
            if (!waits[I].IsPhysicsProcess)
                if (!waits[I].Run()) {
                    ArrayManipulation.Remove(I, ref waits);
                    --I;
                }
    }

    public override void _PhysicsProcess(float delta) {
        for (int I = 0; I < ArrayManipulation.ArrayLength(waits); I++)
            if (waits[I].IsPhysicsProcess)
                if (!waits[I].Run()) {
                    ArrayManipulation.Remove(I, ref waits);
                    --I;
                }
    }
    /// <summary>Starts a collating process from an <seealso cref="System.Collections.IEnumerator"/>.</summary>
    public static void StartCoroutine(Coroutine? enumerator) {
        if (enumerator is null) throw new ArgumentNullException(nameof(Coroutine));
        ArrayManipulation.Add(new CoroutineItem(enumerator), ref lastCoroutine!.waits);
    }
    /// <summary>Ends all open Coroutines.</summary>
    internal static void StopCoroutine(Coroutine? Coroutine) {
        if (Coroutine is null) throw new ArgumentNullException(nameof(Coroutine));

        foreach (var item in lastCoroutine!.waits)
            if (item.ID == Coroutine.ID) {
                item.Cancel();
                break;
            }
    }
    /// <summary>Ends all open Coroutines.</summary>
    internal static void StopAllCoroutines() {
        foreach (var item in lastCoroutine!.waits)
            item.Cancel();
    }
}
