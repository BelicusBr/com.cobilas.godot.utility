using Godot;
using System;

namespace Cobilas.GodotEngine.Utility;
/// <summary>Represents a reference to a Godot node with automatic path resolution and node caching.</summary>
/// <remarks>
/// This class provides a convenient way to store and resolve node references in Godot,
/// with implicit conversions between NodePath, string, and Node types.
/// </remarks>
[Serializable]
public class ObjectRef {
	/// <summary>The cached node instance resolved from the path.</summary>
	protected Node node;
	/// <summary>The path to the node in the Godot scene tree.</summary>
	protected NodePath path;
	/// <summary>Initializes a new instance of the <see cref="ObjectRef"/> class with empty path and null node.</summary>
	public ObjectRef() {
		path = string.Empty;
		node = NullNode.Null;
	}
	/// <summary>Initializes a new instance of the <see cref="ObjectRef"/> class with the specified node path.</summary>
	/// <param name="path">The node path to initialize the reference with.</param>
	protected ObjectRef(NodePath path) : this() => this.path = path;

	internal void Set(NodePath? path) => this.path = path ?? string.Empty;
	/// <summary>Implicitly converts an <see cref="ObjectRef"/> to a <see cref="NodePath"/>.</summary>
	/// <param name="obj">The ObjectRef to convert.</param>
	/// <returns>The NodePath stored in the ObjectRef.</returns>
	/// <exception cref="ArgumentNullException">Thrown when obj is null.</exception>
	public static implicit operator NodePath(ObjectRef? obj)
		=> obj is null ? throw new ArgumentNullException(nameof(obj), "ObjectRef? to NodePath") : obj.path;
	/// <summary>Implicitly converts an <see cref="ObjectRef"/> to a string.</summary>
	/// <param name="obj">The ObjectRef to convert.</param>
	/// <returns>The string representation of the NodePath.</returns>
	/// <exception cref="ArgumentNullException">Thrown when obj is null.</exception>
	public static implicit operator string(ObjectRef? obj)
		=> obj is null ? throw new ArgumentNullException(nameof(obj), "ObjectRef? to string") : obj.path;
	/// <summary>Implicitly converts a <see cref="NodePath"/> to an <see cref="ObjectRef"/>.</summary>
	/// <param name="obj">The NodePath to convert.</param>
	/// <returns>A new ObjectRef instance with the specified NodePath.</returns>
	/// <exception cref="ArgumentNullException">Thrown when obj is null.</exception>
	public static implicit operator ObjectRef(NodePath? obj)
		=> new(obj is null ? throw new ArgumentNullException(nameof(obj), "NodePath? to ObjectRef") : obj);
	/// <summary>Implicitly converts a string to an <see cref="ObjectRef"/>.</summary>
	/// <param name="obj">The string representing a node path to convert.</param>
	/// <returns>A new ObjectRef instance with the specified node path.</returns>
	/// <exception cref="ArgumentNullException">Thrown when obj is null.</exception>
	public static implicit operator ObjectRef(string? obj)
		=> new(obj is null ? throw new ArgumentNullException(nameof(obj), "string? to ObjectRef") : obj);
	/// <summary>Implicitly converts an <see cref="ObjectRef"/> to a <see cref="Node"/>.</summary>
	/// <param name="obj">The ObjectRef to convert.</param>
	/// <remarks>
	/// This operator automatically resolves the node from the stored path and caches it.
	/// If the node's path has changed, it will re-resolve the node from the stored path.
	/// </remarks>
	/// <returns>The resolved Node instance.</returns>
	/// <exception cref="ArgumentNullException">Thrown when obj is null.</exception>
	public static implicit operator Node(ObjectRef? obj) {
		if (obj is null) throw new ArgumentNullException(nameof(obj), "ObjectRef? to Node");
		else if (obj.node is null) return obj.node = obj.path.GetNode();
		else if (!obj.node.IsInsideTree()) return obj.node = obj.path.GetNode();
		else if (obj.node.GetPath() != obj.path) return obj.node = obj.path.GetNode();
		return obj.node;
	}
}