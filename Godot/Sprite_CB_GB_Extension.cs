using System;
using Cobilas.GodotEngine.Utility;
using Cobilas.GodotEngine.Utility.Numerics;

namespace Godot;

public static class Sprite_CB_GB_Extension
{
    public static Rect2D GetCTLRect(this Sprite? ctl)
        => ctl switch {
            null => throw new ArgumentNullException(nameof(ctl)),
            _ => DoNotGetCTLRectposition(ctl).SetPosition(ctl.Position),
        };

    public static void SetCTLRect(this Sprite? ctl, Rect2D rect) {
        if (ctl is null) throw new ArgumentNullException(nameof(ctl));
        ctl.Position = rect.Position;
        SetNoCTLRectPosition(ctl, rect);
    }

    public static Rect2D GetGlobalCTLRect(this Sprite? ctl)
        => ctl switch {
            null => throw new ArgumentNullException(nameof(ctl)),
            _ => DoNotGetCTLRectposition(ctl).SetPosition(ctl.GlobalPosition),
        };

    public static void SetGlobalCTLRect(this Sprite? ctl, Rect2D rect) {
        if (ctl is null) throw new ArgumentNullException(nameof(ctl));
        ctl.GlobalPosition = rect.Position;
        SetNoCTLRectPosition(ctl, rect);
    }

    private static void SetNoCTLRectPosition(this Sprite ctl, Rect2D rect)
    {
        ctl.RotationDegrees = rect.Rotation;
        ctl.Scale = rect.Scale;
        ctl.Offset = rect.Pivot;
    }

    private static Rect2D DoNotGetCTLRectposition(this Sprite ctl)
        => Rect2D.Empty.SetSize(ctl.Texture is null ? Vector2.Zero : ctl.Texture.GetSize())
            .SetMinSize(Vector2D.Zero)
            .SetRotation(ctl.RotationDegrees)
            .SetScale(ctl.Scale)
            .SetPivot(ctl.Offset);
}
