using System.IO;

namespace Cobilas.GodotEngine.Utility.IO.Interfaces;
/// <summary>Represents information about an archive file and provides operations for working with archive contents.</summary>
/// <remarks>
/// This interface extends <see cref="IDataInfo"/> to provide archive-specific functionality
/// including file extension information and stream-based access to archive contents.
/// </remarks>
public interface IArchiveInfo : IDataInfo {
	/// <summary>Gets the file extension of the archive.</summary>
	/// <returns>The archive file extension including the dot (e.g., ".zip", ".rar").</returns>
	string ArchiveExtension { get; }
	/// <summary>Gets the name of the archive without the file extension.</summary>
	/// <returns>The archive name excluding the extension.</returns>
	string NameWithoutExtension { get; }
	/// <summary>Opens the archive with the specified access mode and <see cref="StreamType"/>.</summary>
	/// <param name="access">The file access mode specifying read and/or write permissions.</param>
	/// <param name="type">The type of stream to use for accessing the archive contents.</param>
	/// <returns>A stream object for reading from or writing to the archive.</returns>
	IStream Open(FileAccess access, StreamType type);
	/// <summary>Opens the archive with the specified access mode using the default <see cref="StreamType"/>.</summary>
	/// <param name="access">The file access mode specifying read and/or write permissions.</param>
	/// <returns>A stream object for reading from or writing to the archive.</returns>
	IStream Open(FileAccess access);
	/// <summary>Copies the archive to the specified destination path.</summary>
	/// <param name="destinationPath">The destination path where the archive will be copied.</param>
	/// <param name="overwrite">Whether to overwrite an existing file at the destination path.</param>
	/// <returns><c>true</c> if the copy operation succeeded; otherwise, <c>false</c>.</returns>
	bool CopyTo(string? destinationPath, bool overwrite);
	/// <summary>Copies the archive to the specified destination path without overwriting existing files.</summary>
	/// <param name="destinationPath">The destination path where the archive will be copied.</param>
	/// <returns><c>true</c> if the copy operation succeeded; otherwise, <c>false</c>.</returns>
	bool CopyTo(string? destinationPath);
	/// <summary>Moves the archive to the specified destination path.</summary>
	/// <param name="destinationPath">The destination path where the archive will be moved.</param>
	/// <returns><c>true</c> if the move operation succeeded; otherwise, <c>false</c>.</returns>
	bool Move(string? destinationPath);
}