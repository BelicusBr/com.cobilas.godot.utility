using Godot;
using System;
using Cobilas.GodotEngine.Utility.Numerics;
using Cobilas.GodotEngine.Utility.EditorSerialization;

namespace Cobilas.GodotEngine.Utility;
[Serializable]
public struct ColorF : IEquatable<ColorF>, IEquatable<Vector4D> {
    [ShowRangeProperty(true, 0f, 1f)] private float r;
    [ShowRangeProperty(true, 0f, 1f)] private float g;
    [ShowRangeProperty(true, 0f, 1f)] private float b;
    [ShowRangeProperty(true, 0f, 1f)] private float a;

    public float R { readonly get => r; set => r = Mathf.Clamp(value, 0f, 1f); }
    public float G { readonly get => g; set => g = Mathf.Clamp(value, 0f, 1f); }
    public float B { readonly get => b; set => b = Mathf.Clamp(value, 0f, 1f); }
    public float A { readonly get => a; set => a = Mathf.Clamp(value, 0f, 1f); }

    public ColorF(float r, float g, float b, float a) {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    public ColorF(Color32 color) : this(color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f) { }

    public ColorF(Color color) : this(color.r, color.g, color.b, color.a) { }

    public ColorF(Vector4D color) : this(color.x, color.y, color.z, color.w) { }

    public bool Equals(ColorF other)
        => other.r == r && other.g == g && other.b == b && other.a == a;

    public bool Equals(Vector4D other)
        => other.x == r && other.y == g && other.z == b && other.w == a;

    public override string ToString()
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
    /*
    x => h
    y => s
    z => v
    w => a
    */
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

    public static implicit operator ColorF(Color c) => new(c);
    public static implicit operator ColorF(Color32 c) => new(c);
    public static implicit operator ColorF(Vector4D c) => new(c);
}
