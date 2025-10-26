namespace Cobilas.Test.IO.Interfaces;
/// <summary>Represents a Godot-specific archive stream interface with additional buffer management capabilities.</summary>
/// <remarks>
/// This interface extends the basic archive stream functionality with Godot-specific features,
/// including the ability to refresh the stream buffer.
/// </remarks>
public interface IGodotArchiveStream : IStream {
	/// <summary>Gets or sets the current position within the stream.</summary>
	/// <returns>The current position within the stream.</returns>
	/// <value>The new position within the stream.</value>
	long Position { get; set; }
	bool IsInternal { get; }
	/// <summary>Gets the name of the Godot archive stream.</summary>
	/// <returns>The name identifier of the stream.</returns>
	string Name { get; }
	/// <summary>Gets or sets the length of the buffer.</summary>
	/// <returns>The current length of the buffer in bytes.</returns>
	/// <value>The new length of the buffer in bytes.</value>
	long BufferLength { get; set; }
	/// <summary>Refreshes the stream buffer, ensuring that any pending changes are applied and the buffer state is updated.</summary>
	/// <remarks>
	/// This method is particularly useful in Godot environment where buffer synchronization
	/// with the underlying resource management system is required.
	/// </remarks>
	void RefreshBuffer();
}