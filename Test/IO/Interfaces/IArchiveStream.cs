namespace Cobilas.Test.IO.Interfaces;
/// <summary>Represents an archive stream interface that extends basic stream functionality with position tracking and naming.</summary>
public interface IArchiveStream : IStream {
	/// <summary>Gets or sets the current position within the stream.</summary>
	/// <returns>The current position within the stream.</returns>
	/// <value>The new position within the stream.</value>
	long Position { get; set; }
	/// <summary>Gets the name of the archive stream.</summary>
	/// <returns>The name identifier of the stream.</returns>
	string Name { get; }
	/// <summary>Gets or sets the length of the buffer.</summary>
	/// <returns>The current length of the buffer in bytes.</returns>
	/// <value>The new length of the buffer in bytes.</value>
	long BufferLength { get; set; }
}