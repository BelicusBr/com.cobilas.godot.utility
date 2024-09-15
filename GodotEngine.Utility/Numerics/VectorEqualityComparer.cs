using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Cobilas.GodotEngine.Utility.Numerics;
[StructLayout(LayoutKind.Sequential)]
public struct VectorEqualityComparer : IEqualityComparer,
    IEqualityComparer<Vector2D>, IEqualityComparer<Vector3D>,
    IEqualityComparer<Vector4D> {
    public new readonly bool Equals(object x, object y) {
        if (x is Vector2D xv2 && y is Vector2D yv2) return xv2.Equals(yv2);
        else if (x is Vector3D xv3 && y is Vector3D yv3) return xv3.Equals(yv3);
        else if (x is Vector4D xv4 && y is Vector4D yv4) return xv4.Equals(yv4);
        return false;
    }

    public readonly bool Equals(Vector2D x, Vector2D y)
        => x.x == y.x && x.y == y.y;

    public readonly bool Equals(Vector3D x, Vector3D y)
        => x.x == y.x && x.y == y.y && x.z == y.z;

    public readonly bool Equals(Vector4D x, Vector4D y)
        => x.x == y.x && x.y == y.y && x.z == y.z && x.w == y.w;

    public readonly int GetHashCode(object obj)
        => obj switch {
            Vector2D v2 => GetHashCode(v2),
            Vector3D v3 => GetHashCode(v3),
            Vector4D v4 => GetHashCode(v4),
            _ => 0
        };

    public readonly int GetHashCode(Vector2D obj) 
        => obj.x.GetHashCode() >> obj.y.GetHashCode();

    public readonly int GetHashCode(Vector3D obj) 
        => obj.x.GetHashCode() >> obj.y.GetHashCode() ^ obj.z.GetHashCode();

    public readonly int GetHashCode(Vector4D obj)
        => obj.x.GetHashCode() >> obj.y.GetHashCode() ^ obj.z.GetHashCode() << obj.w.GetHashCode();
}