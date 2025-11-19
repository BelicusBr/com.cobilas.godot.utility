using System;

namespace Cobilas.GodotEngine.Utility.IO.Interfaces;
/// <summary>Represents information about a data item within an archive or file system.</summary>
/// <remarks>
/// This interface provides metadata about data items including attributes, timing information,
/// and hierarchical relationships. It supports both Godot-specific and general file system operations.
/// </remarks>
public interface IDataInfo : IDisposable, IFormattable {
	/// <summary>Gets the name of the data item without the path.</summary>
	/// <returns>The name of the data item.</returns>
	string Name { get; }
	/// <summary>Gets the full path and name of the data item.</summary>
	/// <returns>The complete path including the item name.</returns>
	string FullName { get; }
	/// <summary>Gets the attributes associated with the data item.</summary>
	/// <returns>The archive attributes defining the item's properties and permissions.</returns>
	ArchiveAttributes Attributes { get; }
	/// <summary>Gets the parent data item of the current item.</summary>
	/// <returns>The parent <see cref="IDataInfo"/> instance, or null if this is the root item.</returns>
	IDataInfo Parent { get; }
	/// <summary>Gets a value indicating whether the data item is internal to the application or archive.</summary>
	/// <returns>true if the item is internal; otherwise, false.</returns>
	bool IsInternal { get; }
	/// <summary>Gets a value indicating whether the data item is located at the Godot engine root.</summary>
	/// <returns>true if the item is at the Godot root; otherwise, false.</returns>
	bool IsGodotRoot { get; }
	/// <summary>Gets the creation time of the data item in local time.</summary>
	/// <returns>The creation date and time in local time.</returns>
	DateTime GetCreationTime { get; }
	/// <summary>Gets the creation time of the data item in Coordinated Universal Time (UTC).</summary>
	/// <returns>The creation date and time in UTC.</returns>
	DateTime GetCreationTimeUtc { get; }
	/// <summary>Gets the last access time of the data item in local time.</summary>
	/// <returns>The last access date and time in local time.</returns>
	DateTime GetLastAccessTime { get; }
	/// <summary>Gets the last access time of the data item in Coordinated Universal Time (UTC).</summary>
	/// <returns>The last access date and time in UTC.</returns>
	DateTime GetLastAccessTimeUtc { get; }
	/// <summary>Gets the last write time of the data item in local time.</summary>
	/// <returns>The last write date and time in local time.</returns>
	DateTime GetLastWriteTime { get; }
	/// <summary>Gets the last write time of the data item in Coordinated Universal Time (UTC).</summary>
	/// <returns>The last write date and time in UTC.</returns>
	DateTime GetLastWriteTimeUtc { get; }
}