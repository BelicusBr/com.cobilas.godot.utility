namespace Cobilas.GodotEngine.Utility.Serialization {
    /// <summary>This interface allows you to create an identification for <see cref="Godot.Node"/> classes.</summary>
    public interface ISerializedObject {
        /// <summary>
        /// Allows you to create a unique ID for this object.
        /// </summary>
        /// <example>
        /// <code>
        /// // One way to use this property is in conjunction with the GetPath() method.
        /// public string ObjectID => this.GetPath().ToString().Hash().ToString()
        /// // Or using the extension NodePath_GD_CB_Extension.StringHash(this NodePath? np).
        /// public string ObjectID => this.GetPath().StringHash()
        /// </code>
        /// </example>
        /// <returns>Returns a unique id for this object.</returns>
        string ObjectID { get; }
    }
}
