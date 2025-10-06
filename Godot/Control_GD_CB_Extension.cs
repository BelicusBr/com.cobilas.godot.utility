using System;
using Cobilas.GodotEngine.Utility;

namespace Godot;

public static class Control_GD_CB_Extension {
    public static CTLRect GetCTLRect(this Control? ctl)
        => ctl switch {
            null => throw new ArgumentNullException(nameof(ctl)),
            _ => DoNotGetCTLRectposition(ctl).SetPosition(ctl.RectPosition),
        };

    public static void SetCTLRect(this Control? ctl, CTLRect rect) {
        if (ctl is null) throw new ArgumentNullException(nameof(ctl));
        ctl.RectPosition = rect.Position;
        SetNoCTLRectPosition(ctl, rect);
    }

    public static CTLRect GetGlobalCTLRect(this Control? ctl)
        => ctl switch {
            null => throw new ArgumentNullException(nameof(ctl)),
            _ => DoNotGetCTLRectposition(ctl).SetPosition(ctl.RectGlobalPosition),
        };

    public static void SetGlobalCTLRect(this Control? ctl, CTLRect rect) {
        if (ctl is null) throw new ArgumentNullException(nameof(ctl));
        ctl.RectGlobalPosition = rect.Position;
        SetNoCTLRectPosition(ctl, rect);
    }

    private static void SetNoCTLRectPosition(this Control ctl, CTLRect rect)
    {
        ctl.RectSize = rect.Size;
        ctl.RectMinSize = rect.MinSize;
        ctl.RectRotation = rect.Rotation;
        ctl.RectScale = rect.Scale;
        ctl.RectPivotOffset = rect.Pivot;
    }

    private static CTLRect DoNotGetCTLRectposition(this Control ctl)
        => CTLRect.Empty.SetSize(ctl.RectSize)
            .SetMinSize(ctl.RectMinSize)
            .SetRotation(ctl.RectRotation)
            .SetScale(ctl.RectScale)
            .SetPivot(ctl.RectPivotOffset);
}
