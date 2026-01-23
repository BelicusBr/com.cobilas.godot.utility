using Godot;
using System;

namespace Cobilas.GodotEngine.Utility.Scene;
/// <summary>This class can be used to manage scene switching.</summary>
public static class SceneManager {
    /// <summary>This event is called when a new scene is loaded.</summary>
    public static event Action<Scene>? LoadedScene {
        add => InternalSceneManager.LoadedScene += value;
        remove => InternalSceneManager.LoadedScene -= value;
    }
    /// <summary>This event is called when the current scene is unloaded.</summary>
    public static event Action<Scene>? UnloadedScene {
        add => InternalSceneManager.UnloadedScene += value;
        remove => InternalSceneManager.UnloadedScene -= value;
    }
    /// <summary>The current scene.</summary>
    /// <returns>Returns the scene that is currently loaded.</returns>
    public static Scene CurrentScene => InternalSceneManager.CurrentScene;
    /// <summary>The compiled scenes.</summary>
    /// <returns>Returns a list of scenes that were compiled along with the project.</returns>
    public static Scene[] BuiltScenes => InternalSceneManager.BuiltScenes;
    /// <summary>The current scene in node form.</summary>
    /// <returns>Returns the currently loaded scene in node form.</returns>
    public static Node? CurrentSceneNode => InternalSceneManager.CurrentSceneNode;
    /// <summary>Prevents an object from being destroyed when switching scenes.</summary>
    /// <param name="obj">The object that will be marked so as not to be destroyed when changing scenes.</param>
    public static void DontDestroyOnLoad(Node obj)
        => InternalSceneManager.DontDestroyOnLoad(obj);
	/// <summary>Sets up the scene utilities on the specified node.</summary>
	/// <param name="obj">The node to configure with scene utilities.</param>
	/// <remarks>This method initializes the necessary components on the node to enable scene management functionality.</remarks>
	public static void SetSceneUtilities(Node obj)
		=> InternalSceneManager.SetSceneUtilities(obj);
	/// <summary>Allows you to load a specific scene.</summary>
	/// <param name="index">The specific scene index.</param>
	/// <returns>Returns <c>true</c> if the scene loaded correctly.</returns>
	public static bool LoadScene(int index)
        => InternalSceneManager.LoadScene(index);
    /// <summary>Allows you to load a specific scene.</summary>
    /// <param name="name">The specific scene name.</param>
    /// <returns>Returns <c>true</c> if the scene loaded correctly.</returns>
    public static bool LoadScene(string name)
        => InternalSceneManager.LoadScene(name);
}
