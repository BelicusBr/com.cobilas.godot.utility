using Godot;
using System;
using Cobilas.Collections;
using Cobilas.GodotEngine.Utility.IO;
using Cobilas.GodotEngine.Utility.Runtime;

namespace Cobilas.GodotEngine.Utility.Scene;
/// <summary>This class can be used to manage scene switching.</summary>
[RunTimeInitializationClass(nameof(InternalSceneManager))]
internal class InternalSceneManager : Node {
    private Scene[] scenes = Array.Empty<Scene>();
    private RunTimeInitialization? int_root = null;
    /// <summary>This event is called when a new scene is loaded.</summary>
    internal static event Action<Scene>? LoadedScene = null;
    /// <summary>This event is called when the current scene is unloaded.</summary>
    internal static event Action<Scene>? UnloadedScene = null;

    private static InternalSceneManager? manager = null;
    /// <summary>The current scene.</summary>
    /// <returns>Returns the scene that is currently loaded.</returns>
    internal static Scene CurrentScene => GetCurrentScene();
    /// <summary>The compiled scenes.</summary>
    /// <returns>Returns a list of scenes that were compiled along with the project.</returns>
    internal static Scene[] BuiltScenes => manager is null ? Array.Empty<Scene>() : manager.scenes;
    /// <summary>The current scene in node form.</summary>
    /// <returns>Returns the currently loaded scene in node form.</returns>
    internal static Node? CurrentSceneNode { get; private set; }
    /// <inheritdoc/>
    public override void _Ready() {
        if (manager == null) {
            manager = this;
            int_root = GetParent<RunTimeInitialization>();
            SceneTree scnt = GetTree();
            CurrentSceneNode = scnt.CurrentScene;
            scnt.Connect("node_added", this, nameof(nodeaddedevent));
            scnt.Connect("node_removed", this, nameof(noderemovedevent));

            using Folder folder = Folder.Create("res://Scenes/");
            Archive[]? archives = folder.GetArchives();
            scenes = new Scene[ArrayManipulation.ArrayLength(archives)];
            for (int I = 0; I < ArrayManipulation.ArrayLength(archives); I++)
                scenes[I] = new(archives![I].Path, I, NullNode.Null);
        }
    }
    
    private void nodeaddedevent(Node node) {
        Scene scn = GetCurrentScene(node);
        if (scn != Scene.Empty)
            LoadedScene?.Invoke(scn);
    }

    private void noderemovedevent(Node node) {
        Scene scn = GetCurrentScene(node);
        if (scn != Scene.Empty)
            UnloadedScene?.Invoke(scn);
    }
    /// <summary>Prevents an object from being destroyed when switching scenes.</summary>
    /// <param name="obj">The object that will be marked so as not to be destroyed when changing scenes.</param>
    internal static void DontDestroyOnLoad(Node obj) {
        obj.RemoveAndSkip();
        manager?.int_root?.AddChild(obj);
    }
    /// <summary>Allows you to load a specific scene.</summary>
    /// <param name="index">The specific scene index.</param>
    /// <returns>Returns <c>true</c> if the scene loaded correctly.</returns>
    internal static bool LoadScene(int index) {
        if (manager is null || CurrentSceneNode is null) return false;
        if (index < 0 || index >= manager.scenes.Length)
            return false;
        PackedScene packed = ResourceLoader.Load<PackedScene>(manager.scenes[index].ScenePath);
        CurrentSceneNode.QueueFree();
        manager.GetTree().Root.CallDeferred("remove_child", CurrentSceneNode);

        manager.GetTree().Root.CallDeferred("add_child", CurrentSceneNode = packed.Instance());
        return true;
    }
    /// <summary>Allows you to load a specific scene.</summary>
    /// <param name="name">The specific scene name.</param>
    /// <returns>Returns <c>true</c> if the scene loaded correctly.</returns>
    internal static bool LoadScene(string name) {
        if (manager is not null) {
            int index = ArrayManipulation.FindIndex(manager.scenes, s => s == name);
            if (index >= 0)
                return LoadScene(index);
        }
        return false;
    }

    private static Scene GetCurrentScene()
        => GetCurrentScene(CurrentSceneNode);

    private static Scene GetCurrentScene(Node? currentSceneNode) {
        if (manager is not null && currentSceneNode is not null) {
            Scene scene = ArrayManipulation.Find(manager.scenes, s => s == currentSceneNode.Filename);
            if (scene != default(Scene))
                return scene.SetSceneNode(currentSceneNode);
        }
        return Scene.Empty;
    }
}
