using Godot;
using Godot.Collections;

namespace Cobilas.GodotEngine.Utility.Physics {
    public struct Hit2D {
        public int ID { get; private set; }
        public RID RID { get; private set; }
        public string Name => Collision.Name;
        public object MetaData { get; private set; }
        public CollisionObject2D Collision { get; private set; }

        private Hit2D(int iD, RID rID, object metaData, CollisionObject2D collision) {
            ID = iD;
            RID = rID;
            MetaData = metaData;
            Collision = collision;
        }

        public static explicit operator Hit2D(Dictionary D)
            => new Hit2D(
                (int)D["collider_id"],
                (RID)D["rid"],
                D["metadata"],
                (CollisionObject2D)D["collider"]
            );
    }
}