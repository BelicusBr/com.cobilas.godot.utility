using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Cobilas.GodotEngine.Utility.Numerics;
/// <summary>Defines methods to support comparing vectors for equality.</summary>
public readonly struct VectorEqualityComparer : IEqualityComparer,
    IEqualityComparer<Vector2D>, IEqualityComparer<Vector3D>,
    IEqualityComparer<Vector4D>, IEqualityComparer<Vector2DInt>,
    IEqualityComparer<Vector3DInt> {
    /// <inheritdoc cref="Equals(Vector3DInt, Vector3DInt)"/>
    public new readonly bool Equals(object x, object y) {
        if (x is Vector2D xv2 && y is Vector2D yv2) return xv2.Equals(yv2);
        else if (x is Vector3D xv3 && y is Vector3D yv3) return xv3.Equals(yv3);
        else if (x is Vector4D xv4 && y is Vector4D yv4) return xv4.Equals(yv4);
        else if (x is Vector2DInt xv2I && y is Vector2DInt yv2I) return xv2I.Equals(yv2I);
        else if (x is Vector3DInt xv3I && y is Vector3DInt yv3I) return xv3I.Equals(yv3I);
        return false;
    }
    /// <inheritdoc cref="Equals(Vector3DInt, Vector3DInt)"/>
    public readonly bool Equals(Vector2D x, Vector2D y) => x.x == y.x && x.y == y.y;
    /// <inheritdoc cref="Equals(Vector3DInt, Vector3DInt)"/>
    public readonly bool Equals(Vector3D x, Vector3D y) => x.x == y.x && x.y == y.y && x.z == y.z;
    /// <inheritdoc cref="Equals(Vector3DInt, Vector3DInt)"/>
    public readonly bool Equals(Vector4D x, Vector4D y) => x.x == y.x && x.y == y.y && x.z == y.z && x.w == y.w;
    /// <inheritdoc cref="Equals(Vector3DInt, Vector3DInt)"/>
    public readonly bool Equals(Vector2DInt x, Vector2DInt y) => x.x == y.x && x.y == y.y;
    /// <summary>Determines whether the specified objects are equal.</summary>
    /// <param name="x">The first object of the parameter type to be compared.</param>
    /// <param name="y">The second object of the parameter type to be compared.</param>
    /// <remarks>Implement this method to provide a custom equality comparison for the specified type.</remarks>
    /// <returns><c>true</c> if the specified objects are equal; otherwise, <c>false</c>.</returns>
    public readonly bool Equals(Vector3DInt x, Vector3DInt y) => x.x == y.x && x.y == y.y && x.z == y.z;
    /// <inheritdoc cref="GetHashCode(Vector3DInt)"/>
    public readonly int GetHashCode(object obj)
        => obj switch {
            Vector2D v2 => GetHashCode(v2),
            Vector3D v3 => GetHashCode(v3),
            Vector4D v4 => GetHashCode(v4),
            Vector2DInt v2I => GetHashCode(v2I),
            Vector3DInt v3I => GetHashCode(v3I),
            _ => 0
        };
    /// <inheritdoc cref="GetHashCode(Vector3DInt)"/>
    public readonly int GetHashCode(Vector2D obj) => obj.GetHashCode();
    /// <inheritdoc cref="GetHashCode(Vector3DInt)"/>
    public readonly int GetHashCode(Vector3D obj) => obj.GetHashCode();
    /// <inheritdoc cref="GetHashCode(Vector3DInt)"/>
    public readonly int GetHashCode(Vector4D obj) => obj.GetHashCode();
    /// <inheritdoc cref="GetHashCode(Vector3DInt)"/>
    public readonly int GetHashCode(Vector2DInt obj) => obj.GetHashCode();
    /// <summary>Returns a hash code for the specified object.</summary>
    /// <param name="obj">The <seealso cref="System.Object"/> for which a hash code is to be returned.</param>
    /// <remarks>Implement this method to provide a customized hash code for type, corresponding to the customized equality comparison provided by the Equals method.</remarks>
    /// <returns>A hash code for the specified object.</returns>
    public readonly int GetHashCode(Vector3DInt obj) => obj.GetHashCode();
}