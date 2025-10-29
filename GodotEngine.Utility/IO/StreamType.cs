namespace Cobilas.GodotEngine.Utility.IO;
/// <summary>Specifies the type of stream to be used for archive operations.</summary>
/// <remarks>
/// This enumeration determines which underlying stream implementation
/// will be used when working with archive files in the Godot engine environment.
/// </remarks>
public enum StreamType : byte {
	/// <summary>Automatically selects the appropriate stream type based on the context and environment.</summary>
	Auto = 0,
	/// <summary>Uses the standard .NET I/O stream implementation for file operations.</summary>
	IOStream = 1,
	/// <summary>Uses the Godot-specific stream implementation that integrates with Godot's file system.</summary>
	GDStream = 2
}