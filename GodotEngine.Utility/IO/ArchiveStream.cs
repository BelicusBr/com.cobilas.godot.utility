using System;
using System.IO;
using System.Text;
using Cobilas.GodotEngine.Utility.IO.Interfaces;

namespace Cobilas.GodotEngine.Utility.IO;
/// <summary>Represents a file-based archive stream implementation that provides read and write operations.</summary>
/// <remarks>
/// This class wraps a <see cref="FileStream"/> to provide archive stream functionality
/// with various data type support including bytes, strings, and character arrays.
/// </remarks>
/// <param name="path">The path to the file. If null, may throw an exception.</param>
/// <param name="access">The file access mode specifying read and/or write permissions.</param>
/// <exception cref="ArgumentNullException">Thrown when path is null.</exception>
/// <exception cref="FileNotFoundException">Thrown when the file does not exist.</exception>
/// <exception cref="UnauthorizedAccessException">Thrown when access is denied.</exception>
public sealed class ArchiveStream(string? path, FileAccess access) : IArchiveStream {
	private readonly FileStream stream = new(path, FileMode.Open, access);
	public bool AutoFlush { get; set; } = false;
	/// <inheritdoc/>
	public string Name => stream.Name;
	/// <inheritdoc/>
	public bool CanRead => stream.CanRead;
	/// <inheritdoc/>
	public bool CanWrite => stream.CanWrite;
	/// <inheritdoc/>
	public long Position { get => stream.Position; set => stream.Position = value; }
	/// <inheritdoc/>
	public long BufferLength { get => stream.Length; set => stream.SetLength(value); }
	/// <inheritdoc/>
	public byte[] Read() {
		long oldPos = Position;
		Position = 0L;
		byte[] result = stream.Read();
		Position = oldPos;
		return result;
	}

	/// <inheritdoc/>
	public void Read(Encoding? encoding, out string result)
		=> result = (encoding ?? Encoding.UTF8).GetString(Read());
	/// <inheritdoc/>
	public void Read(Encoding? encoding, out StringBuilder result)
		=> result = new((encoding ?? Encoding.UTF8).GetString(Read()));
	/// <inheritdoc/>
	public void Read(Encoding? encoding, out char[] result)
		=> result = (encoding ?? Encoding.UTF8).GetChars(Read());
	/// <inheritdoc/>
	public void Read(out string result) => Read(Encoding.UTF8, out result);
	/// <inheritdoc/>
	public void Read(out StringBuilder result) => Read(Encoding.UTF8, out result);
	/// <inheritdoc/>
	public void Read(out char[] result) => Read(Encoding.UTF8, out result);
	/// <inheritdoc/>
	public void ReplaceBuffer(byte[]? newBuffer) {
		BufferLength = 0L;
		Write(newBuffer);
	}
	/// <inheritdoc/>
	public void ReplaceBuffer(char[]? newBuffer, Encoding? encoding)
		=> ReplaceBuffer((encoding ?? Encoding.UTF8).GetBytes(newBuffer ?? throw new ArgumentNullException(nameof(newBuffer))));
	/// <inheritdoc/>
	public void ReplaceBuffer(string? newBuffer, Encoding? encoding)
		=> ReplaceBuffer((encoding ?? Encoding.UTF8).GetBytes(newBuffer ?? throw new ArgumentNullException(nameof(newBuffer))));
	/// <inheritdoc/>
	public void ReplaceBuffer(StringBuilder? newBuffer, Encoding? encoding)
		=> ReplaceBuffer((encoding ?? Encoding.UTF8).GetBytes(newBuffer?.ToString() ?? throw new ArgumentNullException(nameof(newBuffer))));
	/// <inheritdoc/>
	public void ReplaceBuffer(char[]? newBuffer)
		=> ReplaceBuffer(Encoding.UTF8.GetBytes(newBuffer ?? throw new ArgumentNullException(nameof(newBuffer))));
	/// <inheritdoc/>
	public void ReplaceBuffer(string? newBuffer)
		=> ReplaceBuffer(Encoding.UTF8.GetBytes(newBuffer ?? throw new ArgumentNullException(nameof(newBuffer))));
	/// <inheritdoc/>
	public void ReplaceBuffer(StringBuilder? newBuffer)
		=> ReplaceBuffer(Encoding.UTF8.GetBytes(newBuffer?.ToString() ?? throw new ArgumentNullException(nameof(newBuffer))));
	/// <inheritdoc/>
	public void Write(byte[]? buffer)
		=> stream.Write(buffer ?? throw new ArgumentNullException(nameof(buffer)));
	/// <inheritdoc/>
	public void Write(char[]? buffer, Encoding? encoding)
		=> Write((encoding ?? Encoding.UTF8).GetBytes(buffer ?? throw new ArgumentNullException(nameof(buffer))));
	/// <inheritdoc/>
	public void Write(string? buffer, Encoding? encoding)
		=> Write((encoding ?? Encoding.UTF8).GetBytes(buffer ?? throw new ArgumentNullException(nameof(buffer))));
	/// <inheritdoc/>
	public void Write(StringBuilder? buffer, Encoding? encoding)
		=> Write((encoding ?? Encoding.UTF8).GetBytes(buffer?.ToString() ?? throw new ArgumentNullException(nameof(buffer))));
	/// <inheritdoc/>
	public void Write(char[]? buffer) => Write(buffer, Encoding.UTF8);
	/// <inheritdoc/>
	public void Write(string? buffer) => Write(buffer, Encoding.UTF8);
	/// <inheritdoc/>
	public void Write(StringBuilder? buffer) => Write(buffer, Encoding.UTF8);
	/// <inheritdoc/>
	public void Flush() => stream.Flush();
	/// <inheritdoc/>
	public void Dispose()
	{
		if (AutoFlush)
			Flush();
		stream.Dispose();
	}
}