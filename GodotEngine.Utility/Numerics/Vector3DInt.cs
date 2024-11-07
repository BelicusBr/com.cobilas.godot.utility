using Godot;
using System;
using System.Globalization;

namespace Cobilas.GodotEngine.Utility.Numerics;
/// <summary>Representation of a three-dimensional vector using integers.</summary>
[Serializable]
public struct Vector3DInt : IIntVector {
    /// <inheritdoc cref="Vector4D.x"/>
    public int x;
    /// <inheritdoc cref="Vector4D.y"/>
    public int y;
    /// <inheritdoc cref="Vector4D.z"/>
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
    /// <summary>Shorthand for writing Vector3(0,0,0).</summary>
    public static Vector3DInt Zero => _zero;
    /// <summary>Shorthand for writing Vector3(1,1,1).</summary>
    public static Vector3DInt One => _one;
    /// <summary>Shorthand for writing Vector3(0,-1,0).</summary>
    public static Vector3DInt Up => _up;
    /// <summary>Shorthand for writing Vector3(0,1,0).</summary>
    public static Vector3DInt Down => _down;
    /// <summary>Shorthand for writing Vector3(1,0,0).</summary>
    public static Vector3DInt Right => _right;
    /// <summary>Shorthand for writing Vector3(-1,0,0).</summary>
    public static Vector3DInt Left => _left;
    /// <summary>Shorthand for writing Vector3(0,0,1).</summary>
    public static Vector3DInt Forward => _forward;
    /// <summary>Shorthand for writing Vector3(0,0,-1).</summary>
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
    /// <inheritdoc cref="Vector4D.Abs(bool, bool, bool, bool)"/>
    public readonly Vector3DInt Abs(bool absX = true, bool absY = true, bool absZ = true) {
        Vector3DInt abs = Abs(this);
        abs[0] = absX ? abs[0] : this[0];
        abs[1] = absY ? abs[1] : this[1];
        abs[2] = absZ ? abs[2] : this[2];
        return abs;
    }
    /// <inheritdoc cref="Vector4D.Neg(bool, bool, bool, bool)"/>
    public readonly Vector3DInt Neg(bool negX = true, bool negY = true, bool negZ = true) {
        Vector3DInt neg = Neg(this);
        neg[0] = negX ? neg[0] : this[0];
        neg[1] = negY ? neg[1] : this[1];
        neg[2] = negZ ? neg[2] : this[2];
        return neg;
    }
    /// <inheritdoc cref="Vector4D.Neg(in Vector4D)"/>
    public static Vector3DInt Neg(in Vector3DInt a) => new(-a[0], -a[1], -a[2]);
    /// <inheritdoc cref="Vector4D.Magnitude(in Vector4D)"/>
    public static float Magnitude(in Vector3DInt a) => Mathf.Sqrt(SqrMagnitude(a));
    /// <inheritdoc cref="Vector4D.Distance(in Vector4D, Vector4D)"/>
    public static float Distance(in Vector3DInt a, in Vector3DInt b) => Magnitude(a - b);
    /// <inheritdoc cref="Vector4D.SqrMagnitude(in Vector4D)"/>
    public static int SqrMagnitude(in Vector3DInt a) => a.x * a.x + a.y * a.y + a.z * a.z;
    /// <inheritdoc cref="Vector4D.Abs(in Vector4D)"/>
    public static Vector3DInt Abs(in Vector3DInt a)  => new(Mathf.Abs(a[0]), Mathf.Abs(a[1]), Mathf.Abs(a[2]));
    /// <summary>Converts a floating-point vector to an integer vector by performing a Round for each value.</summary>
    /// <param name="a">The vector that will be converted and rounded</param>
    /// <returns>Returns the converted and rounded vector</returns>
    public static Vector3DInt RoundToInt(in Vector3D a) => new(Mathf.RoundToInt(a.x), Mathf.RoundToInt(a.y), Mathf.RoundToInt(a.z));
    /// <inheritdoc cref="Vector4D.Min(in Vector4D, in Vector4D)"/>
    public static Vector3DInt Min(Vector3DInt lhs, Vector3DInt rhs) => new(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z));
    /// <inheritdoc cref="Vector4D.Max(in Vector4D, in Vector4D)"/>
    public static Vector3DInt Max(Vector3DInt lhs, Vector3DInt rhs) => new(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z));
    /// <summary>Converts a floating-point vector to an integer vector and applies a Floor to each value.</summary>
    /// <param name="a">The vector to be converted is floor</param>
    /// <returns>Returns the converted vector and floor</returns>
    public static Vector3DInt FloorToInt(in Vector3D a) {
        Vector3DInt result = Vector3DInt._zero;
        result[0] = Mathf.FloorToInt(result[0]);
        result[1] = Mathf.FloorToInt(result[1]);
        result[2] = Mathf.FloorToInt(result[2]);
        return result;
    }
    /// <summary>Converts a floating-point vector to an integer vector and applies a Ceiling to each value.</summary>
    /// <param name="a">The vector to be converted is ceiling</param>
    /// <returns>Returns the converted vector and ceiling</returns>
    public static Vector3DInt CeilToInt(in Vector3D a) {
        Vector3DInt result = Vector3DInt._zero;
        result[0] = Mathf.CeilToInt(result[0]);
        result[1] = Mathf.CeilToInt(result[1]);
        result[2] = Mathf.CeilToInt(result[2]);
        return result;
    }
    /// <summary>Addition operation between two values.(<seealso cref="Vector3DInt"/> + <seealso cref="Vector3DInt"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the addition.</returns>
    public static Vector3DInt operator +(Vector3DInt a, Vector3DInt b) {
        Vector3DInt result = Vector3DInt._zero;
        result.x = a.x + b.x;
        result.y = a.y + b.y;
        result.z = a.z + b.z;
        return result;
    }
    /// <summary>Subtraction operation between two values.(<seealso cref="Vector3DInt"/> - <seealso cref="Vector3DInt"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the subtraction.</returns>
    public static Vector3DInt operator -(Vector3DInt a, Vector3DInt b) {
        Vector3DInt result = Vector3DInt._zero;
        result.x = a.x - b.x;
        result.y = a.y - b.y;
        result.z = a.z - b.z;
        return result;
    }
    /// <summary>Division operation between two values.(<seealso cref="Vector3DInt"/> / <seealso cref="Vector3DInt"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the division.</returns>
    public static Vector3DInt operator /(Vector3DInt a, Vector3DInt b) {
        Vector3DInt result = Vector3DInt._zero;
        result.x = a.x / b.x;
        result.y = a.y / b.y;
        result.z = a.z / b.z;
        return result;
    }
    /// <summary>Multiplication operation between two values.(<seealso cref="Vector3DInt"/> * <seealso cref="Vector3DInt"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Vector3DInt operator *(Vector3DInt a, Vector3DInt b) {
        Vector3DInt result = Vector3DInt._zero;
        result.x = a.x * b.x;
        result.y = a.y * b.y;
        result.z = a.z * b.z;
        return result;
    }
    /// <summary>Division operation between two values.(<seealso cref="Vector3DInt"/> / <seealso cref="int"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the division.</returns>
    public static Vector3DInt operator /(Vector3DInt a, int b) {
        Vector3DInt result = Vector3DInt._zero;
        result.x = a.x / b;
        result.y = a.y / b;
        result.z = a.z / b;
        return result;
    }
    /// <summary>Multiplication operation between two values.(<seealso cref="Vector3DInt"/> * <seealso cref="int"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Vector3DInt operator *(Vector3DInt a, int b) {
        Vector3DInt result = Vector3DInt._zero;
        result.x = a.x * b;
        result.y = a.y * b;
        result.z = a.z * b;
        return result;
    }
    /// <summary>Indicates whether this instance is equal to another instance of the same type.</summary>
    /// <param name="lhs">Object to be compared.</param>
    /// <param name="rhs">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator ==(in Vector3DInt lhs, in Vector3DInt rhs) => lhs.Equals(rhs);
    /// <summary>Indicates whether this instance is different from another instance of the same type.</summary>
    /// <param name="lhs">Object to be compared.</param>
    /// <param name="rhs">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator !=(in Vector3DInt lhs, in Vector3DInt rhs) => !lhs.Equals(rhs);
    /// <summary>Implicit conversion operator.(<seealso cref="Vector3DInt"/> to <seealso cref="Vector2DInt"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static implicit operator Vector2DInt(Vector3DInt v) => new(v.x, v.y);
    /// <summary>Implicit conversion operator.(<seealso cref="Vector3DInt"/> to <seealso cref="Vector2"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static implicit operator Vector2(Vector3DInt v) => new(v.x, v.y);
    /// <summary>Implicit conversion operator.(<seealso cref="Vector3DInt"/> to <seealso cref="Vector2D"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static implicit operator Vector2D(Vector3DInt v) => new(v.x, v.y);
    /// <summary>Implicit conversion operator.(<seealso cref="Vector3DInt"/> to <seealso cref="Vector3D"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static implicit operator Vector3D(Vector3DInt v) => new(v.x, v.y, v.z);
    /// <summary>Implicit conversion operator.(<seealso cref="Vector3DInt"/> to <seealso cref="Vector4D"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static implicit operator Vector4D(Vector3DInt v) => new(v.x, v.y, v.z);
    /// <summary>Explicit conversion operator.(<seealso cref="Vector2D"/> to <seealso cref="Vector3DInt"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static explicit operator Vector3DInt(Vector2D v) => new((int)v.x, (int)v.y);
    /// <summary>Explicit conversion operator.(<seealso cref="Vector2"/> to <seealso cref="Vector3DInt"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static explicit operator Vector3DInt(Vector2 v) => new((int)v.x, (int)v.y);
    /// <summary>Explicit conversion operator.(<seealso cref="Vector3D"/> to <seealso cref="Vector3DInt"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static explicit operator Vector3DInt(Vector3D v) => new((int)v.x, (int)v.y, (int)v.z);
    /// <summary>Explicit conversion operator.(<seealso cref="Vector4D"/> to <seealso cref="Vector3DInt"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static explicit operator Vector3DInt(Vector4D v) => new((int)v.x, (int)v.y, (int)v.z);
}