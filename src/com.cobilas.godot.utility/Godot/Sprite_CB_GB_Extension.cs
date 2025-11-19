using System;
using Cobilas.GodotEngine.Utility;
using Cobilas.GodotEngine.Utility.Numerics;

namespace Godot;
/// <summary>
/// Provides extension methods for <seealso cref="Godot"/>'s Sprite class, allowing more convenient operations
/// with 2D rectangles (Rect2D).
/// </summary>
/// <remarks>
/// This class facilitates conversion between <seealso cref="Sprite"/> properties
/// and the custom <seealso cref="Rect2D"/> type, for both local and global coordinates.
/// </remarks>
public static class Sprite_CB_GB_Extension {
    /// <summary>
    /// Gets a <seealso cref="Rect2D"/> representing the local rectangle of the <seealso cref="Control"/>.
    /// </summary>
    /// <param name="ctl"><seealso cref="Control"/> target (can be null)</param>
    /// <returns>A <seealso cref="Rect2D"/> containing the position, size, rotation, scale, and pivot of the <seealso cref="Control"/></returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Control"/> is null</exception>
    public static Rect2D GetRect2D(this Sprite? ctl)
        => ctl switch {
            null => throw new ArgumentNullException(nameof(ctl)),
            _ => DoNotGetRect2Dposition(ctl).SetPosition(ctl.Position),
        };
    /// <summary>
    /// Sets the properties of the <seealso cref="Control"/> based on a <seealso cref="Rect2D"/>.
    /// </summary>
    /// <param name="ctl"><seealso cref="Control"/> target (can be null)</param>
    /// <param name="rect"><seealso cref="Rect2D"/> contendo as propriedades a serem aplicadas</param>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Control"/> is null</exception>
    public static void SetRect2D(this Sprite? ctl, Rect2D rect) {
        if (ctl is null) throw new ArgumentNullException(nameof(ctl));
        ctl.Position = rect.Position;
        SetNoRect2DPosition(ctl, rect);
    }
    /// <summary>
    /// Gets a <seealso cref="Rect2D"/> representing the global rectangle of the <seealso cref="Control"/>.
    /// </summary>
    /// <param name="ctl"><seealso cref="Control"/> target (can be null)</param>
    /// <returns>A <seealso cref="Rect2D"/> containing the global position, size, rotation, scale, and pivot of the <seealso cref="Control"/></returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Control"/> is null</exception>
    public static Rect2D GetGlobalRect2D(this Sprite? ctl)
        => ctl switch {
            null => throw new ArgumentNullException(nameof(ctl)),
            _ => DoNotGetRect2Dposition(ctl).SetPosition(ctl.GlobalPosition),
        };
    /// <summary>
    /// Sets the properties of the <seealso cref="Control"/> based on a <seealso cref="Rect2D"/>, using global coordinates.
    /// </summary>
    /// <param name="ctl"><seealso cref="Control"/> target (can be null)</param>
    /// <param name="rect"><seealso cref="Rect2D"/> containing the global properties to be applied</param>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Control"/> is null</exception>
    public static void SetGlobalRect2D(this Sprite? ctl, Rect2D rect) {
        if (ctl is null) throw new ArgumentNullException(nameof(ctl));
        ctl.GlobalPosition = rect.Position;
        SetNoRect2DPosition(ctl, rect);
    }

    private static void SetNoRect2DPosition(this Sprite ctl, Rect2D rect)
    {
        ctl.RotationDegrees = rect.Rotation;
        ctl.Scale = rect.Scale;
        ctl.Offset = rect.Pivot;
    }

    private static Rect2D DoNotGetRect2Dposition(this Sprite ctl)
        => Rect2D.Empty.SetSize(ctl.Texture is null ? Vector2.Zero : ctl.Texture.GetSize())
            .SetMinSize(Vector2D.Zero)
            .SetRotation(ctl.RotationDegrees)
            .SetScale(ctl.Scale)
            .SetPivot(ctl.Offset);
}
