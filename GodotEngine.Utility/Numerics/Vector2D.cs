using Godot;

namespace Cobilas.GodotEngine.Utility.Numerics;
public struct Vector2D {
    public float x;
    public float y;

    public readonly float magnitude => Magnitude(this);
    public readonly float sqrtMagnitude => SqrtMagnitude(this);

    private static readonly Vector2D _zero = new(0f, 0f);
    private static readonly Vector2D _one = new(1f, 1f);
    private static readonly Vector2D _up = new(0f, -1f);
    private static readonly Vector2D _down = new(0f, 1f);
    private static readonly Vector2D _right = new(1f, 0f);
    private static readonly Vector2D _left = new(-1f, 0f);
    
    public static Vector2D Zero => _zero;
    public static Vector2D One => _one;
    public static Vector2D Up => _up;
    public static Vector2D Down => _down;
    public static Vector2D Right => _right;
    public static Vector2D Left => _left;

    public Vector2D(float x, float y) : this() {
        this.x = x;
        this.y = y;
    }

    public Vector2D(Vector2D vector) : this(vector.x, vector.y) {}

    public Vector2D(Vector2 vector) : this(vector.x, vector.y) {}

    public static float Distance(Vector2D a, Vector2D b) => SqrtMagnitude(a - b);

    public static float Dot(Vector2D lhs, Vector2D rhs)
        => (float) (lhs.x * (double)rhs.x + lhs.y * (double)rhs.y);

    public static float Magnitude(Vector2D a) => (float)(a.x * (double)a.x + a.y * (double)a.y);

    public static float SqrtMagnitude(Vector2D a) => Mathf.Sqrt(Magnitude(a));

    public static Vector2D operator +(Vector2D a, Vector2D b) {
        Vector2D result = Vector2D._zero;
        result.x = a.x + b.x;
        result.y = a.y + b.y;
        return result;
    }

    public static Vector2D operator -(Vector2D a, Vector2D b) {
        Vector2D result = Vector2D._zero;
        result.x = a.x - b.x;
        result.y = a.y - b.y;
        return result;
    }

    public static Vector2D operator /(Vector2D a, Vector2D b) {
        Vector2D result = Vector2D._zero;
        result.x = a.x / b.x;
        result.y = a.y / b.y;
        return result;
    }

    public static Vector2D operator *(Vector2D a, Vector2D b) {
        Vector2D result = Vector2D._zero;
        result.x = a.x * b.x;
        result.y = a.y * b.y;
        return result;
    }

    public static Vector2D operator /(Vector2D a, float b) {
        Vector2D result = Vector2D._zero;
        result.x = a.x / b;
        result.y = a.y / b;
        return result;
    }

    public static Vector2D operator *(Vector2D a, float b) {
        Vector2D result = Vector2D._zero;
        result.x = a.x * b;
        result.y = a.y * b;
        return result;
    }

    public static implicit operator Vector2D(Vector2 v) => new (v.x, v.y);
    public static implicit operator Vector2(Vector2D v) => new (v.x, v.y);

    public static implicit operator Vector2D(Vector3 v) => new (v.x, v.y);
    public static implicit operator Vector3(Vector2D v) => new (v.x, v.y, 0f);

    public static implicit operator Vector3D(Vector2D v) => new (v.x, v.y);
    public static implicit operator Vector4D(Vector2D v) => new (v.x, v.y);
}