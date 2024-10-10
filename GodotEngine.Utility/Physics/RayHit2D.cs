using Godot;
using System;
using Godot.Collections;

namespace Cobilas.GodotEngine.Utility.Physics; 
/// <summary>Responsible for storing information about a 2D collision ray.</summary>
public struct RayHit2D {
    /// <summary>Represents normal key.</summary>
    public const string Normal_Key = "normal";
    /// <summary>Represents position key.</summary>
    public const string Position_Key = "position";

    /// <summary>The colliding object's ID.</summary>
    /// <returns>Returns the object ID.</returns>
    public int ID { get; private set; }
    /// <summary>The intersecting object's <see cref="Godot.RID"/>.</summary>
    /// <returns>Returns the RID of the object.</returns>
    public RID RID { get; private set; }
    /// <summary>The colliding object.</summary>
    /// <returns>Returns the collider of the object.</returns>
    public Node Collision { get; private set; }
    /// <summary>The object's surface normal at the intersection point.</summary>
    /// <returns>Returns the surface normal of the object at the intersection point.</returns>
    public Vector2 Normal { get; private set; }
    /// <summary>The intersecting shape's metadata. This metadata is different from
    /// <seealso cref="Godot.Object.GetMeta(string, object)"/> , and is set with <seealso cref="Physics2DServer.ShapeSetData(RID, object)"/>.</summary>
    /// <returns>Returns the object's metadata.</returns>
    public object MetaData { get; private set; }
    /// <summary>The intersection point.</summary>
    /// <returns>Returns the intersection point.</returns>
    public Vector2 Position { get; private set; }
    /// <summary>The name of the object.</summary>
    /// <returns>Returns the name of the object.</returns>
    public readonly string Name => Collision.Name;

    private RayHit2D(int iD, RID rID, Vector2 normal, object metaData, Vector2 position, Node collision) : this() {
        ID = iD;
        RID = rID;
        Normal = normal;
        MetaData = metaData;
        Position = position;
        Collision = collision;
    }
    /// <summary>Checks if the <seealso cref="Dictionary"/> type value is valid for conversion.</summary>
    /// <param name="dictionary">The object to be checked.</param>
    /// <returns>Returns <c>true</c> when the <seealso cref="Dictionary"/> type value is valid.</returns>
    public static bool IsValid(Dictionary? dictionary) {
        if (dictionary == null) throw new ArgumentNullException(nameof(dictionary));
        return Hit2D.IsValid(dictionary) && dictionary.Contains(Position_Key) && dictionary.Contains(Normal_Key);
    }
    /// <summary>Explicit conversion operator.(<seealso cref="Dictionary"/> to <seealso cref="RayHit2D"/>)</summary>
    /// <remarks>
    /// The conversion from <seealso cref="Dictionary"/> to <seealso cref="RayHit2D"/> will be valid when <seealso cref="Dictionary"/> contains the keys ​​"collider_id", "rid", "metadata", "normal", "position" and "collider".
    /// </remarks> 
    /// <param name="D">Object to be converted.</param>
    /// <exception cref="ArgumentNullException">The exception is thrown when <seealso cref="Dictionary"/> object is null.</exception>
    /// <exception cref="InvalidCastException">The exception is called when the Dictionary object is not valid for conversion because it does not contain the keys "collider_id", "rid", "metadata", "normal", "position" and "collider".</exception>
    public static explicit operator RayHit2D(Dictionary? D) {
        if (D == null) throw new ArgumentNullException(nameof(D));
        else if (IsValid(D))
            return new(
                (int)D![Hit2D.ID_Key],
                (RID)D![Hit2D.RID_Key],
                (Vector2)D![Normal_Key],
                D![Hit2D.MetaData_Key],
                (Vector2)D![Position_Key],
                (Node)D![Hit2D.Collision_Key]
            );
        throw new InvalidCastException("The Dictionary object is not valid for conversion because it does not contain the keys \"collider_id\", \"rid\", \"metadata\", \"normal\", \"position\" and \"collider\".");
    }
}