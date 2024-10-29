using System;

namespace Cobilas.GodotEngine.Utility;

public readonly struct LastFixedRunTimeSecond : IYieldFixedUpdate {
    private readonly TimeSpan delay;
    TimeSpan IYieldCoroutine.Delay => delay;
    bool IYieldCoroutine.IsLastCoroutine => true;

    public LastFixedRunTimeSecond(double second) {
        delay = TimeSpan.FromSeconds(second);
    }
}