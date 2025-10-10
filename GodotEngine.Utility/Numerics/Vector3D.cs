using Godot;
using System;
using System.Globalization;
using Cobilas.GodotEditor.Utility.Serialization;

namespace Cobilas.GodotEngine.Utility.Numerics;
/// <summary>Represents a three-dimensional vector.</summary>
[Serializable]
public struct Vector3D : IVectorGeneric<Vector3D> {
    /// <inheritdoc cref="Vector4D.x"/>
    [ShowProperty(true)] public float x;
    /// <inheritdoc cref="Vector4D.y"/>
    [ShowProperty(true)] public float y;
    /// <inheritdoc cref="Vector4D.z"/>
    [ShowProperty(true)] public float z;
    /// <inheritdoc/>
    public readonly Vector3D Normalized => Normalize(this);
    /// <inheritdoc/>
    public readonly Vector3D floor => Floor(this);
    /// <inheritdoc/>
    public readonly Vector3D ceil => Ceil(this);
    /// <inheritdoc/>
    public readonly float magnitude => Magnitude(this);
    /// <inheritdoc/>
    public readonly float aspect => Vector2D.Aspect(this);
    /// <inheritdoc/>
    public readonly float sqrMagnitude => SqrMagnitude(this);
    /// <inheritdoc/>
    public readonly int AxisCount => 3;

    readonly IVector IVector.Normalized => Normalize(this);
    readonly IVector IVector.floor => Floor(this);
    readonly IVector IVector.ceil => Ceil(this);

    private static readonly Vector3D _zero = new(0f, 0f);
    private static readonly Vector3D _one = new(1f, 1f, 1f);
    private static readonly Vector3D _up = new(0f, -1f);
    private static readonly Vector3D _down = new(0f, 1f);
    private static readonly Vector3D _right = new(1f, 0f);
    private static readonly Vector3D _left = new(-1f, 0f);
    private static readonly Vector3D _forward = new(0f, 0f, 1f);
    private static readonly Vector3D _back = new(0f, 0f, -1f);
    /// <summary>Shorthand for writing Vector3(0,0,0).</summary>
    public static Vector3D Zero => _zero;
    /// <summary>Shorthand for writing Vector3(1,1,1).</summary>
    public static Vector3D One => _one;
    /// <summary>Shorthand for writing Vector3(0,-1f,0).</summary>
    public static Vector3D Up => _up;
    /// <summary>Shorthand for writing Vector3(0,1f,0).</summary>
    public static Vector3D Down => _down;
    /// <summary>Shorthand for writing Vector3(1f,0,0).</summary>
    public static Vector3D Right => _right;
    /// <summary>Shorthand for writing Vector3(-1f,0,0).</summary>
    public static Vector3D Left => _left;
    /// <summary>Shorthand for writing Vector3(0,0,1f).</summary>
    public static Vector3D Forward => _forward;
    /// <summary>Shorthand for writing Vector3(0,0,-1f).</summary>
    public static Vector3D Back => _back;
    /// <inheritdoc/>
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
    /// <summary>Starts a new instance of the object.</summary>
    public Vector3D(float x, float y) : this(x, y, 0f) {}
    /// <summary>Starts a new instance of the object.</summary>
    public Vector3D(float x, float y, float z) : this() {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    /// <summary>Starts a new instance of the object.</summary>
    public Vector3D(Vector3D vector) : this(vector.x, vector.y, vector.z) {}
    /// <summary>Starts a new instance of the object.</summary>
    public Vector3D(Vector3 vector) : this(vector.x, vector.y, vector.z) {}

#region Methods
    /// <inheritdoc cref="Vector4D.Abs(bool, bool, bool, bool)"/>
    public readonly Vector3D Abs(bool absX = true, bool absY = true, bool absZ = true) {
        Vector2D abs = Abs(this);
        abs[0] = absX ? abs[0] : this[0];
        abs[1] = absY ? abs[1] : this[1];
        abs[2] = absZ ? abs[2] : this[2];
        return abs;
    }
    /// <inheritdoc cref="Vector4D.Neg(bool, bool, bool, bool)"/>
    public readonly Vector3D Neg(bool negX = true, bool negY = true, bool negZ = true) {
        Vector2D abs = Neg(this);
        abs[0] = negX ? abs[0] : this[0];
        abs[1] = negY ? abs[1] : this[1];
        abs[2] = negZ ? abs[2] : this[2];
        return abs;
    }
    /// <inheritdoc/>
    public readonly bool Equals(Vector3D other)
        => other.x == this.x && other.y == this.y && other.z == this.z;
    /// <inheritdoc/>
    public readonly string ToString(string format, IFormatProvider formatProvider)
        => string.Format(formatProvider, format ?? "(x:{0:N3} y:{1:N3} z:{2:N3})", this.x, this.y, this.z);
    /// <inheritdoc/>
    public readonly string ToString(string format) => ToString(format, CultureInfo.InvariantCulture);
    /// <inheritdoc/>
    public override readonly string ToString() => ToString("(x:{0:N3} y:{1:N3} z:{2:N3})");
    /// <inheritdoc/>
    public override readonly bool Equals(object obj)
        => obj is Vector3D other && Equals(other);
    /// <inheritdoc/>
    public override readonly int GetHashCode() => x.GetHashCode() ^ y.GetHashCode() << 2 ^ z.GetHashCode();
    /// <inheritdoc/>
    public readonly Vector3D Round() => Round(this);
    readonly IVector IVector.Round() => Round(this);
#endregion
#region Static Methods
    /// <inheritdoc cref="Vector4D.Neg(in Vector4D)"/>
    public static Vector3D Neg(in Vector3D a) => new(-a.x, -a.y, -a.z);
    /// <inheritdoc cref="Vector4D.Magnitude(in Vector4D)"/>
    public static float Magnitude(in Vector3D a) => Mathf.Sqrt(SqrMagnitude(a));
    /// <inheritdoc cref="Vector4D.Distance(in Vector4D, Vector4D)"/>
    public static float Distance(in Vector3D a, in Vector3D b) => Magnitude(a - b);
    /// <inheritdoc cref="Vector4D.Abs(in Vector4D)"/>
    public static Vector3D Abs(in Vector3D a) => new(Mathf.Abs(a[0]), Mathf.Abs(a[1]), Mathf.Abs(a[2]));
    /// <inheritdoc cref="Vector4D.Round(in Vector4D)"/>
    public static Vector3D Round(in Vector3D a) => new(Mathf.Round(a.x), Mathf.Round(a.y), Mathf.Round(a.z));
    /// <inheritdoc cref="Vector4D.Dot(in Vector4D, in Vector4D)"/>
    public static float Dot(in Vector3D lhs, in Vector3D rhs) => lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
    /// <inheritdoc cref="Vector4D.SqrMagnitude(in Vector4D)"/>
    public static float SqrMagnitude(in Vector3D a) => (float)(a.x * (double)a.x + a.y * (double)a.y + a.z * (double)a.z);
    /// <summary>Returns the angle in degrees between from and to.</summary>
    /// <param name="lhs">The vector from which the angular difference is measured.</param>
    /// <param name="rhs">The vector to which the angular difference is measured.</param>
    /// <returns>The angle in degrees between the two vectors.</returns>
    public static float AngleTo(in Vector2D lhs, in Vector2D rhs) => Mathf.Atan2(Magnitude(Cross(lhs, rhs)), Dot(lhs, rhs));
    /// <inheritdoc cref="Vector4D.Min(in Vector4D, in Vector4D)"/>
    public static Vector3D Min(Vector3D lhs, Vector3D rhs) => new(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z));
    /// <inheritdoc cref="Vector4D.Max(in Vector4D, in Vector4D)"/>
    public static Vector3D Max(Vector3D lhs, Vector3D rhs) => new(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z));
    /// <summary>Cross Product of two vectors.</summary>
    /// <param name="lhs">One of the values.</param>
    /// <param name="rhs">The other value.</param>
    /// <returns>Returns the cross product of vectors.</returns>
    public static Vector3D Cross(in Vector3D lhs, in Vector3D rhs) => new(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);
    /// <inheritdoc cref="Vector4D.Normalize(in Vector4D)"/>
    public static Vector3D Normalize(in Vector3D a) {
        float num = Vector3D.Magnitude(a);
        return (double)num > 9.99999974737875E-06 ? a / num : Vector3D.Zero;
    }
    /// <inheritdoc cref="Vector4D.Ceil(in Vector4D)"/>
    public static Vector3D Ceil(in Vector3D a) {
        Vector3D result = a;
        result[0] = Mathf.Ceil(result[0]);
        result[1] = Mathf.Ceil(result[1]);
        result[2] = Mathf.Ceil(result[2]);
        return result;
    }
    /// <inheritdoc cref="Vector4D.Floor(in Vector4D)"/>
    public static Vector3D Floor(in Vector3D a) {
        Vector3D result = a;
        result[0] = Mathf.Floor(result[0]);
        result[1] = Mathf.Floor(result[1]);
        result[2] = Mathf.Floor(result[2]);
        return result;
    }
#endregion
    /// <summary>Addition operation between two values.(<seealso cref="Vector3D"/> + <seealso cref="Vector3D"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the addition.</returns>
    public static Vector3D operator +(Vector3D a, Vector3D b) {
        Vector3D result = Vector3D._zero;
        result.x = a.x + b.x;
        result.y = a.y + b.y;
        result.z = a.z + b.z;
        return result;
    }
    /// <summary>Addition operation between two values.(<seealso cref="Vector3D"/> + <seealso cref="Vector3"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the addition.</returns>
    public static Vector3D operator +(Vector3D a, Vector3 b) {
        Vector3D result = Vector3D._zero;
        result.x = a.x + b.x;
        result.y = a.y + b.y;
        result.z = a.z + b.z;
        return result;
    }
    /// <summary>Addition operation between two values.(<seealso cref="Vector3"/> + <seealso cref="Vector3D"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the addition.</returns>
    public static Vector3D operator +(Vector3 a, Vector3D b) {
        Vector3D result = Vector3D._zero;
        result.x = a.x + b.x;
        result.y = a.y + b.y;
        result.z = a.z + b.z;
        return result;
    }
    /// <summary>Subtraction operation between two values.(<seealso cref="Vector3D"/> - <seealso cref="Vector3D"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the subtraction.</returns>
    public static Vector3D operator -(Vector3D a, Vector3D b) {
        Vector3D result = Vector3D._zero;
        result.x = a.x - b.x;
        result.y = a.y - b.y;
        result.z = a.z - b.z;
        return result;
    }
    /// <summary>Subtraction operation between two values.(<seealso cref="Vector3D"/> - <seealso cref="Vector3"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the subtraction.</returns>
    public static Vector3D operator -(Vector3D a, Vector3 b) {
        Vector3D result = Vector3D._zero;
        result.x = a.x - b.x;
        result.y = a.y - b.y;
        result.z = a.z - b.z;
        return result;
    }
    /// <summary>Subtraction operation between two values.(<seealso cref="Vector3"/> - <seealso cref="Vector3D"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the subtraction.</returns>
    public static Vector3D operator -(Vector3 a, Vector3D b) {
        Vector3D result = Vector3D._zero;
        result.x = a.x - b.x;
        result.y = a.y - b.y;
        result.z = a.z - b.z;
        return result;
    }
    /// <summary>The operator allows us to reverse the value.</summary>
    /// <param name="a">Or value that will be invested.</param>
    /// <returns>Returns the result of the inversion.</returns>
    public static Vector3D operator -(Vector3D a) => a.Neg();
    /// <summary>Division operation between two values.(<seealso cref="Vector3D"/> / <seealso cref="Vector3D"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the division.</returns>
    public static Vector3D operator /(Vector3D a, Vector3D b) {
        Vector3D result = Vector3D._zero;
        result.x = a.x / b.x;
        result.y = a.y / b.y;
        result.z = a.z / b.z;
        return result;
    }
    /// <summary>Division operation between two values.(<seealso cref="Vector3D"/> / <seealso cref="Vector3"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the division.</returns>
    public static Vector3D operator /(Vector3D a, Vector3 b) {
        Vector3D result = Vector3D._zero;
        result.x = a.x / b.x;
        result.y = a.y / b.y;
        result.z = a.z / b.z;
        return result;
    }
    /// <summary>Division operation between two values.(<seealso cref="Vector3"/> / <seealso cref="Vector3D"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the division.</returns>
    public static Vector3D operator /(Vector3 a, Vector3D b) {
        Vector3D result = Vector3D._zero;
        result.x = a.x / b.x;
        result.y = a.y / b.y;
        result.z = a.z / b.z;
        return result;
    }
    /// <summary>Division operation between two values.(<seealso cref="Vector3D"/> / <seealso cref="float"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the division.</returns>
    public static Vector3D operator /(Vector3D a, float b) {
        Vector3D result = Vector3D._zero;
        result.x = a.x / b;
        result.y = a.y / b;
        result.z = a.z / b;
        return result;
    }
    /// <summary>Multiplication operation between two values.(<seealso cref="Vector3D"/> * <seealso cref="Vector3D"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Vector3D operator *(Vector3D a, Vector3D b) {
        Vector3D result = Vector3D._zero;
        result.x = a.x * b.x;
        result.y = a.y * b.y;
        result.z = a.z * b.z;
        return result;
    }
    /// <summary>Multiplication operation between two values.(<seealso cref="Vector3D"/> * <seealso cref="Vector3"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Vector3D operator *(Vector3D a, Vector3 b) {
        Vector3D result = Vector3D._zero;
        result.x = a.x * b.x;
        result.y = a.y * b.y;
        result.z = a.z * b.z;
        return result;
    }
    /// <summary>Multiplication operation between two values.(<seealso cref="Vector3"/> * <seealso cref="Vector3D"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Vector3D operator *(Vector3 a, Vector3D b) {
        Vector3D result = Vector3D._zero;
        result.x = a.x * b.x;
        result.y = a.y * b.y;
        result.z = a.z * b.z;
        return result;
    }
    /// <summary>Multiplication operation between two values.(<seealso cref="Vector3D"/> * <seealso cref="float"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Vector3D operator *(Vector3D a, float b)
    {
        Vector3D result = Vector3D._zero;
        result.x = a.x * b;
        result.y = a.y * b;
        result.z = a.z * b;
        return result;
    }
    /// <summary>Modulo operation between two values.(<seealso cref="Vector3D"/> + <seealso cref="Vector3D"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the module.</returns>
    public static Vector3D operator %(Vector3D a, Vector3D b) {
        Vector3D result = Vector3D._zero;
        result.x = a.x % b.x;
        result.y = a.y % b.y;
        result.z = a.z % b.z;
        return result;
    }
    /// <summary>Modulo operation between two values.(<seealso cref="Vector3D"/> + <seealso cref="Vector3"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the module.</returns>
    public static Vector3D operator %(Vector3D a, Vector3 b) {
        Vector3D result = Vector3D._zero;
        result.x = a.x % b.x;
        result.y = a.y % b.y;
        result.z = a.z % b.z;
        return result;
    }
    /// <summary>Modulo operation between two values.(<seealso cref="Vector3"/> + <seealso cref="Vector3D"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the module.</returns>
    public static Vector3D operator %(Vector3 a, Vector3D b) {
        Vector3D result = Vector3D._zero;
        result.x = a.x % b.x;
        result.y = a.y % b.y;
        result.z = a.z % b.z;
        return result;
    }
    /// <summary>Modulo operation between two values.(<seealso cref="Vector3D"/> + <seealso cref="float"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the module.</returns>
    public static Vector3D operator %(Vector3D a, float b) {
        Vector3D result = Vector3D._zero;
        result.x = a.x % b;
        result.y = a.y % b;
        result.z = a.z % b;
        return result;
    }
    /// <summary>Indicates whether this instance is equal to another instance of the same type.</summary>
    /// <param name="lhs">Object to be compared.</param>
    /// <param name="rhs">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator ==(in Vector3D lhs, in Vector3D rhs) => lhs.Equals(rhs);
    /// <summary>Indicates whether this instance is different from another instance of the same type.</summary>
    /// <param name="lhs">Object to be compared.</param>
    /// <param name="rhs">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator !=(in Vector3D lhs, in Vector3D rhs) => !lhs.Equals(rhs);
    /// <summary>Implicit conversion operator.(<seealso cref="Vector3"/> to <seealso cref="Vector3D"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static implicit operator Vector3D(Vector3 v) => new (v.x, v.y, v.z);
    /// <summary>Implicit conversion operator.(<seealso cref="Vector3D"/> to <seealso cref="Vector3"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static implicit operator Vector3(Vector3D v) => new (v.x, v.y, v.z);
    /// <summary>Implicit conversion operator.(<seealso cref="Vector2"/> to <seealso cref="Vector3D"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static implicit operator Vector3D(Vector2 v) => new (v.x, v.y);
    /// <summary>Implicit conversion operator.(<seealso cref="Vector3D"/> to <seealso cref="Vector2"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static implicit operator Vector2(Vector3D v) => new (v.x, v.y);
    /// <summary>Implicit conversion operator.(<seealso cref="Vector3D"/> to <seealso cref="Vector2D"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static implicit operator Vector2D(Vector3D v) => new (v.x, v.y);
    /// <summary>Implicit conversion operator.(<seealso cref="Vector3D"/> to <seealso cref="Vector4D"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static implicit operator Vector4D(Vector3D v) => new (v.x, v.y);
}