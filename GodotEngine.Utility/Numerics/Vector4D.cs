using Godot;
using System;

namespace Cobilas.GodotEngine.Utility.Numerics;
[Serializable]
public struct Vector4D : IVector<Vector4D> {
    public float x;
    public float y;
    public float z;
    public float w;

    public const float KEpsilon = 1E-06f;

    private static readonly Vector4D _zero = new(0f, 0f);
    private static readonly Vector4D _one = new(1f, 1f, 1f, 1f);

    public static Vector4D Zero => _zero;
    public static Vector4D One => _one;

    public Vector4D Normalized => throw new NotImplementedException();

    public Vector4D floor => throw new NotImplementedException();

    public Vector4D ceil => throw new NotImplementedException();

    public float magnitude => throw new NotImplementedException();

    public float sqrMagnitude => throw new NotImplementedException();

    IVector IVector.floor => throw new NotImplementedException();

    IVector IVector.ceil => throw new NotImplementedException();

    public float aspect => throw new NotImplementedException();

    public int AxisCount => throw new NotImplementedException();

    IVector IVector.Normalized => throw new NotImplementedException();

    public float this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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

    public static bool IsNormalized(IVector a)
        => Mathf.Abs(a.sqrMagnitude - 1f) < KEpsilon;

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

    public bool Equals(Vector4D other)
    {
        throw new NotImplementedException();
    }

    public string ToString(string format)
    {
        throw new NotImplementedException();
    }

    public string ToString(string format, IFormatProvider formatProvider)
    {
        throw new NotImplementedException();
    }

    public static Vector4D operator +(Vector4D a, Vector4D b) {
        Vector4D result = Vector4D._zero;
        result.x = a.x + b.x;
        result.y = a.y + b.y;
        result.z = a.z + b.z;
        result.w = a.w + b.w;
        return result;
    }

    public static Vector4D operator -(Vector4D a, Vector4D b) {
        Vector4D result = Vector4D._zero;
        result.x = a.x - b.x;
        result.y = a.y - b.y;
        result.z = a.z - b.z;
        result.w = a.w - b.w;
        return result;
    }

    public static Vector4D operator /(Vector4D a, Vector4D b) {
        Vector4D result = Vector4D._zero;
        result.x = a.x / b.x;
        result.y = a.y / b.y;
        result.z = a.z / b.z;
        result.w = a.w / b.w;
        return result;
    }

    public static Vector4D operator *(Vector4D a, Vector4D b) {
        Vector4D result = Vector4D._zero;
        result.x = a.x * b.x;
        result.y = a.y * b.y;
        result.z = a.z * b.z;
        result.w = a.w * b.w;
        return result;
    }

    public static Vector4D operator /(Vector4D a, float b) {
        Vector4D result = Vector4D._zero;
        result.x = a.x / b;
        result.y = a.y / b;
        result.z = a.z / b;
        result.w = a.w / b;
        return result;
    }

    public static Vector4D operator *(Vector4D a, float b) {
        Vector4D result = Vector4D._zero;
        result.x = a.x * b;
        result.y = a.y * b;
        result.z = a.z * b;
        result.w = a.w * b;
        return result;
    }

    public static implicit operator Vector4D(Vector3 v) => new (v.x, v.y, v.z);
    public static implicit operator Vector3(Vector4D v) => new (v.x, v.y, v.z);

    public static implicit operator Vector4D(Vector2 v) => new (v.x, v.y);
    public static implicit operator Vector2(Vector4D v) => new (v.x, v.y);
    
    public static implicit operator Vector2D(Vector4D v) => new (v.x, v.y);
    public static implicit operator Vector3D(Vector4D v) => new (v.x, v.y, v.z);
}