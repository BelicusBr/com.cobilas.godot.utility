namespace Godot; 

public static class Rect2_GD_CB_Extension {
    public static float Top(this Rect2 R) => R.Position.y;
    public static float Bottom(this Rect2 R) => R.Position.y + R.Size.y;
    public static float Left(this Rect2 R) => R.Position.x;
    public static float Right(this Rect2 R) => R.Position.x + R.Size.x;
    public static Vector2 Center(this Rect2 R) => R.Position + R.Size * .5f;
}