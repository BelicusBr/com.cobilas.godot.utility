using Godot;
using System;
using System.Collections.Generic;
using Cobilas.GodotEngine.Utility.Scene;
using Cobilas.GodotEngine.Utility.Runtime;

namespace Cobilas.GodotEngine.Utility; 

/// <summary>Gizmos are used to give visual debugging or setup aids in the Scene view.</summary>
[RunTimeInitializationClass(nameof(InternalGizmos), Priority.StartLater)]
internal class InternalGizmos : CanvasLayer {
    private Node2D? canvasItem = null;
    private bool movingToNextScene;

    private static InternalGizmos? gizmos = null;
    private static event Action? drawFunc = null;
    private static readonly Dictionary<Color, ColorTarget> colors = [];

    /// <summary>Sets the Color of the gizmos that are drawn next.</summary>
    /// <value>Returns or sets the color of the next gizmo.</value>
    internal static Color Color { get; set; }

    /// <inheritdoc/>
    public override void _Ready() {
        if (gizmos == null) {
            gizmos = this;
            movingToNextScene = false;
            Layer = -1;
            canvasItem = new Node2D {
                Name = "GizmosItem"
            };
            Color = Colors.Black;
            canvasItem.Connect("draw", this, nameof(DrawGizmos));
            SceneManager.CurrentSceneNode!.AddChild(canvasItem);

            SceneManager.UnloadedScene += (s) => {
                if (s.SceneNode is null)
                    throw new NullReferenceException("Scene.SceneNode was passed null in the UnloadedScene event.");
                movingToNextScene = true;
                canvasItem.SetParent(this);
            };
            SceneManager.LoadedScene += (s) => {
                if (s.SceneNode is null)
                    throw new NullReferenceException("Scene.SceneNode was passed null in LoadedScene event.");
                canvasItem.SetParent(s.SceneNode);
                movingToNextScene = false;
            };
        }
    }

    /// <inheritdoc/>
    public override void _Process(float delta) {
        if (movingToNextScene || canvasItem is null) return;
        canvasItem!.Update();
        Node parent = canvasItem.GetParent();
        if (canvasItem.GetIndex() != parent.GetChildCount() - 1)
			parent.MoveChild(canvasItem, parent.GetChildCount() - 1);
    }

    private void DrawGizmos() => drawFunc?.Invoke();
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
	internal static void DrawLine(Vector2 start, Vector2 end, float width)
        => SetDrawFunc(Color, (c) => gizmos!.canvasItem!.DrawLine(start, end, c, width));
    /// <summary>
    /// Draws a line from a 2D point to another, with a given color and width. It can
    /// be optionally antialiased. See also <seealso cref="CanvasItem.DrawMultiline(Vector2[], Color, float, bool)"/>
    /// and <seealso cref="CanvasItem.DrawPolyline(Vector2[], Color, float, bool)"/>.
    /// <para>Note: Line drawing is not accelerated by batching if <c>antialiased</c> is <c>true</c>.</para>
    /// <para>Note: Due to how it works, built-in antialiasing will not look correct for translucent lines and may not work on certain platforms. As a workaround, install the <a href="https://github.com/godot-extended-libraries/godot-antialiased-line2d">Antialiased Line2D</a> add-on then create an AntialiasedLine2D node. That node relies on a texture with custom mipmaps to perform antialiasing. 2D batching is also still supported with those antialiased lines.</para>
    /// </summary>
    /// <param name="start">The beginning of the line.</param>
    /// <param name="end">The end of the line.</param>
    internal static void DrawLine(Vector2 start, Vector2 end)
        => DrawLine(start, end, 1f);
    /// <summary>
    /// <para>Draws a solid body rectangle.</para>
    /// <para>Note: Due to how it works, built-in antialiasing will not look correct for translucent polygons and may not work on certain platforms. As a workaround, install the <a href="https://github.com/godot-extended-libraries/godot-antialiased-line2d">Antialiased Line2D</a> add-on then create an AntialiasedPolygon2D node. That node relies on a texture with custom mipmaps to perform antialiasing.</para>
    /// </summary>
    /// <param name="rect">The dimensions of the rectangle.</param>
    internal static void DrawRect(Rect2 rect)
        => SetDrawFunc(Color, (c) => gizmos!.canvasItem!.DrawRect(rect, c));
    /// <summary>
    /// Draw a rectangle of a skeletonized body.
    /// <para>Note: Due to how it works, built-in antialiasing will not look correct for translucent polygons and may not work on certain platforms. As a workaround, install the <a href="https://github.com/godot-extended-libraries/godot-antialiased-line2d">Antialiased Line2D</a> add-on then create an AntialiasedPolygon2D node. That node relies on a texture with custom mipmaps to perform antialiasing.</para>
    /// </summary>
    /// <param name="rect">The dimensions of the rectangle.</param>
    /// <param name="width">The thickness of the line.</param>
    internal static void DrawWireRect(Rect2 rect, float width)
        => SetDrawFunc(Color, (c) => gizmos!.canvasItem!.DrawRect(rect, c, false, width));
    /// <summary>
    /// Draw a rectangle of a skeletonized body.
    /// <para>Note: Due to how it works, built-in antialiasing will not look correct for translucent polygons and may not work on certain platforms. As a workaround, install the <a href="https://github.com/godot-extended-libraries/godot-antialiased-line2d">Antialiased Line2D</a> add-on then create an AntialiasedPolygon2D node. That node relies on a texture with custom mipmaps to perform antialiasing.</para>
    /// </summary>
    /// <param name="rect">The dimensions of the rectangle.</param>
    internal static void DrawWireRect(Rect2 rect) => DrawWireRect(rect, 1f);
    /// <summary>
    /// <para>Draws a colored, filled circle. See also <see cref="M:Godot.CanvasItem.DrawArc(Godot.Vector2,System.Single,System.Single,System.Single,System.Int32,Godot.Color,System.Single,System.Boolean)" />, <see cref="M:Godot.CanvasItem.DrawPolyline(Godot.Vector2[],Godot.Color,System.Single,System.Boolean)" /> and <see cref="M:Godot.CanvasItem.DrawPolygon(Godot.Vector2[],Godot.Color[],Godot.Vector2[],Godot.Texture,Godot.Texture,System.Boolean)" />.</para>
    /// <para>Note: Built-in antialiasing is not provided for <see cref="M:Godot.CanvasItem.DrawCircle(Godot.Vector2,System.Single,Godot.Color)" />. As a workaround, install the <a href="https://github.com/godot-extended-libraries/godot-antialiased-line2d">Antialiased Line2D</a> add-on then create an AntialiasedRegularPolygon2D node. That node relies on a texture with custom mipmaps to perform antialiasing.</para>
    /// </summary>
    /// <param name="position">The central position of the circle.</param>
    /// <param name="radius">The radius of the circle.</param>
    internal static void DrawCircle(Vector2 position, float radius)
        => SetDrawFunc(Color, (c) => gizmos!.canvasItem!.DrawCircle(position, radius, c));
    /// <summary>
    /// <para>Draws multiple disconnected lines with a uniform <c>color</c>. When drawing large amounts of lines, this is faster than using individual <see cref="M:Godot.CanvasItem.DrawLine(Godot.Vector2,Godot.Vector2,Godot.Color,System.Single,System.Boolean)" /> calls. To draw interconnected lines, use <see cref="M:Godot.CanvasItem.DrawPolyline(Godot.Vector2[],Godot.Color,System.Single,System.Boolean)" /> instead.</para>
    /// <para>Note: <c>width</c> and <c>antialiased</c> are currently not implemented and have no effect. As a workaround, install the <a href="https://github.com/godot-extended-libraries/godot-antialiased-line2d">Antialiased Line2D</a> add-on then create an AntialiasedLine2D node. That node relies on a texture with custom mipmaps to perform antialiasing. 2D batching is also still supported with those antialiased lines.</para>
    /// </summary>
    /// <param name="points">The points that form the line.</param>
    /// <param name="width">The thickness of the line.</param>
    internal static void DrawMultiline(Vector2[] points, float width)
        => SetDrawFunc(Color, (c) => gizmos!.canvasItem!.DrawMultiline(points, c, width));
    /// <summary>
    /// <para>Draws multiple disconnected lines with a uniform <c>color</c>. When drawing large amounts of lines, this is faster than using individual <see cref="M:Godot.CanvasItem.DrawLine(Godot.Vector2,Godot.Vector2,Godot.Color,System.Single,System.Boolean)" /> calls. To draw interconnected lines, use <see cref="M:Godot.CanvasItem.DrawPolyline(Godot.Vector2[],Godot.Color,System.Single,System.Boolean)" /> instead.</para>
    /// <para>Note: <c>width</c> and <c>antialiased</c> are currently not implemented and have no effect. As a workaround, install the <a href="https://github.com/godot-extended-libraries/godot-antialiased-line2d">Antialiased Line2D</a> add-on then create an AntialiasedLine2D node. That node relies on a texture with custom mipmaps to perform antialiasing. 2D batching is also still supported with those antialiased lines.</para>
    /// </summary>
    /// <param name="points">The points that form the line.</param>
    internal static void DrawMultiline(Vector2[] points) => DrawMultiline(points, 1f);
    /// <summary>
    /// <para>Draws multiple disconnected lines with a uniform <c>color</c>. When drawing large amounts of lines, this is faster than using individual <see cref="M:Godot.CanvasItem.DrawLine(Godot.Vector2,Godot.Vector2,Godot.Color,System.Single,System.Boolean)" /> calls. To draw interconnected lines, use <see cref="M:Godot.CanvasItem.DrawPolyline(Godot.Vector2[],Godot.Color,System.Single,System.Boolean)" /> instead.</para>
    /// <para>Note: <c>width</c> and <c>antialiased</c> are currently not implemented and have no effect. As a workaround, install the <a href="https://github.com/godot-extended-libraries/godot-antialiased-line2d">Antialiased Line2D</a> add-on then create an AntialiasedLine2D node. That node relies on a texture with custom mipmaps to perform antialiasing. 2D batching is also still supported with those antialiased lines.</para>
    /// </summary>
    /// <param name="points">The points that form the line.</param>
    /// <param name="width">The thickness of the line.</param>
    internal static void DrawMultiline(List<Vector2> points, float width) => DrawMultiline(points.ToArray(), width);
    /// <summary>
    /// <para>Draws multiple disconnected lines with a uniform <c>color</c>. When drawing large amounts of lines, this is faster than using individual <see cref="M:Godot.CanvasItem.DrawLine(Godot.Vector2,Godot.Vector2,Godot.Color,System.Single,System.Boolean)" /> calls. To draw interconnected lines, use <see cref="M:Godot.CanvasItem.DrawPolyline(Godot.Vector2[],Godot.Color,System.Single,System.Boolean)" /> instead.</para>
    /// <para>Note: <c>width</c> and <c>antialiased</c> are currently not implemented and have no effect. As a workaround, install the <a href="https://github.com/godot-extended-libraries/godot-antialiased-line2d">Antialiased Line2D</a> add-on then create an AntialiasedLine2D node. That node relies on a texture with custom mipmaps to perform antialiasing. 2D batching is also still supported with those antialiased lines.</para>
    /// </summary>
    /// <param name="points">The points that form the line.</param>
    internal static void DrawMultiline(List<Vector2> points) => DrawMultiline(points, 1f);
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
    internal static void DrawArc(Vector2 center, float radius, float startAngle, float endAngle, int pointCount, float width)
        => SetDrawFunc(Color, (c) => gizmos!.canvasItem!.DrawArc(center, radius, startAngle, endAngle, pointCount, c, width));
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
    internal static void DrawArc(Vector2 center, float radius, float startAngle, float endAngle, int pointCount)
        => DrawArc(center, radius, startAngle, endAngle, pointCount, 1f);
    /// <summary>
    /// <para>Draws a <see cref="T:Godot.Mesh" /> in 2D, using the provided texture. See <see cref="T:Godot.MeshInstance2D" /> for related documentation.</para>
    /// </summary>
    /// <param name="mesh">The mesh that will be used to shape the design.</param>
    /// <param name="texture">The texture that will be used to draw on the mesh.</param>
    /// <param name="normalMap">The texture normal map.</param>
    /// <param name="transform">If the parameter is null, then the default value is Transform2D.Identity</param>
    internal static void DrawMesh(Mesh mesh, Texture texture, Texture? normalMap = null, Transform2D? transform = null)
        => SetDrawFunc(Color, (c) => gizmos!.canvasItem!.DrawMesh(mesh, texture, normalMap, transform, c));
    /// <summary>
    /// <para>Draws a texture at a given position.</para>
    /// </summary>
    internal static void DrawTexture(Texture texture, Vector2 position, Texture? normalMap = null)
        => SetDrawFunc(Color, (c) => gizmos!.canvasItem!.DrawTexture(texture, position, c, normalMap));

    /// <summary>
    /// <para>Draws a textured rectangle at a given position, optionally modulated by a color. If <c>transpose</c> is <c>true</c>, the texture will have its X and Y coordinates swapped.</para>
    /// </summary>
    internal static void DrawTextureRect(Texture texture, Rect2 rect, bool tile, bool transpose = false, Texture? normalMap = null)
        => SetDrawFunc(Color, (c) => gizmos!.canvasItem!.DrawTextureRect(texture, rect, tile, c, transpose, normalMap));

    private static void SetDrawFunc(Color colorTarget, Action<Color> func) {
        if (colors.TryGetValue(colorTarget, out ColorTarget target)) target.DrawFunc += func;
        else {
            target = new(colorTarget);
			target.DrawFunc += func;
            drawFunc += target.Invok;
            colors.Add(colorTarget, target);
		}
    }

	private sealed class ColorTarget {
        public event Action<Color>? DrawFunc;

        private Color myColor { get; set; }

		public ColorTarget(Color color) {
            myColor = color;
		}

		public void Invok() {
            DrawFunc?.Invoke(myColor);
            DrawFunc = null;
        }
	}
}