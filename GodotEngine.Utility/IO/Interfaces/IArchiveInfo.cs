using System.IO;

namespace Cobilas.GodotEngine.Utility.IO.Interfaces;

public interface IArchiveInfo : IDataInfo {
    string ArchiveExtension { get; }
    string NameWithoutExtension { get; }
	IStream Open(FileAccess access, StreamType type);
	IStream Open(FileAccess access);
	bool CopyTo(string? destinationPath, bool overwrite);
	bool CopyTo(string? destinationPath);
	bool Move(string? destinationPath);
}
