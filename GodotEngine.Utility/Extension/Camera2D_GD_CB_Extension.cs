using Cobilas.GodotEngine.Utility;

namespace Godot;

public static class Camera2D_GD_CB_Extension {
    public static Vector2 ScreenToWorldPoint(this Camera2D C, Vector2 mousePosition) {
        Rect2 rect;
        Vector2 size = Screen.CurrentResolution * C.Zoom;
        mousePosition *= C.Zoom;
        if (C.AnchorMode == Camera2D.AnchorModeEnum.DragCenter)
            rect = new Rect2(C.Position - size * .5f, size);
        else rect = new Rect2(C.Position, size);
        return mousePosition + Vector2.Right * rect.Left() + Vector2.Down * rect.Top();
    }

    public static Vector2 WorldToScreenPoint(this Camera2D C, Vector2 position) {
        Vector2 rect = C.Position;
        if (C.AnchorMode == Camera2D.AnchorModeEnum.DragCenter)
            rect = C.Position - (Screen.CurrentResolution * C.Zoom) * .5f;
        return position - rect;
    }
}