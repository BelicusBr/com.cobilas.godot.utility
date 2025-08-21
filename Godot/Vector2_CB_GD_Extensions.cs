namespace Godot {
    public static class Vector2_CB_GD_Extensions {
        public static Vector2 Neg(this Vector2 v, bool negX = true, bool negY = true) {
            v.x = negX ? -v.x : v.x;
            v.y = negY ? -v.y : v.y;
            return v;
        }
    }
}