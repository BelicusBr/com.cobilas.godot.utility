using System;
using System.IO;

namespace Cobilas.GodotEngine.Utility.IO;
/// <summary>The class allows manipulation of system paths in godot.</summary>
public static class GodotPath {
    /// <summary>Contains the directory separator used in godot.</summary>
    public const char DirectorySeparatorChar = '/';

    private const string res_path = "res://";
    private const string user_path = "user://";

    /// <inheritdoc cref="Environment.CurrentDirectory"/>
    public static string CurrentDirectory => Environment.CurrentDirectory;
    /// <summary>The path to the project's root folder.</summary>
    /// <returns>Returns the root path of the project folder. 
    /// When the project is compiled the property will use the <seealso cref="GodotPath.CurrentDirectory"/> property.</returns>
    public static string ProjectPath {
        get {
            if (GDFeature.HasEditor)
                return Godot.ProjectSettings.GlobalizePath(res_path);
            return CurrentDirectory;
        }
    }
    /// <summary>The path to the project's persistent files directory.</summary>
    /// <returns>Returns a string containing the path to the project's persistent files directory.</returns>
    public static string PersistentFilePath => Godot.ProjectSettings.GlobalizePath(user_path);
    /// <inheritdoc cref="Path.Combine(string[])"/>
    public static string Combine(params string[] paths) => ICombine(paths);
    /// <inheritdoc cref="Path.Combine(string, string)"/>
    public static string Combine(string path1, string path2) => ICombine(path1, path2);
    /// <inheritdoc cref="Path.Combine(string, string, string)"/>
    public static string Combine(string path1, string path2, string path3) => ICombine(path1, path2, path3);
    /// <inheritdoc cref="Path.Combine(string, string, string, string)"/>
    public static string Combine(string path1, string path2, string path3, string path4) => ICombine(path1, path2, path3, path4);
    /// <inheritdoc cref="Path.GetFileName(string)"/>
    public static string GetFileName(string path) => Path.GetFileName(path);
    /// <inheritdoc cref="Path.GetPathRoot(string)"/>
    public static string GetPathRoot(string path) => IGetPathRoot(path);
    /// <inheritdoc cref="Path.HasExtension(string)"/>
    public static bool HasExtension(string path) => Path.HasExtension(path);
    /// <inheritdoc cref="Path.GetInvalidPathChars"/>
    public static char[] GetInvalidPathChars() => Path.GetInvalidPathChars();
    /// <inheritdoc cref="Path.GetExtension(string)"/>
    public static string GetExtension(string path) => Path.GetExtension(path);
    /// <inheritdoc cref="Path.GetInvalidFileNameChars"/>
    public static char[] GetInvalidFileNameChars() => Path.GetInvalidFileNameChars();
    /// <inheritdoc cref="Path.GetDirectoryName(string)"/>
    public static string GetDirectoryName(string path)
        => (path = Path.GetDirectoryName(path)) switch {
            "res:" => res_path,
            "user:" => user_path,
            _ => path
        };
    /// <inheritdoc cref="Path.GetFileNameWithoutExtension(string)"/>
    public static string GetFileNameWithoutExtension(string path) => Path.GetFileNameWithoutExtension(path);
    /// <summary>Allows you to check if the system file name contains an invalid character.</summary>
    /// <param name="name">The name to be checked.</param>
    /// <param name="invalidChar">Returns the invalid character.</param>
    /// <returns>Returns <c>true</c> when an invalid character is encountered.</returns>
    public static bool IsInvalidFileName(string name, out char invalidChar) {
        invalidChar = char.MinValue;
        if (string.IsNullOrEmpty(name)) return false;
        char[] chars = GetInvalidFileNameChars();
        for (int I = 0, J = 0; I < name.Length; J++) {
            if (J >= chars.Length) {
                J = -1;
                I++;
                continue;
            }
            if (name[I] == chars[J]) {
                invalidChar = chars[J];
                return true;
            }
        }
        return false;
    }
    /// <inheritdoc cref="Godot.ProjectSettings.GlobalizePath(string)"/>
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
