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
    /// <summary>Gets or sets the data information object for this data file.</summary>
    /// <returns>An <see cref="IDataInfo"/> object containing information about the data file.</returns>
    public abstract IDataInfo DataInfo { get; protected set; }
    /// <summary>The attributes of the data file.</summary>
    /// <returns>Returns the attributes of the data file.</returns>
    public abstract ArchiveAttributes Attributes { get; protected set; }
    /// <summary>Creates a new instance of a data base object.</summary>
    /// <param name="parent">The parent data object, or null if this is a root object.</param>
    /// <param name="dataName">The name of the data file, or null if unnamed.</param>
    /// <param name="attributes">The attributes associated with this data file.</param>
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
    
    public static string ToString(DataBase? data)
    {
        throw new NotImplementedException();
    }
}
