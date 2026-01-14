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
	/// <summary>
	/// Gets the size of the sprite's texture, optionally scaled by the sprite's scale.
	/// </summary>
	/// <param name="ctl">The sprite to get the texture size from.</param>
	/// <returns>A <see cref="Vector2D"/> representing the texture size, scaled by the sprite's scale.</returns>
	/// <exception cref="ArgumentNullException">Thrown when <paramref name="ctl"/> is null.</exception>
	/// <remarks>
	/// This method returns the base texture size multiplied by the sprite's scale.
	/// If the sprite's texture is null, returns <see cref="Vector2D.Zero"/>.
	/// </remarks>
	public static Vector2D GetTextureSize(this Sprite? ctl) => GetTextureSize(ctl, true);
	/// <summary>
	/// Gets the size of the sprite's texture, with an option to apply the sprite's scale.
	/// </summary>
	/// <param name="ctl">The sprite to get the texture size from.</param>
	/// <param name="useScale">If true, the texture size is multiplied by the sprite's scale; otherwise, returns the base texture size.</param>
	/// <returns>A <see cref="Vector2D"/> representing the texture size, optionally scaled.</returns>
	/// <exception cref="ArgumentNullException">Thrown when <paramref name="ctl"/> is null.</exception>
	/// <remarks>
	/// When <paramref name="useScale"/> is true, the returned size is the base texture size multiplied by the sprite's scale.
	/// When false, the base texture size is returned without scaling.
	/// If the sprite's texture is null, returns <see cref="Vector2D.Zero"/>.
	/// </remarks>
	public static Vector2D GetTextureSize(this Sprite? ctl, bool useScale) {
		if (ctl is null) throw new ArgumentNullException(nameof(ctl));
		else if (ctl.Texture is null) return Vector2D.Zero;
		if (useScale)
			return ctl.Texture.GetSize() * ctl.Scale;
		return ctl.Texture.GetSize();
	}
	/// <summary>
	/// Sets the texture size of the <see cref="Sprite"/> by adjusting its scale.
	/// </summary>
	/// <param name="ctl"><see cref="Sprite"/> target (can be null)</param>
	/// <param name="newSize">The desired texture size as a <see cref="Vector2DInt"/></param>
	/// <exception cref="ArgumentNullException">Thrown when <see cref="Sprite"/> is null</exception>
	/// <remarks>
	/// This method calculates and applies the appropriate scale to achieve the desired texture size.
	/// If the sprite's texture is null, the method returns without making changes.
	/// </remarks>
	public static void SetTextureSize(this Sprite? ctl, Vector2DInt newSize) {
		if (ctl is null) throw new ArgumentNullException(nameof(ctl));
		else if (ctl.Texture is null) return;
		ctl.Scale = newSize / ctl.Texture.GetSize();
	}

	private static void SetNoRect2DPosition(this Sprite ctl, Rect2D rect) {
        ctl.RotationDegrees = rect.Rotation;
        ctl.Scale = rect.Scale;
        ctl.Offset = rect.Pivot;
    }

    private static Rect2D DoNotGetRect2Dposition(this Sprite ctl)
        => Rect2D.Empty.SetSize(ctl.Texture is null ? Vector2.Zero : ctl.Texture.GetSize())
            .SetMinSize(Vector2D.Zero)
            .SetRotation(ctl.RotationDegrees)
            .SetScale(ctl.Scale)
            .SetPivot((ctl.Centered ? GetTextureSize(ctl) * .5f : Vector2D.Zero) + ctl.Offset);
}
