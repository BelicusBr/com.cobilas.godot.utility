using Godot;
using System;
using System.Collections.Generic;
using Cobilas.GodotEngine.Utility.Scene;
using Cobilas.GodotEngine.Utility.Runtime;

namespace Cobilas.GodotEngine.Utility; 

[RunTimeInitializationClass(Priority.StartLater, nameof(Gizmos))]
public class Gizmos : CanvasLayer {
    private Node2D? canvasItem = null;
    private bool movingToNextScene;

    private static Gizmos? gizmos = null;
    private static event Action? drawFunc = null;

    public static Color Color { get; set; }

    public override void _Ready() {
        if (gizmos == null) {
            gizmos = this;
            movingToNextScene = false;
            Layer = -1;
            canvasItem = new Node2D {
                Name = "GizmosItem"
            };
            Color = Colors.Black;
            canvasItem.Connect("draw", this, "DrawGizmos");
            SceneManager.CurrentSceneNode!.AddChild(canvasItem);

            SceneManager.UnloadedScene += (s) => {
                movingToNextScene = true;
                SceneManager.CurrentSceneNode.RemoveChild(canvasItem);
                AddChild(canvasItem);
            };
            SceneManager.LoadedScene += (s) => {
                RemoveChild(canvasItem);
                SceneManager.CurrentSceneNode.AddChild(canvasItem);
                movingToNextScene = false;
            };
        }
    }

    public override void _Process(float delta) {
        if (movingToNextScene) return;
        canvasItem!.Update();
        if (canvasItem.GetIndex() != SceneManager.CurrentSceneNode!.GetChildCount() - 1) {
            SceneManager.CurrentSceneNode.RemoveChild(canvasItem);
            SceneManager.CurrentSceneNode.AddChild(canvasItem);
        }
    }

    private void DrawGizmos() {
        drawFunc?.Invoke();
        drawFunc = null;
    }

    public static void DrawLine(Vector2 start, Vector2 end, float width)
        => drawFunc += () => gizmos!.canvasItem!.DrawLine(start, end, Color, width);

    public static void DrawLine(Vector2 start, Vector2 end)
        => DrawLine(start, end, 1f);

    public static void DrawRect(Rect2 rect)
        => drawFunc += () => gizmos!.canvasItem!.DrawRect(rect, Color);

    public static void DrawWireRect(Rect2 rect, float width)
        => drawFunc += () => gizmos!.canvasItem!.DrawRect(rect, Color, false, width);

    public static void DrawWireRect(Rect2 rect) => DrawWireRect(rect, 1f);

    public static void DrawCircle(Vector2 position, float radius)
        => drawFunc += () => gizmos!.canvasItem!.DrawCircle(position, radius, Color);

    public static void DrawMultiline(Vector2[] points, float width)
        => drawFunc += () => gizmos!.canvasItem!.DrawMultiline(points, Color, width);

    public static void DrawMultiline(Vector2[] points) => DrawMultiline(points, 1f);

    public static void DrawMultiline(List<Vector2> points, float width) => DrawMultiline(points.ToArray(), width);
    public static void DrawMultiline(List<Vector2> points) => DrawMultiline(points, 1f);

    public static void DrawArc(Vector2 center, float radius, float startAngle, float endAngle, int pointCount, float width)
        => drawFunc += () => gizmos!.canvasItem!.DrawArc(center, radius, startAngle, endAngle, pointCount, Color, width);

    public static void DrawArc(Vector2 center, float radius, float startAngle, float endAngle, int pointCount)
        => DrawArc(center, radius, startAngle, endAngle, pointCount, 1f);

    public static void DrawMesh(Mesh mesh, Texture texture, Texture? normalMap = null, Transform2D? transform = null)
        => drawFunc += () => gizmos!.canvasItem!.DrawMesh(mesh, texture, normalMap, transform, Color);

    public static void DrawTexture(Texture texture, Vector2 position, Texture? normalMap = null)
        => drawFunc += () => gizmos!.canvasItem!.DrawTexture(texture, position, Color, normalMap);

    public static void DrawTextureRect(Texture texture, Rect2 rect, bool tile, bool transpose = false, Texture? normalMap = null)
        => drawFunc += () => gizmos!.canvasItem!.DrawTextureRect(texture, rect, tile, Color, transpose, normalMap);
}