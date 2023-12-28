using System;

namespace Cobilas.GodotEngine.Utility {
    public struct FixedRunTimeSecond : IYieldFixedUpdate {

        TimeSpan delay;
        TimeSpan IYieldCoroutine.Delay { get => delay; }

        public FixedRunTimeSecond(double second) {
            delay = TimeSpan.FromSeconds(second);
        }
    }
}