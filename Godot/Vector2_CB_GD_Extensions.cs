namespace Godot {
    /// <summary>Extension to the <see cref="Godot.Vector2"/> struct.</summary>
    public static class Vector2_CB_GD_Extensions {
        /// <summary>Inverts the axes of a vector.</summary>
        /// <param name="v">The vector that will be inverted.</param>
        /// <param name="negX">Allows you to invert the X axis.</param>
        /// <param name="negY">Allows you to invert the Y axis.</param>
        /// <returns>Returns the already transformed vector.</returns>
        public static Vector2 Neg(this Vector2 v, bool negX = true, bool negY = true) {
            v.x = negX ? -v.x : v.x;
            v.y = negY ? -v.y : v.y;
            return v;
        }
    }
}