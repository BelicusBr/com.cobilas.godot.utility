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
    /// <summary>Gets a pre-defined black color (R:0, G:0, B:0, A:1).</summary>
    /// <returns>A ColorF representing black color.</returns>
    public static ColorF Black => new(0f, 0f, 0f, 1f);
    /// <summary>Gets a pre-defined white color (R:1, G:1, B:1, A:1).</summary>
    /// <returns>A ColorF representing white color.</returns>
    public static ColorF White => new(1f, 1f, 1f, 1f);
    /// <summary>Gets a pre-defined transparent color (R:0, G:0, B:0, A:0).</summary>
    /// <returns>A ColorF representing fully transparent color.</returns>
    public static ColorF Clear => new(0f, 0f, 0f, 0f);
    /// <summary>Gets a pre-defined red color (R:1, G:0, B:0, A:1).</summary>
    /// <returns>A ColorF representing red color.</returns>
    public static ColorF Red => new(1f, 0f, 0f, 1f);
    /// <summary>Gets a pre-defined green color (R:0, G:1, B:0, A:1).</summary>
    /// <returns>A ColorF representing green color.</returns>
    public static ColorF Green => new(0f, 1f, 0f, 1f);
    /// <summary>Gets a pre-defined blue color (R:0, G:0, B:1, A:1).</summary>
    /// <returns>A ColorF representing blue color.</returns>
    public static ColorF Blue => new(0f, 0f, 1f, 1f);
    /// <summary>Gets a pre-defined yellow color (R:1, G:0.92, B:0.016, A:1).</summary>
    /// <returns>A ColorF representing yellow color.</returns>
    public static ColorF Yellow => new(1f, .92f, .016f, 1f);
    /// <summary>Gets a pre-defined magenta color (R:1, G:0, B:1, A:1).</summary>
    /// <returns>A ColorF representing magenta color.</returns>
    public static ColorF Magenta => new(1f, 0f, 1f, 1f);
    /// <summary>Gets a pre-defined gray color (R:0.5, G:0.5, B:0.5, A:1).</summary>
    /// <returns>A ColorF representing gray color.</returns>
    public static ColorF Gray => new(0.5f, 0.5f, 0.5f, 1f);
    /// <summary>Gets a pre-defined cyan color (R:0, G:1, B:1, A:1).</summary>
    /// <returns>A ColorF representing cyan color.</returns>
    public static ColorF Cyan => new(0f, 1f, 1f, 1f);
    /// <summary>Gets a pre-defined AliceBlue color (R:0.94, G:0.97, B:1, A:1).</summary>
    /// <returns>A ColorF representing AliceBlue color.</returns>
    public static ColorF AliceBlue => new(0.94f, 0.97f, 1f, 1f);
    /// <summary>Gets a pre-defined AntiqueWhite color (R:0.98, G:0.92, B:0.84, A:1).</summary>
    /// <returns>A ColorF representing AntiqueWhite color.</returns>
    public static ColorF AntiqueWhite => new(0.98f, 0.92f, 0.84f, 1f);
    /// <summary>Gets a pre-defined Aqua color (R:0, G:1, B:1, A:1).</summary>
    /// <returns>A ColorF representing Aqua color.</returns>
    public static ColorF Aqua => new(0f, 1f, 1f, 1f);
    /// <summary>Gets a pre-defined Aquamarine color (R:0.5, G:1, B:0.83, A:1).</summary>
    /// <returns>A ColorF representing Aquamarine color.</returns>
    public static ColorF Aquamarine => new(0.5f, 1f, 0.83f, 1f);
    /// <summary>Gets a pre-defined Azure color (R:0.94, G:1, B:1, A:1).</summary>
    /// <returns>A ColorF representing Azure color.</returns>
    public static ColorF Azure => new(0.94f, 1f, 1f, 1f);
    /// <summary>Gets a pre-defined Beige color (R:0.96, G:0.96, B:0.86, A:1).</summary>
    /// <returns>A ColorF representing Beige color.</returns>
    public static ColorF Beige => new(0.96f, 0.96f, 0.86f, 1f);
    /// <summary>Gets a pre-defined Bisque color (R:1, G:0.89, B:0.77, A:1).</summary>
    /// <returns>A ColorF representing Bisque color.</returns>
    public static ColorF Bisque => new(1f, 0.89f, 0.77f, 1f);
    /// <summary>Gets a pre-defined BlanchedAlmond color (R:1, G:0.92, B:0.8, A:1).</summary>
    /// <returns>A ColorF representing BlanchedAlmond color.</returns>
    public static ColorF BlanchedAlmond => new(1f, 0.92f, 0.8f, 1f);
    /// <summary>Gets a pre-defined BlueViolet color (R:0.54, G:0.17, B:0.89, A:1).</summary>
    /// <returns>A ColorF representing BlueViolet color.</returns>
    public static ColorF BlueViolet => new(0.54f, 0.17f, 0.89f, 1f);
    /// <summary>Gets a pre-defined Brown color (R:0.65, G:0.16, B:0.16, A:1).</summary>
    /// <returns>A ColorF representing Brown color.</returns>
    public static ColorF Brown => new(0.65f, 0.16f, 0.16f, 1f);
    /// <summary>Gets a pre-defined BurlyWood color (R:0.87, G:0.72, B:0.53, A:1).</summary>
    /// <returns>A ColorF representing BurlyWood color.</returns>
    public static ColorF BurlyWood => new(0.87f, 0.72f, 0.53f, 1f);
    /// <summary>Gets a pre-defined CadetBlue color (R:0.37, G:0.62, B:0.63, A:1).</summary>
    /// <returns>A ColorF representing CadetBlue color.</returns>
    public static ColorF CadetBlue => new(0.37f, 0.62f, 0.63f, 1f);
    /// <summary>Gets a pre-defined Chartreuse color (R:0.5, G:1, B:0, A:1).</summary>
    /// <returns>A ColorF representing Chartreuse color.</returns>
    public static ColorF Chartreuse => new(0.5f, 1f, 0f, 1f);
    /// <summary>Gets a pre-defined Chocolate color (R:0.82, G:0.41, B:0.12, A:1).</summary>
    /// <returns>A ColorF representing Chocolate color.</returns>
    public static ColorF Chocolate => new(0.82f, 0.41f, 0.12f, 1f);
    /// <summary>Gets a pre-defined Coral color (R:1, G:0.5, B:0.31, A:1).</summary>
    /// <returns>A ColorF representing Coral color.</returns>
    public static ColorF Coral => new(1f, 0.5f, 0.31f, 1f);
    /// <summary>Gets a pre-defined Cornflower color (R:0.39, G:0.58, B:0.93, A:1).</summary>
    /// <returns>A ColorF representing Cornflower color.</returns>
    public static ColorF Cornflower => new(0.39f, 0.58f, 0.93f, 1f);
    /// <summary>Gets a pre-defined Cornsilk color (R:1, G:0.97, B:0.86, A:1).</summary>
    /// <returns>A ColorF representing Cornsilk color.</returns>
    public static ColorF Cornsilk => new(1f, 0.97f, 0.86f, 1f);
    /// <summary>Gets a pre-defined Crimson color (R:0.86, G:0.08, B:0.24, A:1).</summary>
    /// <returns>A ColorF representing Crimson color.</returns>
    public static ColorF Crimson => new(0.86f, 0.08f, 0.24f, 1f);
    /// <summary>Gets a pre-defined DarkBlue color (R:0, G:0, B:0.55, A:1).</summary>
    /// <returns>A ColorF representing DarkBlue color.</returns>
    public static ColorF DarkBlue => new(0f, 0f, 0.55f, 1f);
    /// <summary>Gets a pre-defined DarkCyan color (R:0, G:0.55, B:0.55, A:1).</summary>
    /// <returns>A ColorF representing DarkCyan color.</returns>
    public static ColorF DarkCyan => new(0f, 0.55f, 0.55f, 1f);
    /// <summary>Gets a pre-defined DarkGoldenRod color (R:0.72, G:0.53, B:0.04, A:1).</summary>
    /// <returns>A ColorF representing DarkGoldenRod color.</returns>
    public static ColorF DarkGoldenRod => new(0.72f, 0.53f, 0.04f, 1f);
    /// <summary>Gets a pre-defined DarkGray color (R:0.66, G:0.66, B:0.66, A:1).</summary>
    /// <returns>A ColorF representing DarkGray color.</returns>
    public static ColorF DarkGray => new(0.66f, 0.66f, 0.66f, 1f);
    /// <summary>Gets a pre-defined DarkGreen color (R:0, G:0.39, B:0, A:1).</summary>
    /// <returns>A ColorF representing DarkGreen color.</returns>
    public static ColorF DarkGreen => new(0f, 0.39f, 0f, 1f);
    /// <summary>Gets a pre-defined DarkKhaki color (R:0.74, G:0.72, B:0.42, A:1).</summary>
    /// <returns>A ColorF representing DarkKhaki color.</returns>
    public static ColorF DarkKhaki => new(0.74f, 0.72f, 0.42f, 1f);
    /// <summary>Gets a pre-defined DarkMagenta color (R:0.55, G:0, B:0.55, A:1).</summary>
    /// <returns>A ColorF representing DarkMagenta color.</returns>
    public static ColorF DarkMagenta => new(0.55f, 0f, 0.55f, 1f);
    /// <summary>Gets a pre-defined DarkOliveGreen color (R:0.33, G:0.42, B:0.18, A:1).</summary>
    /// <returns>A ColorF representing DarkOliveGreen color.</returns>
    public static ColorF DarkOliveGreen => new(0.33f, 0.42f, 0.18f, 1f);
    /// <summary>Gets a pre-defined DarkOrange color (R:1, G:0.55, B:0, A:1).</summary>
    /// <returns>A ColorF representing DarkOrange color.</returns>
    public static ColorF DarkOrange => new(1f, 0.55f, 0f, 1f);
    /// <summary>Gets a pre-defined DarkOrchid color (R:0.6, G:0.2, B:0.8, A:1).</summary>
    /// <returns>A ColorF representing DarkOrchid color.</returns>
    public static ColorF DarkOrchid => new(0.6f, 0.2f, 0.8f, 1f);
    /// <summary>Gets a pre-defined DarkRed color (R:0.55, G:0, B:0, A:1).</summary>
    /// <returns>A ColorF representing DarkRed color.</returns>
    public static ColorF DarkRed => new(0.55f, 0f, 0f, 1f);
    /// <summary>Gets a pre-defined DarkSalmon color (R:0.91, G:0.59, B:0.48, A:1).</summary>
    /// <returns>A ColorF representing DarkSalmon color.</returns>
    public static ColorF DarkSalmon => new(0.91f, 0.59f, 0.48f, 1f);
    /// <summary>Gets a pre-defined DarkSeaGreen color (R:0.56, G:0.74, B:0.56, A:1).</summary>
    /// <returns>A ColorF representing DarkSeaGreen color.</returns>
    public static ColorF DarkSeaGreen => new(0.56f, 0.74f, 0.56f, 1f);
    /// <summary>Gets a pre-defined DarkSlateBlue color (R:0.28, G:0.24, B:0.55, A:1).</summary>
    /// <returns>A ColorF representing DarkSlateBlue color.</returns>
    public static ColorF DarkSlateBlue => new(0.28f, 0.24f, 0.55f, 1f);
    /// <summary>Gets a pre-defined DarkSlateGray color (R:0.18, G:0.31, B:0.31, A:1).</summary>
    /// <returns>A ColorF representing DarkSlateGray color.</returns>
    public static ColorF DarkSlateGray => new(0.18f, 0.31f, 0.31f, 1f);
    /// <summary>Gets a pre-defined DarkTurquoise color (R:0, G:0.81, B:0.82, A:1).</summary>
    /// <returns>A ColorF representing DarkTurquoise color.</returns>
    public static ColorF DarkTurquoise => new(0f, 0.81f, 0.82f, 1f);
    /// <summary>Gets a pre-defined DarkViolet color (R:0.58, G:0, B:0.83, A:1).</summary>
    /// <returns>A ColorF representing DarkViolet color.</returns>
    public static ColorF DarkViolet => new(0.58f, 0f, 0.83f, 1f);
    /// <summary>Gets a pre-defined DeepPink color (R:1, G:0.08, B:0.58, A:1).</summary>
    /// <returns>A ColorF representing DeepPink color.</returns>
    public static ColorF DeepPink => new(1f, 0.08f, 0.58f, 1f);
    /// <summary>Gets a pre-defined DeepSkyBlue color (R:0, G:0.75, B:1, A:1).</summary>
    /// <returns>A ColorF representing DeepSkyBlue color.</returns>
    public static ColorF DeepSkyBlue => new(0f, 0.75f, 1f, 1f);
    /// <summary>Gets a pre-defined DimGray color (R:0.41, G:0.41, B:0.41, A:1).</summary>
    /// <returns>A ColorF representing DimGray color.</returns>
    public static ColorF DimGray => new(0.41f, 0.41f, 0.41f, 1f);
    /// <summary>Gets a pre-defined DodgerBlue color (R:0.12, G:0.56, B:1, A:1).</summary>
    /// <returns>A ColorF representing DodgerBlue color.</returns>
    public static ColorF DodgerBlue => new(0.12f, 0.56f, 1f, 1f);
    /// <summary>Gets a pre-defined FireBrick color (R:0.7, G:0.13, B:0.13, A:1).</summary>
    /// <returns>A ColorF representing FireBrick color.</returns>
    public static ColorF FireBrick => new(0.7f, 0.13f, 0.13f, 1f);
    /// <summary>Gets a pre-defined FloralWhite color (R:1, G:0.98, B:0.94, A:1).</summary>
    /// <returns>A ColorF representing FloralWhite color.</returns>
    public static ColorF FloralWhite => new(1f, 0.98f, 0.94f, 1f);
    /// <summary>Gets a pre-defined ForestGreen color (R:0.13, G:0.55, B:0.13, A:1).</summary>
    /// <returns>A ColorF representing ForestGreen color.</returns>
    public static ColorF ForestGreen => new(0.13f, 0.55f, 0.13f, 1f);
    /// <summary>Gets a pre-defined Fuchsia color (R:1, G:0, B:1, A:1).</summary>
    /// <returns>A ColorF representing Fuchsia color.</returns>
    public static ColorF Fuchsia => new(1f, 0f, 1f, 1f);
    /// <summary>Gets a pre-defined Gainsboro color (R:0.86, G:0.86, B:0.86, A:1).</summary>
    /// <returns>A ColorF representing Gainsboro color.</returns>
    public static ColorF Gainsboro => new(0.86f, 0.86f, 0.86f, 1f);
    /// <summary>Gets a pre-defined GhostWhite color (R:0.97, G:0.97, B:1, A:1).</summary>
    /// <returns>A ColorF representing GhostWhite color.</returns>
    public static ColorF GhostWhite => new(0.97f, 0.97f, 1f, 1f);
    /// <summary>Gets a pre-defined Gold color (R:1, G:0.84, B:0, A:1).</summary>
    /// <returns>A ColorF representing Gold color.</returns>
    public static ColorF Gold => new(1f, 0.84f, 0f, 1f);
    /// <summary>Gets a pre-defined GoldenRod color (R:0.85, G:0.65, B:0.13, A:1).</summary>
    /// <returns>A ColorF representing GoldenRod color.</returns>
    public static ColorF GoldenRod => new(0.85f, 0.65f, 0.13f, 1f);
    /// <summary>Gets a pre-defined GreenYellow color (R:0.68, G:1, B:0.18, A:1).</summary>
    /// <returns>A ColorF representing GreenYellow color.</returns>
    public static ColorF GreenYellow => new(0.68f, 1f, 0.18f, 1f);
    /// <summary>Gets a pre-defined HoneyDew color (R:0.94, G:1, B:0.94, A:1).</summary>
    /// <returns>A ColorF representing HoneyDew color.</returns>
    public static ColorF HoneyDew => new(0.94f, 1f, 0.94f, 1f);
    /// <summary>Gets a pre-defined HotPink color (R:1, G:0.41, B:0.71, A:1).</summary>
    /// <returns>A ColorF representing HotPink color.</returns>
    public static ColorF HotPink => new(1f, 0.41f, 0.71f, 1f);
    /// <summary>Gets a pre-defined IndianRed color (R:0.8, G:0.36, B:0.36, A:1).</summary>
    /// <returns>A ColorF representing IndianRed color.</returns>
    public static ColorF IndianRed => new(0.8f, 0.36f, 0.36f, 1f);
    /// <summary>Gets a pre-defined Indigo color (R:0.29, G:0, B:0.51, A:1).</summary>
    /// <returns>A ColorF representing Indigo color.</returns>
    public static ColorF Indigo => new(0.29f, 0f, 0.51f, 1f);
    /// <summary>Gets a pre-defined Ivory color (R:1, G:1, B:0.94, A:1).</summary>
    /// <returns>A ColorF representing Ivory color.</returns>
    public static ColorF Ivory => new(1f, 1f, 0.94f, 1f);
    /// <summary>Gets a pre-defined Khaki color (R:0.94, G:0.9, B:0.55, A:1).</summary>
    /// <returns>A ColorF representing Khaki color.</returns>
    public static ColorF Khaki => new(0.94f, 0.9f, 0.55f, 1f);
    /// <summary>Gets a pre-defined Lavender color (R:0.9, G:0.9, B:0.98, A:1).</summary>
    /// <returns>A ColorF representing Lavender color.</returns>
    public static ColorF Lavender => new(0.9f, 0.9f, 0.98f, 1f);
    /// <summary>Gets a pre-defined LavenderBlush color (R:1, G:0.94, B:0.96, A:1).</summary>
    /// <returns>A ColorF representing LavenderBlush color.</returns>
    public static ColorF LavenderBlush => new(1f, 0.94f, 0.96f, 1f);
    /// <summary>Gets a pre-defined LawnGreen color (R:0.49, G:0.99, B:0, A:1).</summary>
    /// <returns>A ColorF representing LawnGreen color.</returns>
    public static ColorF LawnGreen => new(0.49f, 0.99f, 0f, 1f);
    /// <summary>Gets a pre-defined LemonChiffon color (R:1, G:0.98, B:0.8, A:1).</summary>
    /// <returns>A ColorF representing LemonChiffon color.</returns>
    public static ColorF LemonChiffon => new(1f, 0.98f, 0.8f, 1f);
    /// <summary>Gets a pre-defined LightBlue color (R:0.68, G:0.85, B:0.9, A:1).</summary>
    /// <returns>A ColorF representing LightBlue color.</returns>
    public static ColorF LightBlue => new(0.68f, 0.85f, 0.9f, 1f);
    /// <summary>Gets a pre-defined LightCoral color (R:0.94, G:0.5, B:0.5, A:1).</summary>
    /// <returns>A ColorF representing LightCoral color.</returns>
    public static ColorF LightCoral => new(0.94f, 0.5f, 0.5f, 1f);
    /// <summary>Gets a pre-defined LightCyan color (R:0.88, G:1, B:1, A:1).</summary>
    /// <returns>A ColorF representing LightCyan color.</returns>
    public static ColorF LightCyan => new(0.88f, 1f, 1f, 1f);
    /// <summary>Gets a pre-defined LightGoldenRod color (R:0.98, G:0.98, B:0.82, A:1).</summary>
    /// <returns>A ColorF representing LightGoldenRod color.</returns>
    public static ColorF LightGoldenRod => new(0.98f, 0.98f, 0.82f, 1f);
    /// <summary>Gets a pre-defined LightGray color (R:0.83, G:0.83, B:0.83, A:1).</summary>
    /// <returns>A ColorF representing LightGray color.</returns>
    public static ColorF LightGray => new(0.83f, 0.83f, 0.83f, 1f);
    /// <summary>Gets a pre-defined LightGreen color (R:0.56, G:0.93, B:0.56, A:1).</summary>
    /// <returns>A ColorF representing LightGreen color.</returns>
    public static ColorF LightGreen => new(0.56f, 0.93f, 0.56f, 1f);
    /// <summary>Gets a pre-defined LightPink color (R:1, G:0.71, B:0.76, A:1).</summary>
    /// <returns>A ColorF representing LightPink color.</returns>
    public static ColorF LightPink => new(1f, 0.71f, 0.76f, 1f);
    /// <summary>Gets a pre-defined LightSalmon color (R:1, G:0.63, B:0.48, A:1).</summary>
    /// <returns>A ColorF representing LightSalmon color.</returns>
    public static ColorF LightSalmon => new(1f, 0.63f, 0.48f, 1f);
    /// <summary>Gets a pre-defined LightSeaGreen color (R:0.13, G:0.7, B:0.67, A:1).</summary>
    /// <returns>A ColorF representing LightSeaGreen color.</returns>
    public static ColorF LightSeaGreen => new(0.13f, 0.7f, 0.67f, 1f);
    /// <summary>Gets a pre-defined LightSkyBlue color (R:0.53, G:0.81, B:0.98, A:1).</summary>
    /// <returns>A ColorF representing LightSkyBlue color.</returns>
    public static ColorF LightSkyBlue => new(0.53f, 0.81f, 0.98f, 1f);
    /// <summary>Gets a pre-defined LightSlateGray color (R:0.47, G:0.53, B:0.6, A:1).</summary>
    /// <returns>A ColorF representing LightSlateGray color.</returns>
    public static ColorF LightSlateGray => new(0.47f, 0.53f, 0.6f, 1f);
    /// <summary>Gets a pre-defined LightSteelBlue color (R:0.69, G:0.77, B:0.87, A:1).</summary>
    /// <returns>A ColorF representing LightSteelBlue color.</returns>
    public static ColorF LightSteelBlue => new(0.69f, 0.77f, 0.87f, 1f);
    /// <summary>Gets a pre-defined LightYellow color (R:1, G:1, B:0.88, A:1).</summary>
    /// <returns>A ColorF representing LightYellow color.</returns>
    public static ColorF LightYellow => new(1f, 1f, 0.88f, 1f);
    /// <summary>Gets a pre-defined Lime color (R:0, G:1, B:0, A:1).</summary>
    /// <returns>A ColorF representing Lime color.</returns>
    public static ColorF Lime => new(0f, 1f, 0f, 1f);
    /// <summary>Gets a pre-defined LimeGreen color (R:0.2, G:0.8, B:0.2, A:1).</summary>
    /// <returns>A ColorF representing LimeGreen color.</returns>
    public static ColorF LimeGreen => new(0.2f, 0.8f, 0.2f, 1f);
    /// <summary>Gets a pre-defined Linen color (R:0.98, G:0.94, B:0.9, A:1).</summary>
    /// <returns>A ColorF representing Linen color.</returns>
    public static ColorF Linen => new(0.98f, 0.94f, 0.9f, 1f);
    /// <summary>Gets a pre-defined Maroon color (R:0.69, G:0.19, B:0.38, A:1).</summary>
    /// <returns>A ColorF representing Maroon color.</returns>
    public static ColorF Maroon => new(0.69f, 0.19f, 0.38f, 1f);
    /// <summary>Gets a pre-defined MediumAquaMarine color (R:0.4, G:0.8, B:0.67, A:1).</summary>
    /// <returns>A ColorF representing MediumAquaMarine color.</returns>
    public static ColorF MediumAquaMarine => new(0.4f, 0.8f, 0.67f, 1f);
    /// <summary>Gets a pre-defined MediumBlue color (R:0, G:0, B:0.8, A:1).</summary>
    /// <returns>A ColorF representing MediumBlue color.</returns>
    public static ColorF MediumBlue => new(0f, 0f, 0.8f, 1f);
    /// <summary>Gets a pre-defined MediumOrchid color (R:0.73, G:0.33, B:0.83, A:1).</summary>
    /// <returns>A ColorF representing MediumOrchid color.</returns>
    public static ColorF MediumOrchid => new(0.73f, 0.33f, 0.83f, 1f);
    /// <summary>Gets a pre-defined MediumPurple color (R:0.58, G:0.44, B:0.86, A:1).</summary>
    /// <returns>A ColorF representing MediumPurple color.</returns>
    public static ColorF MediumPurple => new(0.58f, 0.44f, 0.86f, 1f);
    /// <summary>Gets a pre-defined MediumSeaGreen color (R:0.24, G:0.7, B:0.44, A:1).</summary>
    /// <returns>A ColorF representing MediumSeaGreen color.</returns>
    public static ColorF MediumSeaGreen => new(0.24f, 0.7f, 0.44f, 1f);
    /// <summary>Gets a pre-defined MediumSlateBlue color (R:0.48, G:0.41, B:0.93, A:1).</summary>
    /// <returns>A ColorF representing MediumSlateBlue color.</returns>
    public static ColorF MediumSlateBlue => new(0.48f, 0.41f, 0.93f, 1f);
    /// <summary>Gets a pre-defined MediumSpringGreen color (R:0, G:0.98, B:0.6, A:1).</summary>
    /// <returns>A ColorF representing MediumSpringGreen color.</returns>
    public static ColorF MediumSpringGreen => new(0f, 0.98f, 0.6f, 1f);
    /// <summary>Gets a pre-defined MediumTurquoise color (R:0.28, G:0.82, B:0.8, A:1).</summary>
    /// <returns>A ColorF representing MediumTurquoise color.</returns>
    public static ColorF MediumTurquoise => new(0.28f, 0.82f, 0.8f, 1f);
    /// <summary>Gets a pre-defined MediumVioletRed color (R:0.78, G:0.08, B:0.52, A:1).</summary>
    /// <returns>A ColorF representing MediumVioletRed color.</returns>
    public static ColorF MediumVioletRed => new(0.78f, 0.08f, 0.52f, 1f);
    /// <summary>Gets a pre-defined MidnightBlue color (R:0.1, G:0.1, B:0.44, A:1).</summary>
    /// <returns>A ColorF representing MidnightBlue color.</returns>
    public static ColorF MidnightBlue => new(0.1f, 0.1f, 0.44f, 1f);
    /// <summary>Gets a pre-defined MintCream color (R:0.96, G:1, B:0.98, A:1).</summary>
    /// <returns>A ColorF representing MintCream color.</returns>
    public static ColorF MintCream => new(0.96f, 1f, 0.98f, 1f);
    /// <summary>Gets a pre-defined MistyRose color (R:1, G:0.89, B:0.88, A:1).</summary>
    /// <returns>A ColorF representing MistyRose color.</returns>
    public static ColorF MistyRose => new(1f, 0.89f, 0.88f, 1f);
    /// <summary>Gets a pre-defined Moccasin color (R:1, G:0.89, B:0.71, A:1).</summary>
    /// <returns>A ColorF representing Moccasin color.</returns>
    public static ColorF Moccasin => new(1f, 0.89f, 0.71f, 1f);
    /// <summary>Gets a pre-defined NavajoWhite color (R:1, G:0.87, B:0.68, A:1).</summary>
    /// <returns>A ColorF representing NavajoWhite color.</returns>
    public static ColorF NavajoWhite => new(1f, 0.87f, 0.68f, 1f);
    /// <summary>Gets a pre-defined NavyBlue color (R:0, G:0, B:0.5, A:1).</summary>
    /// <returns>A ColorF representing NavyBlue color.</returns>
    public static ColorF NavyBlue => new(0f, 0f, 0.5f, 1f);
    /// <summary>Gets a pre-defined OldLace color (R:0.99, G:0.96, B:0.9, A:1).</summary>
    /// <returns>A ColorF representing OldLace color.</returns>
    public static ColorF OldLace => new(0.99f, 0.96f, 0.9f, 1f);
    /// <summary>Gets a pre-defined Olive color (R:0.5, G:0.5, B:0, A:1).</summary>
    /// <returns>A ColorF representing Olive color.</returns>
    public static ColorF Olive => new(0.5f, 0.5f, 0f, 1f);
    /// <summary>Gets a pre-defined OliveDrab color (R:0.42, G:0.56, B:0.14, A:1).</summary>
    /// <returns>A ColorF representing OliveDrab color.</returns>
    public static ColorF OliveDrab => new(0.42f, 0.56f, 0.14f, 1f);
    /// <summary>Gets a pre-defined Orange color (R:1, G:0.65, B:0, A:1).</summary>
    /// <returns>A ColorF representing Orange color.</returns>
    public static ColorF Orange => new(1f, 0.65f, 0f, 1f);
    /// <summary>Gets a pre-defined OrangeRed color (R:1, G:0.27, B:0, A:1).</summary>
    /// <returns>A ColorF representing OrangeRed color.</returns>
    public static ColorF OrangeRed => new(1f, 0.27f, 0f, 1f);
    /// <summary>Gets a pre-defined Orchid color (R:0.85, G:0.44, B:0.84, A:1).</summary>
    /// <returns>A ColorF representing Orchid color.</returns>
    public static ColorF Orchid => new(0.85f, 0.44f, 0.84f, 1f);
    /// <summary>Gets a pre-defined PaleGoldenRod color (R:0.93, G:0.91, B:0.67, A:1).</summary>
    /// <returns>A ColorF representing PaleGoldenRod color.</returns>
    public static ColorF PaleGoldenRod => new(0.93f, 0.91f, 0.67f, 1f);
    /// <summary>Gets a pre-defined PaleGreen color (R:0.6, G:0.98, B:0.6, A:1).</summary>
    /// <returns>A ColorF representing PaleGreen color.</returns>
    public static ColorF PaleGreen => new(0.6f, 0.98f, 0.6f, 1f);
    /// <summary>Gets a pre-defined PaleTurquoise color (R:0.69, G:0.93, B:0.93, A:1).</summary>
    /// <returns>A ColorF representing PaleTurquoise color.</returns>
    public static ColorF PaleTurquoise => new(0.69f, 0.93f, 0.93f, 1f);
    /// <summary>Gets a pre-defined PaleVioletRed color (R:0.86, G:0.44, B:0.58, A:1).</summary>
    /// <returns>A ColorF representing PaleVioletRed color.</returns>
    public static ColorF PaleVioletRed => new(0.86f, 0.44f, 0.58f, 1f);
    /// <summary>Gets a pre-defined PapayaWhip color (R:1, G:0.94, B:0.84, A:1).</summary>
    /// <returns>A ColorF representing PapayaWhip color.</returns>
    public static ColorF PapayaWhip => new(1f, 0.94f, 0.84f, 1f);
    /// <summary>Gets a pre-defined PeachPuff color (R:1, G:0.85, B:0.73, A:1).</summary>
    /// <returns>A ColorF representing PeachPuff color.</returns>
    public static ColorF PeachPuff => new(1f, 0.85f, 0.73f, 1f);
    /// <summary>Gets a pre-defined Peru color (R:0.8, G:0.52, B:0.25, A:1).</summary>
    /// <returns>A ColorF representing Peru color.</returns>
    public static ColorF Peru => new(0.8f, 0.52f, 0.25f, 1f);
    /// <summary>Gets a pre-defined Pink color (R:1, G:0.75, B:0.8, A:1).</summary>
    /// <returns>A ColorF representing Pink color.</returns>
    public static ColorF Pink => new(1f, 0.75f, 0.8f, 1f);
    /// <summary>Gets a pre-defined Plum color (R:0.87, G:0.63, B:0.87, A:1).</summary>
    /// <returns>A ColorF representing Plum color.</returns>
    public static ColorF Plum => new(0.87f, 0.63f, 0.87f, 1f);
    /// <summary>Gets a pre-defined PowderBlue color (R:0.69, G:0.88, B:0.9, A:1).</summary>
    /// <returns>A ColorF representing PowderBlue color.</returns>
    public static ColorF PowderBlue => new(0.69f, 0.88f, 0.9f, 1f);
    /// <summary>Gets a pre-defined Purple color (R:0.63, G:0.13, B:0.94, A:1).</summary>
    /// <returns>A ColorF representing Purple color.</returns>
    public static ColorF Purple => new(0.63f, 0.13f, 0.94f, 1f);
    /// <summary>Gets a pre-defined RebeccaPurple color (R:0.4, G:0.2, B:0.6, A:1).</summary>
    /// <returns>A ColorF representing RebeccaPurple color.</returns>
    public static ColorF RebeccaPurple => new(0.4f, 0.2f, 0.6f, 1f);
    /// <summary>Gets a pre-defined RosyBrown color (R:0.74, G:0.56, B:0.56, A:1).</summary>
    /// <returns>A ColorF representing RosyBrown color.</returns>
    public static ColorF RosyBrown => new(0.74f, 0.56f, 0.56f, 1f);
    /// <summary>Gets a pre-defined RoyalBlue color (R:0.25, G:0.41, B:0.88, A:1).</summary>
    /// <returns>A ColorF representing RoyalBlue color.</returns>
    public static ColorF RoyalBlue => new(0.25f, 0.41f, 0.88f, 1f);
    /// <summary>Gets a pre-defined SaddleBrown color (R:0.55, G:0.27, B:0.07, A:1).</summary>
    /// <returns>A ColorF representing SaddleBrown color.</returns>
    public static ColorF SaddleBrown => new(0.55f, 0.27f, 0.07f, 1f);
    /// <summary>Gets a pre-defined Salmon color (R:0.98, G:0.5, B:0.45, A:1).</summary>
    /// <returns>A ColorF representing Salmon color.</returns>
    public static ColorF Salmon => new(0.98f, 0.5f, 0.45f, 1f);
    /// <summary>Gets a pre-defined SandyBrown color (R:0.96, G:0.64, B:0.38, A:1).</summary>
    /// <returns>A ColorF representing SandyBrown color.</returns>
    public static ColorF SandyBrown => new(0.96f, 0.64f, 0.38f, 1f);
    /// <summary>Gets a pre-defined SeaGreen color (R:0.18, G:0.55, B:0.34, A:1).</summary>
    /// <returns>A ColorF representing SeaGreen color.</returns>
    public static ColorF SeaGreen => new(0.18f, 0.55f, 0.34f, 1f);
    /// <summary>Gets a pre-defined SeaShell color (R:1, G:0.96, B:0.93, A:1).</summary>
    /// <returns>A ColorF representing SeaShell color.</returns>
    public static ColorF SeaShell => new(1f, 0.96f, 0.93f, 1f);
    /// <summary>Gets a pre-defined Sienna color (R:0.63, G:0.32, B:0.18, A:1).</summary>
    /// <returns>A ColorF representing Sienna color.</returns>
    public static ColorF Sienna => new(0.63f, 0.32f, 0.18f, 1f);
    /// <summary>Gets a pre-defined Silver color (R:0.75, G:0.75, B:0.75, A:1).</summary>
    /// <returns>A ColorF representing Silver color.</returns>
    public static ColorF Silver => new(0.75f, 0.75f, 0.75f, 1f);
    /// <summary>Gets a pre-defined SkyBlue color (R:0.53, G:0.81, B:0.92, A:1).</summary>
    /// <returns>A ColorF representing SkyBlue color.</returns>
    public static ColorF SkyBlue => new(0.53f, 0.81f, 0.92f, 1f);
    /// <summary>Gets a pre-defined SlateBlue color (R:0.42, G:0.35, B:0.8, A:1).</summary>
    /// <returns>A ColorF representing SlateBlue color.</returns>
    public static ColorF SlateBlue => new(0.42f, 0.35f, 0.8f, 1f);
    /// <summary>Gets a pre-defined SlateGray color (R:0.44, G:0.5, B:0.56, A:1).</summary>
    /// <returns>A ColorF representing SlateGray color.</returns>
    public static ColorF SlateGray => new(0.44f, 0.5f, 0.56f, 1f);
    /// <summary>Gets a pre-defined Snow color (R:1, G:0.98, B:0.98, A:1).</summary>
    /// <returns>A ColorF representing Snow color.</returns>
    public static ColorF Snow => new(1f, 0.98f, 0.98f, 1f);
    /// <summary>Gets a pre-defined SpringGreen color (R:0, G:1, B:0.5, A:1).</summary>
    /// <returns>A ColorF representing SpringGreen color.</returns>
    public static ColorF SpringGreen => new(0f, 1f, 0.5f, 1f);
    /// <summary>Gets a pre-defined SteelBlue color (R:0.27, G:0.51, B:0.71, A:1).</summary>
    /// <returns>A ColorF representing SteelBlue color.</returns>
    public static ColorF SteelBlue => new(0.27f, 0.51f, 0.71f, 1f);
    /// <summary>Gets a pre-defined Tan color (R:0.82, G:0.71, B:0.55, A:1).</summary>
    /// <returns>A ColorF representing Tan color.</returns>
    public static ColorF Tan => new(0.82f, 0.71f, 0.55f, 1f);
    /// <summary>Gets a pre-defined Teal color (R:0, G:0.5, B:0.5, A:1).</summary>
    /// <returns>A ColorF representing Teal color.</returns>
    public static ColorF Teal => new(0f, 0.5f, 0.5f, 1f);
    /// <summary>Gets a pre-defined Thistle color (R:0.85, G:0.75, B:0.85, A:1).</summary>
    /// <returns>A ColorF representing Thistle color.</returns>
    public static ColorF Thistle => new(0.85f, 0.75f, 0.85f, 1f);
    /// <summary>Gets a pre-defined Tomato color (R:1, G:0.39, B:0.28, A:1).</summary>
    /// <returns>A ColorF representing Tomato color.</returns>
    public static ColorF Tomato => new(1f, 0.39f, 0.28f, 1f);
    /// <summary>Gets a pre-defined Turquoise color (R:0.25, G:0.88, B:0.82, A:1).</summary>
    /// <returns>A ColorF representing Turquoise color.</returns>
    public static ColorF Turquoise => new(0.25f, 0.88f, 0.82f, 1f);
    /// <summary>Gets a pre-defined Violet color (R:0.93, G:0.51, B:0.93, A:1).</summary>
    /// <returns>A ColorF representing Violet color.</returns>
    public static ColorF Violet => new(0.93f, 0.51f, 0.93f, 1f);
    /// <summary>Gets a pre-defined WebGreen color (R:0, G:0.5, B:0, A:1).</summary>
    /// <returns>A ColorF representing WebGreen color.</returns>
    public static ColorF WebGreen => new(0f, 0.5f, 0f, 1f);
    /// <summary>Gets a pre-defined WebGray color (R:0.5, G:0.5, B:0.5, A:1).</summary>
    /// <returns>A ColorF representing WebGray color.</returns>
    public static ColorF WebGray => new(0.5f, 0.5f, 0.5f, 1f);
    /// <summary>Gets a pre-defined WebMaroon color (R:0.5, G:0, B:0, A:1).</summary>
    /// <returns>A ColorF representing WebMaroon color.</returns>
    public static ColorF WebMaroon => new(0.5f, 0f, 0f, 1f);
    /// <summary>Gets a pre-defined WebPurple color (R:0.5, G:0, B:0.5, A:1).</summary>
    /// <returns>A ColorF representing WebPurple color.</returns>
    public static ColorF WebPurple => new(0.5f, 0f, 0.5f, 1f);
    /// <summary>Gets a pre-defined Wheat color (R:0.96, G:0.87, B:0.7, A:1).</summary>
    /// <returns>A ColorF representing Wheat color.</returns>
    public static ColorF Wheat => new(0.96f, 0.87f, 0.7f, 1f);
    /// <summary>Gets a pre-defined WhiteSmoke color (R:0.96, G:0.96, B:0.96, A:1).</summary>
    /// <returns>A ColorF representing WhiteSmoke color.</returns>
    public static ColorF WhiteSmoke => new(0.96f, 0.96f, 0.96f, 1f);
    /// <summary>Gets a pre-defined YellowGreen color (R:0.6, G:0.8, B:0.2, A:1).</summary>
    /// <returns>A ColorF representing YellowGreen color.</returns>
    public static ColorF YellowGreen => new(0.6f, 0.8f, 0.2f, 1f);
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
        else
        {
            if (c.g >= max) h = 2f + (c.b - c.r) / delta;
            else h = 4f + (c.r - c.g) / delta;
        }

        h *= 60f;
        if (h < 0f) h += 360f;

        return new(h, s, v, c.a);
    }
    /// <summary>Adds two <seealso cref="ColorF"/> values component-wise.</summary>
    /// <param name="A">The first <seealso cref="ColorF"/> value.</param>
    /// <param name="B">The second <seealso cref="ColorF"/> value to add.</param>
    /// <returns>A new <seealso cref="ColorF"/> with the sum of each component.</returns>
    public static ColorF operator +(ColorF A, ColorF B) {
        A.r += B.r;
        A.g += B.g;
        A.b += B.b;
        A.a += B.a;
        return A;
    }
    /// <summary>Adds a <seealso cref="Color"/> and a <seealso cref="ColorF"/> value component-wise.</summary>
    /// <param name="A">The first <seealso cref="Color"/> value.</param>
    /// <param name="B">The second <seealso cref="ColorF"/> value to add.</param>
    /// <returns>A new <seealso cref="ColorF"/> with the sum of each component.</returns>
    public static ColorF operator +(Color A, ColorF B) {
        A.r += B.r;
        A.g += B.g;
        A.b += B.b;
        A.a += B.a;
        return A;
    }
    /// <summary>Adds a <seealso cref="ColorF"/> and a <seealso cref="Color"/> value component-wise.</summary>
    /// <param name="A">The first <seealso cref="ColorF"/> value.</param>
    /// <param name="B">The second <seealso cref="Color"/> value to add.</param>
    /// <returns>A new <seealso cref="ColorF"/> with the sum of each component.</returns>
    public static ColorF operator +(ColorF A, Color B) {
        A.r += B.r;
        A.g += B.g;
        A.b += B.b;
        A.a += B.a;
        return A;
    }
    /// <summary>Subtracts two <seealso cref="ColorF"/> values component-wise.</summary>
    /// <param name="A">The first <seealso cref="ColorF"/> value.</param>
    /// <param name="B">The second <seealso cref="ColorF"/> value to subtract.</param>
    /// <returns>A new <seealso cref="ColorF"/> with the difference of each component.</returns>
    public static ColorF operator -(ColorF A, ColorF B) {
        A.r -= B.r;
        A.g -= B.g;
        A.b -= B.b;
        A.a -= B.a;
        return A;
    }
    /// <summary>Subtracts a <seealso cref="ColorF"/> from a <seealso cref="Color"/> value component-wise.</summary>
    /// <param name="A">The first <seealso cref="Color"/> value.</param>
    /// <param name="B">The second <seealso cref="ColorF"/> value to subtract.</param>
    /// <returns>A new <seealso cref="ColorF"/> with the difference of each component.</returns>
    public static ColorF operator -(Color A, ColorF B) {
        A.r -= B.r;
        A.g -= B.g;
        A.b -= B.b;
        A.a -= B.a;
        return A;
    }
    /// <summary>Subtracts a <seealso cref="Color"/> from a <seealso cref="ColorF"/> value component-wise.</summary>
    /// <param name="A">The first <seealso cref="ColorF"/> value.</param>
    /// <param name="B">The second <seealso cref="Color"/> value to subtract.</param>
    /// <returns>A new <seealso cref="ColorF"/> with the difference of each component.</returns>
    public static ColorF operator -(ColorF A, Color B) {
        A.r -= B.r;
        A.g -= B.g;
        A.b -= B.b;
        A.a -= B.a;
        return A;
    }
    /// <summary>Negates all components of a <seealso cref="ColorF"/> value.</summary>
    /// <param name="A">The <seealso cref="ColorF"/> value to negate.</param>
    /// <returns>A new <seealso cref="ColorF"/> with negated components.</returns>
    public static ColorF operator -(ColorF A) {
        A.r = -A.r;
        A.g = -A.g;
        A.b = -A.b;
        A.a = -A.a;
        return A;
    }
    /// <summary>Multiplies two <seealso cref="ColorF"/> values component-wise.</summary>
    /// <param name="A">The first <seealso cref="ColorF"/> value.</param>
    /// <param name="B">The second <seealso cref="ColorF"/> value to multiply.</param>
    /// <returns>A new <seealso cref="ColorF"/> with the product of each component.</returns>
    public static ColorF operator *(ColorF A, ColorF B) {
        A.r *= B.r;
        A.g *= B.g;
        A.b *= B.b;
        A.a *= B.a;
        return A;
    }
    /// <summary>Multiplies a <seealso cref="Color"/> and a <seealso cref="ColorF"/> value component-wise.</summary>
    /// <param name="A">The first <seealso cref="Color"/> value.</param>
    /// <param name="B">The second <seealso cref="ColorF"/> value to multiply.</param>
    /// <returns>A new <seealso cref="ColorF"/> with the product of each component.</returns>
    public static ColorF operator *(Color A, ColorF B) {
        A.r *= B.r;
        A.g *= B.g;
        A.b *= B.b;
        A.a *= B.a;
        return A;
    }
    /// <summary>Multiplies a <seealso cref="ColorF"/> and a <seealso cref="Color"/> value component-wise.</summary>
    /// <param name="A">The first <seealso cref="ColorF"/> value.</param>
    /// <param name="B">The second <seealso cref="Color"/> value to multiply.</param>
    /// <returns>A new <seealso cref="ColorF"/> with the product of each component.</returns>
    public static ColorF operator *(ColorF A, Color B) {
        A.r *= B.r;
        A.g *= B.g;
        A.b *= B.b;
        A.a *= B.a;
        return A;
    }
    /// <summary>Multiplies a <seealso cref="ColorF"/> by a scalar value.</summary>
    /// <param name="scale">The scalar value to multiply.</param>
    /// <param name="A">The <seealso cref="ColorF"/> value to scale.</param>
    /// <returns>A new <seealso cref="ColorF"/> with each component multiplied by the scalar.</returns>
    public static ColorF operator *(float scale, ColorF A) {
        A.r *= scale;
        A.g *= scale;
        A.b *= scale;
        A.a *= scale;
        return A;
    }
    /// <summary>Multiplies a <seealso cref="ColorF"/> by a scalar value.</summary>
    /// <param name="A">The <seealso cref="ColorF"/> value to scale.</param>
    /// <param name="scale">The scalar value to multiply.</param>
    /// <returns>A new <seealso cref="ColorF"/> with each component multiplied by the scalar.</returns>
    public static ColorF operator *(ColorF A, float scale) {
        A.r *= scale;
        A.g *= scale;
        A.b *= scale;
        A.a *= scale;
        return A;
    }
    /// <summary>Divides two <seealso cref="ColorF"/> values component-wise.</summary>
    /// <param name="A">The first <seealso cref="ColorF"/> value.</param>
    /// <param name="B">The second <seealso cref="ColorF"/> value to divide by.</param>
    /// <returns>A new <seealso cref="ColorF"/> with the quotient of each component.</returns>
    public static ColorF operator /(ColorF A, ColorF B) {
        A.r /= B.r;
        A.g /= B.g;
        A.b /= B.b;
        A.a /= B.a;
        return A;
    }
    /// <summary>Divides a <seealso cref="Color"/> by a <seealso cref="ColorF"/> value component-wise.</summary>
    /// <param name="A">The first <seealso cref="Color"/> value.</param>
    /// <param name="B">The second <seealso cref="ColorF"/> value to divide by.</param>
    /// <returns>A new <seealso cref="ColorF"/> with the quotient of each component.</returns>
    public static ColorF operator /(Color A, ColorF B) {
        A.r /= B.r;
        A.g /= B.g;
        A.b /= B.b;
        A.a /= B.a;
        return A;
    }
    /// <summary>Divides a <seealso cref="ColorF"/> by a <seealso cref="Color"/> value component-wise.</summary>
    /// <param name="A">The first <seealso cref="ColorF"/> value.</param>
    /// <param name="B">The second <seealso cref="Color"/> value to divide by.</param>
    /// <returns>A new <seealso cref="ColorF"/> with the quotient of each component.</returns>
    public static ColorF operator /(ColorF A, Color B) {
        A.r /= B.r;
        A.g /= B.g;
        A.b /= B.b;
        A.a /= B.a;
        return A;
    }
    /// <summary>Divides a scalar value by a <seealso cref="ColorF"/> component-wise.</summary>
    /// <param name="scale">The scalar value to divide.</param>
    /// <param name="A">The <seealso cref="ColorF"/> value to divide by.</param>
    /// <returns>A new <seealso cref="ColorF"/> with the scalar divided by each component.</returns>
    public static ColorF operator /(float scale, ColorF A) {
        A.r /= scale;
        A.g /= scale;
        A.b /= scale;
        A.a /= scale;
        return A;
    }
    /// <summary>Divides a <seealso cref="ColorF"/> by a scalar value component-wise.</summary>
    /// <param name="A">The <seealso cref="ColorF"/> value to divide.</param>
    /// <param name="scale">The scalar value to divide by.</param>
    /// <returns>A new <seealso cref="ColorF"/> with each component divided by the scalar.</returns>
    public static ColorF operator /(ColorF A, float scale) {
        A.r /= scale;
        A.g /= scale;
        A.b /= scale;
        A.a /= scale;
        return A;
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
	/// <summary>Explicitly converts a <see cref="ColorF"/> structure to a Godot <see cref="Color"/>.</summary>
	/// <param name="c">The <see cref="ColorF"/> structure to convert.</param>
	/// <returns>A Godot <see cref="Color"/> with equivalent RGBA values.</returns>
	/// <remarks>
	/// This conversion uses the floating-point RGBA values from the ColorF structure
	/// to create a corresponding Godot color instance with the same color values.
	/// </remarks>
	public static explicit operator Color(ColorF c) => new(c.r, c.g, c.b, c.a);
}
