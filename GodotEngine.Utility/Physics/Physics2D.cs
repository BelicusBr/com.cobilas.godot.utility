using Godot;
using Godot.Collections;
using System.Collections;
using Cobilas.Collections;
using System.Collections.Generic;
using Cobilas.GodotEngine.Utility.Runtime;

namespace Cobilas.GodotEngine.Utility.Physics {
    [RunTimeInitializationClass(nameof(Physics2D))]
    public class Physics2D : Node2D {

        private CircleShape2D circleShape2D;
        private RectangleShape2D rectangleShape2D;
        private Physics2DShapeQueryParameters parameters;

        private static Physics2D rayCast;
        private readonly static Array arrayTemp = new Array();

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

        public static bool RayCastCircle(Camera2D camera, Vector2 position, float radius, out Hit2D hit)
            => RayCastCircle(camera, position, radius, null, 2147483647U, out hit);

        public static bool RayCastCircle(Camera2D camera, Vector2 position, float radius, uint collisionLayer, out Hit2D hit)
            => RayCastCircle(camera, position, radius, null, collisionLayer, out hit);

        public static bool RayCastCircle(Camera2D camera, Vector2 position, float radius, CollisionObject2D[] exclude, out Hit2D hit)
            => RayCastCircle(camera, position, radius, exclude, 2147483647U, out hit);

        public static bool RayCastCircle(Camera2D camera, Vector2 position, float radius, CollisionObject2D[] exclude, uint collisionLayer, out Hit2D hit) {
            if (RayCastAllCircle(camera, position, radius, exclude, collisionLayer, out List<Hit2D> list)) {
                hit = list[0];
                return true;
            }
            hit = default;
            return false;
        }

        public static bool RayCastAllCircle(Camera2D camera, Vector2 position, float radius, out List<Hit2D> list)
            => RayCastAllCircle(camera, position, radius, null, 2147483647U, out list);

        public static bool RayCastAllCircle(Camera2D camera, Vector2 position, float radius, uint collisionLayer, out List<Hit2D> list)
            => RayCastAllCircle(camera, position, radius, null, collisionLayer, out list);

        public static bool RayCastAllCircle(Camera2D camera, Vector2 position, float radius, CollisionObject2D[] exclude, out List<Hit2D> list)
            => RayCastAllCircle(camera, position, radius, exclude, 2147483647U, out list);

        public static bool RayCastAllCircle(Camera2D camera, Vector2 position, float radius, CollisionObject2D[] exclude, uint collisionLayer, out List<Hit2D> list) {
            float zoom = (camera.Zoom.x + camera.Zoom.y) * .5f;
            rayCast.circleShape2D.Radius = radius * zoom;
            rayCast.parameters.Transform = new Transform2D(0f, camera.ScreenToWorldPoint(position));
            rayCast.parameters.SetShape(rayCast.circleShape2D);
            rayCast.parameters.Exclude = CreateExclude(exclude);
            rayCast.parameters.CollisionLayer = collisionLayer;
            Array array = rayCast.GetWorld2d().DirectSpaceState.IntersectShape(rayCast.parameters, 1024);
            if (array.Count == 0) {
                list = null;
                return false;
            }
            list = new List<Hit2D>();
            foreach (var item in array)
                list.Add((Hit2D)(item as Dictionary));
            return true;            
        }

        public static bool RayCastBox(Camera2D camera, Vector2 position, Vector2 size, out Hit2D hit)
            => RayCastBox(camera, position, size, null, 2147483647U, out hit);

        public static bool RayCastBox(Camera2D camera, Vector2 position, Vector2 size, uint collisionLayer, out Hit2D hit)
            => RayCastBox(camera, position, size, null, collisionLayer, out hit);

        public static bool RayCastBox(Camera2D camera, Vector2 position, Vector2 size, CollisionObject2D[] exclude, out Hit2D hit)
            => RayCastBox(camera, position, size, exclude, 2147483647U, out hit);

        public static bool RayCastBox(Camera2D camera, Vector2 position, Vector2 size, CollisionObject2D[] exclude, uint collisionLayer, out Hit2D hit) {
            if (RayCastAllBox(camera, position, size, exclude, collisionLayer, out List<Hit2D> list)) {
                hit = list[0];
                return true;
            }
            hit = default;
            return false;
        }

        public static bool RayCastAllBox(Camera2D camera, Vector2 position, Vector2 size, out List<Hit2D> list)
            => RayCastAllBox(camera, position, size, null, 2147483647U, out list);

        public static bool RayCastAllBox(Camera2D camera, Vector2 position, Vector2 size, uint collisionLayer, out List<Hit2D> list)
            => RayCastAllBox(camera, position, size, null, collisionLayer, out list);

        public static bool RayCastAllBox(Camera2D camera, Vector2 position, Vector2 size, CollisionObject2D[] exclude, out List<Hit2D> list)
            => RayCastAllBox(camera, position, size, exclude, 2147483647U, out list);

        public static bool RayCastAllBox(Camera2D camera, Vector2 position, Vector2 size, CollisionObject2D[] exclude, uint collisionLayer, out List<Hit2D> list) {
            rayCast.rectangleShape2D.Extents = size * camera.Zoom;
            rayCast.parameters.Transform = new Transform2D(0f, camera.ScreenToWorldPoint(position));
            rayCast.parameters.SetShape(rayCast.rectangleShape2D);
            rayCast.parameters.Exclude = CreateExclude(exclude);
            rayCast.parameters.CollisionLayer = collisionLayer;
            Array array = rayCast.GetWorld2d().DirectSpaceState.IntersectShape(rayCast.parameters, 1024);
            if (array.Count == 0) {
                list = null;
                return false;
            }
            list = new List<Hit2D>();
            foreach (var item in array)
                list.Add((Hit2D)(item as Dictionary));
            return true;
        }

        public static bool RayCastHit(Camera2D camera, Vector2 position, out Hit2D hit)
            => RayCastHit(camera, position, null, 2147483647U, out hit);

        public static bool RayCastHit(Camera2D camera, Vector2 position, uint collisionLayer, out Hit2D hit)
            => RayCastHit(camera, position, null, collisionLayer, out hit);

        public static bool RayCastHit(Camera2D camera, Vector2 position, CollisionObject2D[] exclude, out Hit2D hit)
            => RayCastHit(camera, position, exclude, 2147483647U, out hit);

        public static bool RayCastHit(Camera2D camera, Vector2 position, CollisionObject2D[] exclude, uint collisionLayer, out Hit2D hit) {
            if (RayCastHitAll(camera, position, exclude, collisionLayer, out List<Hit2D> list)) {
                hit = list[0];
                return true;
            }
            hit = default;
            return false;
        }
//2147483647
        public static bool RayCastHitAll(Camera2D camera, Vector2 position, out List<Hit2D> list)
            => RayCastHitAll(camera, position, null, 2147483647U, out list);

        public static bool RayCastHitAll(Camera2D camera, Vector2 position, uint collisionLayer, out List<Hit2D> list)
            => RayCastHitAll(camera, position, null, collisionLayer, out list);

        public static bool RayCastHitAll(Camera2D camera, Vector2 position, CollisionObject2D[] exclude, out List<Hit2D> list)
            => RayCastHitAll(camera, position, exclude, 2147483647U, out list);

        public static bool RayCastHitAll(Camera2D camera, Vector2 position, CollisionObject2D[] exclude, uint collisionLayer, out List<Hit2D> list) {
            Array array = rayCast.GetWorld2d().DirectSpaceState.IntersectPoint(camera.ScreenToWorldPoint(position),
             1024, CreateExclude(exclude), collisionLayer, collideWithAreas:true);
            if (array.Count == 0) {
                list = null;
                return false;
            }
            list = new List<Hit2D>();
            foreach (var item in array )
                list.Add((Hit2D)(item as Dictionary));
            return true;
        }

        public static bool RayCast(Camera2D camera, Vector2 from, Vector2 to, out RayHit2D hit)
            => RayCast(camera, from, to, null, 2147483647U, out hit);

        public static bool RayCast(Camera2D camera, Vector2 from, Vector2 to, uint collisionLayer, out RayHit2D hit)
            => RayCast(camera, from, to, null, collisionLayer, out hit);

        public static bool RayCast(Camera2D camera, Vector2 from, Vector2 to, CollisionObject2D[] exclude, out RayHit2D hit)
            => RayCast(camera, from, to, exclude, 2147483647U, out hit);

        public static bool RayCast(Camera2D camera, Vector2 from, Vector2 to, CollisionObject2D[] exclude, uint collisionLayer, out RayHit2D hit) {
            Dictionary dictionary = rayCast.GetWorld2d().DirectSpaceState.IntersectRay(
                camera.ScreenToWorldPoint(from), camera.ScreenToWorldPoint(to), CreateExclude(exclude), collisionLayer, collideWithAreas:true);
            if (dictionary.Count == 0) {
                hit = default;
                return false;
            }
            hit = (RayHit2D)dictionary;
            return true;
        }

        private static Array CreateExclude(CollisionObject2D[] exclude) {
            if (ArrayManipulation.EmpytArray(exclude))
                return new Array((IEnumerable)new CollisionObject2D[0]);
            return new Array((IEnumerable)exclude);
        }
    }
}