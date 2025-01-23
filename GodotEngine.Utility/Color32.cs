using Godot;
using System;
using System.Globalization;
using Cobilas.GodotEngine.Utility.Numerics;
using Cobilas.GodotEngine.Utility.EditorSerialization;

namespace Cobilas.GodotEngine.Utility;
/// <summary>Represents an ARGB value between (0 and 255).</summary>
[Serializable]
public struct Color32 : IEquatable<Color32> {
    [ShowRangeProperty(true, 0, 255)] private byte r;
    [ShowRangeProperty(true, 0, 255)] private byte g;
    [ShowRangeProperty(true, 0, 255)] private byte b;
    [ShowRangeProperty(true, 0, 255)] private byte a;
    /// <inheritdoc cref="ColorF.R"/>
    public byte R { readonly get => r; set => r = (byte)Mathf.Clamp(value, 0, 255); }
    /// <inheritdoc cref="ColorF.G"/>
    public byte G { readonly get => g; set => g = (byte)Mathf.Clamp(value, 0, 255); }
    /// <inheritdoc cref="ColorF.B"/>
    public byte B { readonly get => b; set => b = (byte)Mathf.Clamp(value, 0, 255); }
    /// <inheritdoc cref="ColorF.A"/>
    public byte A { readonly get => a; set => a = (byte)Mathf.Clamp(value, 0, 255); }
    /// <summary>Creates a new instance of this object.</summary>
    public Color32(byte r, byte g, byte b, byte a) {
        this.r = r;
        this.g = g;
        this.b = b;
        this.a = a;
    }
    /// <summary>Creates a new instance of this object.</summary>
    public Color32(ColorF color) {
        this.r = (byte)(color.R * 255);
        this.g = (byte)(color.G * 255);
        this.b = (byte)(color.B * 255);
        this.a = (byte)(color.A * 255);
    }
    /// <summary>Creates a new instance of this object.</summary>
    public Color32(Color color) {
        R = (byte)(color.r * 255);
        G = (byte)(color.g * 255);
        B = (byte)(color.b * 255);
        A = (byte)(color.a * 255);
    }
    /// <summary>Creates a new instance of this object.</summary>
    public Color32(Vector4D color) {
        R = (byte)(color.x * 255);
        G = (byte)(color.y * 255);
        B = (byte)(color.z * 255);
        A = (byte)(color.w * 255);
    }
    /// <inheritdoc/>
    public readonly bool Equals(Color32 other) 
        => other.r == r && other.g == g && other.b == b && other.a == a;
    /// <inheritdoc/>
    public override readonly string ToString()
        => $"(r:{r} g:{g} b:{b} a:{a})";
    /// <summary>Converts a Hexadecimal value to a <seealso cref="Color32"/> value.</summary>
    /// <param name="hex">The hexadecimal value to convert.</param>
    /// <returns>Returns a hexadecimal value converted to <seealso cref="Color32"/>.</returns>
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
    /// <summary>Converts a <seealso cref="Color32"/> value to hexadecimal.</summary>
    /// <param name="color">The <seealso cref="Color32"/> value to convert.</param>
    /// <returns>Returns a <seealso cref="string"/> containing the hexadecimal value.</returns>
    public static string Color32ToHex(Color32 color) {
        if (color.a == 255) return $"#{color.r:X2}{color.g:X2}{color.b:X2}";
        return $"#{color.a:X2}{color.r:X2}{color.g:X2}{color.b:X2}";
    }
    /// <summary>Implicit conversion operator.(<seealso cref="Color"/> to <seealso cref="Color32"/>)</summary>
    /// <param name="c">Object to be converted.</param>
    public static implicit operator Color32(Color c) => new(c);
    /// <summary>Implicit conversion operator.(<seealso cref="ColorF"/> to <seealso cref="Color32"/>)</summary>
    /// <param name="c">Object to be converted.</param>
    public static implicit operator Color32(ColorF c) => new(c);
    /// <summary>Implicit conversion operator.(<seealso cref="Vector4D"/> to <seealso cref="Color32"/>)</summary>
    /// <param name="c">Object to be converted.</param>
    public static implicit operator Color32(Vector4D c) => new(c);
    /// <summary>Explicit conversion operator.(<seealso cref="Color32"/> to <seealso cref="string"/>)</summary>
    /// <param name="c">Object to be converted.</param>
    public static explicit operator string(Color32 c) => Color32ToHex(c);
    /// <summary>Explicit conversion operator.(<seealso cref="string"/> to <seealso cref="Color32"/>)</summary>
    /// <param name="stg">Object to be converted.</param>
    public static explicit operator Color32(string stg) => HexToColor32(stg);
}
