using System;

namespace Cobilas.GodotEngine.Utility; 
/// <summary>A base interface for all Yield class.</summary>
public interface IYieldCoroutine {
    /// <summary>The delay of the Yield class.</summary>
    /// <returns>Returns a delay value in the form of <see cref="TimeSpan"/>.</returns>
    TimeSpan Delay { get; }
    /// <summary>Indicates that it is the last to be executed.</summary>
    /// <returns>Returns <c>true</c> when class Yield is marked to run last.</returns>
    bool IsLastCoroutine { get; }
}