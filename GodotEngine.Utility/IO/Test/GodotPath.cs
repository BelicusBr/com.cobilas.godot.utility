using System;
using System.IO;

namespace Cobilas.GodotEngine.Utility.IO.Test;

public static class GodotPath {

    public const char DirectorySeparatorChar = '/';

    public static string CurrentDirectory => Environment.CurrentDirectory;
    public static string ProjectPath {
        get {
            if (GDFeature.HasEditor)
                return Godot.ProjectSettings.GlobalizePath("res://");
            return CurrentDirectory;
        }
    }
    public static string PersistentFilePath => Godot.ProjectSettings.GlobalizePath("user://");

    public static string Combine(params string[] paths) => ICombine(paths);
    public static string Combine(string path1, string path2) => ICombine(path1, path2);
    public static string Combine(string path1, string path2, string path3) => ICombine(path1, path2, path3);
    public static string Combine(string path1, string path2, string path3, string path4) => ICombine(path1, path2, path3, path4);

    public static string GetFileName(string path) => Path.GetFileName(path);
    public static string GetPathRoot(string path) => IGetPathRoot(path);
    public static bool HasExtension(string path) => Path.HasExtension(path);
    public static char[] GetInvalidPathChars() => Path.GetInvalidPathChars();
    public static string GetExtension(string path) => Path.GetExtension(path);
    public static char[] GetInvalidFileNameChars() => Path.GetInvalidFileNameChars();
    public static string GetDirectoryName(string path) => Path.GetDirectoryName(path);
    public static string GetFileNameWithoutExtension(string path) => Path.GetFileNameWithoutExtension(path);

    public static string GlobalizePath(string path) 
        => IGetPathRoot(path) switch {
                "res://" => GDFeature.HasEditor ? Godot.ProjectSettings.GlobalizePath(path) : ICombine(CurrentDirectory, Godot.ProjectSettings.GlobalizePath(path)),
                "user://" => Godot.ProjectSettings.GlobalizePath(path),
                _ => path,
            };

    private static string ICombine(params string[] paths) => Path.Combine(paths).Replace('\\', DirectorySeparatorChar);

    private static string IGetPathRoot(string path) {
        int index = path.IndexOf("://");
        if (index != -1) return $"{path.Remove(index)}://";
        return Path.GetPathRoot(path);
    }
}
