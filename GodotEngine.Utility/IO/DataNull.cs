using System;
using Cobilas.GodotEngine.Utility.IO.Interfaces;

namespace Cobilas.GodotEngine.Utility.IO;
/// <summary>
/// Represents a null or empty data information structure that implements 
/// the <see cref="IDataInfo"/> interface.
/// </summary>
/// <remarks>
/// This structure serves as a null object pattern implementation for data information,
/// providing default values for all properties and safe no-op operations.
/// </remarks>
public readonly struct DataNull : IDataInfo {
	private readonly static DataNull @null = new();
	/// <inheritdoc/>
	public readonly string Name => string.Empty;
	/// <inheritdoc/>
	public readonly string FullName => string.Empty;
	/// <inheritdoc/>
	public readonly ArchiveAttributes Attributes => ArchiveAttributes.Nil;
	/// <inheritdoc/>
	public readonly IDataInfo Parent => @null;
	/// <inheritdoc/>
	public readonly bool IsInternal => false;
	/// <inheritdoc/>
	public readonly bool IsGodotRoot => false;
	/// <summary>Gets the singleton instance of the null data item.</summary>
	/// <returns>The static <see cref="DataNull"/> instance.</returns>
	public static DataNull Null => @null;
	/// <inheritdoc/>
	public readonly DateTime GetCreationTime => DateTime.MinValue;
	/// <inheritdoc/>
	public readonly DateTime GetCreationTimeUtc => DateTime.MinValue;
	/// <inheritdoc/>
	public readonly DateTime GetLastAccessTime => DateTime.MinValue;
	/// <inheritdoc/>
	public readonly DateTime GetLastAccessTimeUtc => DateTime.MinValue;
	/// <inheritdoc/>
	public readonly DateTime GetLastWriteTime => DateTime.MinValue;
	/// <inheritdoc/>
	public readonly DateTime GetLastWriteTimeUtc => DateTime.MinValue;
	/// <inheritdoc/>
	public void Dispose() { }
	/// <inheritdoc/>
	public readonly string ToString(string format, IFormatProvider formatProvider) => string.Empty;
}