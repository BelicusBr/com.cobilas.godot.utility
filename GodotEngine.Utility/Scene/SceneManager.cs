using Godot;
using System;
using Cobilas.Collections;
using Cobilas.GodotEngine.Utility.Runtime;

namespace Cobilas.GodotEngine.Utility.Scene;
/// <summary>This class can be used to manage scene switching.</summary>
[RunTimeInitializationClass(nameof(SceneManager))]
public class SceneManager : Node {
    private Scene[] scenes = Array.Empty<Scene>();
    private RunTimeInitialization? int_root = null;
    /// <summary>This event is called when a new scene is loaded.</summary>
    public static Action<Scene>? LoadedScene = null;
    /// <summary>This event is called when the current scene is unloaded.</summary>
    public static Action<Scene>? UnloadedScene = null;

    private static SceneManager? manager = null;
    /// <summary>The current scene.</summary>
    /// <returns>Returns the scene that is currently loaded.</returns>
    public static Scene CurrentScene => GetCurrentScene();
    /// <summary>The compiled scenes.</summary>
    /// <returns>Returns a list of scenes that were compiled along with the project.</returns>
    public static Scene[] BuiltScenes => manager is null ? Array.Empty<Scene>() : manager.scenes;
    /// <summary>The current scene in node form.</summary>
    /// <returns>Returns the currently loaded scene in node form.</returns>
    public static Node? CurrentSceneNode { get; private set; }
    /// <inheritdoc/>
    public override void _Ready() {
        if (manager == null) {
            manager = this;
            int_root = GetParent<RunTimeInitialization>();
            SceneTree scnt = GetTree();
            CurrentSceneNode = scnt.CurrentScene;
            scnt.Connect("node_added", this, "nodeaddedevent");
            scnt.Connect("node_removed", this, "noderemovedevent");

            using GDDirectory gdd = GDDirectory.GetGDDirectory()!;
            GDFile[] files = gdd.GetDirectory("res://Scenes/")!.GetFiles();
            scenes = new Scene[ArrayManipulation.ArrayLength(files)];
            for (int I = 0; I < ArrayManipulation.ArrayLength(files); I++)
                scenes[I] = new Scene(files[I].Path, I, NullNode.Null);
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
    public static void DontDestroyOnLoad(Node obj) {
        obj.RemoveAndSkip();
        manager!.int_root!.AddChild(obj);
    }
    /// <summary>Allows you to load a specific scene.</summary>
    /// <param name="index">The specific scene index.</param>
    /// <returns>Returns <c>true</c> if the scene loaded correctly.</returns>
    public static bool LoadScene(int index) {
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
    public static bool LoadScene(string name) {
        if (manager is not null)
            for (int I = 0; I < manager.scenes.Length; I++)
                if (manager.scenes[I] == name)
                    return LoadScene(I);
        return false;
    }

    private static Scene GetCurrentScene()
        => GetCurrentScene(CurrentSceneNode);

    private static Scene GetCurrentScene(Node? currentSceneNode) {
        if (manager is not null && currentSceneNode is not null)
            for (int I = 0; I < ArrayManipulation.ArrayLength(manager.scenes); I++)
                if (manager.scenes[I] == currentSceneNode.Filename)
                    return manager.scenes[I].SetSceneNode(currentSceneNode);
        return Scene.Empty;
    }
}
