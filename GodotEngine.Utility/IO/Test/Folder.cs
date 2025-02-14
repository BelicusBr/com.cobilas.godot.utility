using Godot;
using System;
using System.Text;
using System.Collections;
using Cobilas.Collections;
using System.Collections.Generic;

using IOFile = System.IO.File;

namespace Cobilas.GodotEngine.Utility.IO.Test;

public class Folder : DataBase, IEnumerable<DataBase> {
    private bool discarded;
    private DataBase[] datas;

    private static bool _noPrintInfo;

    public override string? Path { get; protected set; }
    public override DataBase? Parent { get; protected set; }
    public override ArchiveAttributes Attributes { get; protected set; }
    public override string Name => GetDataBaseName(Path ?? string.Empty);

    private static readonly Folder @null = new(null, string.Empty, ArchiveAttributes.Null);

    public static Folder Null => @null;

    public Folder(DataBase? parent, string? path, ArchiveAttributes attributes) : base(parent, path, attributes) {}

    public Folder CreateFolder(string folderName) {
        using Directory directory = new();
        return directory.Open(Path) switch {
            Error.Ok => directory.MakeDir(folderName) switch {
                Error.Ok => new(this, GodotPath.Combine(Path ?? string.Empty, folderName), ArchiveAttributes.Directory),
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
        fileName = GodotPath.Combine(Path, fileName);
        string fpath = GodotPath.GlobalizePath(fileName);
        string root1 = GodotPath.GetPathRoot(Path);
        string root2 = GodotPath.GetPathRoot(fileName);
        if (root1 != root2) 
            throw new System.InvalidOperationException($"path '{fileName}' does not belong to the same root as '{Path}'.");

        IOFile.Create(fpath).Dispose();

        return new(this, fileName, ArchiveAttributes.File);
    }

    public Folder[] GetFolders() {
        Folder[] result = [];
        foreach (DataBase item in datas)
            if (item is Folder fd)
                ArrayManipulation.Add(fd, ref result);
        return result;
    }

    public Folder GetFolder(string? folderName, bool recursive = false) {
        foreach (DataBase item in datas)
            if (item is Folder fd) {
                if (fd.Name == folderName)
                    return fd;
                if (recursive) {
                    Folder temp = fd.GetFolder(folderName);
                    if (temp != Null)  return temp;
                }
            }
        return Null;
    }

    public Archive[] GetArchives(string? search, bool recursive = false) {
        search ??= string.Empty;
        string[] research = search.Split(new char[] { '|' }, System.StringSplitOptions.RemoveEmptyEntries);
        Archive[] result = [];

        for (var A = 0; A < datas.Length; A++) {
            switch (datas[A]) {
                case Archive ac:
                    if (research.Length != 0) {
                        for (var I = 0; I < research.Length; I++)
                            if (ac.Name.Contains(research[I])) {
                                result = ArrayManipulation.Add(ac, result);
                                break;
                            }
                    } else result = ArrayManipulation.Add(ac, result);
                    break;
                case Folder fd:
                    if (recursive)
                        result = ArrayManipulation.Add(fd.GetArchives(search, recursive), result);
                    break;
            }
        }

        return result;
    }

    public Archive GetArchive(string? fileName, bool recursive = false) {
        if (fileName is null) return Archive.Null;

        for (var A = 0; A < datas.Length; A++) {
            switch (datas[A]) {
                case Archive ac:
                    if (ac.Name == fileName || ac.NameWithoutExtension == fileName)
                        return ac;
                    break;
                case Folder fd:
                    if (recursive) {
                        Archive temp = fd.GetArchive(fileName, recursive);
                        if (temp != Archive.Null)
                            return temp;
                    }
                    break;
            }
        }

        return Archive.Null;
    }

    public override string ToString() {
        StringBuilder builder = new();
        if (!_noPrintInfo) {
            _noPrintInfo = true;
            builder.AppendFormat("@>{0}\r\n", Name);
            builder.AppendFormat("@>{0}\r\n", Attributes);
            builder.AppendFormat("#>{0}\r\n", Parent is null ? string.Empty : Parent.Name);
            builder.AppendLine("=================================================");
            builder.AppendLine(Path);
            foreach (DataBase item in this)
                switch (item) {
                    case Folder fd: builder.Append(fd.ToString()); break;
                    case Archive ac: builder.AppendLine(ac.Path); break;
                }
        } else {
            builder.AppendLine(Path);
            foreach (DataBase item in this)
                switch (item) {
                    case Folder fd: builder.Append(fd.ToString()); break;
                    case Archive ac: builder.AppendLine(ac.Path); break;
                }
        }
        return builder.ToString();
    }

    public IEnumerator<DataBase> GetEnumerator() => new ArrayToIEnumerator<DataBase>(datas);

    public override void Dispose() {
        if (discarded) throw new ObjectDisposedException(nameof(Folder));
        foreach (DataBase item in datas)
            item.Dispose();
        Attributes = ArchiveAttributes.Null;
        ArrayManipulation.ClearArraySafe(ref datas);
    }

    IEnumerator IEnumerable.GetEnumerator() => new ArrayToIEnumerator<DataBase>(datas);

    public static Folder Create(string? path, Folder root) {
        if (path is null) throw new ArgumentNullException(nameof(path));
        Folder result = Null;

        using Directory directory = new();
        if (directory.Open(path) == Error.Ok) {
            ArchiveAttributes attributes = GDFeature.HasEditor ? ArchiveAttributes.Directory : ArchiveAttributes.Directory | ArchiveAttributes.ReadOnly;
            result = new(root, path, attributes);

            directory.ListDirBegin(true, true);
            string fileName = directory.GetNext();

            while (!string.IsNullOrEmpty(fileName)) {
                if (directory.CurrentIsDir())
                    result.datas = ArrayManipulation.Add((DataBase)Create(GodotPath.Combine(path, fileName), result), result.datas);
                else {
                    attributes = GDFeature.HasEditor ? ArchiveAttributes.File : ArchiveAttributes.File | ArchiveAttributes.ReadOnly;
                    Archive archive = new(result, GodotPath.Combine(path, fileName), attributes);
                    result.datas = ArrayManipulation.Add(archive, result.datas);
                }
                fileName = directory.GetNext();
            }

            directory.ListDirEnd();
        }

        return result;
    }
}
