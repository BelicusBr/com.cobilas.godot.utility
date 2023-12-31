using Godot;
using Godot.Collections;

namespace Cobilas.GodotEngine.Utility.Physics {
    public struct RayHit2D {
        public int ID { get; private set; }
        public RID RID { get; private set; }
        public string Name => Collision.Name;
        public Vector2 Normal { get; private set; }
        public Node Collision { get; private set; }
        public object MetaData { get; private set; }
        public Vector2 Position { get; private set; }

        private RayHit2D(int iD, RID rID, Vector2 normal, object metaData, Vector2 position, Node collision) : this() {
            ID = iD;
            RID = rID;
            Normal = normal;
            MetaData = metaData;
            Position = position;
            Collision = collision;
        }

        public static explicit operator RayHit2D(Dictionary D)
            => new RayHit2D(
                (int)D["collider_id"],
                (RID)D["rid"],
                (Vector2)D["normal"],
                D["metadata"],
                (Vector2)D["position"],
                (Node)D["collider"]
            );
    }
}