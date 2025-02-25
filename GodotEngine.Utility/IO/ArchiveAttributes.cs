using System;

namespace Cobilas.GodotEngine.Utility.IO;
/// <summary>Represents the attributes of a file or folder.</summary>
[Flags]
public enum ArchiveAttributes : uint {
    /// <summary>Indicates whether the file or folder is null.</summary>
    Null = 0,
    /// <summary>Indicates that it is a directory file.</summary>
    Directory = 2,
    /// <summary>Indicates that it is a file.</summary>
    File = 4,
    /// <summary>Indicates whether the file or folder is read-only.</summary>
    ReadOnly = 8,
    /// <summary>Indicates whether the file or folder is hidden on the system.</summary>
    Hidden = 16
}
