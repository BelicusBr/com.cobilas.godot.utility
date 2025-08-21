using Cobilas.GodotEngine.Utility.Numerics;

namespace Godot;

/// <summary>Extension to the <see cref="Godot.Rect2"/> struct.</summary>
public static class Rect2_GD_CB_Extension {
    /// <summary>Gets the top position of <see cref="Godot.Rect2"/>.</summary>
    /// <param name="R"><see cref="Godot.Rect2"/> that will be used.</param>
    /// <returns>Returns a floating-point with the top position of <see cref="Godot.Rect2"/>.</returns>
    public static float Top(this Rect2 R) => R.Position.y;
    /// <summary>Gets the bottom position of <see cref="Godot.Rect2"/>.</summary>
    /// <param name="R"><see cref="Godot.Rect2"/> that will be used.</param>
    /// <returns>Returns a floating-point value with the bottom position of <see cref="Godot.Rect2"/>.</returns>
    public static float Bottom(this Rect2 R) => R.Position.y + R.Size.y;
    /// <summary>Gets the left position of <see cref="Godot.Rect2"/>.</summary>
    /// <param name="R"><see cref="Godot.Rect2"/> that will be used.</param>
    /// <returns>Returns a floating-point left position of <see cref="Godot.Rect2"/>.</returns>
    public static float Left(this Rect2 R) => R.Position.x;
    /// <summary>Gets the right position of <see cref="Godot.Rect2"/>.</summary>
    /// <param name="R"><see cref="Godot.Rect2"/> that will be used.</param>
    /// <returns>Returns a floating-point right position of <see cref="Godot.Rect2"/>.</returns>
    public static float Right(this Rect2 R) => R.Position.x + R.Size.x;
    
    public static bool Contains(this Rect2 rect, in Vector2D mousePosition) {
        if (mousePosition.x < rect.Right() && mousePosition.x > rect.Left() &&
            mousePosition.y > rect.Top() && mousePosition.y < rect.Bottom())
            return true;
        return false;
    }
}