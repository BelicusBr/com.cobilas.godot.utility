using System;

namespace Cobilas.GodotEngine.Utility;

public readonly struct LastRunTimeSecond : IYieldUpdate {
    private readonly TimeSpan delay;
    TimeSpan IYieldCoroutine.Delay => delay;
    bool IYieldCoroutine.IsLastCoroutine => true;

    public LastRunTimeSecond(double second)
    {
        delay = TimeSpan.FromSeconds(second);
    }
}
