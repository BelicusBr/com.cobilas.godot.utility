using Godot;
using System.Collections.Generic;

namespace Cobilas.GodotEngine.Utility;
/// <summary>Gizmos are used to give visual debugging or setup aids in the Scene view.</summary>
public static class Gizmos {

    /// <summary>Sets the Color of the gizmos that are drawn next.</summary>
    /// <value>Returns or sets the color of the next gizmo.</value>
    public static Color Color {
        get => InternalGizmos.Color;
        set => InternalGizmos.Color = value;
    }
    /// <summary>
    /// Draws a line from a 2D point to another, with a given color and width. It can
    /// be optionally antialiased. See also <seealso cref="CanvasItem.DrawMultiline(Vector2[], Color, float, bool)"/>
    /// and <seealso cref="CanvasItem.DrawPolyline(Vector2[], Color, float, bool)"/>.
    /// <para>Note: Line drawing is not accelerated by batching if <c>antialiased</c> is <c>true</c>.</para>
    /// <para>Note: Due to how it works, built-in antialiasing will not look correct for translucent lines and may not work on certain platforms. As a workaround, install the <a href="https://github.com/godot-extended-libraries/godot-antialiased-line2d">Antialiased Line2D</a> add-on then create an AntialiasedLine2D node. That node relies on a texture with custom mipmaps to perform antialiasing. 2D batching is also still supported with those antialiased lines.</para>
    /// </summary>
    /// <param name="start">The beginning of the line.</param>
    /// <param name="end">The end of the line.</param>
    /// <param name="width">The thickness of the line.</param>
    public static void DrawLine(Vector2 start, Vector2 end, float width)
        => InternalGizmos.DrawLine(start, end, width);
    /// <summary>
    /// Draws a line from a 2D point to another, with a given color and width. It can
    /// be optionally antialiased. See also <seealso cref="CanvasItem.DrawMultiline(Vector2[], Color, float, bool)"/>
    /// and <seealso cref="CanvasItem.DrawPolyline(Vector2[], Color, float, bool)"/>.
    /// <para>Note: Line drawing is not accelerated by batching if <c>antialiased</c> is <c>true</c>.</para>
    /// <para>Note: Due to how it works, built-in antialiasing will not look correct for translucent lines and may not work on certain platforms. As a workaround, install the <a href="https://github.com/godot-extended-libraries/godot-antialiased-line2d">Antialiased Line2D</a> add-on then create an AntialiasedLine2D node. That node relies on a texture with custom mipmaps to perform antialiasing. 2D batching is also still supported with those antialiased lines.</para>
    /// </summary>
    /// <param name="start">The beginning of the line.</param>
    /// <param name="end">The end of the line.</param>
    public static void DrawLine(Vector2 start, Vector2 end)
        => InternalGizmos.DrawLine(start, end);
    /// <summary>
    /// <para>Draws a solid body rectangle.</para>
    /// <para>Note: Due to how it works, built-in antialiasing will not look correct for translucent polygons and may not work on certain platforms. As a workaround, install the <a href="https://github.com/godot-extended-libraries/godot-antialiased-line2d">Antialiased Line2D</a> add-on then create an AntialiasedPolygon2D node. That node relies on a texture with custom mipmaps to perform antialiasing.</para>
    /// </summary>
    /// <param name="rect">The dimensions of the rectangle.</param>
    public static void DrawRect(Rect2 rect)
        => InternalGizmos.DrawRect(rect);
    /// <summary>
    /// Draw a rectangle of a skeletonized body.
    /// <para>Note: Due to how it works, built-in antialiasing will not look correct for translucent polygons and may not work on certain platforms. As a workaround, install the <a href="https://github.com/godot-extended-libraries/godot-antialiased-line2d">Antialiased Line2D</a> add-on then create an AntialiasedPolygon2D node. That node relies on a texture with custom mipmaps to perform antialiasing.</para>
    /// </summary>
    /// <param name="rect">The dimensions of the rectangle.</param>
    /// <param name="width">The thickness of the line.</param>
    public static void DrawWireRect(Rect2 rect, float width)
        => InternalGizmos.DrawWireRect(rect, width);
    /// <summary>
    /// Draw a rectangle of a skeletonized body.
    /// <para>Note: Due to how it works, built-in antialiasing will not look correct for translucent polygons and may not work on certain platforms. As a workaround, install the <a href="https://github.com/godot-extended-libraries/godot-antialiased-line2d">Antialiased Line2D</a> add-on then create an AntialiasedPolygon2D node. That node relies on a texture with custom mipmaps to perform antialiasing.</para>
    /// </summary>
    /// <param name="rect">The dimensions of the rectangle.</param>
    public static void DrawWireRect(Rect2 rect) => InternalGizmos.DrawWireRect(rect);
    /// <summary>
    /// <para>Draws a colored, filled circle. See also <see cref="M:Godot.CanvasItem.DrawArc(Godot.Vector2,System.Single,System.Single,System.Single,System.Int32,Godot.Color,System.Single,System.Boolean)" />, <see cref="M:Godot.CanvasItem.DrawPolyline(Godot.Vector2[],Godot.Color,System.Single,System.Boolean)" /> and <see cref="M:Godot.CanvasItem.DrawPolygon(Godot.Vector2[],Godot.Color[],Godot.Vector2[],Godot.Texture,Godot.Texture,System.Boolean)" />.</para>
    /// <para>Note: Built-in antialiasing is not provided for <see cref="M:Godot.CanvasItem.DrawCircle(Godot.Vector2,System.Single,Godot.Color)" />. As a workaround, install the <a href="https://github.com/godot-extended-libraries/godot-antialiased-line2d">Antialiased Line2D</a> add-on then create an AntialiasedRegularPolygon2D node. That node relies on a texture with custom mipmaps to perform antialiasing.</para>
    /// </summary>
    /// <param name="position">The central position of the circle.</param>
    /// <param name="radius">The radius of the circle.</param>
    public static void DrawCircle(Vector2 position, float radius)
        => InternalGizmos.DrawCircle(position, radius);
    /// <summary>
    /// <para>Draws multiple disconnected lines with a uniform <c>color</c>. When drawing large amounts of lines, this is faster than using individual <see cref="M:Godot.CanvasItem.DrawLine(Godot.Vector2,Godot.Vector2,Godot.Color,System.Single,System.Boolean)" /> calls. To draw interconnected lines, use <see cref="M:Godot.CanvasItem.DrawPolyline(Godot.Vector2[],Godot.Color,System.Single,System.Boolean)" /> instead.</para>
    /// <para>Note: <c>width</c> and <c>antialiased</c> are currently not implemented and have no effect. As a workaround, install the <a href="https://github.com/godot-extended-libraries/godot-antialiased-line2d">Antialiased Line2D</a> add-on then create an AntialiasedLine2D node. That node relies on a texture with custom mipmaps to perform antialiasing. 2D batching is also still supported with those antialiased lines.</para>
    /// </summary>
    /// <param name="points">The points that form the line.</param>
    /// <param name="width">The thickness of the line.</param>
    public static void DrawMultiline(Vector2[] points, float width)
        => InternalGizmos.DrawMultiline(points, width);
    /// <summary>
    /// <para>Draws multiple disconnected lines with a uniform <c>color</c>. When drawing large amounts of lines, this is faster than using individual <see cref="M:Godot.CanvasItem.DrawLine(Godot.Vector2,Godot.Vector2,Godot.Color,System.Single,System.Boolean)" /> calls. To draw interconnected lines, use <see cref="M:Godot.CanvasItem.DrawPolyline(Godot.Vector2[],Godot.Color,System.Single,System.Boolean)" /> instead.</para>
    /// <para>Note: <c>width</c> and <c>antialiased</c> are currently not implemented and have no effect. As a workaround, install the <a href="https://github.com/godot-extended-libraries/godot-antialiased-line2d">Antialiased Line2D</a> add-on then create an AntialiasedLine2D node. That node relies on a texture with custom mipmaps to perform antialiasing. 2D batching is also still supported with those antialiased lines.</para>
    /// </summary>
    /// <param name="points">The points that form the line.</param>
    public static void DrawMultiline(Vector2[] points) => InternalGizmos.DrawMultiline(points);
    /// <summary>
    /// <para>Draws multiple disconnected lines with a uniform <c>color</c>. When drawing large amounts of lines, this is faster than using individual <see cref="M:Godot.CanvasItem.DrawLine(Godot.Vector2,Godot.Vector2,Godot.Color,System.Single,System.Boolean)" /> calls. To draw interconnected lines, use <see cref="M:Godot.CanvasItem.DrawPolyline(Godot.Vector2[],Godot.Color,System.Single,System.Boolean)" /> instead.</para>
    /// <para>Note: <c>width</c> and <c>antialiased</c> are currently not implemented and have no effect. As a workaround, install the <a href="https://github.com/godot-extended-libraries/godot-antialiased-line2d">Antialiased Line2D</a> add-on then create an AntialiasedLine2D node. That node relies on a texture with custom mipmaps to perform antialiasing. 2D batching is also still supported with those antialiased lines.</para>
    /// </summary>
    /// <param name="points">The points that form the line.</param>
    /// <param name="width">The thickness of the line.</param>
    public static void DrawMultiline(List<Vector2> points, float width) => DrawMultiline(points.ToArray(), width);
    /// <summary>
    /// <para>Draws multiple disconnected lines with a uniform <c>color</c>. When drawing large amounts of lines, this is faster than using individual <see cref="M:Godot.CanvasItem.DrawLine(Godot.Vector2,Godot.Vector2,Godot.Color,System.Single,System.Boolean)" /> calls. To draw interconnected lines, use <see cref="M:Godot.CanvasItem.DrawPolyline(Godot.Vector2[],Godot.Color,System.Single,System.Boolean)" /> instead.</para>
    /// <para>Note: <c>width</c> and <c>antialiased</c> are currently not implemented and have no effect. As a workaround, install the <a href="https://github.com/godot-extended-libraries/godot-antialiased-line2d">Antialiased Line2D</a> add-on then create an AntialiasedLine2D node. That node relies on a texture with custom mipmaps to perform antialiasing. 2D batching is also still supported with those antialiased lines.</para>
    /// </summary>
    /// <param name="points">The points that form the line.</param>
    public static void DrawMultiline(List<Vector2> points) => DrawMultiline(points, 1f);
    /// <summary>
    /// <para>Draws a unfilled arc between the given angles. The larger the value of <c>point_count</c>, the smoother the curve. See also <see cref="M:Godot.CanvasItem.DrawCircle(Godot.Vector2,System.Single,Godot.Color)" />.</para>
    /// <para>Note: Line drawing is not accelerated by batching if <c>antialiased</c> is <c>true</c>.</para>
    /// <para>Note: Due to how it works, built-in antialiasing will not look correct for translucent lines and may not work on certain platforms. As a workaround, install the <a href="https://github.com/godot-extended-libraries/godot-antialiased-line2d">Antialiased Line2D</a> add-on then create an AntialiasedRegularPolygon2D node. That node relies on a texture with custom mipmaps to perform antialiasing. 2D batching is also still supported with those antialiased lines.</para>
    /// </summary>
    /// <param name="center">The central position of the arc.</param>
    /// <param name="radius">The radius of the arc.</param>
    /// <param name="startAngle">The angle at which the arc will be drawn.</param>
    /// <param name="endAngle">The angle where the arc will finish being drawn.</param>
    /// <param name="pointCount">The number of stitches used in the arc.
    /// <para>This parameter will define the visual aspect of the bow, whether it will have a smoother or more serrated curve.</para>
    /// </param>
    /// <param name="width">The thickness of the line.</param>
    public static void DrawArc(Vector2 center, float radius, float startAngle, float endAngle, int pointCount, float width)
        => InternalGizmos.DrawArc(center, radius, startAngle, endAngle, pointCount, width);
    /// <summary>
    /// <para>Draws a unfilled arc between the given angles. The larger the value of <c>point_count</c>, the smoother the curve. See also <see cref="M:Godot.CanvasItem.DrawCircle(Godot.Vector2,System.Single,Godot.Color)" />.</para>
    /// <para>Note: Line drawing is not accelerated by batching if <c>antialiased</c> is <c>true</c>.</para>
    /// <para>Note: Due to how it works, built-in antialiasing will not look correct for translucent lines and may not work on certain platforms. As a workaround, install the <a href="https://github.com/godot-extended-libraries/godot-antialiased-line2d">Antialiased Line2D</a> add-on then create an AntialiasedRegularPolygon2D node. That node relies on a texture with custom mipmaps to perform antialiasing. 2D batching is also still supported with those antialiased lines.</para>
    /// </summary>
    /// <param name="center">The central position of the arc.</param>
    /// <param name="radius">The radius of the arc.</param>
    /// <param name="startAngle">The angle at which the arc will be drawn.</param>
    /// <param name="endAngle">The angle where the arc will finish being drawn.</param>
    /// <param name="pointCount">The number of stitches used in the arc.
    /// <para>This parameter will define the visual aspect of the bow, whether it will have a smoother or more serrated curve.</para>
    /// </param>
    public static void DrawArc(Vector2 center, float radius, float startAngle, float endAngle, int pointCount)
        => InternalGizmos.DrawArc(center, radius, startAngle, endAngle, pointCount);
    /// <summary>
    /// <para>Draws a <see cref="T:Godot.Mesh" /> in 2D, using the provided texture. See <see cref="T:Godot.MeshInstance2D" /> for related documentation.</para>
    /// </summary>
    /// <param name="mesh">The mesh that will be used to shape the design.</param>
    /// <param name="texture">The texture that will be used to draw on the mesh.</param>
    /// <param name="normalMap">The texture normal map.</param>
    /// <param name="transform">If the parameter is null, then the default value is Transform2D.Identity</param>
    public static void DrawMesh(Mesh mesh, Texture texture, Texture? normalMap = null, Transform2D? transform = null)
        => InternalGizmos.DrawMesh(mesh, texture, normalMap, transform);
    /// <summary>
    /// <para>Draws a texture at a given position.</para>
    /// </summary>
    public static void DrawTexture(Texture texture, Vector2 position, Texture? normalMap = null)
        => InternalGizmos.DrawTexture(texture, position, normalMap);

    /// <summary>
    /// <para>Draws a textured rectangle at a given position, optionally modulated by a color. If <c>transpose</c> is <c>true</c>, the texture will have its X and Y coordinates swapped.</para>
    /// </summary>
    public static void DrawTextureRect(Texture texture, Rect2 rect, bool tile, bool transpose = false, Texture? normalMap = null)
        => InternalGizmos.DrawTextureRect(texture, rect, tile, transpose, normalMap);
}
