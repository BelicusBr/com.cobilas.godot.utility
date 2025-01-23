using Godot;
using System;
using Cobilas.GodotEngine.Utility.Numerics;
using Cobilas.GodotEngine.Utility.EditorSerialization;
using System.Globalization;
using System.Text;

namespace Cobilas.GodotEngine.Utility;
[Serializable]
public struct Color32 : IEquatable<Color32> {
    [ShowRangeProperty(true, 0, 255)] private byte r;
    [ShowRangeProperty(true, 0, 255)] private byte g;
    [ShowRangeProperty(true, 0, 255)] private byte b;
    [ShowRangeProperty(true, 0, 255)] private byte a;

    public byte R { readonly get => r; set => r = (byte)Mathf.Clamp(value, 0, 255); }
    public byte G { readonly get => g; set => g = (byte)Mathf.Clamp(value, 0, 255); }
    public byte B { readonly get => b; set => b = (byte)Mathf.Clamp(value, 0, 255); }
    public byte A { readonly get => a; set => a = (byte)Mathf.Clamp(value, 0, 255); }

    public Color32(byte r, byte g, byte b, byte a) {
        this.r = r;
        this.g = g;
        this.b = b;
        this.a = a;
    }

    public Color32(ColorF color) {
        this.r = (byte)(color.R * 255);
        this.g = (byte)(color.G * 255);
        this.b = (byte)(color.B * 255);
        this.a = (byte)(color.A * 255);
    }

    public Color32(Color color) {
        R = (byte)(color.r * 255);
        G = (byte)(color.g * 255);
        B = (byte)(color.b * 255);
        A = (byte)(color.a * 255);
    }

    public Color32(Vector4D color) {
        R = (byte)(color.x * 255);
        G = (byte)(color.y * 255);
        B = (byte)(color.z * 255);
        A = (byte)(color.w * 255);
    }

    public bool Equals(Color32 other) 
        => other.r == r && other.g == g && other.b == b && other.a == a;

    public override string ToString()
        => $"(r:{r} g:{g} b:{b} a:{a})";

    public static Color32 HexToColor32(string hex) {
        hex = hex.Replace("#", string.Empty);
        byte r, g, b, a;

        if (hex.Length > 6) {
            a = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
            r = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
            g = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
            b = byte.Parse(hex.Substring(6, 2), NumberStyles.HexNumber);
        } else {
            r = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
            g = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
            b = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
            a = hex.Length == 8 ? byte.Parse(hex.Substring(6, 2), NumberStyles.HexNumber) : (byte)255;
        }

        return new(r, g, b, a);
    }

    public static string Color32ToHex(Color32 color) {
        if (color.a == 255) return $"#{color.r:X2}{color.g:X2}{color.b:X2}";
        return $"#{color.a:X2}{color.r:X2}{color.g:X2}{color.b:X2}";
    }

    public static implicit operator Color32(Color c) => new(c);
    public static implicit operator Color32(ColorF c) => new(c);
    public static implicit operator Color32(Vector4D c) => new(c);
    public static explicit operator string(Color32 c) => Color32ToHex(c);
    public static explicit operator Color32(string stg) => HexToColor32(stg);
}
