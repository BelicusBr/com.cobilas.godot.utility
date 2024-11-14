using Godot;
using System;
using System.Globalization;

namespace Cobilas.GodotEngine.Utility.Numerics;
/// <summary>Quaternions are used to represent rotations.</summary>
[Serializable]
public struct Quaternion : IEquatable<Quaternion>, IFormattable {
    /// <summary>X component of the Quaternion. Don't modify this directly unless you know quaternions inside out.</summary>
    public float x;
    /// <summary>Y component of the Quaternion. Don't modify this directly unless you know quaternions inside out.</summary>
    public float y;
    /// <summary>Z component of the Quaternion. Don't modify this directly unless you know quaternions inside out.</summary>
    public float z;
    /// <summary>W component of the Quaternion. Do not directly modify quaternions.</summary>
    public float w;
    /// <summary>A small value used to compare quaternions for equality.</summary>
    /// <remarks>
    /// The <c>kEpsilon</c> constant is used to determine if two quaternions are nearly equal, accounting for floating-point precision errors.
    /// </remarks>
    public const float KEpsilon = 1E-06f;
    /// <summary>Degrees-to-radians conversion constant (Read Only).</summary>
    public const double Rad2Deg = 360d / (Math.PI * 2d);
    /// <summary>Radians-to-degrees conversion constant (Read Only).</summary>
    public const double Deg2Rad = (Math.PI * 2d) / 360d;
    /// <summary>Returns or sets the euler angle representation of the rotation.</summary>
    public readonly Vector3D Euler => ToEuler(this);
    /// <summary>Returns this quaternion with a magnitude of 1 (Read Only).</summary>
    public readonly Quaternion Normalized => Normalize(this);

    private static readonly Quaternion identityQuaternion = new(0.0f, 0.0f, 0.0f, 1f);
    /// <summary>The identity rotation (Read Only).</summary>
    public static Quaternion Identity => identityQuaternion;
    /// <summary>Starts a new instance of the object.</summary>
    public Quaternion(float x, float y) : this(x, y, 0f, 0f) {}
    /// <summary>Starts a new instance of the object.</summary>
    public Quaternion(float x, float y, float z) : this(x, y, z, 0f) {}
    /// <summary>Starts a new instance of the object.</summary>
    public Quaternion(float x, float y, float z, float w) : this() {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }
    /// <summary>Starts a new instance of the object.</summary>
    public Quaternion(Quaternion vector) : this(vector.x, vector.y, vector.z, vector.w) {}
    /// <summary>Starts a new instance of the object.</summary>
    public Quaternion(Vector4D vector) : this(vector.x, vector.y, vector.z, vector.w) {}
#region Methods
    /// <inheritdoc/>
    public readonly bool Equals(Quaternion other)
        => other.x == this.x && other.y == this.y && other.z == this.z && other.w == this.w;
    /// <inheritdoc/>
    public override readonly bool Equals(object obj)
        => obj is Quaternion quat && Equals(quat);
    /// <inheritdoc/>
    public override readonly int GetHashCode() 
        => x.GetHashCode() ^ y.GetHashCode() << 2 ^ z.GetHashCode() >> 2 ^ w.GetHashCode();
    /// <inheritdoc/>
    public override readonly string ToString() => ToString("(x:{0:N3} y:{1:N3} z:{2:N3} w:{3:N3})");
    /// <inheritdoc cref="IFormattable.ToString(string, IFormatProvider)"/>
    public readonly string ToString(string format) => ToString(format, CultureInfo.InvariantCulture);
    /// <inheritdoc/>
    public readonly string ToString(string format, IFormatProvider formatProvider)
        => string.Format(formatProvider, format ?? "(x:{0:N3} y:{1:N3} z:{2:N3} w:{2:N3})", this.x, this.y, this.z, this.w);
#endregion
#region static methods
    /// <summary>Converts this quaternion to one with the same orientation but with a magnitude of 1.</summary>
    /// <param name="q">The Quaternion that will be normalized.</param>
    /// <returns>Returns an already normalized Quaternion.</returns>
    public static Quaternion Normalize(Quaternion q) {
        float num1 = Mathf.Sqrt(Dot(q, q));
        return num1 < (double)Mathf.Epsilon ? identityQuaternion : new(q.x / num1, q.y / num1, q.z / num1, q.w / num1);
    }
    /// <summary>Returns the angle in degrees between two rotations a and b.</summary>
    /// <param name="a">Object to be compared.</param>
    /// <param name="b">Object of comparison.</param>
    /// <returns>The floating point angle.</returns>
    public static float Angle(Quaternion a, Quaternion b) {
      float num = Quaternion.Dot(a, b);
      return Quaternion.IsEqualUsingDot(num) ? 0.0f : (float)((double)Mathf.Acos(Mathf.Min(Mathf.Abs(num), 1f)) * 2.0d * Rad2Deg);
    }
    /// <summary>The dot product between two rotations.</summary>
    /// <param name="a">Object to be compared.</param>
    /// <param name="b">Object of comparison.</param>
    /// <returns>Returns the dot product of two quaternions.</returns>
    public static float Dot(Quaternion a, Quaternion b)
        => (float)(a.x * (double)b.x + a.y * (double)b.y + a.z * (double)b.z + a.w * (double)b.w);
    /// <summary>Convert euler-angle to quaternion.</summary>
    /// <param name="vector">Euler-angle that will be converted.</param>
    /// <returns>Return result of euler to quaternion conversion.</returns>
    public static Quaternion ToQuaternion(Vector3D vector) {
        float cX = Mathf.Cos(vector.x * .5f);
        float sX = Mathf.Sin(vector.x * .5f);
        float cY = Mathf.Cos(vector.y * .5f);
        float sY = Mathf.Sin(vector.y * .5f);
        float cZ = Mathf.Cos(vector.z * .5f);
        float sZ = Mathf.Sin(vector.z * .5f);

        return new Quaternion(
            sX * cY * cZ - cX * sY * sZ,
            cX * sY * cZ + sX * cY * sZ,
            cX * cY * sZ - sX * sY * cZ,
            cX * cY * cZ + sX * sY * sZ
        );
    }
    /// <summary>Convert quaternion to euler-angle.</summary>
    /// <param name="quaternion">The quaternion that will be converted.</param>
    /// <returns>The result of the conversion from quaternion to euler.</returns>
    public static Vector3D ToEuler(Quaternion quaternion) {
        Vector3D angles;

        // roll (x-axis rotation)
        double sinr_cosp = 2 * (quaternion.w * quaternion.x + quaternion.y * quaternion.z);
        double cosr_cosp = 1 - 2 * (quaternion.x * quaternion.x + quaternion.y * quaternion.y);
        angles.x = (float)Math.Atan2(sinr_cosp, cosr_cosp);

        // pitch (y-axis rotation)
        double sinp = Math.Sqrt(1 + 2 * (quaternion.w * quaternion.y - quaternion.x * quaternion.z));
        double cosp = Math.Sqrt(1 - 2 * (quaternion.w * quaternion.y - quaternion.x * quaternion.z));
        angles.y = 2 * (float)(Math.Atan2(sinp, cosp) - Math.PI / 2);

        // yaw (z-axis rotation)
        double siny_cosp = 2 * (quaternion.w * quaternion.z + quaternion.x * quaternion.y);
        double cosy_cosp = 1 - 2 * (quaternion.y * quaternion.y + quaternion.z * quaternion.z);
        angles.z = (float)Math.Atan2(siny_cosp, cosy_cosp);

        return angles;
    }

    private static bool IsEqualUsingDot(float dot) => dot > 0.999998986721039d;
#endregion
#region operator
    /// <summary>Multiplication operation between two values.(<seealso cref="Quaternion"/> * <seealso cref="Vector3D"/>)</summary>
    /// <param name="rotation">First module.</param>
    /// <param name="point">Second module.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Vector3D operator *(Quaternion rotation, Vector3D point) {
        float num1 = rotation.x * 2f;
        float num2 = rotation.y * 2f;
        float num3 = rotation.z * 2f;
        float num4 = rotation.x * num1;
        float num5 = rotation.y * num2;
        float num6 = rotation.z * num3;
        float num7 = rotation.x * num2;
        float num8 = rotation.x * num3;
        float num9 = rotation.y * num3;
        float num10 = rotation.w * num1;
        float num11 = rotation.w * num2;
        float num12 = rotation.w * num3;
        Vector3D vector3;
        vector3.x = (float) ((1.0d - ((double) num5 + (double) num6)) * (double) point.x + ((double) num7 - (double) num12) * (double) point.y + ((double) num8 + (double) num11) * (double) point.z);
        vector3.y = (float) (((double) num7 + (double) num12) * (double) point.x + (1.0d - ((double) num4 + (double) num6)) * (double) point.y + ((double) num9 - (double) num10) * (double) point.z);
        vector3.z = (float) (((double) num8 - (double) num11) * (double) point.x + ((double) num9 + (double) num10) * (double) point.y + (1.0d - ((double) num4 + (double) num5)) * (double) point.z);
        return vector3;
    }
    /// <summary>Multiplication operation between two values.(<seealso cref="Quaternion"/> * <seealso cref="Quaternion"/>)</summary>
    /// <param name="lhs">First module.</param>
    /// <param name="rhs">Second module.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Quaternion operator *(Quaternion lhs, Quaternion rhs) 
        => new((float) (lhs.w * (double)rhs.x + lhs.x * (double)rhs.w + lhs.y * (double)rhs.z - lhs.z * (double)rhs.y),
            (float) (lhs.w * (double)rhs.y + lhs.y * (double)rhs.w + lhs.z * (double)rhs.x - lhs.x * (double)rhs.z),
            (float) (lhs.w * (double)rhs.z + lhs.z * (double)rhs.w + lhs.x * (double)rhs.y - lhs.y * (double)rhs.x),
            (float) (lhs.w * (double)rhs.w - lhs.x * (double)rhs.x - lhs.y * (double)rhs.y - lhs.z * (double)rhs.z));
    /// <summary>Indicates whether this instance is equal to another instance of the same type.</summary>
    /// <param name="lhs">Object to be compared.</param>
    /// <param name="rhs">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator ==(Quaternion lhs, Quaternion rhs) => lhs.Equals(rhs);
    /// <summary>Indicates whether this instance is different from another instance of the same type.</summary>
    /// <param name="lhs">Object to be compared.</param>
    /// <param name="rhs">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator !=(Quaternion lhs, Quaternion rhs) => !lhs.Equals(rhs);
    /// <summary>Implicit conversion operator.(<seealso cref="Quaternion"/> to <seealso cref="Vector4D"/>)</summary>
    /// <param name="v">Object to be converted.</param>
    public static implicit operator Vector4D(Quaternion v) => new(v.x, v.y, v.z, v.w);
}
#endregion