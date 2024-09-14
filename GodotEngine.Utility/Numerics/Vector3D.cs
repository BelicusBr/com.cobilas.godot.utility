using Godot;
using System;

namespace Cobilas.GodotEngine.Utility.Numerics;
[Serializable]
public struct Vector3D : IVector<Vector3D> {
    public float x;
    public float y;
    public float z;

    public readonly Vector3D Normalized => Normalize(this);
    public readonly Vector3D floor => Floor(this);
    public readonly Vector3D ceil => Ceil(this);
    public readonly float magnitude => Magnitude(this);
    public readonly float sqrMagnitude => SqrMagnitude(this);
    public readonly int AxisCount => 3;

    readonly IVector IVector.Normalized => Normalize(this);
    readonly IVector IVector.floor => Floor(this);
    readonly IVector IVector.ceil => Ceil(this);
    readonly float IVector.aspect => throw new NotImplementedException();

    private static readonly Vector3D _zero = new(0f, 0f);
    private static readonly Vector3D _one = new(1f, 1f, 1f);
    private static readonly Vector3D _up = new(0f, -1f);
    private static readonly Vector3D _down = new(0f, 1f);
    private static readonly Vector3D _right = new(1f, 0f);
    private static readonly Vector3D _left = new(-1f, 0f);
    private static readonly Vector3D _forward = new(0f, 0f, 1f);
    private static readonly Vector3D _back = new(0f, 0f, -1f);

    public static Vector3D Zero => _zero;
    public static Vector3D One => _one;
    public static Vector3D Up => _up;
    public static Vector3D Down => _down;
    public static Vector3D Right => _right;
    public static Vector3D Left => _left;
    public static Vector3D Forward => _forward;
    public static Vector3D Back => _back;

    public float this[int index] {
        readonly get => index switch {
            0 => x,
            1 => y,
            2 => z,
            _ => throw new ArgumentOutOfRangeException(nameof(index))
        };
        set {
            switch (index) {
                case 0: x = value; break;
                case 1: y = value; break;
                case 2: z = value; break;
                default: throw new ArgumentOutOfRangeException(nameof(index));
            }
            
        }
    }

    public Vector3D(float x, float y) : this(x, y, 0f) {}

    public Vector3D(float x, float y, float z) : this() {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vector3D(Vector3D vector) : this(vector.x, vector.y, vector.z) {}

    public Vector3D(Vector3 vector) : this(vector.x, vector.y, vector.z) {}

#region Methods
    public readonly Vector3D Abs(bool absX = true, bool absY = true, bool absZ = true) {
        Vector2D abs = Abs(this);
        abs[0] = absX ? abs[0] : this[0];
        abs[1] = absY ? abs[1] : this[1];
        abs[1] = absZ ? abs[1] : this[1];
        return abs;
    }

    public readonly bool Equals(Vector3D other)
        => other.x == this.x && other.y == this.y && other.z == this.z;

    public readonly string ToString(string format) => ToString("(x:{0:N3} x:{1:N3} x:{2:N3})");

    public readonly string ToString(string format, IFormatProvider formatProvider)
        => string.Format(formatProvider, format, this.x, this.y, this.z);

    public override readonly bool Equals(object obj)
        => obj is Vector3D other && Equals(other);

    public override readonly int GetHashCode() => x.GetHashCode() ^ y.GetHashCode() << 2 ^ z.GetHashCode();
#endregion
#region Static Methods
    public static float SqrMagnitude(in Vector3D a)
        => (float)(a.x * (double)a.x + a.y * (double)a.y + a.z * (double)a.z);

    public static float Magnitude(in Vector3D a) => Mathf.Sqrt(SqrMagnitude(a));

    public static Vector3D Normalize(in Vector3D a) {
        float num1 = SqrMagnitude(a);
        if (num1 == 0) return Vector2.Zero;
        float num2 = Mathf.Sqrt(num1);
        return new(a[0] / num2, a[1] / num2, a[2] / num2);
    }

    public static Vector3D Ceil(in Vector3D a) {
        Vector2 result = a;
        result[0] = Mathf.Ceil(result[0]);
        result[1] = Mathf.Ceil(result[1]);
        result[2] = Mathf.Ceil(result[2]);
        return result;
    }
    
    public static Vector3D Floor(in Vector3D a) {
        Vector2 result = a;
        result[0] = Mathf.Floor(result[0]);
        result[1] = Mathf.Floor(result[1]);
        result[2] = Mathf.Floor(result[2]);
        return result;
    }

    public static float Dot(in Vector3D lhs, in Vector3D rhs)
        => lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;

    public static float AngleTo(in Vector2D lhs, in Vector2D rhs)
        => Mathf.Atan2(Magnitude(Cross(lhs, rhs)), Dot(lhs, rhs));

    public static Vector3D Cross(in Vector3D lhs, in Vector3D rhs)
        => new(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);

    public static Vector3D Abs(in Vector3D a)
        => new(Mathf.Abs(a[0]), Mathf.Abs(a[1]), Mathf.Abs(a[2]));

    public static float Distance(in Vector3D a, in Vector3D b) => Magnitude(a - b);

    public static Vector3D Min(Vector3D lhs, Vector3D rhs) => new(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z));
    public static Vector3D Max(Vector3D lhs, Vector3D rhs) => new(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z));
#endregion

    public static Vector3D operator +(Vector3D a, Vector3D b) {
        Vector3D result = Vector3D._zero;
        result.x = a.x + b.x;
        result.y = a.y + b.y;
        result.z = a.z + b.z;
        return result;
    }

    public static Vector3D operator -(Vector3D a, Vector3D b) {
        Vector3D result = Vector3D._zero;
        result.x = a.x - b.x;
        result.y = a.y - b.y;
        result.z = a.z - b.z;
        return result;
    }

    public static Vector3D operator /(Vector3D a, Vector3D b) {
        Vector3D result = Vector3D._zero;
        result.x = a.x / b.x;
        result.y = a.y / b.y;
        result.z = a.z / b.z;
        return result;
    }

    public static Vector3D operator *(Vector3D a, Vector3D b) {
        Vector3D result = Vector3D._zero;
        result.x = a.x * b.x;
        result.y = a.y * b.y;
        result.z = a.z * b.z;
        return result;
    }

    public static Vector3D operator /(Vector3D a, float b) {
        Vector3D result = Vector3D._zero;
        result.x = a.x / b;
        result.y = a.y / b;
        result.z = a.z / b;
        return result;
    }

    public static Vector3D operator *(Vector3D a, float b) {
        Vector3D result = Vector3D._zero;
        result.x = a.x * b;
        result.y = a.y * b;
        result.z = a.z * b;
        return result;
    }

    public static bool operator ==(in Vector3D lhs, in Vector3D rhs) => lhs.Equals(rhs);
    public static bool operator !=(in Vector3D lhs, in Vector3D rhs) => !lhs.Equals(rhs);

    public static implicit operator Vector3D(Vector3 v) => new (v.x, v.y, v.z);
    public static implicit operator Vector3(Vector3D v) => new (v.x, v.y, v.z);

    public static implicit operator Vector3D(Vector2 v) => new (v.x, v.y);
    public static implicit operator Vector2(Vector3D v) => new (v.x, v.y);
    
    public static implicit operator Vector2D(Vector3D v) => new (v.x, v.y);
    public static implicit operator Vector4D(Vector3D v) => new (v.x, v.y);
}