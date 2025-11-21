using System;
using Cobilas.Collections;
using Cobilas.GodotEngine.Utility;
using Cobilas.GodotEngine.Utility.Numerics;

namespace Godot;

/// <summary>Extension for <see cref="Godot.Node"/> class.</summary>
public static class Node_GD_CB_Extension
{
    /// <summary>Allows you to define the position of the <see cref="Godot.Node"/> object.
    /// <para>This method will only take effect when the <see cref="Godot.Node"/> object inherits the <see cref="Godot.Node2D"/> class or the <see cref="Godot.Spatial"/> class.</para>
    /// </summary>
    /// <param name="N">The <see cref="Godot.Node"/> that will be used.</param>
    /// <param name="position">The new position of the <see cref="Godot.Node"/> object.</param>
    public static void SetNodePosition(this Node N, Vector3D position)
    {
        if (N is Node2D node2D) node2D.Position = position;
        else if (N is Spatial node3D) node3D.Translation = position;
    }
    /// <summary>Allows you to define the rotation of the <see cref="Godot.Node"/> object.
    /// <para>This method will only take effect when the <see cref="Godot.Node"/> object inherits the <see cref="Godot.Node2D"/> class or the <see cref="Godot.Spatial"/> class.</para>
    /// </summary>
    /// <param name="N">The <see cref="Godot.Node"/> that will be used.</param>
    /// <param name="rotation">The new rotation of the <see cref="Godot.Node"/> object.</param>
    public static void SetNodeRotation(this Node N, Vector3D rotation)
    {
        if (N is Node2D node2D) node2D.Rotation = rotation.z;
        else if (N is Spatial node3D) node3D.Rotation = rotation;
    }
    /// <summary>Allows you to define the scale of the <see cref="Godot.Node"/> object.
    /// <para>This method will only take effect when the <see cref="Godot.Node"/> object inherits the <see cref="Godot.Node2D"/> class or the <see cref="Godot.Spatial"/> class.</para>
    /// </summary>
    /// <param name="N">The <see cref="Godot.Node"/> that will be used.</param>
    /// <param name="scale">The new size or scale of the <see cref="Godot.Node"/> object.</param>
    public static void SetNodeScale(this Node N, Vector3D scale)
    {
        if (N is Node2D node2D) node2D.Scale = new(scale.x, scale.y);
        else if (N is Spatial node3D) node3D.Scale = scale;
    }
    /// <summary>Obtem a posição atual do objeto <see cref="Godot.Node"/>.
    /// <para>This method will only take effect when the <see cref="Godot.Node"/> object inherits the <see cref="Godot.Node2D"/> class or the <see cref="Godot.Spatial"/> class.</para>
    /// </summary>
    /// <param name="N">The <see cref="Godot.Node"/> that will be used.</param>
    /// <returns>Returns a <seealso cref="Vector3D"/> containing the current position of the <see cref="Godot.Node"/> object.</returns>
    public static Vector3D GetNodePosition(this Node N)
    {
        if (N is Node2D node2D) return node2D.Position;
        else if (N is Spatial node3D) return node3D.Translation;
        return Vector3.Zero;
    }
    /// <summary>Gets the current rotation of the <see cref="Godot.Node"/> object.
    /// <para>This method will only take effect when the <see cref="Godot.Node"/> object inherits the <see cref="Godot.Node2D"/> class or the <see cref="Godot.Spatial"/> class.</para>
    /// </summary>
    /// <param name="N">The <see cref="Godot.Node"/> that will be used.</param>
    /// <returns>Returns a <seealso cref="Vector3D"/> containing the current rotation of the <see cref="Godot.Node"/> object.</returns>
    public static Vector3D GetNodeRotation(this Node N)
    {
        if (N is Node2D node2D) return Vector3.Back * node2D.Rotation;
        else if (N is Spatial node3D) return node3D.Rotation;
        return Vector3.Zero;
    }
    /// <summary>Gets the current scale of the <see cref="Godot.Node"/> object.
    /// <para>This method will only take effect when the <see cref="Godot.Node"/> object inherits the <see cref="Godot.Node2D"/> class or the <see cref="Godot.Spatial"/> class.</para>
    /// </summary>
    /// <param name="N">The <see cref="Godot.Node"/> that will be used.</param>
    /// <returns>Returns a Vector3D containing the current scale of the Node object.</returns>
    public static Vector3D GetNodeScale(this Node N)
    {
        if (N is Node2D node2D) return node2D.Scale;
        else if (N is Spatial node3D) return node3D.Scale;
        return Vector3.Zero;
    }
    /// <summary>Get the nodes from a type.</summary>
    /// <param name="typeNode">The type to look for.</param>
    /// <param name="recusive">Also look for your children.</param>
    /// <param name="N">The <see cref="Godot.Node"/> that will be used.</param>
    /// <returns>Returns a list of nodes.</returns>
    public static Node[] FindNodes(this Node N, Type typeNode, bool recusive)
    {
        Node[]? nodes = null;
        for (int I = 0; I < N.GetChildCount(); I++)
        {
            Node node = N.GetChild(I);
            if (node.CompareTypeAndSubType(typeNode))
                ArrayManipulation.Add(node, ref nodes);
            if (node.GetChildCount() != 0 && recusive)
                ArrayManipulation.Add(FindNodes(node, typeNode, recusive), ref nodes);
        }
        if (nodes is null) return Array.Empty<Node>();
        return nodes;
    }

    /// <summary>
    /// Get the nodes from a type.
    /// <para>By default, the method searches recursively.(<seealso cref="bool"/> recusive = true)</para>
    /// </summary>
    /// <param name="typeNode">The type to look for.</param>
    /// <param name="N">The <see cref="Godot.Node"/> that will be used.</param>
    /// <returns>Returns a list of nodes.</returns>
    public static Node[] FindNodes(this Node N, Type typeNode)
        => FindNodes(N, typeNode, true);

    /// <summary>
    /// Get the nodes from a type.
    /// </summary>
    /// <param name="recusive">Also look for your children.</param>
    /// <typeparam name="T">The type to look for.</typeparam>
    /// <param name="N">The <see cref="Godot.Node"/> that will be used.</param>
    /// <returns>Returns a list of nodes.</returns>
    public static T[] FindNodes<T>(this Node N, bool recusive) where T : Node
        => Array.ConvertAll<Node, T>(FindNodes(N, typeof(T), recusive), (n) => (T)n);

    /// <summary>
    /// Get the nodes from a type.
    /// <para>By default, the method searches recursively.(<seealso cref="bool"/> recusive = true)</para>
    /// </summary>
    /// <typeparam name="T">The type to look for.</typeparam>
    /// <returns>Returns a list of nodes.</returns>
    public static T[] FindNodes<T>(this Node N) where T : Node
        => FindNodes<T>(N, true);

    /// <summary>
    /// Get a node from name.
    /// </summary>
    /// <param name="name">The node name</param>
    /// <param name="typeNode">The type to look for.</param>
    /// <param name="recusive">Also look for your children.</param>
    /// <param name="N">The <see cref="Godot.Node"/> that will be used.</param>
    /// <returns>The method returns the object based on its name, if not found the method returns <seealso cref="NullNode"/>.</returns>
    public static Node FindNodeByName(this Node N, string name, Type typeNode, bool recusive)
    {
        foreach (var item in FindNodes(N, typeNode, recusive))
            if (item.Name == name)
                return item;
        return NullNode.Null;
    }

    /// <summary>
    /// Get a node from name.
    /// <para>By default, the method looks for a node of type node.(<seealso cref="Type"/> typeNode = typeof(<seealso cref="Node"/>))</para>
    /// </summary>
    /// <param name="name">The node name</param>
    /// <param name="recusive">Also look for your children.</param>
    /// <param name="N">The <see cref="Godot.Node"/> that will be used.</param>
    public static Node FindNodeByName(this Node N, string name, bool recusive)
        => FindNodeByName(N, name, typeof(Node), recusive);

    /// <summary>
    /// Get a node from name.
    /// <para>By default, the method looks for a node of type node.(<seealso cref="Type"/> typeNode = typeof(<seealso cref="Node"/>))</para>
    /// <para>By default, the method searches recursively.(<seealso cref="bool"/> recusive = true)</para>
    /// </summary>
    /// <param name="name">The node name</param>
    /// <param name="N">The <see cref="Godot.Node"/> that will be used.</param>
    public static Node FindNodeByName(this Node N, string name)
        => FindNodeByName(N, name, typeof(Node), true);

    /// <summary>
    /// Get a node from name.
    /// </summary>
    /// <param name="name">The node name</param>
    /// <param name="recusive">Also look for your children.</param>
    /// <typeparam name="T">The type to look for.</typeparam>
    /// <param name="N">The <see cref="Godot.Node"/> that will be used.</param>
    public static T FindNodeByName<T>(this Node N, string name, bool recusive) where T : Node
        => (T)FindNodeByName(N, name, typeof(T), recusive)!;

    /// <summary>
    /// Get a node from name.
    /// <para>By default, the method searches recursively.(<seealso cref="bool"/> recusive = true)</para>
    /// </summary>
    /// <param name="name">The node name</param>
    /// <param name="N">The <see cref="Godot.Node"/> that will be used.</param>
    public static T FindNodeByName<T>(this Node N, string name) where T : Node
        => FindNodeByName<T>(N, name, true);

    /// <inheritdoc cref="Node.Duplicate(int)"/>
    public static T Duplicate<T>(this Node node, int flags = 15) where T : Node
        => (T)node.Duplicate(flags);
        
    /// <summary>Allows you to check whether a given <seealso cref="Godot.Node"/> object is a child.</summary>
    /// <param name="origin">The target object.</param>
    /// <param name="node">The <seealso cref="Godot.Node"/> to be compared.</param>
    /// <returns>Returns <c>true</c> when the target object contains the comparison object.</returns>
    public static bool ContainsNode(this Node origin, Node node) {
        for (int I = 0; I < origin.GetChildCount(); I++)
            if (origin.GetChild(I) == node)
                return true;
        return false;
    }
    /// <summary>The method allows you to change the object's parent.</summary>
    /// <param name="p">The target object.</param>
    /// <param name="parent">The object that will be the parent of the target object.</param>
    /// <exception cref="ArgumentNullException">Occurs when the target object parameter is passed as null.</exception>
    public static void SetParent(this Node? p, Node? parent) {
        if (p is null) throw new ArgumentNullException(nameof(p));
        Node old_parent = p.GetParent();
        old_parent?.RemoveChild(p);
        parent?.AddChild(p);
    }
}