namespace Cobilas.GodotEngine.Utility; 

/// <summary>Represents the file attributes.</summary>
public enum GDFileAttributes : byte {
    /// <summary>Indicates that it is a directory file.</summary>
    Directory = 0,
    /// <summary>Indicates that it is a file.</summary>
    File = 1,
    /// <summary>Indicates that it is a null item(Exe: <seealso cref="GDIONull"/>).</summary>
    Null = 3
}