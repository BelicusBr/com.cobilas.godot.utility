using Godot;
using System.Runtime.InteropServices;

namespace Cobilas.GodotEngine.Utility.Runtime;

/// <summary>Provides RunTime values ​​and functions.</summary>
[StructLayout(LayoutKind.Sequential, Size = 1)]
public readonly struct RunTime {
    /// <summary>The interval in seconds from the last frame to the current one (Read Only).</summary>
    public const float DeltaTime = .33333333f;
    /// <summary>The interval in seconds of in-game time at which physics and other fixed frame rate updates  are performed.</summary>
    public const float FixedDeltaTime = .02f;
    /// <summary>
    /// Controls how fast or slow the in-game clock ticks versus the real life one. It
    /// defaults to 1.0. A value of 2.0 means the game moves twice as fast as real life,
    /// whilst a value of 0.5 means the game moves at half the regular speed. This also
    /// affects Godot.Timer and Godot.SceneTreeTimer (see Godot.SceneTree.CreateTimer(System.Single,System.Boolean)
    /// for how to control this).
    /// </summary>
    public static float TimeScale { get => Engine.TimeScale; set => Engine.TimeScale = value; }
    /// <summary>The total number of frames since the start of the game (Read Only).</summary>
    /// <returns>The total number of frames</returns>
    public static int FrameCount => Engine.GetFramesDrawn();
}