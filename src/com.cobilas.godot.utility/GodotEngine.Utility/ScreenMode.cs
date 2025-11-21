namespace Cobilas.GodotEngine.Utility; 

/// <summary>Represents screen modes.</summary>
public enum ScreenMode : byte {
    /// <summary>This mode enables the screen in resizable windowed mode.</summary>
    Resizable = 0,
    /// <summary>This mode will maintain a borderless, non-resizable window.</summary>
    Borderless = 1,
    /// <summary>This mode will make the screen exclusively for the application.</summary>
    Fullscreen = 2
}