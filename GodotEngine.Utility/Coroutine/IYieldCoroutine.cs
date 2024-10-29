using System;

namespace Cobilas.GodotEngine.Utility; 

public interface IYieldCoroutine {
    TimeSpan Delay { get; }
    bool IsLastCoroutine { get; }
}