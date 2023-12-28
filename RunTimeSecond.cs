using System;

namespace Cobilas.GodotEngine.Utility {
    public struct RunTimeSecond : IYieldUpdate {

        TimeSpan delay;
        TimeSpan IYieldCoroutine.Delay { get => delay; }

        public RunTimeSecond(double second) {
            delay = TimeSpan.FromSeconds(second);
        }
    }
}