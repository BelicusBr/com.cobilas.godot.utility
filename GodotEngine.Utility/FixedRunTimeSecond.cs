using System;

namespace Cobilas.GodotEngine.Utility; 

public readonly struct FixedRunTimeSecond : IYieldFixedUpdate {
    private readonly TimeSpan delay;
    TimeSpan IYieldCoroutine.Delay { get => delay; }

    public FixedRunTimeSecond(double second) {
        delay = TimeSpan.FromSeconds(second);
    }
}