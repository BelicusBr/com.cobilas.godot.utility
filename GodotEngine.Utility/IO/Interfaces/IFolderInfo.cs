using System.Collections.Generic;

namespace Cobilas.GodotEngine.Utility.IO.Interfaces;
/// <summary>Represents information about a folder/directory and provides operations for folder management.</summary>
/// <remarks>
/// This interface extends <see cref="IDataInfo"/> and <see cref="IEnumerable{T}"/> to provide
/// folder-specific functionality including enumeration of contents and folder manipulation operations.
/// </remarks>
public interface IFolderInfo : IDataInfo, IEnumerable<IDataInfo> {
	/// <summary>Gets the total number of data items contained within the folder.</summary>
	/// <returns>The count of all data items (files and subfolders) in the folder.</returns>
	long DataCount { get; }
	/// <summary>Gets all subfolders contained within this folder.</summary>
	/// <returns>An array of <see cref="IFolderInfo"/> representing all subfolders.</returns>
	IFolderInfo[] GetFolders();
	/// <summary>Gets all archive files contained within this folder.</summary>
	/// <returns>An array of <see cref="IArchiveInfo"/> representing all archive files.</returns>
	IArchiveInfo[] GetArchives();
	/// <summary>Creates a new subfolder with the specified name.</summary>
	/// <param name="name">The name of the subfolder to create.</param>
	/// <returns>An <see cref="IDataInfo"/> representing the newly created folder.</returns>
	IDataInfo CreateFolder(string? name);
	/// <summary>Creates a new archive file with the specified name.</summary>
	/// <param name="name">The name of the archive file to create.</param>
	/// <returns>An <see cref="IDataInfo"/> representing the newly created archive.</returns>
	IDataInfo CreateArchive(string? name);
	/// <summary>Renames a subfolder within this folder.</summary>
	/// <param name="oldName">The current name of the folder to rename.</param>
	/// <param name="newName">The new name for the folder.</param>
	/// <returns><c>true</c> if the rename operation succeeded; otherwise, <c>false</c>.</returns>
	bool RenameFolder(string? oldName, string? newName);
	/// <summary>Renames an archive file within this folder.</summary>
	/// <param name="oldName">The current name of the archive to rename.</param>
	/// <param name="newName">The new name for the archive.</param>
	/// <returns><c>true</c> if the rename operation succeeded; otherwise, <c>false</c>.</returns>
	bool RenameArchive(string? oldName, string? newName);
	/// <summary>Removes a subfolder with the specified name.</summary>
	/// <param name="name">The name of the folder to remove.</param>
	/// <returns><c>true</c> if the removal succeeded; otherwise, <c>false</c>.</returns>
	bool RemoveFolder(string? name);
	/// <summary>Removes an archive file with the specified name.</summary>
	/// <param name="name">The name of the archive to remove.</param>
	/// <returns><c>true</c> if the removal succeeded; otherwise, <c>false</c>.</returns>
	bool RemoveArchive(string? name);
	/// <summary>Checks whether a data item with the specified name exists in this folder.</summary>
	/// <param name="name">The name of the data item to check.</param>
	/// <returns><c>true</c> if a data item with the specified name exists; otherwise, <c>false</c>.</returns>
	bool Existe(string? name);
	/// <summary>Moves a data item from this folder to another location.</summary>
	/// <param name="name">The name of the data item to move.</param>
	/// <param name="path">The destination path where the item will be moved.</param>
	/// <returns><c>true</c> if the move operation succeeded; otherwise, <c>false</c>.</returns>
	bool MoveTo(string? name, string? path);
	/// <summary>Moves the entire folder to a new location.</summary>
	/// <param name="destinationPath">The destination path where the folder will be moved.</param>
	/// <returns><c>true</c> if the move operation succeeded; otherwise, <c>false</c>.</returns>
	bool Move(string? destinationPath);
	/// <summary>Refreshes the folder contents, updating the internal state to reflect any external changes.</summary>
	void Refresh();
}