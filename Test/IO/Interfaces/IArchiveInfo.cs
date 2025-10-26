using System.IO;

namespace Cobilas.Test.IO.Interfaces;

public interface IArchiveInfo : IDataInfo {
    string ArchiveExtension { get; }
    string NameWithoutExtension { get; }
	IStream Open(string? path, FileAccess access);
}
