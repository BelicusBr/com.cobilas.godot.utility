using System;
using System.IO;
using System.Text;
using Cobilas.GodotEngine.Utility.IO.Interfaces;

using GDFile = Godot.File;

namespace Cobilas.GodotEngine.Utility.IO;

/// <summary>Provides static methods for working with archive files (files) in both Godot and system file systems.</summary>
/// <remarks>
/// This class offers a unified interface for file operations that works seamlessly
/// across Godot's virtual file system and the underlying operating system's file system,
/// supporting various file operations including creation, opening, moving, and copying.
/// </remarks>
public static class Archive {
	[ThreadStatic] internal readonly static StringBuilder builder = new();
	/// <summary>Gets the archive attributes for the specified data information.</summary>
	/// <remarks>For internal Godot files, returns a combination of Archive, ReadOnly, and Hidden attributes.</remarks>
	/// <param name="data">The data information to get attributes for.</param>
	/// <returns>The <see cref="ArchiveAttributes"/> representing the file's attributes.</returns>
	/// <exception cref="ArgumentNullException">Thrown when data is null.</exception>
	public static ArchiveAttributes GetArchiveAttributes(IDataInfo? data) {
		if (data is null) throw new ArgumentNullException(nameof(data));
		else if (data.IsInternal) return ArchiveAttributes.Archive | ArchiveAttributes.ReadOnly | ArchiveAttributes.Hidden;
		return (ArchiveAttributes)File.GetAttributes(GodotPath.GlobalizePath(data.FullName));
	}
	/// <summary>Gets the archive attributes for the specified file path.</summary>
	/// <param name="path">The path to the file to get attributes for.</param>
	/// <returns>The <see cref="ArchiveAttributes"/> representing the file's attributes.</returns>
	/// <exception cref="ArgumentNullException">Thrown when path is null.</exception>
	public static ArchiveAttributes GetArchiveAttributes(string? path) {
		using ArchiveInfo ark = new(path);
		return GetArchiveAttributes(ark);
	}
	/// <summary>Determines whether the specified file exists.</summary>
	/// <remarks>This method works with both system files and Godot's virtual file system.</remarks>
	/// <param name="path">The path to check for existence.</param>
	/// <returns>true if the file exists; otherwise, false.</returns>
	/// <exception cref="ArgumentNullException">Thrown when path is null.</exception>
	public static bool Exists(string? path) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		else if (!GodotPath.IsGodotRoot(path))
			return File.Exists(path);
		using GDFile file = new();
		return file.FileExists(path);
	}
	/// <summary>Creates a new empty file at the specified path.</summary>
	/// <remarks>
	/// This method will not create files in the Godot resource path when running in release mode.
	/// If the file already exists, the operation will return false.
	/// </remarks>
	/// <param name="path">The path where the file should be created.</param>
	/// <returns>true if the file was successfully created; otherwise, false.</returns>
	/// <exception cref="ArgumentNullException">Thrown when path is null.</exception>
	public static bool Create(string? path) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		else if (GDFeature.HasRelease)
			if (GodotPath.GetPathRoot(path) == GodotPath.ResPath)
				return false;
		path = GodotPath.GlobalizePath(path);
		if (File.Exists(path))
			return false;
		File.Create(path).Dispose();
		return true;
	}
	/// <summary>Opens a file at the specified path with the given access mode and stream type.</summary>
	/// <param name="path">The path to the file to open.</param>
	/// <param name="access">The file access mode specifying read and/or write permissions.</param>
	/// <param name="type">The type of stream to use for accessing the file.</param>
	/// <returns>An <see cref="IStream"/> instance for reading from or writing to the file.</returns>
	/// <exception cref="ArgumentNullException">Thrown when path is null.</exception>
	public static IStream Open(string? path, FileAccess access, StreamType type) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		using ArchiveInfo archive = new(path);
		return archive.Open(access, type);
	}
	/// <summary>Opens a file at the specified path with the given access mode using automatic stream type detection.</summary>
	/// <param name="path">The path to the file to open.</param>
	/// <param name="access">The file access mode specifying read and/or write permissions.</param>
	/// <returns>An <see cref="IStream"/> instance for reading from or writing to the file.</returns>
	/// <exception cref="ArgumentNullException">Thrown when path is null.</exception>
	public static IStream Open(string? path, FileAccess access)
		=> Open(path, access, StreamType.Auto);
	/// <summary>Opens a file at the specified path with read-write access using automatic stream type detection.</summary>
	/// <param name="path">The path to the file to open.</param>
	/// <returns>An <see cref="IStream"/> instance for reading from and writing to the file.</returns>
	/// <exception cref="ArgumentNullException">Thrown when path is null.</exception>
	public static IStream Open(string? path)
		=> Open(path, FileAccess.ReadWrite, StreamType.Auto);
	/// <summary>Moves a file to a new location.</summary>
	/// <param name="path">The current path of the file.</param>
	/// <param name="destinationPath">The new path for the file.</param>
	/// <returns>true if the file was successfully moved; otherwise, false.</returns>
	/// <exception cref="ArgumentNullException">Thrown when either path is null.</exception>
	public static bool Move(string? path, string? destinationPath) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		else if (destinationPath is null) throw new ArgumentNullException(nameof(destinationPath));
		using ArchiveInfo archive = new(path);
		return archive.Move(destinationPath);
	}
	/// <summary>Copies a file to a new location.</summary>
	/// <param name="path">The current path of the file.</param>
	/// <param name="destinationPath">The destination path for the copy.</param>
	/// <returns>true if the file was successfully copied; otherwise, false.</returns>
	/// <exception cref="ArgumentNullException">Thrown when either path is null.</exception>
	public static bool Copy(string? path, string? destinationPath) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		else if (destinationPath is null) throw new ArgumentNullException(nameof(destinationPath));
		using ArchiveInfo archive = new(path);
		return archive.CopyTo(destinationPath);
	}
	/// <summary>Gets the creation time of the specified file in local time.</summary>
	/// <param name="path">The path to the file.</param>
	/// <returns>A <see cref="DateTime"/> representing the file's creation time in local time.</returns>
	/// <exception cref="ArgumentNullException">Thrown when path is null.</exception>
	public static DateTime GetCreationTime(string? path) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		using ArchiveInfo archive = new(path);
		return archive.GetCreationTime;
	}
	/// <summary>Gets the creation time of the specified file in UTC time.</summary>
	/// <param name="path">The path to the file.</param>
	/// <returns>A <see cref="DateTime"/> representing the file's creation time in UTC.</returns>
	/// <exception cref="ArgumentNullException">Thrown when path is null.</exception>
	public static DateTime GetCreationTimeUtc(string? path) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		using ArchiveInfo archive = new(path);
		return archive.GetCreationTimeUtc;
	}
	/// <summary>Gets the last access time of the specified file in local time.</summary>
	/// <param name="path">The path to the file.</param>
	/// <returns>A <see cref="DateTime"/> representing the file's last access time in local time.</returns>
	/// <exception cref="ArgumentNullException">Thrown when path is null.</exception>
	public static DateTime GetLastAccessTime(string? path) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		using ArchiveInfo archive = new(path);
		return archive.GetLastAccessTime;
	}
	/// <summary>Gets the last access time of the specified file in UTC time.</summary>
	/// <param name="path">The path to the file.</param>
	/// <returns>A <see cref="DateTime"/> representing the file's last access time in UTC.</returns>
	/// <exception cref="ArgumentNullException">Thrown when path is null.</exception>
	public static DateTime GetLastAccessTimeUtc(string? path) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		using ArchiveInfo archive = new(path);
		return archive.GetLastAccessTimeUtc;
	}
	/// <summary>Gets the last write time of the specified file in local time.</summary>
	/// <param name="path">The path to the file.</param>
	/// <returns>A <see cref="DateTime"/> representing the file's last write time in local time.</returns>
	/// <exception cref="ArgumentNullException">Thrown when path is null.</exception>
	public static DateTime GetLastWriteTime(string? path) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		using ArchiveInfo archive = new(path);
		return archive.GetLastWriteTime;
	}
	/// <summary>Gets the last write time of the specified file in UTC time.</summary>
	/// <param name="path">The path to the file.</param>
	/// <returns>A <see cref="DateTime"/> representing the file's last write time in UTC.</returns>
	/// <exception cref="ArgumentNullException">Thrown when path is null.</exception>
	public static DateTime GetLastWriteTimeUtc(string? path) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		using ArchiveInfo archive = new(path);
		return archive.GetLastWriteTimeUtc;
	}
	/// <summary>Gets the parent folder of the specified archive information.</summary>
	/// <param name="data">The archive information to get the parent of.</param>
	/// <returns>An <see cref="IDataInfo"/> representing the parent folder.</returns>
	public static IDataInfo GetParent(IArchiveInfo? data) => Folder.GetParent(data);
}