using Godot;
using System;

namespace Cobilas.GodotEngine.Utility.Numerics; 
public struct Quaternion {
    public float x;
    public float y;
    public float z;
    public float w;

    public readonly Vector3D Euler => ToEuler(this);
    public readonly Quaternion Normalized => Normalize(this);

    private static readonly Quaternion identityQuaternion = new(0.0f, 0.0f, 0.0f, 1f);

    public static Quaternion Identity => identityQuaternion;

    public Quaternion(float x, float y) : this(x, y, 0f, 0f) {}

    public Quaternion(float x, float y, float z) : this(x, y, z, 0f) {}

    public Quaternion(float x, float y, float z, float w) : this() {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }

    public Quaternion(Quaternion vector) : this(vector.x, vector.y, vector.z, vector.w) {}

    public Quaternion(Vector4D vector) : this(vector.x, vector.y, vector.z, vector.w) {}

    public static Quaternion Normalize(Quaternion q) {
        float num1 = Mathf.Sqrt(Dot(q, q));
        return num1 < (double)Mathf.Epsilon ? identityQuaternion : new(q.x / num1, q.y / num1, q.z / num1, q.w / num1);
    }

    public static float Dot(Quaternion a, Quaternion b)
        => (float) (a.x * (double)b.x + a.y * (double)b.y + a.z * (double)b.z + a.w * (double)b.w);
    
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

    public static Quaternion operator *(Quaternion lhs, Quaternion rhs) 
        => new((float) (lhs.w * (double)rhs.x + lhs.x * (double)rhs.w + lhs.y * (double)rhs.z - lhs.z * (double)rhs.y),
            (float) (lhs.w * (double)rhs.y + lhs.y * (double)rhs.w + lhs.z * (double)rhs.x - lhs.x * (double)rhs.z),
            (float) (lhs.w * (double)rhs.z + lhs.z * (double)rhs.w + lhs.x * (double)rhs.y - lhs.y * (double)rhs.x),
            (float) (lhs.w * (double)rhs.w - lhs.x * (double)rhs.x - lhs.y * (double)rhs.y - lhs.z * (double)rhs.z));
}