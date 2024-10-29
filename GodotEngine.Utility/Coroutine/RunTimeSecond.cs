using System;

namespace Cobilas.GodotEngine.Utility; 

public readonly struct RunTimeSecond : IYieldUpdate {
    private readonly TimeSpan delay;
    TimeSpan IYieldCoroutine.Delay => delay;
    bool IYieldCoroutine.IsLastCoroutine => false;

    public RunTimeSecond(double second)
    {
        delay = TimeSpan.FromSeconds(second);
    }
}