using System;
using Cobilas.Test.IO.Interfaces;

namespace Cobilas.Test.IO;

public readonly struct DataNull : IDataInfo {
	private readonly static DataNull @null = new();

	public readonly string Name => string.Empty;
	public readonly string FullName => string.Empty;
	public readonly ArchiveAttributes Attributes => ArchiveAttributes.Nil;
	public readonly IDataInfo Parent => @null;
	public readonly bool IsInternal => false;
	public readonly bool IsGodotRoot => false;

	public static DataNull Null => @null;

	public readonly DateTime GetCreationTime => DateTime.MinValue;
	public readonly DateTime GetCreationTimeUtc => DateTime.MinValue;
	public readonly DateTime GetLastAccessTime => DateTime.MinValue;
	public readonly DateTime GetLastAccessTimeUtc => DateTime.MinValue;
	public readonly DateTime GetLastWriteTime => DateTime.MinValue;
	public readonly DateTime GetLastWriteTimeUtc => DateTime.MinValue;

	public void Dispose() { }

	public readonly string ToString(string format, IFormatProvider formatProvider) => string.Empty;
}