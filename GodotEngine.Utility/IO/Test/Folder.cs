using Godot;
using System.Collections;
using Cobilas.Collections;
using System.Collections.Generic;

namespace Cobilas.GodotEngine.Utility.IO.Test;

public class Folder : DataBase, IEnumerable<DataBase> {
    private DataBase[] datas;

    public override string? Path { get; protected set; }
    public override DataBase? Parent { get; protected set; }
    public override ArchiveAttributes Attributes { get; protected set; }

    private static readonly Folder @null = new(null, string.Empty, ArchiveAttributes.Null);

    public static Folder Null => @null;

    public Folder(DataBase? parent, string? path, ArchiveAttributes attributes) : base(parent, path, attributes) {}

    public Folder CreateFolder(string relativePath, bool recursive = false) {
        using Directory directory = new();
        return directory.Open(Path) switch {
            Error.Ok => (recursive ? directory.MakeDirRecursive(relativePath) : directory.MakeDir(relativePath)) switch {
                Error.Ok => new(this, $"{Path}/{relativePath.TrimStart('/', '\\')}", ArchiveAttributes.Directory),
                _ => @null,
            },
            Error.FileCantRead => throw new System.InvalidOperationException("Error.FileCantRead"),
            Error.FileCantWrite => throw new System.InvalidOperationException("Error.FileCantWrite"),
            Error.FileNotFound => throw new System.IO.FileNotFoundException("File Not Found", Path),
            _ => @null
        };
    }

    public Archive CreateArchive(string fileName) {
        if (Attributes.HasFlag(ArchiveAttributes.ReadOnly))
            throw new System.InvalidOperationException("Is ReadOnly");

        if (Path is null) return Archive.Null;
        string folderPath = Path.Replace("res://", string.Empty).Replace("user://", string.Empty);
        folderPath = System.IO.Path.Combine(folderPath, fileName);

        return new(this, folderPath, ArchiveAttributes.File);
    }

    public Folder[] GetFolders() {
        Folder[] result = [];
        foreach (DataBase item in datas)
            if (item is Folder fd)
                ArrayManipulation.Add(fd, ref result);
        return result;
    }

    public Folder GetFolder(string? folderName) {
        foreach (DataBase item in datas)
            if (item is Folder fd)
                if (sys)
    }

    public IEnumerator<DataBase> GetEnumerator() => new ArrayToIEnumerator<DataBase>(datas);

    IEnumerator IEnumerable.GetEnumerator() => new ArrayToIEnumerator<DataBase>(datas);
}
