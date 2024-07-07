using System;

namespace Cobilas.GodotEngine.Utility; 

public readonly struct RunTimeSecond : IYieldUpdate {
    private readonly TimeSpan delay;
    TimeSpan IYieldCoroutine.Delay { get => delay; }

    public RunTimeSecond(double second)
    {
        delay = TimeSpan.FromSeconds(second);
    }
}