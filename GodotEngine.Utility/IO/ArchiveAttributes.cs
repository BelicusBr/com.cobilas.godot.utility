using System;
using System.Runtime.InteropServices;

namespace Cobilas.GodotEngine.Utility.IO;
/// <summary>Provides attributes for files and directories.</summary>
[Serializable, ComVisible(true), Flags]
public enum ArchiveAttributes : uint {
	/// <summary>The file is null.</summary>
	Nil = 0,
	/// <summary>The file is read-only.</summary>
	ReadOnly = 1,
	/// <summary>The file is hidden and therefore not included in a regular directory listing.</summary>
	Hidden = 2,
	/// <summary>
	/// The file is a system file. That is, the file is part of the operating 
	/// system or is used exclusively by the operating system.
	/// </summary>
	System = 4,
	/// <summary>The file is a directory.</summary>
	Directory = 0x10,
	/// <summary>The file is a candidate for backup or removal.</summary>
	Archive = 0x20,
	/// <summary>Reserved for future use.</summary>
	Device = 0x40,
	/// <summary>The file is a standard file with no special attributes. This attribute is only valid if used alone.</summary>
	Normal = 0x80,
	/// <summary>
	/// The file is temporary. A temporary file contains data needed during an application's execution 
	/// but not needed after the application finishes. File systems attempt to keep all data in memory for
	/// faster access rather than flushing the data back to mass storage. A temporary file should be 
	/// deleted by the application as soon as it is no longer needed.
	/// </summary>
	Temporary = 0x100,
	/// <summary>
	/// The file is a sparse file. In general, sparse files are large files 
	/// whose data consists primarily of zeros.
	/// </summary>
	SparseFile = 0x200,
	/// <summary>
	/// The file contains a reparse point, which is a user-defined block of data 
	/// associated with a file or directory.
	/// </summary>
	ReparsePoint = 0x400,
	/// <summary>The file is compressed.</summary>
	Compressed = 0x800,
	/// <summary>The file is offline. The file data is not immediately available.</summary>
	Offline = 0x1000,
	/// <summary>The file will not be indexed by the operating system's content indexing service.</summary>
	NotContentIndexed = 0x2000,
	/// <summary>
	/// The file or directory is encrypted. For a file, this means that all the data in the file is encrypted. 
	/// For a directory, this means that encryption is the default for newly created files and directories.
	/// </summary>
	Encrypted = 0x4000,
	/// <summary>
	/// The file or directory includes data integrity support. When this value is applied to a file, 
	/// all data streams in the file have integrity support. When this value is applied to a directory, 
	/// all new files and subfolders in the directory include integrity support by default.
	/// </summary>
	[ComVisible(false)]
	IntegrityStream = 0x8000,
	/// <summary>
	/// The file or directory is excluded from data integrity checking. When this value is applied to a 
	/// folder, by default, all new files and subdirectories within the directory are excluded from 
	/// data integrity checking.
	/// </summary>
	[ComVisible(false)]
	NoScrubData = 0x20000
}