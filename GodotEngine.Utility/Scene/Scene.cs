using System;
using System.IO;

namespace Cobilas.GodotEngine.Utility.Scene; 
/// <summary>Contains the scene information.</summary>
public struct Scene : IEquatable<Scene>, IEquatable<int>, IEquatable<string?> {
    /// <summary>The scene index.</summary>
    /// <returns>Returns the scene index.</returns>
    public int Index { get; private set; }
    /// <summary>Stores the root <see cref="Godot.Node"/> of the scenario.</summary>
    /// <returns>Returns a <see cref="Godot.Node"/> type object that is the root of the scenario.</returns>
    public Godot.Node? SceneNode { get; private set; }
    /// <summary>The path of the scene file.</summary>
    /// <returns>Returns the full or relative path of the scene file.</returns>
    public string? ScenePath { get; private set; }
    /// <summary>The name of the scene file.</summary>
    /// <returns>Returns a string containing the name of the scene file with its extension.</returns>
    /// <exception cref="ArgumentException">path contains one or more of the invalid characters defined in <seealso cref="Path.GetInvalidPathChars"/>.</exception>
    public readonly string Name => Path.GetFileName(ScenePath);
    /// <summary>The name of the scene file without the extension.</summary>
    /// <returns>Returns a string containing the name of the scene file without its extension.</returns>
    /// <exception cref="ArgumentException">path contains one or more of the invalid characters defined in <seealso cref="Path.GetInvalidPathChars"/>.</exception>
    public readonly string NameWithoutExtension => Path.GetFileNameWithoutExtension(ScenePath);

    private readonly static Scene _Empty = new("None", -1, NullNode.Null);
    /// <summary>Empty scenario.</summary>
    /// <returns>Returns a representation of an empty scenario.</returns>
    public static Scene Empty => _Empty;
    /// <summary>Starts a new instance of the object.</summary>
    public Scene(string scenePath, int index, Godot.Node sceneNode) {
        Index = index;
        ScenePath = scenePath;
        SceneNode = sceneNode;
    }
    /// <inheritdoc/>
    public readonly bool Equals(int other) => Index.Equals(other);
    /// <inheritdoc/>
    public readonly bool Equals(Scene other) => Equals(other.ScenePath) && Equals(other.Index);
    /// <inheritdoc/>
    public readonly bool Equals(string? other) {
        if (ScenePath is not null) return ScenePath.Equals(other);
        else if (Name is not null) return Name.Equals(other);
        else if (NameWithoutExtension is not null) return NameWithoutExtension.Equals(other);
        return false;
    }
    /// <inheritdoc/>
    public override readonly bool Equals(object obj)
        => obj switch {
            Scene scn => Equals(scn),
            int ind => Equals(ind),
            string stg => Equals(stg),
            _ => false
        };
    internal Scene SetSceneNode(Godot.Node sceneNode) {
        this.SceneNode = sceneNode;
        return this;
    }
    /// <inheritdoc/>
    public override readonly int GetHashCode()
        => base.GetHashCode() >> 2 ^ Index.GetHashCode() << 2 ^ (ScenePath ?? string.Empty).GetHashCode();
    /// <inheritdoc/>
    public override readonly string ToString()
        => $"[Name:{Name}, Index:{Index}, Path:{ScenePath}]";
    /// <summary>Indicates whether this instance is equal to another instance of the same type.</summary>
    /// <param name="left">Object to be compared.</param>
    /// <param name="right">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator ==(Scene left, Scene right) => left.Equals(right);
    /// <summary>Indicates whether this instance is different from another instance of the same type.</summary>
    /// <param name="left">Object to be compared.</param>
    /// <param name="right">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator !=(Scene left, Scene right) => !left.Equals(right);
    /// <summary>Indicates whether this instance is equal to another instance of the same type.</summary>
    /// <param name="left">Object to be compared.</param>
    /// <param name="right">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator ==(Scene left, string right) => left.Equals(right);
    /// <summary>Indicates whether this instance is different from another instance of the same type.</summary>
    /// <param name="left">Object to be compared.</param>
    /// <param name="right">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator !=(Scene left, string right) => !left.Equals(right);
    /// <summary>Indicates whether this instance is equal to another instance of the same type.</summary>
    /// <param name="left">Object to be compared.</param>
    /// <param name="right">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator ==(Scene left, int right) => left.Equals(right);
    /// <summary>Indicates whether this instance is different from another instance of the same type.</summary>
    /// <param name="left">Object to be compared.</param>
    /// <param name="right">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator !=(Scene left, int right) => !left.Equals(right);
    /// <summary>Explicit conversion operator.(<seealso cref="Scene"/> to <seealso cref="Int32"/>)</summary>
    /// <param name="scene">Object to be converted.</param>
    public static explicit operator int(Scene scene) => scene.Index;
    /// <summary>Explicit conversion operator.(<seealso cref="Scene"/> to <seealso cref="String"/>)</summary>
    /// <param name="value">Object to be converted.</param>
    public static explicit operator string(Scene value) => value.ScenePath ?? string.Empty;
}