using System;

namespace Cobilas.GodotEngine.Utility.IO.Interfaces;

public interface IDataInfo : IDisposable, IFormattable {
    string Name { get; }
    string FullName { get; }
    ArchiveAttributes Attributes { get; }
    IDataInfo Parent { get; }
    bool IsInternal { get; }
    bool IsGodotRoot { get; }
    DateTime GetCreationTime { get; }
    DateTime GetCreationTimeUtc { get; }
    DateTime GetLastAccessTime { get; }
    DateTime GetLastAccessTimeUtc { get; }
    DateTime GetLastWriteTime { get; }
    DateTime GetLastWriteTimeUtc { get; }
}
