namespace Cobilas.GodotEditor.Utility.Serialization;
/// <summary>Represents the information of a <see cref="RenderObjects.SerializedNode"/>.</summary>
public readonly struct SNInfo {
    private readonly string id;
    private readonly string nodePath;

    private static readonly SNInfo _Empty = new(string.Empty, string.Empty);
    /// <summary>Represents an empty <seealso cref="SNInfo"/>.</summary>
    /// <returns>Returns an empty representation of <seealso cref="SNInfo"/>.</returns>
    public static SNInfo Empty => _Empty;

    private SNInfo(string id, string nodePath) {
        this.id = id;
        this.nodePath = nodePath;
    }
    /// <summary>The indexer to get values ​​from the object.
    /// <para><c>"id"</c> = Get the value of the id.</para>
    /// <para><c>"nodePath"</c> = Get the value of the NodePath.</para>
    /// </summary>
    /// <param name="tag">The tag to get the value from.</param>
    /// <returns>Returns a specific value from the object.</returns>
    public object this[string tag]
        => tag switch {
            nameof(id) => id,
            nameof(nodePath) => nodePath,
            _ => NullObject.Null
        };
    /// <summary>The indexer to get values ​​from the object.
    /// <para><c>0</c> = Get the value of the id.</para>
    /// <para><c>1</c> = Get the value of the NodePath.</para>
    /// </summary>
    /// <param name="index">The index from which the value will be obtained.</param>
    /// <returns>Returns a specific value from the object.</returns>
    public object this[int index]
        => index switch {
            0 => id,
            1 => nodePath,
            _ => NullObject.Null
        };
    /// <summary>Creates an instance of the <seealso cref="SNInfo"/> object.</summary>
    /// <param name="values">The values ​​that will be inserted in the constructor.
    /// <para><c>0</c> = Gets the id value.</para>
    /// <para><c>1</c> = Gets the NodePath value.</para>
    /// </param>
    /// <returns>Returns an instance of the <seealso cref="SNInfo"/> object.</returns>
    public static SNInfo Create(params object[] values)
        => new((string)values[0], (string)values[1]);
}
