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
    public bool IsInternal => ArchiveInfo._IsInternal(_path);
    /// <inheritdoc/>
    public bool IsGodotRoot => GodotPath.IsGodotRoot(_path);
    /// <inheritdoc/>
    public DateTime GetCreationTime => IsInternal ? DateTime.MinValue : File.GetCreationTime(GodotPath.GlobalizePath(_path));
    /// <inheritdoc/>
    public DateTime GetCreationTimeUtc => IsInternal ? DateTime.MinValue : File.GetCreationTimeUtc(GodotPath.GlobalizePath(_path));
    /// <inheritdoc/>
    public DateTime GetLastAccessTime => IsInternal ? DateTime.MinValue : File.GetLastAccessTime(GodotPath.GlobalizePath(_path));
    /// <inheritdoc/>
    public DateTime GetLastAccessTimeUtc => IsInternal ? DateTime.MinValue : File.GetLastAccessTimeUtc(GodotPath.GlobalizePath(_path));
    /// <inheritdoc/>
    public DateTime GetLastWriteTime => IsInternal ? DateTime.MinValue : File.GetLastWriteTime(GodotPath.GlobalizePath(_path));
    /// <inheritdoc/>
    public DateTime GetLastWriteTimeUtc => IsInternal ? DateTime.MinValue : File.GetLastWriteTimeUtc(GodotPath.GlobalizePath(_path));
}
