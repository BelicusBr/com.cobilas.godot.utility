using Godot;
using System;
using System.Globalization;

namespace Cobilas.GodotEngine.Utility.Numerics;

[Serializable]
public struct Vector3DInt : IIntVector {
    public int x;
    public int y;
    public int z;
    /// <inheritdoc/>
    public readonly int AxisCount => 2;
    /// <inheritdoc/>
    public readonly float magnitude => Magnitude(this);
    /// <inheritdoc/>
    public readonly float aspect => Vector2D.Aspect(this);
    /// <inheritdoc/>
    public readonly int sqrMagnitude => SqrMagnitude(this);
    /// <inheritdoc/>
    public readonly Vector3DInt ceilToInt => CeilToInt(this);
    /// <inheritdoc/>
    public readonly Vector3DInt floorToInt => FloorToInt(this);

    private static readonly Vector3DInt _zero = new(0, 0);
    private static readonly Vector3DInt _one = new(1, 1, 1);
    private static readonly Vector3DInt _up = new(0, -1);
    private static readonly Vector3DInt _down = new(0, 1);
    private static readonly Vector3DInt _right = new(1, 0);
    private static readonly Vector3DInt _left = new(-1, 0);
    private static readonly Vector3DInt _forward = new(0, 0, 1);
    private static readonly Vector3DInt _back = new(0, 0, -1);

    public static Vector3DInt Zero => _zero;
    public static Vector3DInt One => _one;
    public static Vector3DInt Up => _up;
    public static Vector3DInt Down => _down;
    public static Vector3DInt Right => _right;
    public static Vector3DInt Left => _left;
    public static Vector3DInt Forward => _forward;
    public static Vector3DInt Back => _back;
    /// <inheritdoc/>
    public int this[int index] { 
        readonly get => index switch {
            0 => x,
            1 => y,
            2 => z,
            _ => throw new ArgumentOutOfRangeException(nameof(index))
        };
        set {
            switch(index) {
                case 0: x = value; break;
                case 1: y = value; break;
                case 2: z = value; break;
                default: throw new ArgumentOutOfRangeException(nameof(index));
            }
        }
    }
    /// <summary>Starts a new instance of the object.</summary>
    public Vector3DInt(in int x, in int y, in int z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    /// <summary>Starts a new instance of the object.</summary>
    public Vector3DInt(in int x, in int y) : this(x, y, 0) {}
    /// <summary>Starts a new instance of the object.</summary>
    public Vector3DInt(in Vector3DInt vector) : this(vector.x, vector.y, vector.z) {}
    /// <inheritdoc/>
    public override readonly bool Equals(object obj) => obj is Vector3DInt vector && Equals(vector);
    /// <inheritdoc/>
    public readonly bool Equals(Vector3DInt other) => other.x == this.x && other.y == this.y && other.z == this.z;
    /// <inheritdoc/>
    public override readonly int GetHashCode() => x.GetHashCode() ^ y.GetHashCode() << 2 ^ z.GetHashCode();
    /// <inheritdoc/>
    public readonly string ToString(string format, IFormatProvider formatProvider) => string.Format(formatProvider, format, this.x, this.y, this.z);
    /// <inheritdoc/>
    public readonly string ToString(string format) => ToString(format, CultureInfo.InvariantCulture);
    /// <inheritdoc/>
    public override readonly string ToString() => ToString("(x:{0} y:{1} z:{2})");

    public readonly Vector3DInt Abs(bool absX = true, bool absY = true, bool absZ = true) {
        Vector3DInt abs = Abs(this);
        abs[0] = absX ? abs[0] : this[0];
        abs[1] = absY ? abs[1] : this[1];
        abs[2] = absZ ? abs[2] : this[2];
        return abs;
    }

    public readonly Vector3DInt Neg(bool negX = true, bool negY = true, bool negZ = true) {
        Vector3DInt neg = Neg(this);
        neg[0] = negX ? neg[0] : this[0];
        neg[1] = negY ? neg[1] : this[1];
        neg[2] = negZ ? neg[2] : this[2];
        return neg;
    }

    public static Vector3DInt Neg(in Vector3DInt a) => new(-a[0], -a[1], -a[2]);
    public static float Magnitude(in Vector3DInt a) => Mathf.Sqrt(SqrMagnitude(a));
    public static float Distance(in Vector3DInt a, in Vector3DInt b) => Magnitude(a - b);
    public static int SqrMagnitude(in Vector3DInt a) => a.x * a.x + a.y * a.y + a.z * a.z;
    public static Vector3DInt Abs(in Vector3DInt a)  => new(Mathf.Abs(a[0]), Mathf.Abs(a[1]), Mathf.Abs(a[2]));
    public static Vector3DInt RoundToInt(in Vector3D a) => new(Mathf.RoundToInt(a.x), Mathf.RoundToInt(a.y), Mathf.RoundToInt(a.z));
    public static Vector3DInt Min(Vector3DInt lhs, Vector3DInt rhs) => new(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z));
    public static Vector3DInt Max(Vector3DInt lhs, Vector3DInt rhs) => new(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z));

    public static Vector3DInt FloorToInt(in Vector3D a) {
        Vector3DInt result = Vector3DInt._zero;
        result[0] = Mathf.FloorToInt(result[0]);
        result[1] = Mathf.FloorToInt(result[1]);
        result[2] = Mathf.FloorToInt(result[2]);
        return result;
    }

    public static Vector3DInt CeilToInt(in Vector3D a) {
        Vector3DInt result = Vector3DInt._zero;
        result[0] = Mathf.CeilToInt(result[0]);
        result[1] = Mathf.CeilToInt(result[1]);
        result[2] = Mathf.CeilToInt(result[2]);
        return result;
    }

    public static Vector3DInt operator +(Vector3DInt a, Vector3DInt b) {
        Vector3DInt result = Vector3DInt._zero;
        result.x = a.x + b.x;
        result.y = a.y + b.y;
        result.z = a.z + b.z;
        return result;
    }

    public static Vector3DInt operator -(Vector3DInt a, Vector3DInt b) {
        Vector3DInt result = Vector3DInt._zero;
        result.x = a.x - b.x;
        result.y = a.y - b.y;
        result.z = a.z - b.z;
        return result;
    }

    public static Vector3DInt operator /(Vector3DInt a, Vector3DInt b) {
        Vector3DInt result = Vector3DInt._zero;
        result.x = a.x / b.x;
        result.y = a.y / b.y;
        result.z = a.z / b.z;
        return result;
    }

    public static Vector3DInt operator *(Vector3DInt a, Vector3DInt b) {
        Vector3DInt result = Vector3DInt._zero;
        result.x = a.x * b.x;
        result.y = a.y * b.y;
        result.z = a.z * b.z;
        return result;
    }

    public static Vector3DInt operator /(Vector3DInt a, int b) {
        Vector3DInt result = Vector3DInt._zero;
        result.x = a.x / b;
        result.y = a.y / b;
        result.z = a.z / b;
        return result;
    }

    public static Vector3DInt operator *(Vector3DInt a, int b) {
        Vector3DInt result = Vector3DInt._zero;
        result.x = a.x * b;
        result.y = a.y * b;
        result.z = a.z * b;
        return result;
    }

    public static bool operator ==(in Vector3DInt lhs, in Vector3DInt rhs) => lhs.Equals(rhs);
    public static bool operator !=(in Vector3DInt lhs, in Vector3DInt rhs) => !lhs.Equals(rhs);

    public static implicit operator Vector2DInt(Vector3DInt v) => new(v.x, v.y);

    public static implicit operator Vector2(Vector3DInt v) => new(v.x, v.y);
    public static implicit operator Vector2D(Vector3DInt v) => new(v.x, v.y);
    public static implicit operator Vector3D(Vector3DInt v) => new(v.x, v.y, v.z);
    public static implicit operator Vector4D(Vector3DInt v) => new(v.x, v.y, v.z);

    public static explicit operator Vector3DInt(Vector2D v) => new((int)v.x, (int)v.y);
    public static explicit operator Vector3DInt(Vector2 v) => new((int)v.x, (int)v.y);
    public static explicit operator Vector3DInt(Vector3D v) => new((int)v.x, (int)v.y, (int)v.z);
    public static explicit operator Vector3DInt(Vector4D v) => new((int)v.x, (int)v.y, (int)v.z);
}