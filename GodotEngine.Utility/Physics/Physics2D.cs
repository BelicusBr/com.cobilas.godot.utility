using Godot;
using Godot.Collections;
using System.Collections;
using Cobilas.Collections;
using System.Collections.Generic;
using Cobilas.GodotEngine.Utility.Runtime;

namespace Cobilas.GodotEngine.Utility.Physics;

[RunTimeInitializationClass(nameof(Physics2D))]
public class Physics2D : Node2D {

    private CircleShape2D? circleShape2D = null;
    private RectangleShape2D? rectangleShape2D = null;
    private Physics2DShapeQueryParameters? parameters = null;

    private static Physics2D? rayCast = null;
    private readonly static Array arrayTemp = new();
    private readonly static List<Hit2D> _emptyPhysicsList = new();

    public override void _Ready() {
        if (rayCast == null) {
            rayCast = this;
            parameters = new Physics2DShapeQueryParameters();
            parameters.CollideWithAreas =
                parameters.CollideWithBodies = true;
            circleShape2D = new CircleShape2D();
            rectangleShape2D = new RectangleShape2D();
        }
    }

    public static bool RayCastCircle(Camera2D camera, Vector2 mousePosition, float radius, out Hit2D hit)
        => RayCastCircle(camera, mousePosition, radius, null, 2147483647U, out hit);

    public static bool RayCastCircle(Camera2D camera, Vector2 mousePosition, float radius, uint collisionLayer, out Hit2D hit)
        => RayCastCircle(camera, mousePosition, radius, null, collisionLayer, out hit);

    public static bool RayCastCircle(Camera2D camera, Vector2 mousePosition, float radius, CollisionObject2D[] exclude, out Hit2D hit)
        => RayCastCircle(camera, mousePosition, radius, exclude, 2147483647U, out hit);

    public static bool RayCastCircle(Camera2D camera, Vector2 mousePosition, float radius, CollisionObject2D[]? exclude, uint collisionLayer, out Hit2D hit) {
        _emptyPhysicsList.Clear();
        if (RayCastAllCircle(camera, mousePosition, radius, exclude, collisionLayer, _emptyPhysicsList)) {
            hit = _emptyPhysicsList[0];
            return true;
        }
        hit = default;
        return false;
    }

    public static bool RayCastAllCircle(Camera2D camera, Vector2 mousePosition, float radius, List<Hit2D> list)
        => RayCastAllCircle(camera, mousePosition, radius, null, 2147483647U, list);

    public static bool RayCastAllCircle(Camera2D camera, Vector2 mousePosition, float radius, uint collisionLayer, List<Hit2D> list)
        => RayCastAllCircle(camera, mousePosition, radius, null, collisionLayer, list);

    public static bool RayCastAllCircle(Camera2D camera, Vector2 mousePosition, float radius, CollisionObject2D[] exclude, List<Hit2D> list)
        => RayCastAllCircle(camera, mousePosition, radius, exclude, 2147483647U, list);

    public static bool RayCastAllCircle(Camera2D camera, Vector2 mousePosition, float radius, CollisionObject2D[]? exclude, uint collisionLayer, List<Hit2D> list) {
        float zoom = (camera.Zoom.x + camera.Zoom.y) * .5f;
        rayCast!.circleShape2D!.Radius = radius * zoom;
        rayCast.parameters!.Transform = new Transform2D(0f, camera.ScreenToWorldPoint(mousePosition));
        rayCast.parameters.SetShape(rayCast.circleShape2D);
        rayCast.parameters.Exclude = CreateExclude(exclude);
        rayCast.parameters.CollisionLayer = collisionLayer;
        Array array = rayCast.GetWorld2d().DirectSpaceState.IntersectShape(rayCast.parameters, 1024);
        if (array.Count == 0) {
            return false;
        }
        foreach (var item in array)
            list.Add((Hit2D)(item as Dictionary));
        return true;            
    }

    public static bool RayCastBox(Camera2D camera, Vector2 mousePosition, Vector2 size, out Hit2D hit)
        => RayCastBox(camera, mousePosition, size, null, 2147483647U, out hit);

    public static bool RayCastBox(Camera2D camera, Vector2 mousePosition, Vector2 size, uint collisionLayer, out Hit2D hit)
        => RayCastBox(camera, mousePosition, size, null, collisionLayer, out hit);

    public static bool RayCastBox(Camera2D camera, Vector2 mousePosition, Vector2 size, CollisionObject2D[] exclude, out Hit2D hit)
        => RayCastBox(camera, mousePosition, size, exclude, 2147483647U, out hit);

    public static bool RayCastBox(Camera2D camera, Vector2 mousePosition, Vector2 size, CollisionObject2D[]? exclude, uint collisionLayer, out Hit2D hit) {
        _emptyPhysicsList.Clear();
        if (RayCastAllBox(camera, mousePosition, size, exclude, collisionLayer, _emptyPhysicsList)) {
            hit = _emptyPhysicsList[0];
            return true;
        }
        hit = default;
        return false;
    }

    public static bool RayCastAllBox(Camera2D camera, Vector2 mousePosition, Vector2 size, List<Hit2D> list)
        => RayCastAllBox(camera, mousePosition, size, null, 2147483647U, list);

    public static bool RayCastAllBox(Camera2D camera, Vector2 mousePosition, Vector2 size, uint collisionLayer, List<Hit2D> list)
        => RayCastAllBox(camera, mousePosition, size, null, collisionLayer, list);

    public static bool RayCastAllBox(Camera2D camera, Vector2 mousePosition, Vector2 size, CollisionObject2D[] exclude, List<Hit2D> list)
        => RayCastAllBox(camera, mousePosition, size, exclude, 2147483647U, list);

    public static bool RayCastAllBox(Camera2D camera, Vector2 mousePosition, Vector2 size, CollisionObject2D[]? exclude, uint collisionLayer, List<Hit2D> list) {
        rayCast!.rectangleShape2D!.Extents = size * camera.Zoom;
        rayCast.parameters!.Transform = new Transform2D(0f, camera.ScreenToWorldPoint(mousePosition));
        rayCast.parameters.SetShape(rayCast.rectangleShape2D);
        rayCast.parameters.Exclude = CreateExclude(exclude);
        rayCast.parameters.CollisionLayer = collisionLayer;
        Array array = rayCast.GetWorld2d().DirectSpaceState.IntersectShape(rayCast.parameters, 1024);
        if (array.Count == 0) {
            return false;
        }
        foreach (var item in array)
            list.Add((Hit2D)(item as Dictionary));
        return true;
    }

    public static bool RayCastHit(Camera2D camera, Vector2 mousePosition, out Hit2D hit)
        => RayCastHit(camera, mousePosition, null, 2147483647U, out hit);

    public static bool RayCastHit(Camera2D camera, Vector2 mousePosition, uint collisionLayer, out Hit2D hit)
        => RayCastHit(camera, mousePosition, null, collisionLayer, out hit);

    public static bool RayCastHit(Camera2D camera, Vector2 mousePosition, CollisionObject2D[] exclude, out Hit2D hit)
        => RayCastHit(camera, mousePosition, exclude, 2147483647U, out hit);

    public static bool RayCastHit(Camera2D camera, Vector2 mousePosition, CollisionObject2D[]? exclude, uint collisionLayer, out Hit2D hit) {
        _emptyPhysicsList.Clear();
        if (RayCastHitAll(camera, mousePosition, exclude, collisionLayer, _emptyPhysicsList)) {
            hit = _emptyPhysicsList[0];
            return true;
        }
        hit = default;
        return false;
    }
//2147483647
    public static bool RayCastHitAll(Camera2D camera, Vector2 mousePosition, List<Hit2D> list)
        => RayCastHitAll(camera, mousePosition, null, 2147483647U, list);

    public static bool RayCastHitAll(Camera2D camera, Vector2 mousePosition, uint collisionLayer, List<Hit2D> list)
        => RayCastHitAll(camera, mousePosition, null, collisionLayer, list);

    public static bool RayCastHitAll(Camera2D camera, Vector2 mousePosition, CollisionObject2D[] exclude, List<Hit2D> list)
        => RayCastHitAll(camera, mousePosition, exclude, 2147483647U, list);

    public static bool RayCastHitAll(Camera2D camera, Vector2 mousePosition, CollisionObject2D[]? exclude, uint collisionLayer, List<Hit2D> list) {
        Array array = rayCast!.GetWorld2d().DirectSpaceState.IntersectPoint(camera.ScreenToWorldPoint(mousePosition),
         1024, CreateExclude(exclude), collisionLayer, collideWithAreas:true);
        if (array.Count == 0) {
            return false;
        }
        foreach (object? item in array)
            list.Add((Hit2D)(item as Dictionary));
        return true;
    }

    public static bool RayCast(Camera2D camera, Vector2 from, Vector2 to, out RayHit2D hit)
        => RayCast(camera, from, to, null, 2147483647U, out hit);

    public static bool RayCast(Camera2D camera, Vector2 from, Vector2 to, uint collisionLayer, out RayHit2D hit)
        => RayCast(camera, from, to, null, collisionLayer, out hit);

    public static bool RayCast(Camera2D camera, Vector2 from, Vector2 to, CollisionObject2D[] exclude, out RayHit2D hit)
        => RayCast(camera, from, to, exclude, 2147483647U, out hit);

    public static bool RayCast(Camera2D camera, Vector2 from, Vector2 to, CollisionObject2D[]? exclude, uint collisionLayer, out RayHit2D hit) {
        Dictionary dictionary = rayCast!.GetWorld2d().DirectSpaceState.IntersectRay(
            camera.ScreenToWorldPoint(from), camera.ScreenToWorldPoint(to), CreateExclude(exclude), collisionLayer, collideWithAreas:true);
        if (dictionary.Count == 0) {
            hit = default;
            return false;
        }
        hit = (RayHit2D)dictionary;
        return true;
    }

    private static Array CreateExclude(CollisionObject2D[]? exclude) {
        if (ArrayManipulation.EmpytArray(exclude))
            return new Array((IEnumerable)new CollisionObject2D[0]);
        return new Array(exclude as IEnumerable);
    }
}