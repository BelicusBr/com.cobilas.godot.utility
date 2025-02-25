using Godot;
using System;
using Cobilas.GodotEngine.Utility.Numerics;
using Cobilas.GodotEditor.Utility.Serialization;

namespace Cobilas.GodotEngine.Utility;
/// <summary>Represents a normalized ARGB value between (0 and 1).</summary>
[Serializable]
public struct ColorF : IEquatable<ColorF>, IEquatable<Vector4D> {
    [ShowRangeProperty(true, 0f, 1f)] private float r;
    [ShowRangeProperty(true, 0f, 1f)] private float g;
    [ShowRangeProperty(true, 0f, 1f)] private float b;
    [ShowRangeProperty(true, 0f, 1f)] private float a;
    /// <summary>Represents the value red.</summary>
    /// <value>Allows you to set the value red.</value>
    /// <returns>Returns the value that represents red.</returns>
    public float R { readonly get => r; set => r = Mathf.Clamp(value, 0f, 1f); }
    /// <summary>Represents the value green.</summary>
    /// <value>Allows you to set the value green.</value>
    /// <returns>Returns the value that represents green.</returns>
    public float G { readonly get => g; set => g = Mathf.Clamp(value, 0f, 1f); }
    /// <summary>Represents the value blue.</summary>
    /// <value>Allows you to set the value blue.</value>
    /// <returns>Returns the value that represents blue.</returns>
    public float B { readonly get => b; set => b = Mathf.Clamp(value, 0f, 1f); }
    /// <summary>Represents the alpha value.</summary>
    /// <value>Allows you to set the alpha value.</value>
    /// <returns>Returns the value that represents the alpha.</returns>
    public float A { readonly get => a; set => a = Mathf.Clamp(value, 0f, 1f); }
    /// <summary>Creates a new instance of this object.</summary>
    public ColorF(float r, float g, float b, float a) {
        R = r;
        G = g;
        B = b;
        A = a;
    }
    /// <summary>Creates a new instance of this object.</summary>
    public ColorF(Color32 color) : this(color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f) { }
    /// <summary>Creates a new instance of this object.</summary>
    public ColorF(Color color) : this(color.r, color.g, color.b, color.a) { }
    /// <summary>Creates a new instance of this object.</summary>
    public ColorF(Vector4D color) : this(color.x, color.y, color.z, color.w) { }
    /// <inheritdoc/>
    public readonly bool Equals(ColorF other)
        => other.r == r && other.g == g && other.b == b && other.a == a;
    /// <inheritdoc/>
    public readonly bool Equals(Vector4D other)
        => other.x == r && other.y == g && other.z == b && other.w == a;
    /// <inheritdoc/>
    public override readonly string ToString()
        => $"(r:{r} g:{g} b:{b} a:{a})";
    /// <inheritdoc cref="Color.FromHsv(float, float, float, float)"/>
    /// <param name="h">Hue value.</param>
    /// <param name="s">Saturation value.</param>
    /// <param name="v">Value.</param>
    /// <param name="a">Alpha value.</param>
    public static ColorF HSVToColorF(float h, float s, float v, float a = 1) {
        float hh, p, q, t, ff;
        long i;

        if (s <= 0f) return new(v, v, v, a);

        hh = h / 60f;
        i = (long)hh;
        ff = hh - i;
        p = v * (1f - s);
        q = v * (1f - (s * ff));
        t = v * (1f - (s * (1f - ff)));
        
        return i switch {
            0 => new(v, t, p, a),
            1 => new(q, v, p, a),
            3 => new(p, q, v, a),
            4 => new(t, p, v, a),
            _ => new(v, p, q, a)
        };
    }
    /// <summary>Converts a <seealso cref="ColorF"/> value to HSV.</summary>
    /// <param name="c">The value to convert.</param>
    /// <returns>Returns a <seealso cref="Vector4D"/> value with HSV values.(<c>x:h / y:s / z:v</c>)</returns>
    public static Vector4D ColorFToHSV(ColorF c) {
        float h, s, v;
        float min, max, delta;

        min = Mathf.Min(Mathf.Min(c.r, c.g), c.b);
        max = Mathf.Max(Mathf.Max(c.r, c.g), c.b);

        v = max;
        delta = max - min;
        
        if (delta < .00001f) return new(0f, 0f, v, c.a);
        if (max > 0f) s = delta / max;
        else return new(0f, 0f, v, c.a);

        if (c.r >= max) h = (c.g - c.b) / delta;
        else {
            if (c.g >= max) h = 2f + (c.b - c.r) / delta;
            else h = 4f + (c.r - c.g) / delta;
        }

        h *= 60f;
        if (h < 0f) h += 360f;

        return new(h, s, v, c.a);
    }
    /// <summary>Implicit conversion operator.(<seealso cref="Color"/> to <seealso cref="ColorF"/>)</summary>
    /// <param name="c">Object to be converted.</param>
    public static implicit operator ColorF(Color c) => new(c);
    /// <summary>Implicit conversion operator.(<seealso cref="Color32"/> to <seealso cref="ColorF"/>)</summary>
    /// <param name="c">Object to be converted.</param>
    public static implicit operator ColorF(Color32 c) => new(c);
    /// <summary>Implicit conversion operator.(<seealso cref="Vector4D"/> to <seealso cref="ColorF"/>)</summary>
    /// <param name="c">Object to be converted.</param>
    public static implicit operator ColorF(Vector4D c) => new(c);
    /// <summary>Explicit conversion operator.(<seealso cref="ColorF"/> to <seealso cref="string"/>)</summary>
    /// <param name="c">Object to be converted.</param>
    public static explicit operator string(ColorF c) => Color32.Color32ToHex(c);
    /// <summary>Explicit conversion operator.(<seealso cref="string"/> to <seealso cref="ColorF"/>)</summary>
    /// <param name="stg">Object to be converted.</param>
    public static explicit operator ColorF(string stg) => Color32.HexToColor32(stg);
}
