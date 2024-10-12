using Godot;
using System;
using System.Globalization;

namespace Cobilas.GodotEngine.Utility.Numerics;
[Serializable]
public struct Vector2D : IVectorGeneric<Vector2D> {
    public float x;
    public float y;
    /// <inheritdoc/>
    public readonly float magnitude => Magnitude(this);
    /// <inheritdoc/>
    public readonly float sqrMagnitude => SqrMagnitude(this);
    /// <inheritdoc/>
    public readonly Vector2D Normalized => Normalize(this);
    /// <inheritdoc/>
    public readonly Vector2D floor => Floor(this);
    /// <inheritdoc/>
    public readonly Vector2D ceil => Ceil(this);
    /// <inheritdoc/>
    public readonly float aspect => Aspect(this);
    /// <inheritdoc/>
    public readonly int AxisCount => 2;

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
    /// <inheritdoc/>
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
    /// <summary>Starts a new instance of the object.</summary>
    public Vector2D(float x, float y) : this() {
        this.x = x;
        this.y = y;
    }
    /// <summary>Starts a new instance of the object.</summary>
    public Vector2D(Vector2D vector) : this(vector.x, vector.y) {}
    /// <summary>Starts a new instance of the object.</summary>
    public Vector2D(Vector2 vector) : this(vector.x, vector.y) {}

#region Methods
    /// <inheritdoc/>
    public readonly bool Equals(Vector2D other)
        => other.x == this.x && other.y == this.y;
    /// <inheritdoc/>
    public readonly string ToString(string format, IFormatProvider formatProvider)
        => string.Format(formatProvider, format, this.x, this.y);
    /// <inheritdoc/>
    public readonly string ToString(string format)
        => ToString(format, CultureInfo.InvariantCulture);
    /// <inheritdoc/>
    public override readonly string ToString() => ToString("(x:{0:N3} y:{1:N3})");
    /// <inheritdoc/>
    public override readonly bool Equals(object obj)
        => obj is Vector2D other && Equals(other);
    /// <inheritdoc/>
    public override readonly int GetHashCode() => x.GetHashCode() ^ y.GetHashCode();

    public readonly Vector2D Abs(bool absX = true, bool absY = true) {
        Vector2D abs = Abs(this);
        abs[0] = absX ? abs[0] : this[0];
        abs[1] = absY ? abs[1] : this[1];
        return abs;
    }

    public readonly Vector2D Neg(bool negX = true, bool negY = true) {
        Vector2D neg = Neg(this);
        neg[0] = negX ? neg[0] : this[0];
        neg[1] = negY ? neg[1] : this[1];
        return neg;
    }
    /// <inheritdoc/>
    public readonly Vector2D Round() => Round(this);
    readonly IVector IVector.Round() => Round(this);
    #endregion

    #region Static methods

    public static float Magnitude(in Vector2D a) => Mathf.Sqrt(SqrMagnitude(a));
    public static float Distance(in Vector2D a, in Vector2D b) => Magnitude(a - b);
    public static Vector2D Round(in Vector2D a) => new(Mathf.Round(a.x), Mathf.Round(a.y));
    public static float Cross(in Vector2D lhs, in Vector2D rhs) => lhs.x * rhs.y - lhs.y * rhs.x;
    public static float SqrMagnitude(in Vector2D a) => (float)(a.x * (double)a.x + a.y * (double)a.y);
    public static float Dot(in Vector2D lhs, in Vector2D rhs) => (float) (lhs.x * (double)rhs.x + lhs.y * (double)rhs.y);

    public static Vector2D Floor(in Vector2D a) {
        Vector2D result = a;
        result[0] = Mathf.Floor(result[0]);
        result[1] = Mathf.Floor(result[1]);
        return result;
    }

    public static Vector2D Ceil(in Vector2D a) {
        Vector2D result = a;
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
        float num = Vector2D.Magnitude(a);
        return (double)num > 9.99999974737875E-06 ? a / num : Vector2D.Zero;
    }

    public static Vector2D Neg(in Vector2D a) => new(-a[0], -a[1]);
    public static Vector2D Abs(in Vector2D a) => new(Mathf.Abs(a[0]), Mathf.Abs(a[1]));

    public static Vector2D Min(Vector2D lhs, Vector2D rhs) => new(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y));
    public static Vector2D Max(Vector2D lhs, Vector2D rhs) => new(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y));
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

    public static bool operator ==(in Vector2D lhs, in Vector2D rhs) => lhs.Equals(rhs);
    public static bool operator !=(in Vector2D lhs, in Vector2D rhs) => !lhs.Equals(rhs);

    public static implicit operator Vector2D(Vector2 v) => new (v.x, v.y);
    public static implicit operator Vector2(Vector2D v) => new (v.x, v.y);

    public static implicit operator Vector2D(Vector3 v) => new (v.x, v.y);
    public static implicit operator Vector3(Vector2D v) => new (v.x, v.y, 0f);

    public static implicit operator Vector3D(Vector2D v) => new (v.x, v.y);
    public static implicit operator Vector4D(Vector2D v) => new (v.x, v.y);
}