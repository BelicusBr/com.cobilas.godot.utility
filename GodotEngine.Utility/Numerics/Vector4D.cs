using Godot;
using System;
using System.Globalization;
using Cobilas.GodotEditor.Utility.Serialization;

namespace Cobilas.GodotEngine.Utility.Numerics;
/// <summary>Represents a four-dimensional vector.(Four-axis vector)</summary>
[Serializable]
public struct Vector4D : IVectorGeneric<Vector4D> {
    /// <summary>X component of the vector.</summary>
    [ShowProperty(true)] public float x;
    /// <summary>Y component of the vector.</summary>
    [ShowProperty(true)] public float y;
    /// <summary>Z component of the vector.</summary>
    [ShowProperty(true)] public float z;
    /// <summary>W component of the vector.</summary>
    [ShowProperty(true)] public float w;

    private static readonly Vector4D _zero = new(0f, 0f);
    private static readonly Vector4D _one = new(1f, 1f, 1f, 1f);
    /// <summary>Shorthand for writing Vector4(0,0,0,0).</summary>
    public static Vector4D Zero => _zero;
    /// <summary>Shorthand for writing Vector4(1,1,1,1).</summary>
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
    /// <summary>Returns an absolute value of the vector.</summary>
    /// <param name="absX">The X axis becomes absolute.</param>
    /// <param name="absY">The Y axis becomes absolute.</param>
    /// <param name="absZ">The Z axis becomes absolute.</param>
    /// <param name="absW">The W axis becomes absolute.</param>
    /// <returns>Returns the vector with its absolute value axes.</returns>
    public readonly Vector4D Abs(bool absX = true, bool absY = true, bool absZ = true, bool absW = true) {
        Vector4D abs = Abs(this);
        abs[0] = absX ? abs[0] : this[0];
        abs[1] = absY ? abs[1] : this[1];
        abs[2] = absZ ? abs[2] : this[2];
        abs[3] = absW ? abs[3] : this[3];
        return abs;
    }
    /// <summary>Inverts the values ​​of a vector.</summary>
    /// <param name="negX">The X axis becomes inverted.</param>
    /// <param name="negY">The Y axis becomes inverted.</param>
    /// <param name="negZ">The Z axis becomes inverted.</param>
    /// <param name="negW">The W axis becomes inverted.</param>
    /// <returns>Returns the axes of an inverted vector.</returns>
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
        => string.Format(formatProvider, format ?? "(x:{0:N3} y:{1:N3} z:{2:N3} w:{3:N3})", this.x, this.y, this.z, this.w);
    /// <inheritdoc/>
    public readonly string ToString(string format) => ToString(format, CultureInfo.InvariantCulture);
    /// <inheritdoc/>
    public override readonly string ToString() => ToString("(x:{0:N3} y:{1:N3} z:{2:N3} w:{3:N3})");
    /// <inheritdoc/>
    public override readonly bool Equals(object obj)
        => obj is Vector3D other && Equals(other);
    /// <inheritdoc/>
    public override readonly int GetHashCode() => x.GetHashCode() ^ y.GetHashCode() << 2 ^ z.GetHashCode() >> 2 ^ w.GetHashCode();
    /// <inheritdoc/>
    public readonly Vector4D Round() => Round(this);
    readonly IVector IVector.Round() => Round(this);
    /// <summary>Returns the squared length of this vector</summary>
    /// <param name="a">One of the values.</param>
    /// <returns>square length of the vector.</returns>
    public static float SqrMagnitude(in Vector4D a) => Vector4D.Dot(a, a);
    /// <inheritdoc cref="Neg(bool, bool, bool, bool)"/>
    /// <param name="a">The vector to be inverted.</param>
    public static Vector4D Neg(in Vector4D a) => new(-a.x, -a.y, -a.z, -a.w);
    /// <summary>Returns the distance between a and b.</summary>
    /// <param name="a">One of the values.</param>
    /// <param name="b">The other value.</param>
    /// <returns>The distance between two vectors.</returns>
    public static float Distance(in Vector4D a, Vector4D b) => Vector4D.Magnitude(a - b);
    /// <summary>Returns the length of this vector</summary>
    /// <param name="a">One of the values.</param>
    /// <returns>vector length.</returns>
    public static float Magnitude(in Vector4D a) => (float)Math.Sqrt((double)Vector4D.Dot(a, a));
    /// <summary>Checks if the vector is normalized.</summary>
    /// <param name="a">The vector to be checked.</param>
    /// <returns>Returns <c>true</c> when vector is normalized.</returns>
    public static bool IsNormalized(in IVector a) => Mathf.Abs(a.sqrMagnitude - 1f) < Quaternion.KEpsilon;
    /// <inheritdoc cref="Abs(bool, bool, bool, bool)"/>
    /// <param name="a">The vector to become absolute.</param>
    public static Vector4D Abs(in Vector4D a) => new(Mathf.Abs(a.x), Mathf.Abs(a.y), Mathf.Abs(a.z), Mathf.Abs(a.w));
    /// <inheritdoc cref="IVector.Round"/>
    /// <param name="a">the vector to be rounded</param>
    public static Vector4D Round(in Vector4D a) => new(Mathf.Round(a.x), Mathf.Round(a.y), Mathf.Round(a.z), Mathf.Round(a.w));
    /// <summary>Dot Product of two vectors.</summary>
    /// <param name="a">One of the values.</param>
    /// <param name="b">The other value.</param>
    /// <returns>returns the result of the dot product.</returns>
    public static float Dot(in Vector4D a, in Vector4D b) => (float)(a.x * (double)b.x + a.y * (double)b.y + a.z * (double)b.z + a.w * (double)b.w);
    /// <summary>Makes this vector have a magnitude of 1.</summary>
    /// <param name="a">The vector to be normalized.</param>
    /// <returns>Returns the already normalized vector.</returns>
    public static Vector4D Normalize(in Vector4D a) {
        float num = Vector4D.Magnitude(a);
        return (double)num > 9.99999974737875E-06 ? a / num : Vector4D.Zero;
    }
    /// <summary>Returns a vector that is made from the smallest components of two vectors.</summary>
    /// <param name="lhs">One of the values.</param>
    /// <param name="rhs">The other value.</param>
    /// <returns>Whichever of the two values is lower.</returns>
    public static Vector4D Min(in Vector4D lhs, in Vector4D rhs) => new(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z), Mathf.Min(lhs.w, rhs.w));
    /// <summary>Returns a vector that is made from the largest components of two vectors.</summary>
    /// <param name="lhs">One of the values.</param>
    /// <param name="rhs">The other value.</param>
    /// <returns>Whichever of the two values is higher.</returns>
    public static Vector4D Max(in Vector4D lhs, in Vector4D rhs) => new(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z), Mathf.Max(lhs.w, rhs.w));
    /// <inheritdoc cref="Godot.Vector2.Ceil"/>
    /// <param name="a">The vector that will be the ceiling.</param>
    public static Vector4D Ceil(in Vector4D a) {
        Vector4D result = a;
        result[0] = Mathf.Ceil(result[0]);
        result[1] = Mathf.Ceil(result[1]);
        result[2] = Mathf.Ceil(result[2]);
        result[3] = Mathf.Ceil(result[3]);
        return result;
    }
    /// <inheritdoc cref="Godot.Vector2.Floor"/>
    /// <param name="a">The vector that will be the floor.</param>
    public static Vector4D Floor(in Vector4D a) {
        Vector4D result = a;
        result[0] = Mathf.Floor(result[0]);
        result[1] = Mathf.Floor(result[1]);
        result[2] = Mathf.Floor(result[2]);
        result[3] = Mathf.Floor(result[3]);
        return result;
    }
    /// <summary>Indicates whether this instance is equal to another instance of the same type.</summary>
    /// <param name="A">Object to be compared.</param>
    /// <param name="B">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator ==(Vector4D A, Vector4D B) => A.Equals(B);
    /// <summary>Indicates whether this instance is different from another instance of the same type.</summary>
    /// <param name="A">Object to be compared.</param>
    /// <param name="B">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator !=(Vector4D A, Vector4D B) => !A.Equals(B);
    /// <summary>Addition operation between two values.(<seealso cref="Vector4D"/> + <seealso cref="Vector4D"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the addition.</returns>
    public static Vector4D operator +(Vector4D a, Vector4D b) => new(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
    /// <summary>Subtraction operation between two values.(<seealso cref="Vector4D"/> - <seealso cref="Vector4D"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the subtraction.</returns>
    public static Vector4D operator -(Vector4D a, Vector4D b) => new(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
    /// <summary>The operator allows us to reverse the value.</summary>
    /// <param name="a">Or value that will be invested.</param>
    /// <returns>Returns the result of the inversion.</returns>
    public static Vector4D operator -(Vector4D a) => a.Neg();
    /// <summary>Division operation between two values.(<seealso cref="Vector4D"/> / <seealso cref="Vector4D"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the division.</returns>
    public static Vector4D operator /(Vector4D a, Vector4D b) => new(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);
    /// <summary>Modulo operation between two values.(<seealso cref="Vector4D"/> + <seealso cref="Vector4D"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the module.</returns>
    public static Vector4D operator %(Vector4D a, Vector4D b) => new(a.x % b.x, a.y % b.y, a.z % b.z, a.w % b.w);
    /// <summary>Multiplication operation between two values.(<seealso cref="Vector4D"/> * <seealso cref="Vector4D"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Vector4D operator *(Vector4D a, Vector4D b) => new(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
    /// <summary>Division operation between two values.(<seealso cref="Vector4D"/> / <seealso cref="float"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the division.</returns>
    public static Vector4D operator /(Vector4D a, float b) => new(a.x / b, a.y / b, a.z / b, a.w / b);
    /// <summary>Multiplication operation between two values.(<seealso cref="Vector4D"/> * <seealso cref="float"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Vector4D operator *(Vector4D a, float b) => new(a.x * b, a.y * b, a.z * b, a.w * b);
    /// <summary>Modulo operation between two values.(<seealso cref="Vector4D"/> + <seealso cref="float"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the module.</returns>
    public static Vector4D operator %(Vector4D a, float b) => new(a.x % b, a.y % b, a.z % b, a.w % b);
    /// <summary>Modulo operation between two values.(<seealso cref="float"/> + <seealso cref="Vector4D"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the module.</returns>
    public static Vector4D operator %(float a, Vector4D b) => new(a % b.x, a % b.y, a % b.z, a % b.w);
    /// <summary>Division operation between two values.(<seealso cref="float"/> / <seealso cref="Vector4D"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the division.</returns>
    public static Vector4D operator /(float a, Vector4D b) => new(a / b.x, a / b.y, a / b.z, a / b.w);
    /// <summary>Multiplication operation between two values.(<seealso cref="float"/> * <seealso cref="Vector4D"/>)</summary>
    /// <param name="a">First module.</param>
    /// <param name="b">Second module.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Vector4D operator *(float a, Vector4D b) => new(a * b.x, a * b.y, a * b.z, a * b.w);
    /// <summary>Implicit conversion operator.(<seealso cref="Vector3"/> to <seealso cref="Vector4D"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static implicit operator Vector4D(Vector3 v) => new (v.x, v.y, v.z);
    /// <summary>Implicit conversion operator.(<seealso cref="Vector4D"/> to <seealso cref="Vector3"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static implicit operator Vector3(Vector4D v) => new (v.x, v.y, v.z);
    /// <summary>Implicit conversion operator.(<seealso cref="Vector2"/> to <seealso cref="Vector4D"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static implicit operator Vector4D(Vector2 v) => new (v.x, v.y);
    /// <summary>Implicit conversion operator.(<seealso cref="Vector4D"/> to <seealso cref="Vector2"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static implicit operator Vector2(Vector4D v) => new (v.x, v.y);
    /// <summary>Implicit conversion operator.(<seealso cref="Vector4D"/> to <seealso cref="Vector2D"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static implicit operator Vector2D(Vector4D v) => new (v.x, v.y);
    /// <summary>Implicit conversion operator.(<seealso cref="Vector4D"/> to <seealso cref="Vector3D"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static implicit operator Vector3D(Vector4D v) => new (v.x, v.y, v.z);
    /// <summary>Implicit conversion operator.(<seealso cref="Vector4D"/> to <seealso cref="Quaternion"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static implicit operator Quaternion(Vector4D v) => new(v.x, v.y, v.z, v.w);
}