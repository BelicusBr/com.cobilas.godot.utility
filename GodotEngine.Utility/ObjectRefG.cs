using Godot;
using System;

namespace Cobilas.GodotEngine.Utility;

/// <summary>
/// Represents a generic reference to a specific type of Godot Node with automatic 
/// path resolution and node caching.
/// </summary>
/// <typeparam name="T">The type of Godot Node this reference points to, must inherit from <see cref="Godot.Node"/>.</typeparam>
/// <remarks>
/// This generic class provides type-safe implicit conversions between NodePath, string, and the specific Node type T,
/// making it easier to work with typed node references in Godot while maintaining type safety.
/// </remarks>
[Serializable]
public class ObjectRef<T> : ObjectRef where T : Node {
	/// <summary>Gets the typed node instance resolved from the path.</summary>
	/// <returns>The resolved Node instance of type T.</returns>
	public T Value {
		get {
			if (node is NullNode) return (T)(node = path.GetNode());
			else if (node is null) return (T)(node = path.GetNode());
			else if (node.GetPath() != path) return (T)(node = path.GetNode());
			return (T)node;
		}
	}
	/// <summary>Initializes a new instance of the <see cref="ObjectRef{T}"/> class with empty path and null node.</summary>
	public ObjectRef() : base() { }
	/// <summary>Initializes a new instance of the <see cref="ObjectRef{T}"/> class with the specified path.</summary>
	/// <param name="path">The NodePath to initialize the reference with.</param>
	protected ObjectRef(NodePath path) : base(path) { }
	/// <summary>Implicitly converts an <see cref="ObjectRef{T}"/> to a <see cref="NodePath"/>.</summary>
	/// <param name="obj">The ObjectRef to convert.</param>
	/// <returns>The NodePath contained in the ObjectRef.</returns>
	/// <exception cref="ArgumentNullException">Thrown when obj is null.</exception>
	public static implicit operator NodePath(ObjectRef<T>? obj)
		=> obj is null ? throw new ArgumentNullException(nameof(obj)) : obj.path;
	/// <summary>Implicitly converts an <see cref="ObjectRef{T}"/> to a string.</summary>
	/// <param name="obj">The ObjectRef to convert.</param>
	/// <returns>The string representation of the NodePath.</returns>
	/// <exception cref="ArgumentNullException">Thrown when obj is null.</exception>
	public static implicit operator string(ObjectRef<T>? obj)
		=> obj is null ? throw new ArgumentNullException(nameof(obj)) : obj.path;
	/// <summary>Implicitly converts a <see cref="NodePath"/> to an <see cref="ObjectRef{T}"/>.</summary>
	/// <param name="obj">The NodePath to convert.</param>
	/// <returns>A new ObjectRef containing the specified NodePath.</returns>
	/// <exception cref="ArgumentNullException">Thrown when obj is null.</exception>
	public static implicit operator ObjectRef<T>(NodePath? obj)
		=> new(obj is null ? throw new ArgumentNullException(nameof(obj)) : obj);
	/// <summary>Implicitly converts a string to an <see cref="ObjectRef{T}"/>.</summary>
	/// <param name="obj">The string representing a NodePath to convert.</param>
	/// <returns>A new ObjectRef containing the specified NodePath.</returns>
	/// <exception cref="ArgumentNullException">Thrown when obj is null.</exception>
	public static implicit operator ObjectRef<T>(string? obj)
		=> new(obj is null ? throw new ArgumentNullException(nameof(obj)) : obj);
	/// <summary>Implicitly converts an <see cref="ObjectRef{T}"/> to the specified Node type T.</summary>
	/// <param name="obj">The ObjectRef to convert.</param>
	/// <remarks>
	/// This operator automatically resolves the node from the path and caches it for future use.
	/// If the node's path has changed, it will re-resolve the node from the current path.
	/// The node is cast to type T, ensuring type safety.
	/// </remarks>
	/// <returns>The resolved Node instance of type T.</returns>
	/// <exception cref="ArgumentNullException">Thrown when obj is null.</exception>
	public static implicit operator T(ObjectRef<T>? obj) {
		if (obj is null) throw new ArgumentNullException(nameof(obj));
		else if (obj.node is null) return (T)(obj.node = obj.path.GetNode());
		else if (obj.node.GetPath() != obj.path) return (T)(obj.node = obj.path.GetNode());
		return (T)obj.node;
	}
}