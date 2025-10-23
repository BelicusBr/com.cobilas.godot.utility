using System;
using System.IO;
using Cobilas.Collections;
using System.Globalization;
using System.Collections.Generic;

namespace Cobilas.GodotEngine.Utility.IO;

/// <summary>Represents a folder node in the filesystem, providing access to its files and subfolders.</summary>
/// <remarks>
/// This class manages folder data and provides enumeration capabilities for both files and subfolders.
/// It handles both internal (Godot resource) and external (filesystem) folders.
/// </remarks>
public sealed class FolderNode : DataBase {
    private KeyValuePair<string, string>[]? datas;
    /// <inheritdoc/>
    public override string? Name { get; protected set; }
    /// <inheritdoc/>
    public override string Path => GetDataPath(this);
    /// <inheritdoc/>
    public override DataBase? Parent { get; protected set; }
    /// <inheritdoc/>
    public override IDataInfo? DataInfo { get; protected set; }
    /// <inheritdoc/>
    public override ArchiveAttributes Attributes { get; protected set; }

    /// <summary>Gets an enumerable collection of subfolders in this folder.</summary>
    /// <returns>An enumerable collection of key-value pairs where the key is "dir" and the value is the folder name.</returns>
    public IEnumerable<KeyValuePair<string, string>> GetFolders {
        get {
            for (long I = 0; I < ArrayManipulation.ArrayLongLength(datas); I++)
                if (datas![I].Key == "dir")
                    yield return datas[I];
        }
    }

    /// <summary>Gets an enumerable collection of files in this folder.</summary>
    /// <returns>An enumerable collection of key-value pairs where the key is "ark" and the value is the file name without extension.</returns>
    public IEnumerable<KeyValuePair<string, string>> GetFiles {
        get {
            for (long I = 0; I < ArrayManipulation.ArrayLongLength(datas); I++)
                if (datas![I].Key == "ark")
                    yield return datas[I];
        }
    }

    /// <summary>Initializes a new instance of the <see cref="FolderNode"/> class.</summary>
    /// <param name="parent">The parent data node, or null if this is a root node.</param>
    /// <param name="dataName">The name of the folder, or null if unnamed.</param>
    /// <param name="attributes">The attributes for this folder node.</param>
    /// <remarks>
    /// This constructor initializes the folder node and populates its internal data structures
    /// with information about the contained files and subfolders if the folder exists in the filesystem.
    /// </remarks>
    public FolderNode(DataBase? parent, string? dataName, ArchiveAttributes attributes) : base(parent, dataName, attributes) {
        string path = Path;
        DataInfo = new FolderInfo(path);
        if (DataInfo.IsInternal) return;
        string[] arks = Directory.GetFiles(path);
        string[] dirs = Directory.GetDirectories(path);
        datas = new KeyValuePair<string, string>[
            ArrayManipulation.ArrayLongLength(arks) + ArrayManipulation.ArrayLongLength(dirs)
            ];
        for (long I = 0; I < datas.LongLength; I++) {
            if (I < ArrayManipulation.ArrayLongLength(dirs))
                datas[I] = new("dir", GodotPath.GetFileName(dirs[I]));
            else {
                long index = I - ArrayManipulation.ArrayLongLength(dirs);
                datas[I] = new("ark", GodotPath.GetFileNameWithoutExtension(arks[index]));
            }
        }
    }
    /// <inheritdoc/>
    public override void Dispose() {
        ArrayManipulation.ClearArraySafe(ref datas);
        Name = null;
        Parent = null;
        DataInfo = null;
        Attributes = ArchiveAttributes.Null;
    }
    /// <inheritdoc/>
    public override string ToString() => ToString("PP", CultureInfo.CurrentCulture);
    /// <inheritdoc/>
    public override string ToString(string format, IFormatProvider formatProvider)
        => format switch {
            // "PR" => ToString(true, true, formatProvider),
            // "PN" => ToString(false, true, formatProvider),
            "PP" => string.Format(formatProvider, "path:'{0}'", Path),
            "PA" => string.Format(formatProvider, "attributes:'{0}'", Attributes),
            "PDN" => string.Format(formatProvider, "name:'{0}'", Name),
            "PPN" => string.Format(formatProvider, "parent_name:'{0}'", Parent?.Name),
            "PDC" => string.Format(formatProvider, "data_count:'{0}'", ArrayManipulation.ArrayLength(datas)),
            _ => throw new FormatException($"The format '{format}' is not recognized!"),
        };

    internal static DataBase? GetFolderNodeRoot(in string? path, in DataBase? root) {
        DataBase? num1 = root;
        string[]? names = GetNames(path);
        if (ArrayManipulation.ArrayLength(names) <= 1) return root;

        for (int I = 0; I < ArrayManipulation.ArrayLength(names); I++)
            num1 = new FolderNode(num1, names![I], ArchiveAttributes.Directory | ArchiveAttributes.ReadOnly);

        return num1;
    }

    private static string[]? GetNames(in string? path) {
        if (path is null) return [];
        string root = GodotPath.GetPathRoot(path);
        string sPath = path.Replace(root, string.Empty).Trim();
        if (string.IsNullOrEmpty(sPath)) return [ root ];
        return ArrayManipulation.Add(sPath.Split('/', '\\'), new string[] { root });
    }
}
