using System;
using System.IO;

namespace Cobilas.GodotEngine.Utility.IO;
/// <summary>Represents information about a folder or directory in the Godot engine.</summary>
/// <remarks>
/// This structure provides access to timestamp information for both filesystem directories and internal Godot resource folders.
/// For internal folders (those with paths starting with GodotPath.ResPath), all timestamp methods return DateTime.MinValue.
/// </remarks>
/// <param name="path">The path to the folder or directory.</param>
public readonly struct FolderInfo(string path) : IDataInfo {
    private readonly string _path = path;
    /// <inheritdoc/>
    public bool IsInternal => !Directory.Exists(GodotPath.GlobalizePath(_path));
    /// <inheritdoc/>
    public DateTime GetCreationTime => IsInternal ? DateTime.MinValue : Directory.GetCreationTime(_path);
    /// <inheritdoc/>
    public DateTime GetCreationTimeUtc => IsInternal ? DateTime.MinValue : Directory.GetCreationTimeUtc(_path);
    /// <inheritdoc/>
    public DateTime GetLastAccessTime => IsInternal ? DateTime.MinValue : Directory.GetLastAccessTime(_path);
    /// <inheritdoc/>
    public DateTime GetLastAccessTimeUtc => IsInternal ? DateTime.MinValue : Directory.GetLastAccessTimeUtc(_path);
    /// <inheritdoc/>
    public DateTime GetLastWriteTime => IsInternal ? DateTime.MinValue : Directory.GetLastWriteTime(_path);
    /// <inheritdoc/>
    public DateTime GetLastWriteTimeUtc => IsInternal ? DateTime.MinValue : Directory.GetLastWriteTimeUtc(_path);
}
