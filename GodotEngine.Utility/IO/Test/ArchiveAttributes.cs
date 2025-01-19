using System;

namespace Cobilas.GodotEngine.Utility.IO.Test;
[Flags]
public enum ArchiveAttributes : uint {
    Null = 0,
    /// <summary>Indicates that it is a directory file.</summary>
    Directory = 2,
    /// <summary>Indicates that it is a file.</summary>
    File = 4,
    ReadOnly = 8,
    Hidden = 16
}
