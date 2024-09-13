using Godot;
using System;
using System.Globalization;

namespace Cobilas.GodotEngine.Utility.Numerics;
[Serializable]
public struct Vector2D : IVector<Vector2D> {
    public float x;
    public float y;

    public readonly float magnitude => Magnitude(this);
    public readonly float sqrtMagnitude => SqrtMagnitude(this);
    public readonly Vector2D Normalized => Normalize(this);
    public readonly Vector2D floor => Floor(this);
    public readonly Vector2D ceil => Ceil(this);
    public readonly float aspect => Aspect(this);

    readonly IVector IVector.Normalized => Normalize(this);
    readonly IVector IVector.floor => Floor(this);
    readonly IVector IVector.ceil => Ceil(this);

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

    public float this[int index] {
        readonly get => index switch {
            0 => x,
            1 => y,
            _ => throw new ArgumentOutOfRangeException(nameof(index))
        };
        set {
            switch (index) {
                case 0: x = value; break;
                case 1: y = value; break;
                default: throw new ArgumentOutOfRangeException(nameof(index));
            }
        }
    }

    public Vector2D(float x, float y) : this() {
        this.x = x;
        this.y = y;
    }

    public Vector2D(Vector2D vector) : this(vector.x, vector.y) {}

    public Vector2D(Vector2 vector) : this(vector.x, vector.y) {}

#region Methods
    public readonly bool Equals(Vector2D other)
        => other.x == this.x && other.y == this.y;

    public readonly string ToString(string format, IFormatProvider formatProvider)
        => string.Format(formatProvider, format, this.x, this.y);

    public readonly string ToString(string format)
        => ToString(format, CultureInfo.InvariantCulture);

    public override readonly string ToString() => ToString("(x:{0:N3} x:{1:N3})");

    public override readonly bool Equals(object obj)
        => obj is Vector2D other && Equals(other);

    public override readonly int GetHashCode() => x.GetHashCode() ^ y.GetHashCode();

    public readonly Vector2D Abs(bool absX = true, bool absY = true)
        => (Vector2D)(this as IVector).Abs(absX, absY);

    readonly IVector IVector.Abs(bool absX, bool absY) {
        Vector2D abs = Abs(this);
        abs[0] = absX ? abs[0] : this[0];
        abs[1] = absY ? abs[1] : this[1];
        return abs;
    }
    #endregion

    #region Static methods
    public static float Distance(in Vector2D a, in Vector2D b) => SqrtMagnitude(a - b);

    public static float Dot(in Vector2D lhs, in Vector2D rhs)
        => (float) (lhs.x * (double)rhs.x + lhs.y * (double)rhs.y);

    public static float Magnitude(in Vector2D a) => (float)(a.x * (double)a.x + a.y * (double)a.y);

    public static float SqrtMagnitude(in Vector2D a) => Mathf.Sqrt(Magnitude(a));

    public static float Cross(in Vector2D lhs, in Vector2D rhs)
        => lhs.x * rhs.y - lhs.y * rhs.x;

    public static Vector2D Floor(in Vector2D a) {
        Vector2 result = a;
        result[0] = Mathf.Floor(result[0]);
        result[1] = Mathf.Floor(result[1]);
        return result;
    }

    public static Vector2D Ceil(in Vector2D a) {
        Vector2 result = a;
        result[0] = Mathf.Ceil(result[0]);
        result[1] = Mathf.Ceil(result[1]);
        return result;
    }

    public static float AngleTo(in Vector2D lhs, in Vector2D rhs)
        => Mathf.Atan2(Cross(lhs, rhs), Dot(lhs, rhs));

    public static float AngleToPoint(in Vector2D lhs, in Vector2D rhs)
        => Mathf.Atan2(lhs.y - rhs.y, lhs.x - rhs.x);

    public static float Aspect(in Vector2D a) => a.x / a.y;

    public static Vector2D Normalize(in Vector2D a) {
        float num1 = Magnitude(a);
        if (num1 == 0) return Vector2.Zero;
        float num2 = Mathf.Sqrt(num1);
        return new(a[0] / num2, a[1] / num2);
    }

    public static Vector2D Abs(in Vector2D a) 
        => new(Mathf.Abs(a[0]), Mathf.Abs(a[1]));

    public static void MaxAxisX(Vector2D a, out bool axisX, out float value) {
        if (axisX = a.x > a.y) value = a.x;
        else value = a.y;
    }

    public static void MinAxisX(Vector2D a, out bool axisX, out float value) {
        if (axisX = a.x < a.y) value = a.x;
        else value = a.y;
    }
    #endregion

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