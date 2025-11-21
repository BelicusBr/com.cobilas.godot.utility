using Godot;
using Godot.Collections;
using System.Collections;
using Cobilas.Collections;
using System.Collections.Generic;
using Cobilas.GodotEngine.Utility.Runtime;

namespace Cobilas.GodotEngine.Utility.Physics;
/// <summary>This class provides methods for detecting 2D physics bodies.</summary>
[RunTimeInitializationClass(nameof(InternalPhysics2D))]
internal class InternalPhysics2D : Node2D {

    private CircleShape2D? circleShape2D = null;
    private RectangleShape2D? rectangleShape2D = null;
    private Physics2DShapeQueryParameters? parameters = null;

    private static InternalPhysics2D? rayCast = null;
    private readonly static List<Hit2D> _emptyPhysicsList = [];
    /// <inheritdoc/>
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
    /// <inheritdoc cref="RayCastCircle(Camera2D, Vector2, float, CollisionObject2D[], uint, out Hit2D)"/>
    internal static bool RayCastCircle(Camera2D? camera, Vector2 mousePosition, float radius, out Hit2D hit)
        => RayCastCircle(camera, mousePosition, radius, null, 2147483647U, out hit);
    /// <inheritdoc cref="RayCastCircle(Camera2D, Vector2, float, CollisionObject2D[], uint, out Hit2D)"/>
    internal static bool RayCastCircle(Camera2D? camera, Vector2 mousePosition, float radius, uint collisionLayer, out Hit2D hit)
        => RayCastCircle(camera, mousePosition, radius, null, collisionLayer, out hit);
    /// <inheritdoc cref="RayCastCircle(Camera2D, Vector2, float, CollisionObject2D[], uint, out Hit2D)"/>
    internal static bool RayCastCircle(Camera2D? camera, Vector2 mousePosition, float radius, CollisionObject2D[]? exclude, out Hit2D hit)
        => RayCastCircle(camera, mousePosition, radius, exclude, 2147483647U, out hit);
    /// <summary>Creates a 2D circle that allows you to detect an object in 2D space.</summary>
    /// <exception cref="System.ArgumentNullException">The exception is thrown when <seealso cref="Camera2D"/> object is null.</exception>
    /// <param name="camera">Camera that will be used to convert a point on the screen to a point in the 2d world.</param>
    /// <param name="mousePosition">The point from which the ray will be thrown.</param>
    /// <param name="radius">Defines the radius of the 2d collision circle.</param>
    /// <param name="exclude">The objects that will be excluded from the search.</param>
    /// <param name="collisionLayer">The collision layers that will be checked by the 2D physical body detection methods.</param>
    /// <param name="hit">The output parameter for the object's collision information.</param>
    /// <returns>Returns <c>true</c> when any 2d object is detected.</returns>
    internal static bool RayCastCircle(Camera2D? camera, Vector2 mousePosition, float radius, CollisionObject2D[]? exclude, uint collisionLayer, out Hit2D hit) {
        _emptyPhysicsList.Clear();
        if (RayCastAllCircle(camera, mousePosition, radius, exclude, collisionLayer, _emptyPhysicsList)) {
            hit = _emptyPhysicsList[0];
            return true;
        }
        hit = default;
        return false;
    }
    /// <inheritdoc cref="RayCastAllCircle(Camera2D, Vector2, float, CollisionObject2D[], uint, List{Hit2D})"/>
    internal static bool RayCastAllCircle(Camera2D? camera, Vector2 mousePosition, float radius, List<Hit2D>? list)
        => RayCastAllCircle(camera, mousePosition, radius, null, 2147483647U, list);
    /// <inheritdoc cref="RayCastAllCircle(Camera2D, Vector2, float, CollisionObject2D[], uint, List{Hit2D})"/>
    internal static bool RayCastAllCircle(Camera2D? camera, Vector2 mousePosition, float radius, uint collisionLayer, List<Hit2D>? list)
        => RayCastAllCircle(camera, mousePosition, radius, null, collisionLayer, list);
    /// <inheritdoc cref="RayCastAllCircle(Camera2D, Vector2, float, CollisionObject2D[], uint, List{Hit2D})"/>
    internal static bool RayCastAllCircle(Camera2D? camera, Vector2 mousePosition, float radius, CollisionObject2D[]? exclude, List<Hit2D>? list)
        => RayCastAllCircle(camera, mousePosition, radius, exclude, 2147483647U, list);
    /// <summary>Creates a 2D circle that allows you to detect multiple objects in 2D space simultaneously.</summary>
    /// <exception cref="System.ArgumentNullException">The exception is thrown when <seealso cref="Camera2D"/> object is null.</exception>
    /// <exception cref="System.ArgumentNullException">The exception is thrown when <seealso cref="List{Hit2D}"/> object is null.</exception>
    /// <param name="camera">Camera that will be used to convert a point on the screen to a point in the 2d world.</param>
    /// <param name="mousePosition">The point from which the ray will be thrown.</param>
    /// <param name="radius">Defines the radius of the 2d collision circle.</param>
    /// <param name="exclude">The objects that will be excluded from the search.</param>
    /// <param name="collisionLayer">The collision layers that will be checked by the 2D physical body detection methods.</param>
    /// <param name="list">Here the objects that were detected by the 2d collision ray will be added.</param>
    /// <returns>Returns <c>true</c> when multiple 2D objects are detected.</returns>
    internal static bool RayCastAllCircle(Camera2D? camera, Vector2 mousePosition, float radius, CollisionObject2D[]? exclude, uint collisionLayer, List<Hit2D>? list) {
        if (camera is null) throw new System.ArgumentNullException(nameof(camera));
        else if (list is null) throw new System.ArgumentNullException(nameof(list));

        float zoom = (camera.Zoom.x + camera.Zoom.y) * .5f;
        rayCast!.circleShape2D!.Radius = radius * zoom;
        rayCast.parameters!.Transform = new Transform2D(0f, camera.ScreenToWorldPoint(mousePosition));
        rayCast.parameters.SetShape(rayCast.circleShape2D);
        rayCast.parameters.Exclude = CreateExclude(exclude);
        rayCast.parameters.CollisionLayer = collisionLayer;
        Array array = rayCast.GetWorld2d().DirectSpaceState.IntersectShape(rayCast.parameters, 1024);
        if (array.Count == 0) return false;
        foreach (var item in array)
            list.Add((Hit2D)(item as Dictionary));
        return true;            
    }
    /// <inheritdoc cref="RayCastBox(Camera2D, Vector2, Vector2, CollisionObject2D[], uint, out Hit2D)"/>
    internal static bool RayCastBox(Camera2D? camera, Vector2 mousePosition, Vector2 size, out Hit2D hit)
        => RayCastBox(camera, mousePosition, size, null, 2147483647U, out hit);
    /// <inheritdoc cref="RayCastBox(Camera2D, Vector2, Vector2, CollisionObject2D[], uint, out Hit2D)"/>
    internal static bool RayCastBox(Camera2D? camera, Vector2 mousePosition, Vector2 size, uint collisionLayer, out Hit2D hit)
        => RayCastBox(camera, mousePosition, size, null, collisionLayer, out hit);
    /// <inheritdoc cref="RayCastBox(Camera2D, Vector2, Vector2, CollisionObject2D[], uint, out Hit2D)"/>
    internal static bool RayCastBox(Camera2D? camera, Vector2 mousePosition, Vector2 size, CollisionObject2D[]? exclude, out Hit2D hit)
        => RayCastBox(camera, mousePosition, size, exclude, 2147483647U, out hit);
    /// <summary>Creates a 2D box that allows you to detect an object in 2D space.</summary>
    /// <exception cref="System.ArgumentNullException">The exception is thrown when <seealso cref="Camera2D"/> object is null.</exception>
    /// <param name="camera">Camera that will be used to convert a point on the screen to a point in the 2d world.</param>
    /// <param name="mousePosition">The point from which the ray will be thrown.</param>
    /// <param name="size">The size of the 2d collision box.</param>
    /// <param name="exclude">The objects that will be excluded from the search.</param>
    /// <param name="collisionLayer">The collision layers that will be checked by the 2D physical body detection methods.</param>
    /// <param name="hit">The output parameter for the object's collision information.</param>
    /// <returns>Returns <c>true</c> when any 2d object is detected.</returns>
    internal static bool RayCastBox(Camera2D? camera, Vector2 mousePosition, Vector2 size, CollisionObject2D[]? exclude, uint collisionLayer, out Hit2D hit) {
        _emptyPhysicsList.Clear();
        if (RayCastAllBox(camera, mousePosition, size, exclude, collisionLayer, _emptyPhysicsList)) {
            hit = _emptyPhysicsList[0];
            return true;
        }
        hit = default;
        return false;
    }
    /// <inheritdoc cref="RayCastAllBox(Camera2D, Vector2, Vector2, CollisionObject2D[], uint, List{Hit2D})"/>
    internal static bool RayCastAllBox(Camera2D? camera, Vector2 mousePosition, Vector2 size, List<Hit2D>? list)
        => RayCastAllBox(camera, mousePosition, size, null, 2147483647U, list);
    /// <inheritdoc cref="RayCastAllBox(Camera2D, Vector2, Vector2, CollisionObject2D[], uint, List{Hit2D})"/>
    internal static bool RayCastAllBox(Camera2D? camera, Vector2 mousePosition, Vector2 size, uint collisionLayer, List<Hit2D>? list)
        => RayCastAllBox(camera, mousePosition, size, null, collisionLayer, list);
    /// <inheritdoc cref="RayCastAllBox(Camera2D, Vector2, Vector2, CollisionObject2D[], uint, List{Hit2D})"/>
    internal static bool RayCastAllBox(Camera2D? camera, Vector2 mousePosition, Vector2 size, CollisionObject2D[]? exclude, List<Hit2D>? list)
        => RayCastAllBox(camera, mousePosition, size, exclude, 2147483647U, list);
    /// <summary>Creates a 2D box that allows you to detect multiple objects in 2D space simultaneously.</summary>
    /// <exception cref="System.ArgumentNullException">The exception is thrown when <seealso cref="Camera2D"/> object is null.</exception>
    /// <exception cref="System.ArgumentNullException">The exception is thrown when <seealso cref="List{Hit2D}"/> object is null.</exception>
    /// <param name="camera">Camera that will be used to convert a point on the screen to a point in the 2d world.</param>
    /// <param name="mousePosition">The point from which the ray will be thrown.</param>
    /// <param name="size">The size of the 2d collision box.</param>
    /// <param name="exclude">The objects that will be excluded from the search.</param>
    /// <param name="collisionLayer">The collision layers that will be checked by the 2D physical body detection methods.</param>
    /// <param name="list">Here the objects that were detected by the 2d collision ray will be added.</param>
    /// <returns>Returns <c>true</c> when multiple 2D objects are detected.</returns>
    internal static bool RayCastAllBox(Camera2D? camera, Vector2 mousePosition, Vector2 size, CollisionObject2D[]? exclude, uint collisionLayer, List<Hit2D>? list) {
        if (camera is null) throw new System.ArgumentNullException(nameof(camera));
        else if (list is null) throw new System.ArgumentNullException(nameof(list));

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
    /// <inheritdoc cref="RayCastHit(Camera2D, Vector2, CollisionObject2D[], uint, out Hit2D)"/>
    internal static bool RayCastHit(Camera2D? camera, Vector2 mousePosition, out Hit2D hit)
        => RayCastHit(camera, mousePosition, null, 2147483647U, out hit);
    /// <inheritdoc cref="RayCastHit(Camera2D, Vector2, CollisionObject2D[], uint, out Hit2D)"/>
    internal static bool RayCastHit(Camera2D? camera, Vector2 mousePosition, uint collisionLayer, out Hit2D hit)
        => RayCastHit(camera, mousePosition, null, collisionLayer, out hit);
    /// <inheritdoc cref="RayCastHit(Camera2D, Vector2, CollisionObject2D[], uint, out Hit2D)"/>
    internal static bool RayCastHit(Camera2D? camera, Vector2 mousePosition, CollisionObject2D[]? exclude, out Hit2D hit)
        => RayCastHit(camera, mousePosition, exclude, 2147483647U, out hit);
    /// <summary>Creates a 2D ray that allows you to detect an object in 2D space.</summary>
    /// <exception cref="System.ArgumentNullException">The exception is thrown when <seealso cref="Camera2D"/> object is null.</exception>
    /// <param name="camera">Camera that will be used to convert a point on the screen to a point in the 2d world.</param>
    /// <param name="mousePosition">The point from which the ray will be thrown.</param>
    /// <param name="exclude">The objects that will be excluded from the search.</param>
    /// <param name="collisionLayer">The collision layers that will be checked by the 2D physical body detection methods.</param>
    /// <param name="hit">The output parameter for the object's collision information.</param>
    /// <returns>Returns <c>true</c> when any 2d object is detected.</returns>
    internal static bool RayCastHit(Camera2D? camera, Vector2 mousePosition, CollisionObject2D[]? exclude, uint collisionLayer, out Hit2D hit) {
        _emptyPhysicsList.Clear();
        if (RayCastHitAll(camera, mousePosition, exclude, collisionLayer, _emptyPhysicsList)) {
            hit = _emptyPhysicsList[0];
            return true;
        }
        hit = default;
        return false;
    }
    /// <inheritdoc cref="RayCastHitAll(Camera2D?, Vector2, CollisionObject2D[], uint, List{Hit2D})"/>
    internal static bool RayCastHitAll(Camera2D? camera, Vector2 mousePosition, List<Hit2D>? list)
        => RayCastHitAll(camera, mousePosition, null, 2147483647U, list);
    /// <inheritdoc cref="RayCastHitAll(Camera2D?, Vector2, CollisionObject2D[], uint, List{Hit2D})"/>
    internal static bool RayCastHitAll(Camera2D? camera, Vector2 mousePosition, uint collisionLayer, List<Hit2D>? list)
        => RayCastHitAll(camera, mousePosition, null, collisionLayer, list);
    /// <inheritdoc cref="RayCastHitAll(Camera2D?, Vector2, CollisionObject2D[], uint, List{Hit2D})"/>
    internal static bool RayCastHitAll(Camera2D? camera, Vector2 mousePosition, CollisionObject2D[]? exclude, List<Hit2D>? list)
        => RayCastHitAll(camera, mousePosition, exclude, 2147483647U, list);
    /// <summary>Creates a 2D ray that allows detecting multiple objects in 2D space simultaneously.</summary>
    /// <exception cref="System.ArgumentNullException">The exception is thrown when <seealso cref="Camera2D"/> object is null.</exception>
    /// <exception cref="System.ArgumentNullException">The exception is thrown when <seealso cref="List{Hit2D}"/> object is null.</exception>
    /// <param name="camera">Camera that will be used to convert a point on the screen to a point in the 2d world.</param>
    /// <param name="mousePosition">The point from which the ray will be thrown.</param>
    /// <param name="exclude">The objects that will be excluded from the search.</param>
    /// <param name="collisionLayer">The collision layers that will be checked by the 2D physical body detection methods.</param>
    /// <param name="list">Here the objects that were detected by the 2d collision ray will be added.</param>
    /// <returns>Returns <c>true</c> when multiple 2D objects are detected.</returns>
    internal static bool RayCastHitAll(Camera2D? camera, Vector2 mousePosition, CollisionObject2D[]? exclude, uint collisionLayer, List<Hit2D>? list) {
        if (camera is null) throw new System.ArgumentNullException(nameof(camera));
        else if (list is null) throw new System.ArgumentNullException(nameof(list));

        Array array = rayCast!.GetWorld2d().DirectSpaceState.IntersectPoint(camera.ScreenToWorldPoint(mousePosition),
         1024, CreateExclude(exclude), collisionLayer, collideWithAreas:true);
        if (array.Count == 0) {
            return false;
        }
        foreach (object? item in array)
            list.Add((Hit2D)(item as Dictionary));
        return true;
    }
    /// <inheritdoc cref="RayCast(Camera2D, Vector2, Vector2, CollisionObject2D[], uint, out RayHit2D)"/>
    internal static bool RayCast(Camera2D? camera, Vector2 from, Vector2 to, out RayHit2D hit)
        => RayCast(camera, from, to, null, 2147483647U, out hit);
    /// <inheritdoc cref="RayCast(Camera2D, Vector2, Vector2, CollisionObject2D[], uint, out RayHit2D)"/>
    internal static bool RayCast(Camera2D? camera, Vector2 from, Vector2 to, uint collisionLayer, out RayHit2D hit)
        => RayCast(camera, from, to, null, collisionLayer, out hit);
    /// <inheritdoc cref="RayCast(Camera2D, Vector2, Vector2, CollisionObject2D[], uint, out RayHit2D)"/>
    internal static bool RayCast(Camera2D? camera, Vector2 from, Vector2 to, CollisionObject2D[]? exclude, out RayHit2D hit)
        => RayCast(camera, from, to, exclude, 2147483647U, out hit);
    /// <summary>Projects a ray that intersects the collider of a 2d object.</summary>
    /// <exception cref="System.ArgumentNullException">The exception is thrown when <seealso cref="Camera2D"/> object is null.</exception>
    /// <param name="camera">Camera that will be used to convert a point on the screen to a point in the 2d world.</param>
    /// <param name="from">The starting point of the collision ray.</param>
    /// <param name="to">The endpoint of the collision ray.</param>
    /// <param name="exclude">The objects that will be excluded from the search.</param>
    /// <param name="collisionLayer">The collision layers that will be checked by the 2D physical body detection methods.</param>
    /// <param name="hit">The output parameter for the object's collision information.</param>
    /// <returns>Returns <c>true</c> when any 2d object is detected.</returns>
    internal static bool RayCast(Camera2D? camera, Vector2 from, Vector2 to, CollisionObject2D[]? exclude, uint collisionLayer, out RayHit2D hit) {
        if (camera is null) throw new System.ArgumentNullException(nameof(camera));

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