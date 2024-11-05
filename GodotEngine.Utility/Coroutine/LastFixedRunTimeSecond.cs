using System;

namespace Cobilas.GodotEngine.Utility;
/// <summary>
/// This class represents a delay in seconds to methods that return <see cref="System.Collections.IEnumerator"/> and use the keyword Yield.
/// <para>This class is performed in the <see cref="Godot.Node._PhysicsProcess(float)"/>.</para>
/// <para>This class allows the corrotine to be called after the methods of updating the current scene.</para>
/// </summary>
public readonly struct LastFixedRunTimeSecond : IYieldFixedUpdate {
    private readonly TimeSpan delay;
    TimeSpan IYieldCoroutine.Delay => delay;
    bool IYieldCoroutine.IsLastCoroutine => true;
    /// <summary>Creates a new instance of this object.</summary>
    public LastFixedRunTimeSecond(double second) {
        delay = TimeSpan.FromSeconds(second);
    }
}