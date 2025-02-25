using System;
using Cobilas.GodotEngine.Utility.IO;

namespace Cobilas.GodotEngine.Utility; 
/// <summary>This is a base class for other classes that represent files or directory files.</summary>
[Obsolete("Use DataBase class")]
public abstract class GDFileBase : IDisposable {
    /// <summary>Item name.</summary>
    /// <returns>Returns the name of the item.</returns>
    public abstract string Name { get; }
    /// <summary>The path of the item.</summary>
    /// <returns>Returns the full or relative path of the item.</returns>
    public abstract string Path { get; protected set; }
    /// <summary>The name of the item without extension.</summary>
    /// <returns>Returns the item name without its extension.</returns>
    public abstract string NameWithoutExtension { get; }
    /// <summary>The item's parent <seealso cref="GDFileBase"/>.</summary>
    /// <returns>Returns the parent GDFileBase that the item is affiliated with.</returns>
    public abstract GDFileBase Parent { get; protected set; }
    /// <summary>The type of the item.</summary>
    /// <returns>Returns the type of attribute the item has.
    /// <para><c>Null</c>: Indicates that the item is a representation of a null file.</para>
    /// <para><c>File</c>: Indicates that the item is a representation of a file.</para>
    /// <para><c>Directory</c>: Indicates that the item is a representation of a directory file.</para>
    /// </returns>
    public abstract ArchiveAttributes Attribute { get; protected set; }
    /// <summary>A null item.</summary>
    /// <returns>Returns a representation of a null item.</returns>
    public static GDFileBase Null => new GDIONull();
    /// <inheritdoc/>
    public abstract void Dispose();
}