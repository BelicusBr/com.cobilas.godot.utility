using System;

namespace Cobilas.GodotEngine.Utility; 
/// <summary>
/// This class represents a delay in seconds to methods that return <see cref="System.Collections.IEnumerator"/> and use the keyword Yield.
/// <para>This class is performed in the <see cref="Godot.Node._Process(float)"/>.</para>
/// </summary>
public readonly struct RunTimeSecond : IYieldUpdate {
    private readonly TimeSpan delay;
    TimeSpan IYieldCoroutine.Delay => delay;
    bool IYieldCoroutine.IsLastCoroutine => false;
    /// <summary>Creates a new instance of this object.</summary>
    public RunTimeSecond(double second) {
        delay = TimeSpan.FromSeconds(second);
    }
}