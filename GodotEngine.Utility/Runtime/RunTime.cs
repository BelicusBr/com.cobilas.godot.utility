using System.Runtime.InteropServices;

namespace Cobilas.GodotEngine.Utility.Runtime;

/// <summary>Provides RunTime values ​​and functions.</summary>
[StructLayout(LayoutKind.Sequential, Size = 1)]
public readonly struct RunTime {
    /// <summary>The interval in seconds from the last frame to the current one (Read Only).</summary>
    public const float DeltaTime = .33333333f;
    /// <summary>The interval in seconds of in-game time at which physics and other fixed frame rate updates  are performed.</summary>
    public const float FixedDeltaTime = .02f;
}