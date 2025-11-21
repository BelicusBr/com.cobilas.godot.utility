using Godot;
using System;
using Godot.Collections;

namespace Cobilas.GodotEngine.Utility.Physics; 
/// <summary>Responsible for storing information about a 2D collision.</summary>
public struct Hit2D {
    /// <summary>Represents the collider_id key.</summary>
    public const string ID_Key = "collider_id";
    /// <summary>Represents the rid key.</summary>
    public const string RID_Key = "rid";
    /// <summary>Represents the collider key.</summary>
    public const string Collision_Key = "collider";
    /// <summary>Represents the metadata key.</summary>
    public const string MetaData_Key = "metadata";

    /// <summary>The colliding object's ID.</summary>
    /// <returns>Returns the object ID.</returns>
    public int ID { get; private set; }
    /// <summary>The intersecting object's <see cref="Godot.RID"/>.</summary>
    /// <returns>Returns the RID of the object.</returns>
    public RID RID { get; private set; }
    /// <summary>The colliding object.</summary>
    /// <returns>Returns the collider of the object.</returns>
    public Node Collision { get; private set; }
    /// <summary>The intersecting shape's metadata. This metadata is different from
    /// <seealso cref="Godot.Object.GetMeta(string, object)"/> , and is set with <seealso cref="Physics2DServer.ShapeSetData(RID, object)"/>.</summary>
    /// <returns>Returns the object's metadata.</returns>
    public object MetaData { get; private set; }
    /// <summary>The name of the object.</summary>
    /// <returns>Returns the name of the object.</returns>
    public readonly string Name => Collision.Name;

    private Hit2D(int iD, RID rID, object metaData, Node collision) {
        ID = iD;
        RID = rID;
        MetaData = metaData;
        Collision = collision;
    }
    /// <summary>Checks if the <seealso cref="Dictionary"/> type value is valid for conversion.</summary>
    /// <param name="dictionary">The object to be checked.</param>
    /// <returns>Returns <c>true</c> when the <seealso cref="Dictionary"/> type value is valid.</returns>
    public static bool IsValid(Dictionary? dictionary) {
        if (dictionary == null) throw new ArgumentNullException(nameof(dictionary));
        return dictionary.Contains(ID_Key) && dictionary.Contains(RID_Key) &&
            dictionary.Contains(MetaData_Key) && dictionary.Contains(Collision_Key);
    }
    /// <summary>Explicit conversion operator.(<seealso cref="Dictionary"/> to <seealso cref="Hit2D"/>)</summary>
    /// <remarks>
    /// The conversion from <seealso cref="Dictionary"/> to <seealso cref="Hit2D"/> will be valid when <seealso cref="Dictionary"/> contains the keys ​​"collider_id", "rid", "metadata" and "collider".
    /// </remarks> 
    /// <param name="D">Object to be converted.</param>
    /// <exception cref="ArgumentNullException">The exception is thrown when <seealso cref="Dictionary"/> object is null.</exception>
    /// <exception cref="InvalidCastException">The exception is called when the Dictionary object is not valid for conversion because it does not contain the keys "collider_id", "rid", "metadata" and "collider".</exception>
    public static explicit operator Hit2D(Dictionary? D) {
        if (D == null) throw new ArgumentNullException(nameof(D));
        else if (IsValid(D))
            return new(
                (int)D![ID_Key],
                (RID)D![RID_Key],
                D![MetaData_Key],
                (Node)D![Collision_Key]
            );
        throw new InvalidCastException("The Dictionary object is not valid for conversion because it does not contain the keys \"collider_id\", \"rid\", \"metadata\" and \"collider\".");
    }
}