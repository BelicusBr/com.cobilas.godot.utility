using Godot;

namespace Cobilas.GodotEngine.Utility.Numerics;
public struct Vector4D {
    public float x;
    public float y;
    public float z;
    public float w;

    private static readonly Vector4D _zero = new(0f, 0f);
    private static readonly Vector4D _one = new(1f, 1f, 1f, 1f);

    public static Vector4D Zero => _zero;
    public static Vector4D One => _one;

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