using System;

namespace Cobilas.GodotEngine.Utility.IO;
/// <summary>Base class for classes that represent system data files.</summary>
public abstract class DataBase : IDisposable, IFormattable {
    /// <summary>Data file name.</summary>
    /// <returns>Returns a string containing the name of the data file.</returns>
    public abstract string? Name { get; protected set; }
    /// <summary>The full path of the data file.</summary>
    /// <returns>Returns a string containing the full path of the data file.</returns>
    public abstract string Path { get; }
    /// <summary>The parent element of the data file.</summary>
    /// <returns>Returns parent element of data file.</returns>
    public abstract DataBase? Parent { get; protected set; }
    /// <summary>The attributes of the data file.</summary>
    /// <returns>Returns the attributes of the data file.</returns>
    public abstract ArchiveAttributes Attributes { get; protected set; }
    /// <summary>Creates a new instance of this object.</summary>
    protected DataBase(DataBase? parent, string? dataName, ArchiveAttributes attributes) {
        Name = dataName;
        Parent = parent;
        Attributes = attributes;
    }
    /// <inheritdoc/>
    public abstract void Dispose();
    /// <inheritdoc/>
    public abstract string ToString(string format, IFormatProvider formatProvider);
    /// <summary>Gets the full path of a data file.</summary>
    /// <param name="data">The data file to be obtained is the full path.</param>
    /// <returns>It will return a string containing the full path of the data file.</returns>
    public static string GetDataPath(DataBase? data) {
        if (data is null) return string.Empty;
        else if (data.Attributes == ArchiveAttributes.Null) return string.Empty;
        return GodotPath.Combine(GetDataPath(data.Parent), data.Name ?? string.Empty);
    }
}
