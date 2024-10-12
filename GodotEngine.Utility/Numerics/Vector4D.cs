using Godot;
using System;
using System.Globalization;

namespace Cobilas.GodotEngine.Utility.Numerics;
[Serializable]
public struct Vector4D : IVectorGeneric<Vector4D> {
    public float x;
    public float y;
    public float z;
    public float w;

    private static readonly Vector4D _zero = new(0f, 0f);
    private static readonly Vector4D _one = new(1f, 1f, 1f, 1f);

    public static Vector4D Zero => _zero;
    public static Vector4D One => _one;
    /// <inheritdoc/>
    public readonly Vector4D Normalized => Normalize(this);
    /// <inheritdoc/>
    public readonly Vector4D floor => Floor(this);
    /// <inheritdoc/>
    public readonly Vector4D ceil => Ceil(this);
    /// <inheritdoc/>
    public readonly float magnitude => Magnitude(this);
    /// <inheritdoc/>
    public readonly float sqrMagnitude => SqrMagnitude(this);
    /// <inheritdoc/>
    public readonly int AxisCount => 4;
    /// <inheritdoc/>
    public readonly float aspect => Vector2D.Aspect(this);

    readonly IVector IVector.floor => Floor(this);
    readonly IVector IVector.ceil => Ceil(this);
    readonly IVector IVector.Normalized => Normalize(this);
    /// <inheritdoc/>
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
    /// <summary>Starts a new instance of the object.</summary>
    public Vector4D(float x, float y) : this(x, y, 0f, 0f) {}
    /// <summary>Starts a new instance of the object.</summary>
    public Vector4D(float x, float y, float z) : this(x, y, z, 0f) {}
    /// <summary>Starts a new instance of the object.</summary>
    public Vector4D(float x, float y, float z, float w) : this() {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }
    /// <summary>Starts a new instance of the object.</summary>
    public Vector4D(Vector4D vector) : this(vector.x, vector.y, vector.z, vector.w) {}
    /// <summary>Starts a new instance of the object.</summary>
    public Vector4D(Quaternion vector) : this(vector.x, vector.y, vector.z, vector.w) {}
    
    public readonly Vector4D Abs(bool absX = true, bool absY = true, bool absZ = true, bool absW = true) {
        Vector4D abs = Abs(this);
        abs[0] = absX ? abs[0] : this[0];
        abs[1] = absY ? abs[1] : this[1];
        abs[2] = absZ ? abs[2] : this[2];
        abs[3] = absW ? abs[3] : this[3];
        return abs;
    }

    public readonly Vector4D Neg(bool negX = true, bool negY = true, bool negZ = true, bool negW = true) {
        Vector4D abs = Neg(this);
        abs[0] = negX ? abs[0] : this[0];
        abs[1] = negY ? abs[1] : this[1];
        abs[2] = negZ ? abs[2] : this[2];
        abs[3] = negW ? abs[3] : this[3];
        return abs;
    }
    /// <inheritdoc/>
    public readonly bool Equals(Vector4D other)
        => other.x == this.x && other.y == this.y && other.z == this.z && other.w == this.w;
    /// <inheritdoc/>
    public readonly string ToString(string format, IFormatProvider formatProvider)
        => string.Format(formatProvider, format, this.x, this.y, this.z, this.w);
    /// <inheritdoc/>
    public readonly string ToString(string format) => ToString(format, CultureInfo.InvariantCulture);
    /// <inheritdoc/>
    public override readonly string ToString() => ToString("(x:{0:N3} y:{1:N3} z:{2:N3} w:{2:N3})");
    /// <inheritdoc/>
    public override readonly bool Equals(object obj)
        => obj is Vector3D other && Equals(other);
    /// <inheritdoc/>
    public override readonly int GetHashCode() => x.GetHashCode() ^ y.GetHashCode() << 2 ^ z.GetHashCode() >> 2 ^ w.GetHashCode();
    /// <inheritdoc/>
    public readonly Vector4D Round() => Round(this);
    readonly IVector IVector.Round() => Round(this);

    public static float SqrMagnitude(in Vector4D a) => Vector4D.Dot(a, a);
    public static Vector4D Neg(in Vector4D a) => new(-a.x, -a.y, -a.z, -a.w);
    public static float Distance(in Vector4D a, Vector4D b) => Vector4D.Magnitude(a - b);
    public static float Magnitude(in Vector4D a) => (float)Math.Sqrt((double)Vector4D.Dot(a, a));
    public static bool IsNormalized(in IVector a) => Mathf.Abs(a.sqrMagnitude - 1f) < Quaternion.KEpsilon;
    public static Vector4D Abs(in Vector4D a) => new(Mathf.Abs(a.x), Mathf.Abs(a.y), Mathf.Abs(a.z), Mathf.Abs(a.w));
    public static Vector4D Round(in Vector4D a) => new(Mathf.Round(a.x), Mathf.Round(a.y), Mathf.Round(a.z), Mathf.Round(a.w));
    public static float Dot(in Vector4D a, in Vector4D b) => (float)(a.x * (double)b.x + a.y * (double)b.y + a.z * (double)b.z + a.w * (double)b.w);

    public static Vector4D Normalize(in Vector4D a) {
        float num = Vector4D.Magnitude(a);
        return (double)num > 9.99999974737875E-06 ? a / num : Vector4D.Zero;
    }

    public static Vector4D Min(in Vector4D lhs, in Vector4D rhs) => new(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z), Mathf.Min(lhs.w, rhs.w));
    public static Vector4D Max(in Vector4D lhs, in Vector4D rhs) => new(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z), Mathf.Max(lhs.w, rhs.w));

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