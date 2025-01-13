using Godot;
using System;
using System.Globalization;
using Cobilas.GodotEngine.Utility.EditorSerialization;

namespace Cobilas.GodotEngine.Utility.Numerics;
/// <summary>Represents a two-dimensional vector</summary>
[Serializable]
public struct Vector2D : IVectorGeneric<Vector2D> {
    /// <inheritdoc cref="Vector4D.x"/>
    [ShowProperty(true)] public float x;
    /// <inheritdoc cref="Vector4D.y"/>
    [ShowProperty(true)] public float y;
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
    /// <summary>Shorthand for writing Vector2(0,0).</summary>
    public static Vector2D Zero => _zero;
    /// <summary>Shorthand for writing Vector2(1f,1f).</summary>
    public static Vector2D One => _one;
    /// <summary>Shorthand for writing Vector2(0,-1f).</summary>
    public static Vector2D Up => _up;
    /// <summary>Shorthand for writing Vector2(0,1f).</summary>
    public static Vector2D Down => _down;
    /// <summary>Shorthand for writing Vector2(1f,0).</summary>
    public static Vector2D Right => _right;
    /// <summary>Shorthand for writing Vector2(-1f,0).</summary>
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
        => string.Format(formatProvider, format ?? "(x:{0:N3} y:{1:N3})", this.x, this.y);
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
    /// <inheritdoc cref="Vector4D.Abs(bool, bool, bool, bool)"/>
    public readonly Vector2D Abs(bool absX = true, bool absY = true) {
        Vector2D abs = Abs(this);
        abs[0] = absX ? abs[0] : this[0];
        abs[1] = absY ? abs[1] : this[1];
        return abs;
    }
    /// <inheritdoc cref="Vector4D.Neg(bool, bool, bool, bool)"/>
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
    /// <inheritdoc cref="Vector4D.Magnitude(in Vector4D)"/>
    public static float Magnitude(in Vector2D a) => Mathf.Sqrt(SqrMagnitude(a));
    /// <inheritdoc cref="Vector4D.Distance(in Vector4D, Vector4D)"/>
    public static float Distance(in Vector2D a, in Vector2D b) => Magnitude(a - b);
    /// <inheritdoc cref="Vector4D.Round(in Vector4D)"/>
    public static Vector2D Round(in Vector2D a) => new(Mathf.Round(a.x), Mathf.Round(a.y));
    /// <inheritdoc cref="Vector3D.Cross(in Vector3D, in Vector3D)"/>
    public static float Cross(in Vector2D lhs, in Vector2D rhs) => lhs.x * rhs.y - lhs.y * rhs.x;
    /// <inheritdoc cref="Vector4D.SqrMagnitude(in Vector4D)"/>
    public static float SqrMagnitude(in Vector2D a) => (float)(a.x * (double)a.x + a.y * (double)a.y);
    /// <inheritdoc cref="Vector4D.Dot(in Vector4D, in Vector4D)"/>
    public static float Dot(in Vector2D lhs, in Vector2D rhs) => (float) (lhs.x * (double)rhs.x + lhs.y * (double)rhs.y);
    /// <inheritdoc cref="Vector4D.Floor(in Vector4D)"/>
    public static Vector2D Floor(in Vector2D a) {
        Vector2D result = a;
        result[0] = Mathf.Floor(result[0]);
        result[1] = Mathf.Floor(result[1]);
        return result;
    }
    /// <inheritdoc cref="Vector4D.Ceil(in Vector4D)"/>
    public static Vector2D Ceil(in Vector2D a) {
        Vector2D result = a;
        result[0] = Mathf.Ceil(result[0]);
        result[1] = Mathf.Ceil(result[1]);
        return result;
    }
    /// <inheritdoc cref="Vector3D.AngleTo(in Vector2D, in Vector2D)"/>
    public static float AngleTo(in Vector2D lhs, in Vector2D rhs)
        => Mathf.Atan2(Cross(lhs, rhs), Dot(lhs, rhs));
    /// <summary>Returns the angle between the line connecting the two points and the X axis, in radians.</summary>
    /// <param name="lhs">One of the values.</param>
    /// <param name="rhs">The other value.</param>
    /// <returns>The angle between the two vectors, in radians.</returns>
    public static float AngleToPoint(in Vector2D lhs, in Vector2D rhs)
        => Mathf.Atan2(lhs.y - rhs.y, lhs.x - rhs.x);
    /// <inheritdoc cref="IVector.aspect"/>
    /// <param name="a">The vector whose aspect will be calculated.</param>
    public static float Aspect(in Vector2D a) => a.x / a.y;
    /// <inheritdoc cref="Vector4D.Normalize(in Vector4D)"/>
    public static Vector2D Normalize(in Vector2D a) {
        float num = Vector2D.Magnitude(a);
        return (double)num > 9.99999974737875E-06 ? a / num : Vector2D.Zero;
    }
    /// <inheritdoc cref="Vector4D.Neg(in Vector4D)"/>
    public static Vector2D Neg(in Vector2D a) => new(-a[0], -a[1]);
    /// <inheritdoc cref="Vector4D.Abs(in Vector4D)"/>
    public static Vector2D Abs(in Vector2D a) => new(Mathf.Abs(a[0]), Mathf.Abs(a[1]));
    /// <inheritdoc cref="Vector4D.Min(in Vector4D, in Vector4D)"/>
    public static Vector2D Min(Vector2D lhs, Vector2D rhs) => new(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y));
    /// <inheritdoc cref="Vector4D.Max(in Vector4D, in Vector4D)"/>
    public static Vector2D Max(Vector2D lhs, Vector2D rhs) => new(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y));
    #endregion
    #region op Addition
    /// <summary>Addition operation between two values.(<seealso cref="Vector2D"/> + <seealso cref="Vector2D"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the addition.</returns>
    public static Vector2D operator +(Vector2D a, Vector2D b) {
        Vector2D result = Vector2D._zero;
        result.x = a.x + b.x;
        result.y = a.y + b.y;
        return result;
    }
    /// <summary>Addition operation between two values.(<seealso cref="Vector2D"/> + <seealso cref="Vector2"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the addition.</returns>
    public static Vector2D operator +(Vector2D a, Vector2 b) {
        Vector2D result = Vector2D._zero;
        result.x = a.x + b.x;
        result.y = a.y + b.y;
        return result;
    }
    /// <summary>Addition operation between two values.(<seealso cref="Vector2"/> + <seealso cref="Vector2D"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the addition.</returns>
    public static Vector2D operator +(Vector2 a, Vector2D b) {
        Vector2D result = Vector2D._zero;
        result.x = a.x + b.x;
        result.y = a.y + b.y;
        return result;
    }
    #endregion
    #region op Subtraction
    /// <summary>Subtraction operation between two values.(<seealso cref="Vector2D"/> - <seealso cref="Vector2D"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the subtraction.</returns>
    public static Vector2D operator -(Vector2D a, Vector2D b) {
        Vector2D result = Vector2D._zero;
        result.x = a.x - b.x;
        result.y = a.y - b.y;
        return result;
    }
    /// <summary>Subtraction operation between two values.(<seealso cref="Vector2D"/> - <seealso cref="Vector2"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the subtraction.</returns>
    public static Vector2D operator -(Vector2D a, Vector2 b) {
        Vector2D result = Vector2D._zero;
        result.x = a.x - b.x;
        result.y = a.y - b.y;
        return result;
    }
    /// <summary>Subtraction operation between two values.(<seealso cref="Vector2"/> - <seealso cref="Vector2D"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the subtraction.</returns>
    public static Vector2D operator -(Vector2 a, Vector2D b) {
        Vector2D result = Vector2D._zero;
        result.x = a.x - b.x;
        result.y = a.y - b.y;
        return result;
    }
    /// <summary>The operator allows us to reverse the value.</summary>
    /// <param name="a">Or value that will be invested.</param>
    /// <returns>Returns the result of the inversion.</returns>
    public static Vector2D operator -(Vector2D a) => a.Neg();
    #endregion
    #region op Division
    /// <summary>Division operation between two values.(<seealso cref="Vector2D"/> / <seealso cref="Vector2D"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the division.</returns>
    public static Vector2D operator /(Vector2D a, Vector2D b) {
        Vector2D result = Vector2D._zero;
        result.x = a.x / b.x;
        result.y = a.y / b.y;
        return result;
    }
    /// <summary>Division operation between two values.(<seealso cref="Vector2D"/> / <seealso cref="Vector2"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the division.</returns>
    public static Vector2D operator /(Vector2D a, Vector2 b) {
        Vector2D result = Vector2D._zero;
        result.x = a.x / b.x;
        result.y = a.y / b.y;
        return result;
    }
    /// <summary>Division operation between two values.(<seealso cref="Vector2"/> / <seealso cref="Vector2D"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the division.</returns>
    public static Vector2D operator /(Vector2 a, Vector2D b) {
        Vector2D result = Vector2D._zero;
        result.x = a.x / b.x;
        result.y = a.y / b.y;
        return result;
    }
    /// <summary>Division operation between two values.(<seealso cref="Vector2D"/> / <seealso cref="float"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the division.</returns>
    public static Vector2D operator /(Vector2D a, float b) {
        Vector2D result = Vector2D._zero;
        result.x = a.x / b;
        result.y = a.y / b;
        return result;
    }
    #endregion
    #region op Multiplication
    /// <summary>Multiplication operation between two values.(<seealso cref="Vector2D"/> * <seealso cref="Vector2D"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Vector2D operator *(Vector2D a, Vector2D b) {
        Vector2D result = Vector2D._zero;
        result.x = a.x * b.x;
        result.y = a.y * b.y;
        return result;
    }
    /// <summary>Multiplication operation between two values.(<seealso cref="Vector2D"/> * <seealso cref="Vector2"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Vector2D operator *(Vector2D a, Vector2 b) {
        Vector2D result = Vector2D._zero;
        result.x = a.x * b.x;
        result.y = a.y * b.y;
        return result;
    }
    /// <summary>Multiplication operation between two values.(<seealso cref="Vector2"/> * <seealso cref="Vector2D"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Vector2D operator *(Vector2 a, Vector2D b) {
        Vector2D result = Vector2D._zero;
        result.x = a.x * b.x;
        result.y = a.y * b.y;
        return result;
    }
    /// <summary>Multiplication operation between two values.(<seealso cref="Vector2D"/> * <seealso cref="float"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Vector2D operator *(Vector2D a, float b) {
        Vector2D result = Vector2D._zero;
        result.x = a.x * b;
        result.y = a.y * b;
        return result;
    }
    #endregion
    /// <summary>Indicates whether this instance is equal to another instance of the same type.</summary>
    /// <param name="lhs">Object to be compared.</param>
    /// <param name="rhs">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator ==(in Vector2D lhs, in Vector2D rhs) => lhs.Equals(rhs);
    /// <summary>Indicates whether this instance is different from another instance of the same type.</summary>
    /// <param name="lhs">Object to be compared.</param>
    /// <param name="rhs">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator !=(in Vector2D lhs, in Vector2D rhs) => !lhs.Equals(rhs);
    /// <summary>Implicit conversion operator.(<seealso cref="Vector2"/> to <seealso cref="Vector2D"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static implicit operator Vector2D(Vector2 v) => new (v.x, v.y);
    /// <summary>Implicit conversion operator.(<seealso cref="Vector2D"/> to <seealso cref="Vector2"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static implicit operator Vector2(Vector2D v) => new (v.x, v.y);
    /// <summary>Implicit conversion operator.(<seealso cref="Vector3"/> to <seealso cref="Vector2D"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static implicit operator Vector2D(Vector3 v) => new (v.x, v.y);
    /// <summary>Implicit conversion operator.(<seealso cref="Vector2D"/> to <seealso cref="Vector3"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static implicit operator Vector3(Vector2D v) => new (v.x, v.y, 0f);
    /// <summary>Implicit conversion operator.(<seealso cref="Vector2D"/> to <seealso cref="Vector3D"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static implicit operator Vector3D(Vector2D v) => new (v.x, v.y);
    /// <summary>Implicit conversion operator.(<seealso cref="Vector2D"/> to <seealso cref="Vector4D"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static implicit operator Vector4D(Vector2D v) => new (v.x, v.y);
}