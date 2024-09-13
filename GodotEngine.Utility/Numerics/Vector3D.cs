using Godot;
using System;

namespace Cobilas.GodotEngine.Utility.Numerics;
[Serializable]
public struct Vector3D {
    public float x;
    public float y;
    public float z;

    private static readonly Vector3D _zero = new(0f, 0f);
    private static readonly Vector3D _one = new(1f, 1f, 1f);
    private static readonly Vector3D _up = new(0f, -1f);
    private static readonly Vector3D _down = new(0f, 1f);
    private static readonly Vector3D _right = new(1f, 0f);
    private static readonly Vector3D _left = new(-1f, 0f);
    private static readonly Vector3D _forward = new(0f, 0f, 1f);
    private static readonly Vector3D _back = new(0f, 0f, -1f);

    public static Vector3D Zero => _zero;
    public static Vector3D One => _one;
    public static Vector3D Up => _up;
    public static Vector3D Down => _down;
    public static Vector3D Right => _right;
    public static Vector3D Left => _left;
    public static Vector3D Forward => _forward;
    public static Vector3D Back => _back;

    public Vector3D(float x, float y) : this(x, y, 0f) {}

    public Vector3D(float x, float y, float z) : this() {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vector3D(Vector3D vector) : this(vector.x, vector.y, vector.z) {}

    public Vector3D(Vector3 vector) : this(vector.x, vector.y, vector.z) {}

    public static Vector3D operator +(Vector3D a, Vector3D b) {
        Vector3D result = Vector3D._zero;
        result.x = a.x + b.x;
        result.y = a.y + b.y;
        result.z = a.z + b.z;
        return result;
    }

    public static Vector3D operator -(Vector3D a, Vector3D b) {
        Vector3D result = Vector3D._zero;
        result.x = a.x - b.x;
        result.y = a.y - b.y;
        result.z = a.z - b.z;
        return result;
    }

    public static Vector3D operator /(Vector3D a, Vector3D b) {
        Vector3D result = Vector3D._zero;
        result.x = a.x / b.x;
        result.y = a.y / b.y;
        result.z = a.z / b.z;
        return result;
    }

    public static Vector3D operator *(Vector3D a, Vector3D b) {
        Vector3D result = Vector3D._zero;
        result.x = a.x * b.x;
        result.y = a.y * b.y;
        result.z = a.z * b.z;
        return result;
    }

    public static Vector3D operator /(Vector3D a, float b) {
        Vector3D result = Vector3D._zero;
        result.x = a.x / b;
        result.y = a.y / b;
        result.z = a.z / b;
        return result;
    }

    public static Vector3D operator *(Vector3D a, float b) {
        Vector3D result = Vector3D._zero;
        result.x = a.x * b;
        result.y = a.y * b;
        result.z = a.z * b;
        return result;
    }

    public static implicit operator Vector3D(Vector3 v) => new (v.x, v.y, v.z);
    public static implicit operator Vector3(Vector3D v) => new (v.x, v.y, v.z);

    public static implicit operator Vector3D(Vector2 v) => new (v.x, v.y);
    public static implicit operator Vector2(Vector3D v) => new (v.x, v.y);
    
    public static implicit operator Vector2D(Vector3D v) => new (v.x, v.y);
    public static implicit operator Vector4D(Vector3D v) => new (v.x, v.y);
}