namespace Cobilas.GodotEngine.Utility.Input; 

/// <summary>Represents mouse triggers.</summary>
public enum MouseButton : byte {
    /// <summary>Unidentified trigger.</summary>
    Unknown = 0,
    /// <inheritdoc cref="Godot.ButtonList.Right"/>
    MouseRight = 2,
    /// <inheritdoc cref="Godot.ButtonList.Left"/>
    MouseLeft = 1,
    /// <inheritdoc cref="Godot.ButtonList.Middle"/>
    MouseMiddle = 3,
    /// <inheritdoc cref="Godot.ButtonList.WheelUp"/>
    MouseWheelUp = 4,
    /// <inheritdoc cref="Godot.ButtonList.WheelDown"/>
    MouseWheelDown = 5,
    /// <inheritdoc cref="Godot.ButtonList.WheelLeft"/>
    MouseWheelLeft = 6,
    /// <inheritdoc cref="Godot.ButtonList.WheelRight"/>
    MouseWheelRight = 7,
    /// <inheritdoc cref="Godot.ButtonList.Xbutton1"/>
    MouseXB1 = 8,
    /// <inheritdoc cref="Godot.ButtonList.Xbutton2"/>
    MouseXB2 = 9,
    /// <summary>Extra mouse button 3 (only present on some mice).</summary>
    MouseXB3 = 10,
    /// <summary>Extra mouse button 4 (only present on some mice).</summary>
    MouseXB4 = 11,
    /// <summary>Extra mouse button 5 (only present on some mice).</summary>
    MouseXB5 = 12,
    /// <summary>Extra mouse button 6 (only present on some mice).</summary>
    MouseXB6 = 13
}