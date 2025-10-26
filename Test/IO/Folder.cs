using System;
using System.IO;
using Cobilas.Test.IO.Interfaces;
using Cobilas.GodotEngine.Utility.IO;

using TDSIDataInfo = Cobilas.Test.IO.Interfaces.IDataInfo;

namespace Cobilas.Test.IO;

public static class Folder {

	private readonly static string[] logicalDrives = Directory.GetLogicalDrives();

	public static string[] LogicalDrives => logicalDrives;

	public static IFolderInfo Create(string? path)	{
		if (path is null) throw new ArgumentNullException(nameof(path));
		string npath = GodotPath.GetDirectoryName(path);
		return (_ = new FolderInfo(npath)).CreateFolder(GodotPath.GetFileName(path));
	}

	public static bool Delete(string? path)	{
		if (path is null) throw new ArgumentNullException(nameof(path));
		else if (string.IsNullOrEmpty(path)) return false;
		using FolderInfo datas = new(path);
		if (datas.IsInternal) return false;
		Directory.Delete(GodotPath.GlobalizePath(datas.FullName), true);
		return true;
	}

	public static bool Exists(string? path)	{
		if (path is null) throw new ArgumentNullException(nameof(path));
		using Godot.Directory directory = new();
		return directory.Open(path) == Godot.Error.Ok;
	}

	public static bool Move(string? path, string? destinationPath) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		else if (destinationPath is null) throw new ArgumentNullException(nameof(destinationPath));
		using FolderInfo datas = new(GodotPath.GetDirectoryName(path));
		return datas.MoveTo(GodotPath.GetFileName(path), destinationPath);
	}

	public static bool Rename(string? folderPath, string? newName) {
		if (folderPath is null) throw new ArgumentNullException(nameof(folderPath));
		else if (newName is null) throw new ArgumentNullException(nameof(newName));
		using FolderInfo datas = new(GodotPath.GetDirectoryName(folderPath));
		return datas.RenameFolder(GodotPath.GetFileName(folderPath), newName);
	}

	public static IArchiveInfo[] GetArchives(string? path) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		using FolderInfo datas = new(path);
		return datas.GetArchives();
	}

	public static IFolderInfo[] GetFolders(string? path) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		using FolderInfo datas = new(path);
		return datas.GetFolders();
	}

	public static DateTime GetCreationTime(string? path) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		using FolderInfo datas = new(path);
		return datas.GetCreationTime;
	}
	public static DateTime GetCreationTimeUtc(string? path) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		using FolderInfo datas = new(path);
		return datas.GetCreationTimeUtc;
	}
	public static DateTime GetLastAccessTime(string? path) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		using FolderInfo datas = new(path);
		return datas.GetLastAccessTime;
	}
	public static DateTime GetLastAccessTimeUtc(string? path) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		using FolderInfo datas = new(path);
		return datas.GetLastAccessTimeUtc;
	}
	public static DateTime GetLastWriteTime(string? path) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		using FolderInfo datas = new(path);
		return datas.GetLastWriteTime;
	}
	public static DateTime GetLastWriteTimeUtc(string? path) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		using FolderInfo datas = new(path);
		return datas.GetLastWriteTimeUtc;
	}

	public static TDSIDataInfo GetParent(IFolderInfo? data) => GetParent(data);

	internal static TDSIDataInfo GetParent(TDSIDataInfo? dataInfo)	{
		switch ((dataInfo ?? throw new ArgumentNullException(nameof(dataInfo))).FullName) {
			case GodotPath.ResPath:
			case GodotPath.UserPath:
				return DataNull.Null;
			default:
				foreach (string item in logicalDrives)
					if (item.Replace('\\', '/') == dataInfo.FullName)
						return DataNull.Null;
				return new FolderInfo(GodotPath.GetDirectoryName(dataInfo.FullName));
		}
	}
}
