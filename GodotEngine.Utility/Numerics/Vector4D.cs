using Godot;
using System;
using System.Globalization;

namespace Cobilas.GodotEngine.Utility.Numerics;
[Serializable]
public struct Vector4D : IVector<Vector4D> {
    public float x;
    public float y;
    public float z;
    public float w;

    private static readonly Vector4D _zero = new(0f, 0f);
    private static readonly Vector4D _one = new(1f, 1f, 1f, 1f);

    public static Vector4D Zero => _zero;
    public static Vector4D One => _one;

    public readonly Vector4D Normalized => Normalize(this);
    public readonly Vector4D floor => Floor(this);
    public readonly Vector4D ceil => Ceil(this);
    public readonly float magnitude => Magnitude(this);
    public readonly float sqrMagnitude => SqrMagnitude(this);
    public readonly int AxisCount => 4;

    readonly IVector IVector.floor => Floor(this);
    readonly IVector IVector.ceil => Ceil(this);
    readonly IVector IVector.Normalized => Normalize(this);
    readonly float IVector.aspect => throw new NotImplementedException();

    public float this[int index] { 
        readonly get => index switch {
            0 => this[0],
            1 => this[1],
            2 => this[2],
            3 => this[3],
            _ => throw new IndexOutOfRangeException(nameof(index))
        };
        set {
            switch (index) {
                case 0: x = value; break;
                case 1: y = value; break;
                case 2: z = value; break;
                case 3: w = value; break;
                default: throw new IndexOutOfRangeException(nameof(index));
            }
            
        }
    }

    public Vector4D(float x, float y) : this(x, y, 0f, 0f) {}

    public Vector4D(float x, float y, float z) : this(x, y, z, 0f) {}

    public Vector4D(float x, float y, float z, float w) : this() {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }

    public Vector4D(Vector4D vector) : this(vector.x, vector.y, vector.z, vector.w) {}

    public Vector4D(Quaternion vector) : this(vector.x, vector.y, vector.z, vector.w) {}
    
    public readonly Vector3D Abs(bool absX = true, bool absY = true, bool absZ = true, bool absW = true) {
        Vector4D abs = Abs(this);
        abs[0] = absX ? abs[0] : this[0];
        abs[1] = absY ? abs[1] : this[1];
        abs[2] = absZ ? abs[2] : this[2];
        abs[3] = absW ? abs[3] : this[3];
        return abs;
    }

    public readonly bool Equals(Vector4D other)
        => other.x == this.x && other.y == this.y && other.z == this.z && other.w == this.w;

    public readonly string ToString(string format, IFormatProvider formatProvider)
        => string.Format(formatProvider, format, this.x, this.y, this.z, this.w);

    public readonly string ToString(string format) => ToString(format, CultureInfo.InvariantCulture);

    public override readonly string ToString() => ToString("(x:{0:N3} y:{1:N3} z:{2:N3} w:{2:N3})");

    public override readonly bool Equals(object obj)
        => obj is Vector3D other && Equals(other);

    public override readonly int GetHashCode() => x.GetHashCode() ^ y.GetHashCode() << 2 ^ z.GetHashCode() >> 2 ^ w.GetHashCode();

    public static bool IsNormalized(IVector a)
        => Mathf.Abs(a.sqrMagnitude - 1f) < Quaternion.KEpsilon;

    public static Vector4D Abs(Vector4D a)
        => new(Mathf.Abs(a.x), Mathf.Abs(a.y), Mathf.Abs(a.z), Mathf.Abs(a.w));

    public static float Dot(Vector4D a, Vector4D b) 
        => (float)(a.x * (double)b.x + a.y * (double)b.y + a.z * (double)b.z + a.w * (double)b.w);

    public static float SqrMagnitude(Vector4D a) => Vector4D.Dot(a, a);

    public static float Magnitude(Vector4D a) => (float)Math.Sqrt((double)Vector4D.Dot(a, a));

    public static Vector4D Normalize(Vector4D a) {
        float num = Vector4D.Magnitude(a);
        return (double)num > 9.99999974737875E-06 ? a / num : Vector4D.Zero;
    }

    public static float Distance(Vector4D a, Vector4D b) => Vector4D.Magnitude(a - b);

    public static Vector4D Min(Vector4D lhs, Vector4D rhs) => new(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z), Mathf.Min(lhs.w, rhs.w));
    public static Vector4D Max(Vector4D lhs, Vector4D rhs) => new(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z), Mathf.Max(lhs.w, rhs.w));

    public static Vector4D Ceil(in Vector4D a) {
        Vector4D result = a;
        result[0] = Mathf.Ceil(result[0]);
        result[1] = Mathf.Ceil(result[1]);
        result[2] = Mathf.Ceil(result[2]);
        result[3] = Mathf.Ceil(result[3]);
        return result;
    }
    
    public static Vector3D Floor(in Vector3D a) {
        Vector4D result = a;
        result[0] = Mathf.Floor(result[0]);
        result[1] = Mathf.Floor(result[1]);
        result[2] = Mathf.Floor(result[2]);
        result[3] = Mathf.Floor(result[3]);
        return result;
    }

    public static Vector4D operator +(Vector4D a, Vector4D b) => new(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
    public static Vector4D operator -(Vector4D a, Vector4D b) => new(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
    public static Vector4D operator /(Vector4D a, Vector4D b) => new(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);
    public static Vector4D operator *(Vector4D a, Vector4D b) => new(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
    public static Vector4D operator /(Vector4D a, float b) => new(a.x / b, a.y / b, a.z / b, a.w / b);
    public static Vector4D operator *(Vector4D a, float b) => new(a.x * b, a.y * b, a.z * b, a.w * b);
    public static Vector4D operator /(float a, Vector4D b) => new(a / b.x, a / b.y, a / b.z, a / b.w);
    public static Vector4D operator *(float a, Vector4D b) => new(a * b.x, a * b.y, a * b.z, a * b.w);

    public static implicit operator Vector4D(Vector3 v) => new (v.x, v.y, v.z);
    public static implicit operator Vector3(Vector4D v) => new (v.x, v.y, v.z);

    public static implicit operator Vector4D(Vector2 v) => new (v.x, v.y);
    public static implicit operator Vector2(Vector4D v) => new (v.x, v.y);
    
    public static implicit operator Vector2D(Vector4D v) => new (v.x, v.y);
    public static implicit operator Vector3D(Vector4D v) => new (v.x, v.y, v.z);
}