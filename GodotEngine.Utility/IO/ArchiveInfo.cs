using System;
using System.IO;

namespace Cobilas.GodotEngine.Utility.IO;
/// <summary>Represents information about an archive file or resource in the Godot engine.</summary>
/// <remarks>
/// This structure provides access to timestamp information for both filesystem files and internal Godot resources.
/// For internal resources (those with paths starting with GodotPath.ResPath), all timestamp methods return DateTime.MinValue.
/// </remarks>
/// <param name="path">The path to the archive file or resource.</param>
public readonly struct ArchiveInfo(string path) : IDataInfo {
    private readonly string _path = path;
    /// <inheritdoc/>
    public bool IsInternal => !File.Exists(GodotPath.GlobalizePath(_path));
    /// <inheritdoc/>
    public bool IsGodotRoot => GodotPath.IsGodotRoot(_path);
    /// <inheritdoc/>
    public DateTime GetCreationTime => IsInternal ? DateTime.MinValue : File.GetCreationTime(_path);
    /// <inheritdoc/>
    public DateTime GetCreationTimeUtc => IsInternal ? DateTime.MinValue : File.GetCreationTimeUtc(_path);
    /// <inheritdoc/>
    public DateTime GetLastAccessTime => IsInternal ? DateTime.MinValue : File.GetLastAccessTime(_path);
    /// <inheritdoc/>
    public DateTime GetLastAccessTimeUtc => IsInternal ? DateTime.MinValue : File.GetLastAccessTimeUtc(_path);
    /// <inheritdoc/>
    public DateTime GetLastWriteTime => IsInternal ? DateTime.MinValue : File.GetLastWriteTime(_path);
    /// <inheritdoc/>
    public DateTime GetLastWriteTimeUtc => IsInternal ? DateTime.MinValue : File.GetLastWriteTimeUtc(_path);
}
