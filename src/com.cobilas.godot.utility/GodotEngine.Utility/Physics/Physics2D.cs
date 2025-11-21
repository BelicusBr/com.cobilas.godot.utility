using Godot;
using System;
using System.Collections.Generic;

namespace Cobilas.GodotEngine.Utility.Physics;
/// <summary>This class provides methods for detecting 2D physics bodies.</summary>
public static class Physics2D {
    /// <inheritdoc cref="RayCastCircle(Camera2D, Vector2, float, CollisionObject2D[], uint, out Hit2D)"/>
    public static bool RayCastCircle(Camera2D? camera, Vector2 mousePosition, float radius, out Hit2D hit)
        => InternalPhysics2D.RayCastCircle(camera, mousePosition, radius, out hit);
    /// <inheritdoc cref="RayCastCircle(Camera2D, Vector2, float, CollisionObject2D[], uint, out Hit2D)"/>
    public static bool RayCastCircle(Camera2D? camera, Vector2 mousePosition, float radius, uint collisionLayer, out Hit2D hit)
        => InternalPhysics2D.RayCastCircle(camera, mousePosition, radius, collisionLayer, out hit);
    /// <inheritdoc cref="RayCastCircle(Camera2D, Vector2, float, CollisionObject2D[], uint, out Hit2D)"/>
    public static bool RayCastCircle(Camera2D? camera, Vector2 mousePosition, float radius, CollisionObject2D[]? exclude, out Hit2D hit)
        => InternalPhysics2D.RayCastCircle(camera, mousePosition, radius, exclude, out hit);
    /// <summary>Creates a 2D circle that allows you to detect an object in 2D space.</summary>
    /// <exception cref="System.ArgumentNullException">The exception is thrown when <seealso cref="Camera2D"/> object is null.</exception>
    /// <param name="camera">Camera that will be used to convert a point on the screen to a point in the 2d world.</param>
    /// <param name="mousePosition">The point from which the ray will be thrown.</param>
    /// <param name="radius">Defines the radius of the 2d collision circle.</param>
    /// <param name="exclude">The objects that will be excluded from the search.</param>
    /// <param name="collisionLayer">The collision layers that will be checked by the 2D physical body detection methods.</param>
    /// <param name="hit">The output parameter for the object's collision information.</param>
    /// <returns>Returns <c>true</c> when any 2d object is detected.</returns>
    public static bool RayCastCircle(Camera2D? camera, Vector2 mousePosition, float radius, CollisionObject2D[]? exclude, uint collisionLayer, out Hit2D hit)
        => InternalPhysics2D.RayCastCircle(camera, mousePosition, radius, exclude, collisionLayer, out hit);
    /// <inheritdoc cref="RayCastAllCircle(Camera2D, Vector2, float, CollisionObject2D[], uint, List{Hit2D})"/>
    public static bool RayCastAllCircle(Camera2D? camera, Vector2 mousePosition, float radius, List<Hit2D>? list)
        => InternalPhysics2D.RayCastAllCircle(camera, mousePosition, radius, list);
    /// <inheritdoc cref="RayCastAllCircle(Camera2D, Vector2, float, CollisionObject2D[], uint, List{Hit2D})"/>
    public static bool RayCastAllCircle(Camera2D? camera, Vector2 mousePosition, float radius, uint collisionLayer, List<Hit2D>? list)
        => InternalPhysics2D.RayCastAllCircle(camera, mousePosition, radius, collisionLayer, list);
    /// <inheritdoc cref="RayCastAllCircle(Camera2D, Vector2, float, CollisionObject2D[], uint, List{Hit2D})"/>
    public static bool RayCastAllCircle(Camera2D? camera, Vector2 mousePosition, float radius, CollisionObject2D[]? exclude, List<Hit2D>? list)
        => InternalPhysics2D.RayCastAllCircle(camera, mousePosition, radius, exclude, list);
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
    public static bool RayCastAllCircle(Camera2D? camera, Vector2 mousePosition, float radius, CollisionObject2D[]? exclude, uint collisionLayer, List<Hit2D>? list)
        => InternalPhysics2D.RayCastAllCircle(camera, mousePosition, radius, exclude, collisionLayer, list);
    /// <inheritdoc cref="RayCastBox(Camera2D, Vector2, Vector2, CollisionObject2D[], uint, out Hit2D)"/>
    public static bool RayCastBox(Camera2D? camera, Vector2 mousePosition, Vector2 size, out Hit2D hit)
        => InternalPhysics2D.RayCastBox(camera, mousePosition, size, out hit);
    /// <inheritdoc cref="RayCastBox(Camera2D, Vector2, Vector2, CollisionObject2D[], uint, out Hit2D)"/>
    public static bool RayCastBox(Camera2D? camera, Vector2 mousePosition, Vector2 size, uint collisionLayer, out Hit2D hit)
        => InternalPhysics2D.RayCastBox(camera, mousePosition, size, collisionLayer, out hit);
    /// <inheritdoc cref="RayCastBox(Camera2D, Vector2, Vector2, CollisionObject2D[], uint, out Hit2D)"/>
    public static bool RayCastBox(Camera2D? camera, Vector2 mousePosition, Vector2 size, CollisionObject2D[]? exclude, out Hit2D hit)
        => InternalPhysics2D.RayCastBox(camera, mousePosition, size, exclude, out hit);
    /// <summary>Creates a 2D box that allows you to detect an object in 2D space.</summary>
    /// <exception cref="System.ArgumentNullException">The exception is thrown when <seealso cref="Camera2D"/> object is null.</exception>
    /// <param name="camera">Camera that will be used to convert a point on the screen to a point in the 2d world.</param>
    /// <param name="mousePosition">The point from which the ray will be thrown.</param>
    /// <param name="size">The size of the 2d collision box.</param>
    /// <param name="exclude">The objects that will be excluded from the search.</param>
    /// <param name="collisionLayer">The collision layers that will be checked by the 2D physical body detection methods.</param>
    /// <param name="hit">The output parameter for the object's collision information.</param>
    /// <returns>Returns <c>true</c> when any 2d object is detected.</returns>
    public static bool RayCastBox(Camera2D? camera, Vector2 mousePosition, Vector2 size, CollisionObject2D[]? exclude, uint collisionLayer, out Hit2D hit)
        => InternalPhysics2D.RayCastBox(camera, mousePosition, size, exclude, collisionLayer, out hit);
    /// <inheritdoc cref="RayCastAllBox(Camera2D, Vector2, Vector2, CollisionObject2D[], uint, List{Hit2D})"/>
    public static bool RayCastAllBox(Camera2D? camera, Vector2 mousePosition, Vector2 size, List<Hit2D>? list)
        => InternalPhysics2D.RayCastAllBox(camera, mousePosition, size, list);
    /// <inheritdoc cref="RayCastAllBox(Camera2D, Vector2, Vector2, CollisionObject2D[], uint, List{Hit2D})"/>
    public static bool RayCastAllBox(Camera2D? camera, Vector2 mousePosition, Vector2 size, uint collisionLayer, List<Hit2D>? list)
        => InternalPhysics2D.RayCastAllBox(camera, mousePosition, size, collisionLayer, list);
    /// <inheritdoc cref="RayCastAllBox(Camera2D, Vector2, Vector2, CollisionObject2D[], uint, List{Hit2D})"/>
    public static bool RayCastAllBox(Camera2D? camera, Vector2 mousePosition, Vector2 size, CollisionObject2D[]? exclude, List<Hit2D>? list)
        => InternalPhysics2D.RayCastAllBox(camera, mousePosition, size, exclude, list);
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
    public static bool RayCastAllBox(Camera2D? camera, Vector2 mousePosition, Vector2 size, CollisionObject2D[]? exclude, uint collisionLayer, List<Hit2D>? list)
        => InternalPhysics2D.RayCastAllBox(camera, mousePosition, size, exclude, collisionLayer, list);
    /// <inheritdoc cref="RayCastHit(Camera2D, Vector2, CollisionObject2D[], uint, out Hit2D)"/>
    public static bool RayCastHit(Camera2D? camera, Vector2 mousePosition, out Hit2D hit)
        => InternalPhysics2D.RayCastHit(camera, mousePosition, out hit);
    /// <inheritdoc cref="RayCastHit(Camera2D, Vector2, CollisionObject2D[], uint, out Hit2D)"/>
    public static bool RayCastHit(Camera2D? camera, Vector2 mousePosition, uint collisionLayer, out Hit2D hit)
        => InternalPhysics2D.RayCastHit(camera, mousePosition, collisionLayer, out hit);
    /// <inheritdoc cref="RayCastHit(Camera2D, Vector2, CollisionObject2D[], uint, out Hit2D)"/>
    public static bool RayCastHit(Camera2D? camera, Vector2 mousePosition, CollisionObject2D[]? exclude, out Hit2D hit)
        => InternalPhysics2D.RayCastHit(camera, mousePosition, exclude, out hit);
    /// <summary>Creates a 2D ray that allows you to detect an object in 2D space.</summary>
    /// <exception cref="System.ArgumentNullException">The exception is thrown when <seealso cref="Camera2D"/> object is null.</exception>
    /// <param name="camera">Camera that will be used to convert a point on the screen to a point in the 2d world.</param>
    /// <param name="mousePosition">The point from which the ray will be thrown.</param>
    /// <param name="exclude">The objects that will be excluded from the search.</param>
    /// <param name="collisionLayer">The collision layers that will be checked by the 2D physical body detection methods.</param>
    /// <param name="hit">The output parameter for the object's collision information.</param>
    /// <returns>Returns <c>true</c> when any 2d object is detected.</returns>
    public static bool RayCastHit(Camera2D? camera, Vector2 mousePosition, CollisionObject2D[]? exclude, uint collisionLayer, out Hit2D hit)
        => InternalPhysics2D.RayCastHit(camera, mousePosition, exclude, collisionLayer, out hit);
    /// <inheritdoc cref="RayCastHitAll(Camera2D?, Vector2, CollisionObject2D[], uint, List{Hit2D})"/>
    public static bool RayCastHitAll(Camera2D? camera, Vector2 mousePosition, List<Hit2D>? list)
        => InternalPhysics2D.RayCastHitAll(camera, mousePosition, list);
    /// <inheritdoc cref="RayCastHitAll(Camera2D?, Vector2, CollisionObject2D[], uint, List{Hit2D})"/>
    public static bool RayCastHitAll(Camera2D? camera, Vector2 mousePosition, uint collisionLayer, List<Hit2D>? list)
        => InternalPhysics2D.RayCastHitAll(camera, mousePosition, collisionLayer, list);
    /// <inheritdoc cref="RayCastHitAll(Camera2D?, Vector2, CollisionObject2D[], uint, List{Hit2D})"/>
    public static bool RayCastHitAll(Camera2D? camera, Vector2 mousePosition, CollisionObject2D[]? exclude, List<Hit2D>? list)
        => InternalPhysics2D.RayCastHitAll(camera, mousePosition, exclude, list);
    /// <summary>Creates a 2D ray that allows detecting multiple objects in 2D space simultaneously.</summary>
    /// <exception cref="System.ArgumentNullException">The exception is thrown when <seealso cref="Camera2D"/> object is null.</exception>
    /// <exception cref="System.ArgumentNullException">The exception is thrown when <seealso cref="List{Hit2D}"/> object is null.</exception>
    /// <param name="camera">Camera that will be used to convert a point on the screen to a point in the 2d world.</param>
    /// <param name="mousePosition">The point from which the ray will be thrown.</param>
    /// <param name="exclude">The objects that will be excluded from the search.</param>
    /// <param name="collisionLayer">The collision layers that will be checked by the 2D physical body detection methods.</param>
    /// <param name="list">Here the objects that were detected by the 2d collision ray will be added.</param>
    /// <returns>Returns <c>true</c> when multiple 2D objects are detected.</returns>
    public static bool RayCastHitAll(Camera2D? camera, Vector2 mousePosition, CollisionObject2D[]? exclude, uint collisionLayer, List<Hit2D>? list)
        => InternalPhysics2D.RayCastHitAll(camera, mousePosition, exclude, collisionLayer, list);
    /// <inheritdoc cref="RayCast(Camera2D, Vector2, Vector2, CollisionObject2D[], uint, out RayHit2D)"/>
    public static bool RayCast(Camera2D? camera, Vector2 from, Vector2 to, out RayHit2D hit)
        => InternalPhysics2D.RayCast(camera, from, to, out hit);
    /// <inheritdoc cref="RayCast(Camera2D, Vector2, Vector2, CollisionObject2D[], uint, out RayHit2D)"/>
    public static bool RayCast(Camera2D? camera, Vector2 from, Vector2 to, uint collisionLayer, out RayHit2D hit)
        => InternalPhysics2D.RayCast(camera, from, to, collisionLayer, out hit);
    /// <inheritdoc cref="RayCast(Camera2D, Vector2, Vector2, CollisionObject2D[], uint, out RayHit2D)"/>
    public static bool RayCast(Camera2D? camera, Vector2 from, Vector2 to, CollisionObject2D[]? exclude, out RayHit2D hit)
        => InternalPhysics2D.RayCast(camera, from, to, exclude, out hit);
    /// <summary>Projects a ray that intersects the collider of a 2d object.</summary>
    /// <exception cref="System.ArgumentNullException">The exception is thrown when <seealso cref="Camera2D"/> object is null.</exception>
    /// <param name="camera">Camera that will be used to convert a point on the screen to a point in the 2d world.</param>
    /// <param name="from">The starting point of the collision ray.</param>
    /// <param name="to">The endpoint of the collision ray.</param>
    /// <param name="exclude">The objects that will be excluded from the search.</param>
    /// <param name="collisionLayer">The collision layers that will be checked by the 2D physical body detection methods.</param>
    /// <param name="hit">The output parameter for the object's collision information.</param>
    /// <returns>Returns <c>true</c> when any 2d object is detected.</returns>
    public static bool RayCast(Camera2D? camera, Vector2 from, Vector2 to, CollisionObject2D[]? exclude, uint collisionLayer, out RayHit2D hit)
        => InternalPhysics2D.RayCast(camera, from, to, exclude, collisionLayer, out hit);
}
