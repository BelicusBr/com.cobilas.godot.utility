using Godot;
using System;
using System.Globalization;

namespace Cobilas.GodotEngine.Utility.Numerics;

[Serializable]
public struct Vector2DInt : IIntVectorGeneric<Vector2DInt> {
    public int x;
    public int y;

    public readonly int AxisCount => 2;
    public readonly float aspect => Aspect(this);
    public readonly float magnitude => Magnitude(this);
    public readonly int sqrMagnitude => SqrMagnitude(this);
    public readonly Vector2DInt ceilToInt => CeilToInt(this);
    public readonly Vector2DInt floorToInt => FloorToInt(this);
    readonly IIntVector IIntVector.ceilToInt => CeilToInt(this);
    readonly IIntVector IIntVector.floorToInt => FloorToInt(this);

    private static readonly Vector2DInt _zero = new(0, 0);
    private static readonly Vector2DInt _one = new(1, 1);
    private static readonly Vector2DInt _up = new(0, -1);
    private static readonly Vector2DInt _down = new(0, 1);
    private static readonly Vector2DInt _right = new(1, 0);
    private static readonly Vector2DInt _left = new(-1, 0);
    
    public static Vector2DInt Zero => _zero;
    public static Vector2DInt One => _one;
    public static Vector2DInt Up => _up;
    public static Vector2DInt Down => _down;
    public static Vector2DInt Right => _right;
    public static Vector2DInt Left => _left;

    public int this[int index] { 
        readonly get => index switch {
            0 => x,
            1 => y,
            _ => throw new ArgumentOutOfRangeException(nameof(index))
        };
        set {
            switch(index) {
                case 0: x = value; break;
                case 1: y = value; break;
                default: throw new ArgumentOutOfRangeException(nameof(index));
            }
        }
    }

    public Vector2DInt(in int x, in int y) {
        this.x = x;
        this.y = y;
    }

    public Vector2DInt(in Vector2DInt vector) : this(vector.x, vector.y) {}

    public readonly bool Equals(Vector2DInt other) => other.x == this.x && other.y == this.y;

    public override readonly bool Equals(object obj)
        => obj is Vector2DInt vector && Equals(vector);

    public override readonly int GetHashCode() => x.GetHashCode() ^ y.GetHashCode();

    public readonly string ToString(string format, IFormatProvider formatProvider)
        => string.Format(formatProvider, format, this.x, this.y);

    public readonly string ToString(string format)
        => ToString(format, CultureInfo.InvariantCulture);

    public override readonly string ToString() => ToString("(x:{0} y:{1})");

    public readonly Vector2DInt Abs(bool absX = true, bool absY = true) {
        Vector2DInt abs = Abs(this);
        abs[0] = absX ? abs[0] : this[0];
        abs[1] = absY ? abs[1] : this[1];
        return abs;
    }

    public readonly Vector2DInt Neg(bool negX = true, bool negY = true) {
        Vector2DInt neg = Neg(this);
        neg[0] = negX ? neg[0] : this[0];
        neg[1] = negY ? neg[1] : this[1];
        return neg;
    }

    public readonly Vector2DInt RoundToInt() => RoundToInt(this);
    readonly IIntVector IIntVector.RoundToInt() => RoundToInt(this);

    public static float Aspect(in Vector2DInt a) => a.x / a.y;
    public static Vector2DInt Neg(in Vector2DInt a) => new(-a[0], -a[1]);
    public static int SqrMagnitude(in Vector2DInt a) => a.x * a.x + a.y * a.y;
    public static float Magnitude(in Vector2DInt a) => Mathf.Sqrt(SqrMagnitude(a));
    public static float Distance(in Vector2DInt a, in Vector2DInt b) => Magnitude(a - b);
    public static Vector2DInt Abs(in Vector2DInt a)  => new(Mathf.Abs(a[0]), Mathf.Abs(a[1]));
    public static Vector2DInt RoundToInt(in Vector2DInt a) => new(Mathf.RoundToInt(a.x), Mathf.RoundToInt(a.y));
    public static Vector2DInt Min(Vector2DInt lhs, Vector2DInt rhs) => new(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y));
    public static Vector2DInt Max(Vector2DInt lhs, Vector2DInt rhs) => new(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y));

    public static Vector2DInt FloorToInt(in Vector2DInt a) {
        Vector2DInt result = a;
        result[0] = Mathf.FloorToInt(result[0]);
        result[1] = Mathf.FloorToInt(result[1]);
        return result;
    }

    public static Vector2DInt CeilToInt(in Vector2DInt a) {
        Vector2DInt result = a;
        result[0] = Mathf.CeilToInt(result[0]);
        result[1] = Mathf.CeilToInt(result[1]);
        return result;
    }

    public static Vector2DInt operator +(Vector2DInt a, Vector2DInt b) {
        Vector2DInt result = Vector2DInt._zero;
        result.x = a.x + b.x;
        result.y = a.y + b.y;
        return result;
    }

    public static Vector2DInt operator -(Vector2DInt a, Vector2DInt b) {
        Vector2DInt result = Vector2DInt._zero;
        result.x = a.x - b.x;
        result.y = a.y - b.y;
        return result;
    }

    public static Vector2DInt operator /(Vector2DInt a, Vector2DInt b) {
        Vector2DInt result = Vector2DInt._zero;
        result.x = a.x / b.x;
        result.y = a.y / b.y;
        return result;
    }

    public static Vector2DInt operator *(Vector2DInt a, Vector2DInt b) {
        Vector2DInt result = Vector2DInt._zero;
        result.x = a.x * b.x;
        result.y = a.y * b.y;
        return result;
    }

    public static Vector2DInt operator /(Vector2DInt a, int b) {
        Vector2DInt result = Vector2DInt._zero;
        result.x = a.x / b;
        result.y = a.y / b;
        return result;
    }

    public static Vector2DInt operator *(Vector2DInt a, int b) {
        Vector2DInt result = Vector2DInt._zero;
        result.x = a.x * b;
        result.y = a.y * b;
        return result;
    }

    public static bool operator ==(in Vector2DInt lhs, in Vector2DInt rhs) => lhs.Equals(rhs);
    public static bool operator !=(in Vector2DInt lhs, in Vector2DInt rhs) => !lhs.Equals(rhs);

    public static implicit operator Vector3DInt(Vector2DInt v) => new(v.x, v.y);

    public static implicit operator Vector2D(Vector2DInt v) => new(v.x, v.y);
    public static implicit operator Vector2(Vector2DInt v) => new(v.x, v.y);
    public static implicit operator Vector3D(Vector2DInt v) => new(v.x, v.y);
    public static implicit operator Vector4D(Vector2DInt v) => new(v.x, v.y);

    public static explicit operator Vector2DInt(Vector2 v) => new((int)v.x, (int)v.y);
    public static explicit operator Vector2DInt(Vector2D v) => new((int)v.x, (int)v.y);
    public static explicit operator Vector2DInt(Vector3D v) => new((int)v.x, (int)v.y);
    public static explicit operator Vector2DInt(Vector4D v) => new((int)v.x, (int)v.y);
}