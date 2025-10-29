using Godot;
using System;
using System.IO;
using System.Text;
using Cobilas.GodotEngine.Utility.IO.Interfaces;

using GDFile = Godot.File;

namespace Cobilas.GodotEngine.Utility.IO;
/// <summary>Represents a Godot-specific archive stream implementation that integrates with Godot's file system.</summary>
/// <remarks>
/// This class uses Godot's File API internally while providing a consistent stream interface.
/// It maintains an in-memory buffer that synchronizes with the Godot file system.
/// </remarks>
public sealed class GodotArchiveStream : IGodotArchiveStream {
	private string? _name;
	private bool _disposedValue;
	private readonly GDFile file;
	private readonly bool isInternal;
	private readonly FileAccess access;
	private readonly MemoryStream memory;
	/// <inheritdoc/>
	public bool AutoFlush { get; set; }
	/// <inheritdoc/>
	public bool IsInternal => isInternal;
	/// <inheritdoc/>
	public bool CanRead => IsSupport(FileAccess.Read);
	/// <inheritdoc/>
	public bool CanWrite => IsSupport(FileAccess.Write);
	/// <inheritdoc/>
	public long Position { get => memory.Position; set => memory.Position = value; }
	/// <inheritdoc/>
	public long BufferLength { get => memory.Length; set => memory.SetLength(value); }
	/// <inheritdoc/>
	public string Name => !_disposedValue ? _name! : throw new ObjectDisposedException(nameof(GodotArchiveStream));
	/// <summary>Initializes a new instance of the <see cref="GodotArchiveStream"/> class with the specified path and access mode.</summary>
	/// <param name="path">The path to the file in the Godot file system.</param>
	/// <param name="access">The file access mode specifying read and/or write permissions.</param>
	/// <exception cref="ArgumentNullException">Thrown when path is null.</exception>
	/// <exception cref="UnauthorizedAccessException">Thrown when the file cannot be opened due to access restrictions.</exception>
	public GodotArchiveStream(string? path, FileAccess access) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		file = new();
		AutoFlush = false;
		this.access = access;
		isInternal = GodotPath.GetPathRoot(path) switch {
            GodotPath.ResPath when GDFeature.HasRelease => true,
            _ => false,
        };
		Error error;
		if ((error = file.Open(path, isInternal ? GDFile.ModeFlags.Read : GDFile.ModeFlags.ReadWrite)) != Error.Ok)
			throw new UnauthorizedAccessException($"[{error}]{path}");
		_name = GodotPath.GetFileName(path);
		(memory = new())
			.Write(file.GetBuffer((long)file.GetLen()));
		Position = 0L;
	}
	/// <inheritdoc/>
	public byte[] Read() {
		if (!CanRead) throw new NotSupportedException("The stream does not support read operations.");
		long oldPos = Position;
		Position = 0L;
		byte[] result = memory.Read();
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
	public void RefreshBuffer() {
		memory.SetLength(0L);
		memory.Write(file.GetBuffer((long)file.GetLen()));
	}
	/// <inheritdoc/>
	public void ReplaceBuffer(byte[]? newBuffer) {
		if (!CanWrite) throw new NotSupportedException("The stream does not support write operations.");
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
	public void ReplaceBuffer(char[]? newBuffer) => ReplaceBuffer(newBuffer, Encoding.UTF8);
	/// <inheritdoc/>
	public void ReplaceBuffer(string? newBuffer) => ReplaceBuffer(newBuffer, Encoding.UTF8);
	/// <inheritdoc/>
	public void ReplaceBuffer(StringBuilder? newBuffer) => ReplaceBuffer(newBuffer, Encoding.UTF8);
	/// <inheritdoc/>
	public void Write(byte[]? buffer) {
		if (!CanWrite) throw new NotSupportedException("The stream does not support write operations.");
		else if (buffer is null) throw new ArgumentNullException(nameof(buffer));
		memory.Write(buffer);
	}
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
	public void Flush() {
		if (!CanWrite) throw new NotSupportedException("The stream does not support write operations.");
		file.Seek(0L);
		file.StoreBuffer(new byte[(long)file.GetLen()]);
		file.Seek(0L);
		file.StoreBuffer(memory.ToArray());
	}
	/// <inheritdoc/>
	public void Dispose() {
		if (AutoFlush)
			Flush();
		_name = null;
		file.Flush();
		file.Dispose();
		memory.Dispose();
		_disposedValue = true;
	}
	private bool IsSupport(FileAccess access) => !_disposedValue ? (this.access & access) != 0 : throw new ObjectDisposedException(nameof(GodotArchiveStream));
}