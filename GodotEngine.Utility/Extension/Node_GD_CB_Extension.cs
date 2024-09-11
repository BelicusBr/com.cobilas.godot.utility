using System;
using Cobilas.Collections;
using Cobilas.GodotEngine.Utility.Numerics;

namespace Godot; 

public static class Node_GD_CB_Extension {

    public static void Print(this Node N, params object[] args)
        => GD.Print(args);

    public static void SetNodePosition(this Node N, Vector3D position) {
        if (N is Node2D node2D) node2D.Position = position;
        else if (N is Spatial node3D) node3D.Translation = position;
    }

    public static void SetNodeRotation(this Node N, Vector3D rotation) {
        if (N is Node2D node2D) node2D.Rotation = rotation.z;
        else if (N is Spatial node3D) node3D.Rotation = rotation;
    }

    public static void SetNodeScale(this Node N, Vector3D scale) {
        if (N is Node2D node2D) node2D.Scale = new(scale.x, scale.y);
        else if (N is Spatial node3D) node3D.Scale = scale;
    }

    public static Vector3D GetNodePosition(this Node N) {
        if (N is Node2D node2D) {
            Vector2 vector = node2D.Position;
            return new(vector.x, vector.y, 0f);
        } else if (N is Spatial node3D) return node3D.Translation;
        return Vector3.Zero;
    }

    public static Vector3D GetNodeRotation(this Node N) {
        if (N is Node2D node2D) {
            float rot = node2D.Rotation;
            return Vector3.Back * rot;
        } else if (N is Spatial node3D) return node3D.Rotation;
        return Vector3.Zero;
    }

    public static Vector3D GetNodeScale(this Node N)  {
        if (N is Node2D node2D) {
            Vector2 vector = node2D.Scale;
            return new(vector.x, vector.y, 0f);
        } else if (N is Spatial node3D) return node3D.Scale;
        return Vector3.Zero;
    }

    /// <summary>
    /// Get the nodes from a type.
    /// </summary>
    /// <param name="typeNode">The type to look for.</param>
    /// <param name="recusive">Also look for your children.</param>
    /// <returns>Returns a list of nodes.</returns>
    public static Node[] FindNodes(this Node N, Type typeNode, bool recusive) {
        Node[] nodes = Array.Empty<Node>();
        for (int I = 0; I < N.GetChildCount(); I++) {
            Node node = N.GetChild(I);
            if (node.CompareTypeAndSubType(typeNode))
                ArrayManipulation.Add(node, ref nodes);
            if (node.GetChildCount() != 0 && recusive)
                ArrayManipulation.Add(FindNodes(node, typeNode, recusive), ref nodes);
        }
        return nodes;
    }

    /// <summary>
    /// Get the nodes from a type.
    /// <para>By default, the method searches recursively.(<seealso cref="bool"/> recusive = true)</para>
    /// </summary>
    /// <param name="typeNode">The type to look for.</param>
    /// <returns>Returns a list of nodes.</returns>
    public static Node[] FindNodes(this Node N, Type typeNode)
        => FindNodes(N, typeNode, true);

    /// <summary>
    /// Get the nodes from a type.
    /// </summary>
    /// <param name="recusive">Also look for your children.</param>
    /// <typeparam name="T">The type to look for.</typeparam>
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
    /// <returns></returns>
    public static Node? FindNodeByName(this Node N, string name, Type typeNode, bool recusive) {
        foreach (var item in FindNodes(N, typeNode, recusive))
        if (item.Name == name)
            return item;
        return null;
    }

    /// <summary>
    /// Get a node from name.
    /// <para>By default, the method looks for a node of type node.(<seealso cref="Type"/> typeNode = <seealso cref="typeof"/>(<seealso cref="Node"/>))</para>
    /// </summary>
    /// <param name="name">The node name</param>
    /// <param name="recusive">Also look for your children.</param>
    public static Node? FindNodeByName(this Node N, string name, bool recusive)
        => FindNodeByName(N, name, typeof(Node), recusive);

    /// <summary>
    /// Get a node from name.
    /// <para>By default, the method looks for a node of type node.(<seealso cref="Type"/> typeNode = <seealso cref="typeof"/>(<seealso cref="Node"/>))</para>
    /// <para>By default, the method searches recursively.(<seealso cref="bool"/> recusive = true)</para>
    /// </summary>
    /// <param name="name">The node name</param>
    public static Node? FindNodeByName(this Node N, string name)
        => FindNodeByName(N, name, typeof(Node), true);

    /// <summary>
    /// Get a node from name.
    /// </summary>
    /// <param name="name">The node name</param>
    /// <param name="recusive">Also look for your children.</param>
    /// <typeparam name="T">The type to look for.</typeparam>
    public static T FindNodeByName<T>(this Node N, string name, bool recusive) where T : Node
        => (T)FindNodeByName(N, name, typeof(T), recusive)!;

    /// <summary>
    /// Get a node from name.
    /// <para>By default, the method searches recursively.(<seealso cref="bool"/> recusive = true)</para>
    /// </summary>
    /// <param name="name">The node name</param>
    public static T FindNodeByName<T>(this Node N, string name) where T : Node
        => FindNodeByName<T>(N, name, true);
}