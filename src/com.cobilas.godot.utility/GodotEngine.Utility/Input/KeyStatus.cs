namespace Cobilas.GodotEngine.Utility.Input; 

/// <summary>represents the state of a key.</summary>
[System.Flags]
public enum KeyStatus : byte {
    /// <summary>No status detected.</summary>
    None = 0,
    /// <summary>Occurs when the key has been released.</summary>
    Up = 2,
    /// <summary>Occurs when the key is being pressed.</summary>
    Press = 4,
    /// <summary>Occurs when the key has been pressed.</summary>
    Down = 16
}