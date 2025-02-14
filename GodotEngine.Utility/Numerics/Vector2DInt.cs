using Godot;
using System;
using System.Globalization;
using Cobilas.GodotEditor.Utility.Serialization;

namespace Cobilas.GodotEngine.Utility.Numerics;
/// <summary>Representation of a two-dimensional vector using integers.</summary>
[Serializable]
public struct Vector2DInt : IIntVector {
    /// <inheritdoc cref="Vector4D.x"/>
    [ShowProperty(true)] public int x;
    /// <inheritdoc cref="Vector4D.y"/>
    [ShowProperty(true)] public int y;
    /// <inheritdoc/>
    public readonly int AxisCount => 2;
    /// <inheritdoc/>
    public readonly float aspect => Aspect(this);
    /// <inheritdoc/>
    public readonly float magnitude => Magnitude(this);
    /// <inheritdoc/>
    public readonly int sqrMagnitude => SqrMagnitude(this);

    private static readonly Vector2DInt _zero = new(0, 0);
    private static readonly Vector2DInt _one = new(1, 1);
    private static readonly Vector2DInt _up = new(0, -1);
    private static readonly Vector2DInt _down = new(0, 1);
    private static readonly Vector2DInt _right = new(1, 0);
    private static readonly Vector2DInt _left = new(-1, 0);
    /// <summary>Shorthand for writing Vector2(0,0).</summary>
    public static Vector2DInt Zero => _zero;
    /// <summary>Shorthand for writing Vector2(1,1).</summary>
    public static Vector2DInt One => _one;
    /// <summary>Shorthand for writing Vector2(0,-1).</summary>
    public static Vector2DInt Up => _up;
    /// <summary>Shorthand for writing Vector2(0,1).</summary>
    public static Vector2DInt Down => _down;
    /// <summary>Shorthand for writing Vector2(1,0).</summary>
    public static Vector2DInt Right => _right;
    /// <summary>Shorthand for writing Vector2(-1,0).</summary>
    public static Vector2DInt Left => _left;
    /// <inheritdoc/>
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
    /// <summary>Starts a new instance of the object.</summary>
    public Vector2DInt(in int x, in int y) {
        this.x = x;
        this.y = y;
    }
    /// <summary>Starts a new instance of the object.</summary>
    public Vector2DInt(in Vector2DInt vector) : this(vector.x, vector.y) {}
    /// <inheritdoc/>
    public readonly bool Equals(Vector2DInt other) => other.x == this.x && other.y == this.y;
    /// <inheritdoc/>
    public override readonly bool Equals(object obj)
        => obj is Vector2DInt vector && Equals(vector);
    /// <inheritdoc/>
    public override readonly int GetHashCode() => x.GetHashCode() ^ y.GetHashCode();
    /// <inheritdoc/>
    public readonly string ToString(string format, IFormatProvider formatProvider)
        => string.Format(formatProvider, format ?? "(x:{0} y:{1})", this.x, this.y);
    /// <inheritdoc/>
    public readonly string ToString(string format)
        => ToString(format, CultureInfo.InvariantCulture);
    /// <inheritdoc/>
    public override readonly string ToString() => ToString("(x:{0} y:{1})");
    /// <inheritdoc cref="Vector4D.Abs(bool, bool, bool, bool)"/>
    public readonly Vector2DInt Abs(bool absX = true, bool absY = true) {
        Vector2DInt abs = Abs(this);
        abs[0] = absX ? abs[0] : this[0];
        abs[1] = absY ? abs[1] : this[1];
        return abs;
    }
    /// <inheritdoc cref="Vector4D.Neg(bool, bool, bool, bool)"/>
    public readonly Vector2DInt Neg(bool negX = true, bool negY = true) {
        Vector2DInt neg = Neg(this);
        neg[0] = negX ? neg[0] : this[0];
        neg[1] = negY ? neg[1] : this[1];
        return neg;
    }
    /// <inheritdoc cref="Vector2D.Aspect(in Vector2D)"/>
    public static float Aspect(in Vector2DInt a) => a.x / a.y;
    /// <inheritdoc cref="Vector4D.Neg(in Vector4D)"/>
    public static Vector2DInt Neg(in Vector2DInt a) => new(-a[0], -a[1]);
    /// <inheritdoc cref="Vector4D.SqrMagnitude(in Vector4D)"/>
    public static int SqrMagnitude(in Vector2DInt a) => a.x * a.x + a.y * a.y;
    /// <inheritdoc cref="Vector4D.Magnitude(in Vector4D)"/>
    public static float Magnitude(in Vector2DInt a) => Mathf.Sqrt(SqrMagnitude(a));
    /// <inheritdoc cref="Vector4D.Distance(in Vector4D, Vector4D)"/>
    public static float Distance(in Vector2DInt a, in Vector2DInt b) => Magnitude(a - b);
    /// <inheritdoc cref="Vector4D.Abs(in Vector4D)"/>
    public static Vector2DInt Abs(in Vector2DInt a)  => new(Mathf.Abs(a[0]), Mathf.Abs(a[1]));
    /// <inheritdoc cref="Vector3DInt.RoundToInt(in Vector3D)"/>
    public static Vector2DInt RoundToInt(in Vector2D a) => new(Mathf.RoundToInt(a.x), Mathf.RoundToInt(a.y));
    /// <inheritdoc cref="Vector4D.Min(in Vector4D, in Vector4D)"/>
    public static Vector2DInt Min(Vector2DInt lhs, Vector2DInt rhs) => new(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y));
    /// <inheritdoc cref="Vector4D.Max(in Vector4D, in Vector4D)"/>
    public static Vector2DInt Max(Vector2DInt lhs, Vector2DInt rhs) => new(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y));
    /// <inheritdoc cref="Vector3DInt.FloorToInt(in Vector3D)"/>
    public static Vector2DInt FloorToInt(in Vector2D a) {
        Vector2DInt result = Vector2DInt._zero;
        result[0] = Mathf.FloorToInt(result[0]);
        result[1] = Mathf.FloorToInt(result[1]);
        return result;
    }
    /// <inheritdoc cref="Vector3DInt.CeilToInt(in Vector3D)"/>
    public static Vector2DInt CeilToInt(in Vector2D a) {
        Vector2DInt result = Vector2DInt._zero;
        result[0] = Mathf.CeilToInt(result[0]);
        result[1] = Mathf.CeilToInt(result[1]);
        return result;
    }
    /// <summary>Addition operation between two values.(<seealso cref="Vector2DInt"/> + <seealso cref="Vector2DInt"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the addition.</returns>
    public static Vector2DInt operator +(Vector2DInt a, Vector2DInt b) {
        Vector2DInt result = Vector2DInt._zero;
        result.x = a.x + b.x;
        result.y = a.y + b.y;
        return result;
    }
    /// <summary>Subtraction operation between two values.(<seealso cref="Vector2DInt"/> - <seealso cref="Vector2DInt"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the subtraction.</returns>
    public static Vector2DInt operator -(Vector2DInt a, Vector2DInt b) {
        Vector2DInt result = Vector2DInt._zero;
        result.x = a.x - b.x;
        result.y = a.y - b.y;
        return result;
    }
    /// <summary>The operator allows us to reverse the value.</summary>
    /// <param name="a">Or value that will be invested.</param>
    /// <returns>Returns the result of the inversion.</returns>
    public static Vector2DInt operator -(Vector2DInt a) => a.Neg();
    /// <summary>Division operation between two values.(<seealso cref="Vector2DInt"/> / <seealso cref="Vector2DInt"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the division.</returns>
    public static Vector2DInt operator /(Vector2DInt a, Vector2DInt b) {
        Vector2DInt result = Vector2DInt._zero;
        result.x = a.x / b.x;
        result.y = a.y / b.y;
        return result;
    }
    /// <summary>Multiplication operation between two values.(<seealso cref="Vector2DInt"/> * <seealso cref="Vector2DInt"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Vector2DInt operator *(Vector2DInt a, Vector2DInt b) {
        Vector2DInt result = Vector2DInt._zero;
        result.x = a.x * b.x;
        result.y = a.y * b.y;
        return result;
    }
    /// <summary>Division operation between two values.(<seealso cref="Vector2DInt"/> / <seealso cref="int"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the division.</returns>
    public static Vector2DInt operator /(Vector2DInt a, int b) {
        Vector2DInt result = Vector2DInt._zero;
        result.x = a.x / b;
        result.y = a.y / b;
        return result;
    }
    /// <summary>Multiplication operation between two values.(<seealso cref="Vector2DInt"/> * <seealso cref="int"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Vector2DInt operator *(Vector2DInt a, int b) {
        Vector2DInt result = Vector2DInt._zero;
        result.x = a.x * b;
        result.y = a.y * b;
        return result;
    }
    /// <summary>Indicates whether this instance is equal to another instance of the same type.</summary>
    /// <param name="lhs">Object to be compared.</param>
    /// <param name="rhs">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator ==(in Vector2DInt lhs, in Vector2DInt rhs) => lhs.Equals(rhs);
    /// <summary>Indicates whether this instance is different from another instance of the same type.</summary>
    /// <param name="lhs">Object to be compared.</param>
    /// <param name="rhs">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator !=(in Vector2DInt lhs, in Vector2DInt rhs) => !lhs.Equals(rhs);
    /// <summary>Implicit conversion operator.(<seealso cref="Vector2DInt"/> to <seealso cref="Vector3DInt"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static implicit operator Vector3DInt(Vector2DInt v) => new(v.x, v.y);
    /// <summary>Implicit conversion operator.(<seealso cref="Vector2DInt"/> to <seealso cref="Vector2D"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static implicit operator Vector2D(Vector2DInt v) => new(v.x, v.y);
    /// <summary>Implicit conversion operator.(<seealso cref="Vector2DInt"/> to <seealso cref="Vector2"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static implicit operator Vector2(Vector2DInt v) => new(v.x, v.y);
    /// <summary>Implicit conversion operator.(<seealso cref="Vector2DInt"/> to <seealso cref="Vector3D"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static implicit operator Vector3D(Vector2DInt v) => new(v.x, v.y);
    /// <summary>Implicit conversion operator.(<seealso cref="Vector2DInt"/> to <seealso cref="Vector4D"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static implicit operator Vector4D(Vector2DInt v) => new(v.x, v.y);
    /// <summary>Explicit conversion operator.(<seealso cref="Vector2"/> to <seealso cref="Vector2DInt"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static explicit operator Vector2DInt(Vector2 v) => new((int)v.x, (int)v.y);
    /// <summary>Explicit conversion operator.(<seealso cref="Vector2D"/> to <seealso cref="Vector2DInt"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static explicit operator Vector2DInt(Vector2D v) => new((int)v.x, (int)v.y);
    /// <summary>Explicit conversion operator.(<seealso cref="Vector3D"/> to <seealso cref="Vector2DInt"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static explicit operator Vector2DInt(Vector3D v) => new((int)v.x, (int)v.y);
    /// <summary>Explicit conversion operator.(<seealso cref="Vector4D"/> to <seealso cref="Vector2DInt"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static explicit operator Vector2DInt(Vector4D v) => new((int)v.x, (int)v.y);
}