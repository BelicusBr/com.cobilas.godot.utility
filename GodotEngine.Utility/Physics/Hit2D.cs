using Godot;
using Godot.Collections;

namespace Cobilas.GodotEngine.Utility.Physics; 
public struct Hit2D {
    public int ID { get; private set; }
    public RID RID { get; private set; }
    public Node Collision { get; private set; }
    public object MetaData { get; private set; }
    public readonly string Name => Collision.Name;

    private Hit2D(int iD, RID rID, object metaData, Node collision) {
        ID = iD;
        RID = rID;
        MetaData = metaData;
        Collision = collision;
    }

    public static explicit operator Hit2D(Dictionary? D) 
        => new(
            (int)D!["collider_id"],
            (RID)D!["rid"],
            D!["metadata"],
            (Node)D!["collider"]
        );
}