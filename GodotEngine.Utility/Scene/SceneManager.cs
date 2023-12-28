using Godot;
using System;
using Cobilas.Collections;
using Cobilas.GodotEngine.Utility.Runtime;

namespace Cobilas.GodotEngine.Utility.Scene {
    [RunTimeInitializationClass(nameof(SceneManager))]
    public class SceneManager : Node {
        private Scene[] scenes;
        private RunTimeInitialization int_root;
        
        public static Action<Scene> LoadedScene;
        public static Action<Scene> UnloadedScene;

        private static SceneManager manager = null;

        public static Scene CurrentScene { get; private set; }
        public static Node CurrentSceneNode { get; private set; }

        public override void _Ready() {
            if (manager == null) {
                manager = this;
                int_root = GetParent<RunTimeInitialization>();
                Viewport root = GetTree().Root;
                CurrentSceneNode = root.GetChild(root.GetChildCount() - 1);
                using (GDDirectory gdd = GDDirectory.GetGDDirectory()) {
                    GDFile[] files = gdd.GetDirectory("res://Scenes/").GetFiles();
                    scenes = new Scene[ArrayManipulation.ArrayLength(files)];
                    for (int I = 0; I < ArrayManipulation.ArrayLength(files); I++)
                        scenes[I] = new Scene(files[I].Path, I);
                }
                CurrentScene = GetCurrentScene();
            }
        }

        public static void DontDestroyOnLoad(Node obj) {
            obj.RemoveAndSkip();
            manager.int_root.AddChild(obj);
        }

        public static bool LoadScene(int index) {
            if (index < 0 || index >= manager.scenes.Length)
                return false;
            PackedScene packed = ResourceLoader.Load<PackedScene>(manager.scenes[index].ScenePath);
            CurrentSceneNode.QueueFree();
            UnloadedScene?.Invoke(CurrentScene);
            CurrentSceneNode = packed.Instance();
            manager.GetTree().Root.AddChild(CurrentSceneNode);
            manager.GetTree().CurrentScene = CurrentSceneNode;
            CurrentScene = GetCurrentScene();
            LoadedScene?.Invoke(CurrentScene);
            return true;
        }

        public static bool LoadScene(string name) {
            for (int I = 0; I < manager.scenes.Length; I++)
                if (manager.scenes[I].Name == name || 
                manager.scenes[I].NameWithoutExtension == name)
                    return LoadScene(I);
            return false;
        }

        private static Scene GetCurrentScene() {
            for (int I = 0; I < ArrayManipulation.ArrayLength(manager.scenes); I++)
                if (manager.scenes[I].ScenePath == CurrentSceneNode.Filename)
                    return manager.scenes[I];
            return default;
        }
    }
}
