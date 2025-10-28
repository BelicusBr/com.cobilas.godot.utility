using System;
using System.IO;
using System.Text;
using Cobilas.GodotEngine.Utility.IO.Interfaces;

using GDFile = Godot.File;

namespace Cobilas.GodotEngine.Utility.IO;

public static class Archive {

    [ThreadStatic] internal readonly static StringBuilder builder = new();

    public static ArchiveAttributes GetArchiveAttributes(IDataInfo? data) {
        if (data is null) throw new ArgumentNullException(nameof(data));
        else if (data.IsInternal) return ArchiveAttributes.Archive | ArchiveAttributes.ReadOnly | ArchiveAttributes.Hidden;
        return (ArchiveAttributes)File.GetAttributes(GodotPath.GlobalizePath(data.FullName));
    }

    public static ArchiveAttributes GetArchiveAttributes(string? path) {
        using ArchiveInfo ark = new(path);
        return GetArchiveAttributes(ark);
    }

	public static bool Exists(string? path)	{
		if (path is null) throw new ArgumentNullException(nameof(path));
		else if (!GodotPath.IsGodotRoot(path))
			return File.Exists(path);
		using GDFile file = new();
		return file.FileExists(path);
	}

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

	public static IStream Open(string? path, FileAccess access, StreamType type) {
		if (path is null) throw new ArgumentNullException(nameof(path));
        using ArchiveInfo archive = new(path);
        return archive.Open(access, type);
	}

	public static IStream Open(string? path, FileAccess access)
		=> Open(path, access, StreamType.Auto);

	public static IStream Open(string? path)
		=> Open(path, FileAccess.ReadWrite, StreamType.Auto);

	public static bool Move(string? path, string? destinationPath) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		else if (destinationPath is null) throw new ArgumentNullException(nameof(destinationPath));
		using ArchiveInfo archive = new(path);
		return archive.Move(destinationPath);
	}

	public static bool Copy(string? path, string? destinationPath) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		else if (destinationPath is null) throw new ArgumentNullException(nameof(destinationPath));
		using ArchiveInfo archive = new(path);
		return archive.CopyTo(destinationPath);
	}

	public static DateTime GetCreationTime(string? path)
	{
		if (path is null) throw new ArgumentNullException(nameof(path));
		using ArchiveInfo archive = new(path);
		return archive.GetCreationTime;
	}
	public static DateTime GetCreationTimeUtc(string? path)
	{
		if (path is null) throw new ArgumentNullException(nameof(path));
		using ArchiveInfo archive = new(path);
		return archive.GetCreationTimeUtc;
	}
	public static DateTime GetLastAccessTime(string? path)
	{
		if (path is null) throw new ArgumentNullException(nameof(path));
		using ArchiveInfo archive = new(path);
		return archive.GetLastAccessTime;
	}
	public static DateTime GetLastAccessTimeUtc(string? path)
	{
		if (path is null) throw new ArgumentNullException(nameof(path));
		using ArchiveInfo archive = new(path);
		return archive.GetLastAccessTimeUtc;
	}
	public static DateTime GetLastWriteTime(string? path)
	{
		if (path is null) throw new ArgumentNullException(nameof(path));
		using ArchiveInfo archive = new(path);
		return archive.GetLastWriteTime;
	}
	public static DateTime GetLastWriteTimeUtc(string? path)
	{
		if (path is null) throw new ArgumentNullException(nameof(path));
		using ArchiveInfo archive = new(path);
		return archive.GetLastWriteTimeUtc;
	}

	public static IDataInfo GetParent(IArchiveInfo? data) => Folder.GetParent(data);
}
