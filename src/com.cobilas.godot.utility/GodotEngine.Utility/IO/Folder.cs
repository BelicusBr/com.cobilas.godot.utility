using System;
using System.IO;
using Cobilas.GodotEngine.Utility.IO.Interfaces;

namespace Cobilas.GodotEngine.Utility.IO;
/// <summary>
/// Provides static methods for working with folders and directories in 
/// both Godot and system file systems.
/// </summary>
/// <remarks>
/// This class offers a unified interface for folder operations that works seamlessly
/// across Godot's virtual file system and the underlying operating system's file system.
/// </remarks>
public static class Folder {
	private readonly static string[] logicalDrives = Directory.GetLogicalDrives();
	/// <summary>Gets the logical drives available on the system.</summary>
	/// <returns>An array of strings representing the logical drives (e.g., "C:\", "D:\").</returns>
	public static string[] LogicalDrives => logicalDrives;
	/// <summary>Opens a folder at the specified path with the option to skip hidden items.</summary>
	/// <param name="path">The path to the folder. If null, an exception will be thrown.</param>
	/// <param name="skipHidden">Whether to skip hidden files and folders in the enumeration.</param>
	/// <returns>An <see cref="IFolderInfo"/> instance representing the opened folder.</returns>
	/// <exception cref="ArgumentNullException">Thrown when path is null.</exception>
	public static IFolderInfo Open(string? path, bool skipHidden) => new FolderInfo(path, skipHidden);
	/// <summary>Opens a folder at the specified path, automatically skipping hidden items.</summary>
	/// <param name="path">The path to the folder. If null, an exception will be thrown.</param>
	/// <returns>An <see cref="IFolderInfo"/> instance representing the opened folder.</returns>
	/// <exception cref="ArgumentNullException">Thrown when path is null.</exception>
	public static IFolderInfo Open(string? path) => Open(path, true);
	/// <summary>Opens the Godot resource path folder with the option to skip hidden items.</summary>
	/// <param name="skipHidden">Whether to skip hidden files and folders in the enumeration.</param>
	/// <returns>An <see cref="IFolderInfo"/> instance representing the Godot resource folder.</returns>
	public static IFolderInfo OpenRes(bool skipHidden) => Open(GodotPath.ResPath, skipHidden);
	/// <summary>Opens the Godot resource path folder, automatically skipping hidden items.</summary>
	/// <returns>An <see cref="IFolderInfo"/> instance representing the Godot resource folder.</returns>
	public static IFolderInfo OpenRes() => OpenRes(true);
	/// <summary>Opens the Godot user data path folder with the option to skip hidden items.</summary>
	/// <param name="skipHidden">Whether to skip hidden files and folders in the enumeration.</param>
	/// <returns>An <see cref="IFolderInfo"/> instance representing the Godot user data folder.</returns>
	public static IFolderInfo OpenUser(bool skipHidden) => Open(GodotPath.UserPath, skipHidden);
	/// <summary>Opens the Godot user data path folder, automatically skipping hidden items.</summary>
	/// <returns>An <see cref="IFolderInfo"/> instance representing the Godot user data folder.</returns>
	public static IFolderInfo OpenUser() => OpenUser(true);
	/// <summary>Creates a new folder at the specified path.</summary>
	/// <param name="path">The full path where the folder should be created.</param>
	/// <returns>An <see cref="IDataInfo"/> instance representing the newly created folder.</returns>
	/// <exception cref="ArgumentNullException">Thrown when path is null.</exception>
	public static IDataInfo Create(string? path) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		string npath = GodotPath.GetDirectoryName(path);
		return (_ = new FolderInfo(npath, false)).CreateFolder(GodotPath.GetFileName(path));
	}
	/// <summary>Deletes the folder at the specified path and all its contents.</summary>
	/// <remarks>
	/// This method will not delete internal Godot folders. If the path is empty or null, 
	/// the operation will return false without throwing an exception.
	/// </remarks>
	/// <param name="path">The path to the folder to delete.</param>
	/// <returns>true if the folder was successfully deleted; otherwise, false.</returns>
	/// <exception cref="ArgumentNullException">Thrown when path is null.</exception>
	public static bool Delete(string? path) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		else if (string.IsNullOrEmpty(path)) return false;
		using FolderInfo datas = new(path, false);
		if (datas.IsInternal) return false;
		Directory.Delete(GodotPath.GlobalizePath(datas.FullName), true);
		return true;
	}
	/// <summary>Determines whether the specified folder exists.</summary>
	/// <remarks>This method works with both system directories and Godot's virtual file system.</remarks>
	/// <param name="path">The path to check for existence.</param>
	/// <returns>true if the folder exists; otherwise, false.</returns>
	/// <exception cref="ArgumentNullException">Thrown when path is null.</exception>
	public static bool Exists(string? path) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		if (!GodotPath.IsGodotRoot(path))
			return Directory.Exists(path);
		using Godot.Directory directory = new();
		return directory.DirExists(path);
	}
	/// <summary>Moves a folder to a new location.</summary>
	/// <param name="path">The current path of the folder.</param>
	/// <param name="destinationPath">The new path for the folder.</param>
	/// <returns>true if the folder was successfully moved; otherwise, false.</returns>
	/// <exception cref="ArgumentNullException">Thrown when either path is null.</exception>
	public static bool Move(string? path, string? destinationPath) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		else if (destinationPath is null) throw new ArgumentNullException(nameof(destinationPath));
		using FolderInfo datas = new(path, false);
		return datas.Move(destinationPath);
	}
	/// <summary>Renames a folder.</summary>
	/// <param name="folderPath">The current full path of the folder.</param>
	/// <param name="newName">The new name for the folder.</param>
	/// <returns>true if the folder was successfully renamed; otherwise, false.</returns>
	/// <exception cref="ArgumentNullException">Thrown when either parameter is null.</exception>
	public static bool Rename(string? folderPath, string? newName) {
		if (folderPath is null) throw new ArgumentNullException(nameof(folderPath));
		else if (newName is null) throw new ArgumentNullException(nameof(newName));
		using FolderInfo datas = new(GodotPath.GetDirectoryName(folderPath), false);
		return datas.RenameFolder(GodotPath.GetFileName(folderPath), newName);
	}
	/// <summary>Gets all archive files in the specified folder.</summary>
	/// <param name="path">The path to search for archive files.</param>
	/// <returns>An array of <see cref="IArchiveInfo"/> representing the archive files found.</returns>
	/// <exception cref="ArgumentNullException">Thrown when path is null.</exception>
	public static IArchiveInfo[] GetArchives(string? path) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		using FolderInfo datas = new(path, false);
		return datas.GetArchives();
	}
	/// <summary>Gets all subfolders in the specified folder.</summary>
	/// <param name="path">The path to search for subfolders.</param>
	/// <returns>An array of <see cref="IFolderInfo"/> representing the subfolders found.</returns>
	/// <exception cref="ArgumentNullException">Thrown when path is null.</exception>
	public static IFolderInfo[] GetFolders(string? path) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		using FolderInfo datas = new(path, false);
		return datas.GetFolders();
	}
	/// <summary>Gets the creation time of the specified folder in local time.</summary>
	/// <param name="path">The path to the folder.</param>
	/// <returns>A <see cref="DateTime"/> representing the folder's creation time in local time.</returns>
	/// <exception cref="ArgumentNullException">Thrown when path is null.</exception>
	public static DateTime GetCreationTime(string? path) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		using FolderInfo datas = new(path, false);
		return datas.GetCreationTime;
	}
	/// <summary>Gets the creation time of the specified folder in UTC time.</summary>
	/// <param name="path">The path to the folder.</param>
	/// <returns>A <see cref="DateTime"/> representing the folder's creation time in UTC.</returns>
	/// <exception cref="ArgumentNullException">Thrown when path is null.</exception>
	public static DateTime GetCreationTimeUtc(string? path) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		using FolderInfo datas = new(path, false);
		return datas.GetCreationTimeUtc;
	}
	/// <summary>Gets the last access time of the specified folder in local time.</summary>
	/// <param name="path">The path to the folder.</param>
	/// <returns>A <see cref="DateTime"/> representing the folder's last access time in local time.</returns>
	/// <exception cref="ArgumentNullException">Thrown when path is null.</exception>
	public static DateTime GetLastAccessTime(string? path) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		using FolderInfo datas = new(path, false);
		return datas.GetLastAccessTime;
	}
	/// <summary>Gets the last access time of the specified folder in UTC time.</summary>
	/// <param name="path">The path to the folder.</param>
	/// <returns>A <see cref="DateTime"/> representing the folder's last access time in UTC.</returns>
	/// <exception cref="ArgumentNullException">Thrown when path is null.</exception>
	public static DateTime GetLastAccessTimeUtc(string? path) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		using FolderInfo datas = new(path, false);
		return datas.GetLastAccessTimeUtc;
	}
	/// <summary>Gets the last write time of the specified folder in local time.</summary>
	/// <param name="path">The path to the folder.</param>
	/// <returns>A <see cref="DateTime"/> representing the folder's last write time in local time.</returns>
	/// <exception cref="ArgumentNullException">Thrown when path is null.</exception>
	public static DateTime GetLastWriteTime(string? path) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		using FolderInfo datas = new(path, false);
		return datas.GetLastWriteTime;
	}
	/// <summary>Gets the last write time of the specified folder in UTC time.</summary>
	/// <param name="path">The path to the folder.</param>
	/// <returns>A <see cref="DateTime"/> representing the folder's last write time in UTC.</returns>
	/// <exception cref="ArgumentNullException">Thrown when path is null.</exception>
	public static DateTime GetLastWriteTimeUtc(string? path) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		using FolderInfo datas = new(path, false);
		return datas.GetLastWriteTimeUtc;
	}
	/// <summary>Gets the parent folder of the specified folder information.</summary>
	/// <param name="data">The folder information to get the parent of.</param>
	/// <returns>An <see cref="IDataInfo"/> representing the parent folder, or <see cref="DataNull.Null"/> if there is no parent.</returns>
	/// <exception cref="ArgumentNullException">Thrown when data is null.</exception>
	public static IDataInfo GetParent(IFolderInfo? data) => GetParent(data);

	internal static IDataInfo GetParent(IDataInfo? dataInfo) {
		switch ((dataInfo ?? throw new ArgumentNullException(nameof(dataInfo))).FullName) {
			case GodotPath.ResPath:
			case GodotPath.UserPath:
				return DataNull.Null;
			default:
				foreach (string item in logicalDrives)
					if (item.Replace('\\', '/') == dataInfo.FullName)
						return DataNull.Null;
				return new FolderInfo(GodotPath.GetDirectoryName(dataInfo.FullName), false);
		}
	}
}