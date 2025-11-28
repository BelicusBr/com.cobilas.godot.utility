using System;
using Cobilas.GodotEngine.Utility;

namespace Godot;
/// <summary>
/// Provides extension methods for the <seealso cref="Control"/> class of <seealso cref="Godot"/>, allowing operations
/// with 2D rectangles (<seealso cref="Rect2D"/>) more conveniently.
/// </summary>
/// <remarks>
/// This class facilitates conversion between the rectangle properties of <seealso cref="Control"/>
/// and the custom <seealso cref="Rect2D"/> type, for both local and global coordinates.
/// </remarks>
public static class Control_GD_CB_Extension {
    /// <summary>
    /// Gets a <seealso cref="Rect2D"/> representing the local rectangle of the <seealso cref="Control"/>.
    /// </summary>
    /// <param name="ctl"><seealso cref="Control"/> target (can be null)</param>
    /// <returns>A <seealso cref="Rect2D"/> containing the position, size, rotation, scale, and pivot of the <seealso cref="Control"/></returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Control"/> is null</exception>
    public static Rect2D GetRect2D(this Control? ctl)
        => ctl switch {
            null => throw new ArgumentNullException(nameof(ctl)),
            _ => DoNotGetRect2Dposition(ctl).SetPosition(ctl.RectPosition + ctl.RectPivotOffset),
        };
    /// <summary>
    /// Sets the properties of the <seealso cref="Control"/> based on a <seealso cref="Rect2D"/>.
    /// </summary>
    /// <param name="ctl"><seealso cref="Control"/> target (can be null)</param>
    /// <param name="rect"><seealso cref="Rect2D"/> contendo as propriedades a serem aplicadas</param>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Control"/> is null</exception>
    public static void SetRect2D(this Control? ctl, Rect2D rect) {
        if (ctl is null) throw new ArgumentNullException(nameof(ctl));
        ctl.RectPosition = rect.Position - ctl.RectPivotOffset;
        SetNoRect2DPosition(ctl, rect);
    }
    /// <summary>
    /// Gets a <seealso cref="Rect2D"/> representing the global rectangle of the <seealso cref="Control"/>.
    /// </summary>
    /// <param name="ctl"><seealso cref="Control"/> target (can be null)</param>
    /// <returns>A <seealso cref="Rect2D"/> containing the global position, size, rotation, scale, and pivot of the <seealso cref="Control"/></returns>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Control"/> is null</exception>
    public static Rect2D GetGlobalRect2D(this Control? ctl)
        => ctl switch {
            null => throw new ArgumentNullException(nameof(ctl)),
            _ => DoNotGetRect2Dposition(ctl).SetPosition(ctl.RectGlobalPosition + ctl.RectPivotOffset),
        };
    /// <summary>
    /// Sets the properties of the <seealso cref="Control"/> based on a <seealso cref="Rect2D"/>, using global coordinates.
    /// </summary>
    /// <param name="ctl"><seealso cref="Control"/> target (can be null)</param>
    /// <param name="rect"><seealso cref="Rect2D"/> containing the global properties to be applied</param>
    /// <exception cref="ArgumentNullException">Thrown when <seealso cref="Control"/> is null</exception>
    public static void SetGlobalRect2D(this Control? ctl, Rect2D rect) {
        if (ctl is null) throw new ArgumentNullException(nameof(ctl));
        ctl.RectGlobalPosition = rect.Position - ctl.RectPivotOffset;
        SetNoRect2DPosition(ctl, rect);
    }

    private static void SetNoRect2DPosition(this Control ctl, Rect2D rect) {
        ctl.RectSize = rect.Size;
        ctl.RectMinSize = rect.MinSize;
        ctl.RectRotation = rect.Rotation;
        ctl.RectScale = rect.Scale;
        ctl.RectPivotOffset = rect.Pivot;
    }

    private static Rect2D DoNotGetRect2Dposition(this Control ctl)
        => Rect2D.Empty.SetSize(ctl.RectSize)
            .SetMinSize(ctl.RectMinSize)
            .SetRotation(ctl.RectRotation)
            .SetScale(ctl.RectScale)
            .SetPivot(ctl.RectPivotOffset);
}