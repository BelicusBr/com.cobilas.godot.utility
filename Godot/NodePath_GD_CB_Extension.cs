using System;
using Cobilas.GodotEngine.Utility;
using Cobilas.GodotEngine.Utility.Scene;

namespace Godot;
/// <summary>Extensions for <seealso cref="NodePath"/> class.</summary>
public static class NodePath_GD_CB_Extension {
    /// <summary>Hash the <seealso cref="NodePath"/> and return a 32 bits unsigned integer.</summary>
    /// <param name="np"><seealso cref="NodePath"/> to be calculated.</param>
    /// <returns>The calculated hash of the <seealso cref="NodePath"/>.</returns>
    /// <exception cref="ArgumentNullException">Occurs when the np parameter is null!</exception>
    public static uint Hash(this NodePath? np) {
        if (np is null) throw new ArgumentNullException(nameof(np));
        return np.ToString().Hash();
    }
    /// <summary>Hash the <seealso cref="string"/> and return a 32 bits unsigned integer.</summary>
    /// <param name="np"><seealso cref="NodePath"/> to be calculated.</param>
    /// <returns>The calculated hash of the <seealso cref="string"/>.</returns>
    /// <exception cref="ArgumentNullException">Occurs when the np parameter is null!</exception>
    public static string StringHash(this NodePath? np) => Hash(np).ToString();
    /// <summary>Fetches a node. The NodePath can be either a relative path (from the current node) or an absolute path (in the scene tree) to a node.</summary>
    /// <param name="np"><seealso cref="NodePath"/> from where the node will be obtained.</param>
    /// <returns>returns the node that was obtained from the <seealso cref="NodePath"/>.</returns>
    public static Node GetNode(this NodePath? np) => IGetNode(np);
    /// <inheritdoc cref="GetNode(NodePath?)"/>
    public static T GetNode<T>(this NodePath? np) where T : Node => (T)IGetNode(np);

    private static Node IGetNode(this NodePath? np) {
        if (SceneManager.CurrentSceneNode is null) return NullNode.Null;
        Node scene = SceneManager.CurrentSceneNode;
        if (np is null) throw new ArgumentNullException(nameof(np));
        return scene.GetNode(np);
    }
}
