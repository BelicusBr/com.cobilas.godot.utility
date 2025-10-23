using Godot;
using System;
using System.Text;
using System.Data;
using System.Collections;
using Cobilas.Collections;
using System.Globalization;
using System.Collections.Generic;

using IOFile = System.IO.File;

namespace Cobilas.GodotEngine.Utility.IO;
/// <summary>A representation of a system folder.</summary>
public class Folder : DataBase, IEnumerable<DataBase> {
    private bool discarded;
    private DataBase[]? datas = [];

    private static readonly char[] separator = { '/' };
    /// <inheritdoc/>
    public override string Path => GetDataPath(this);
    /// <inheritdoc/>
    public override DataBase? Parent { get; protected set; }
    /// <inheritdoc/>
    public override ArchiveAttributes Attributes { get; protected set; }
    /// <inheritdoc/>
    public override string? Name { get; protected set; }
    /// <inheritdoc/>
    public override IDataInfo? DataInfo { get; protected set; }

    private static readonly Folder @null = new(null, string.Empty, ArchiveAttributes.Null);
    /// <summary>A null representation of the <seealso cref="Folder"/> object.</summary>
    /// <returns>Returns a null representation of the <seealso cref="Folder"/> object.</returns>
    public static Folder Null => @null;
    /// <summary>Creates a new instance of this object.</summary>
    public Folder(DataBase? parent, string? dataName, ArchiveAttributes attributes) : base(parent, dataName, attributes) {
        DataInfo = new FolderInfo(Path);
    }
    /// <summary>Allows the creation of a new folder in the current folder.</summary>
    /// <param name="folderName">The name of the new folder.</param>
    /// <param name="recursive">Allows the creation of a folder within another in a cascade fashion. <c>(exp: Folder1/Folder2/Folder3/Folder4)</c></param>
    /// <exception cref="ArgumentNullException">Occurs if the <paramref name="folderName"/> parameter is null.</exception>
    /// <returns>Returns the newly created folder.</returns>
    public Folder CreateFolder(string? folderName, bool recursive = false) {
        if (folderName is null) throw new ArgumentNullException(nameof(folderName));
        Folder result = CreateRecursiveFolder(this, 0, recursive ? folderName.Split(separator, StringSplitOptions.RemoveEmptyEntries) : new string[] { folderName });
        ReorderList();
        return result;
    }
    /// <summary>Allows you to create a new file in the current folder.</summary>
    /// <param name="fileName">The name of this new file.</param>
    /// <returns>Returns the new file that was created in the current folder.</returns>
    /// <exception cref="ReadOnlyException">Will occur if the method is called on an object that is marked as read-only.</exception>
    /// <exception cref="ArgumentNullException">Occurs if the <paramref name="fileName"/> parameter is null.</exception>
    /// <exception cref="System.InvalidOperationException">Occurs when the name of the new file has an invalid character.</exception>
    public Archive CreateArchive(string? fileName) {
        if (fileName is null) throw new ArgumentNullException(nameof(fileName));
        else if (Attributes.HasFlag(ArchiveAttributes.ReadOnly))
            throw new ReadOnlyException("Is ReadOnly");
        else if (GodotPath.IsInvalidFileName(fileName, out char ic))
            throw new System.InvalidOperationException($"The name '{fileName}' has the invalid character '{ic.EscapeSequenceToString()}'.");

        if (Path is null) return Archive.Null;
        fileName = GodotPath.Combine(Path, fileName);
        string fpath = GodotPath.GlobalizePath(fileName);

        IOFile.Create(fpath).Dispose();
        Archive result = new(this, fileName, ArchiveAttributes.File);
        ArrayManipulation.Add(result, ref datas);
        ReorderList();
        return result;
    }
    /// <summary>Allows you to rename the folder.</summary>
    /// <param name="oldName">The name of the folder.</param>
    /// <param name="newName">The new name of the folder.</param>
    /// <returns>Returns <c>true</c> when the rename operation was successful.</returns>
    /// <exception cref="ReadOnlyException">Will occur if the method is called on an object that is marked as read-only.</exception>
    /// <exception cref="ArgumentNullException">Occurs if the <paramref name="oldName"/> parameter is null.</exception>
    /// <exception cref="ArgumentNullException">Occurs if the <paramref name="newName"/> parameter is null.</exception>
    /// <exception cref="System.InvalidOperationException">Occurs when the name of the new file has an invalid character.</exception>
    public bool RenameFolder(string? oldName, string? newName) {
        if (oldName is null) throw new ArgumentNullException(nameof(oldName));
        else if (newName is null) throw new ArgumentNullException(nameof(newName));
        else if (Attributes.HasFlag(ArchiveAttributes.ReadOnly))
            throw new System.InvalidOperationException("Is ReadOnly");
        else if (GodotPath.IsInvalidFileName(newName, out char ic))
            throw new System.InvalidOperationException($"The name '{newName}' has the invalid character '{ic.EscapeSequenceToString()}'.");
        
        using Directory directory = new();
        if (oldName == newName || directory.Open(Path) != Error.Ok) return false;
        Folder folder = GetFolder(oldName);
        if (folder == @null) return false;

        oldName = GodotPath.Combine(Path ?? string.Empty, oldName);
        newName = GodotPath.Combine(Path ?? string.Empty, newName);
        if (directory.Rename(oldName, newName) == Error.Ok) {
            folder.Name = GodotPath.GetFileName(newName);
            return true;
        }
        return false;
    }
    /// <summary>Allows the removal of a folder.</summary>
    /// <param name="folderName">The name of the folder.</param>
    /// <returns>Returns <c>true</c> when the remove operation is successful.</returns>
    /// <exception cref="ReadOnlyException">Will occur if the method is called on an object that is marked as read-only.</exception>
    /// <exception cref="ArgumentNullException">Occurs if the <paramref name="folderName"/> parameter is null.</exception>
    public bool RemoveFolder(string? folderName) {
        if (folderName is null) throw new ArgumentNullException(nameof(folderName));
        else if (Attributes.HasFlag(ArchiveAttributes.ReadOnly))
            throw new ReadOnlyException("Is ReadOnly");

        using Directory directory = new();
        if (directory.Open(Path) != Error.Ok) return false;
        Folder folder = GetFolder(folderName);
        if (folder == @null) return false;

        folderName = GodotPath.Combine(Path ?? string.Empty, folderName);

        if (directory.Remove(folderName) == Error.Ok) {
            datas = ArrayManipulation.Remove(folder, datas);
            return true;
        }

        return false;
    }
    /// <summary>Allows you to remove a file in the current folder.</summary>
    /// <param name="archiveName">The name of the archive.</param>
    /// <returns>Returns <c>true</c> when the remove operation is successful.</returns>
    /// <exception cref="ReadOnlyException">Will occur if the method is called on an object that is marked as read-only.</exception>
    /// <exception cref="ArgumentNullException">Occurs if the <paramref name="archiveName"/> parameter is null.</exception>
    public bool RemoveArchive(string? archiveName) {
        if (archiveName is null) throw new ArgumentNullException(nameof(archiveName));
        else if (Attributes.HasFlag(ArchiveAttributes.ReadOnly))
            throw new ReadOnlyException("Is ReadOnly");

        Archive archive = GetArchive(archiveName);
        if (archive == Archive.Null) return false;
        else if (!IOFile.Exists(GodotPath.GlobalizePath(archive.Path))) return false;

        IOFile.Delete(GodotPath.GlobalizePath(archive.Path));
        datas = ArrayManipulation.Remove(archive, datas);
        
        return true;
    }
    /// <summary>Allows renaming of a file in the current folder.</summary>
    /// <param name="oldName">The name of the archive.</param>
    /// <param name="newName">The new name of the archive.</param>
    /// <returns>Returns <c>true</c> when the rename operation was successful.</returns>
    /// <exception cref="ReadOnlyException">Will occur if the method is called on an object that is marked as read-only.</exception>
    /// <exception cref="ArgumentNullException">Occurs if the <paramref name="oldName"/> parameter is null.</exception>
    /// <exception cref="ArgumentNullException">Occurs if the <paramref name="newName"/> parameter is null.</exception>
    /// <exception cref="InvalidOperationException">Occurs when the name of the new file has an invalid character.</exception>
    public bool RenameArchive(string? oldName, string? newName) {
        if (oldName is null) throw new ArgumentNullException(nameof(oldName));
        else if (newName is null) throw new ArgumentNullException(nameof(newName));
        else if (Attributes.HasFlag(ArchiveAttributes.ReadOnly))
            throw new ReadOnlyException("Is ReadOnly");
        else if (GodotPath.IsInvalidFileName(newName, out char ic))
            throw new InvalidOperationException($"The name '{newName}' has the invalid character '{ic.EscapeSequenceToString()}'.");

        if (oldName == newName) return false;
        
        Archive archive = GetArchive(oldName);
        if (archive == Archive.Null) return false;
        else if (!IOFile.Exists(GodotPath.GlobalizePath(archive.Path))) return false;

        return Archive.RenameArchive(archive, newName);
    }
    /// <summary>Checks if a folder exists.</summary>
    /// <param name="folderName">The name of the folder.</param>
    /// <returns>Returns <c>true</c> when the specified element exists.</returns>
    public bool FolderExists(string folderName) => DataExists(folderName, typeof(Folder));
    /// <summary>Checks if a file exists.</summary>
    /// <param name="archiveName">The name of the archive.</param>
    /// <returns>Returns <c>true</c> when the specified element exists.</returns>
    public bool ArchiveExists(string archiveName) => DataExists(archiveName, typeof(Archive));
    /// <summary>Gets all folders in the current folder.</summary>
    /// <returns>Returns a list of all folders in the current folder.</returns>
    public Folder[]? GetFolders() {
        Folder[]? result = [];
        if (datas is not null)
            foreach (DataBase item in datas)
                if (item is Folder fd)
                    ArrayManipulation.Add(fd, ref result);
        return result;
    }
    /// <summary>Gets the target folder from the current folder.</summary>
    /// <param name="folderName">The name of the folder.</param>
    /// <param name="recursive">Allows you to get a specified folder in the current folder or its subfolders.</param>
    /// <returns>Returns the specified folder. If not found, a null representation will be returned.</returns>
    public Folder GetFolder(string? folderName, bool recursive = false) {
        if (folderName is null || datas is null) return @null;
        foreach (DataBase item in datas)
            if (item is Folder fd) {
                if (fd.Name == folderName)
                    return fd;
                if (recursive) {
                    Folder temp = fd.GetFolder(folderName);
                    if (temp != @null)  return temp;
                }
            }
        return @null;
    }
    /// <summary>Gets all archives in the current folder.</summary>
    /// <param name="search">Allows you to collect specific files. Use '|' to separate search conditions. (exp:".jpeg|.png|.txt")</param>
    /// <param name="recursive">Allows you to get a specified archives in the current folder or its subfolders.</param>
    /// <returns>Returns a list of all archives in the current folder.</returns>
    public Archive[]? GetArchives(string? search, bool recursive = false) {
        search ??= string.Empty;
        string[] research = search.Split(new char[] { '|' }, System.StringSplitOptions.RemoveEmptyEntries);
        Archive[]? result = (Archive[]?)null;

        if (datas is null) return result;

        for (var A = 0; A < datas.Length; A++) {
            switch (datas[A]) {
                case Archive ac:
                    if (research.Length != 0) {
                        for (var I = 0; I < research.Length; I++)
                            if ((ac.Name ?? string.Empty).Contains(research[I])) {
                                result = ArrayManipulation.Add(ac, result);
                                break;
                            }
                    }
                    else result = ArrayManipulation.Add(ac, result);
                    break;
                case Folder fd:
                    if (recursive)
                        result = ArrayManipulation.Add(fd.GetArchives(search, recursive), result);
                    break;
            }
        }

        return result;
    }
    /// <inheritdoc cref="GetArchive(string?, bool)"/>
    public Archive[]? GetArchives(bool recursive = false) => GetArchives(string.Empty, recursive);
    /// <summary>Gets the target archive from the current folder.</summary>
    /// <param name="fileName">The name of the archive.</param>
    /// <param name="recursive">Allows you to get a specified archive in the current folder or its subfolders</param>
    /// <returns>Returns the specified archive. If not found, a null representation will be returned.</returns>
    public Archive GetArchive(string? fileName, bool recursive = false) {
        if (fileName is null || datas is null) return Archive.Null;
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
    /// <inheritdoc cref="ToString()"/>
    public string ToString(bool recursive) => ToString(recursive ? "PR" : "PN", CultureInfo.CurrentCulture);
    /// <inheritdoc/>
    public override string ToString() => ToString(false);
    /// <inheritdoc/>
    public override string ToString(string format, IFormatProvider formatProvider) 
        => format switch {
            "PR" => ToString(true, true, formatProvider),
            "PN" => ToString(false, true, formatProvider),
            "PP" => string.Format(formatProvider, "path:'{0}'", Path),
            "PA" => string.Format(formatProvider, "attributes:'{0}'", Attributes),
            "PDN" => string.Format(formatProvider, "name:'{0}'", Name),
            "PPN" => string.Format(formatProvider, "parent_name:'{0}'", Parent?.Name),
            "PDC" => string.Format(formatProvider, "data_count:'{0}'", ArrayManipulation.ArrayLength(datas)),
            _ => throw new FormatException($"The format '{format}' is not recognized!"),
        };
    /// <inheritdoc/>
    public IEnumerator<DataBase> GetEnumerator() => new ArrayToIEnumerator<DataBase>(datas ?? []);
    /// <inheritdoc/>
    public override void Dispose() {
        if (discarded) throw new ObjectDisposedException(nameof(Folder));
        discarded = true;
        foreach (DataBase item in this)
            item.Dispose();
        Attributes = ArchiveAttributes.Null;
        ArrayManipulation.ClearArraySafe(ref datas);
    }

    IEnumerator IEnumerable.GetEnumerator() => new ArrayToIEnumerator<DataBase>(datas ?? []);

    private bool DataExists(string dataName, Type dataType) {
        foreach (DataBase item in this)
            if (item.CompareType(dataType) && item.Name == dataName)
                return true;
        return false;
    }

    private string ToString(bool recursive, bool printInfo, IFormatProvider formatProvider) {
        StringBuilder builder = new();
        if (printInfo) {
            builder.AppendFormat(formatProvider, "@>{0}\r\n", Name);
            builder.AppendFormat(formatProvider, "@>{0}\r\n", Attributes);
            builder.AppendFormat(formatProvider, "#>{0}\r\n", Parent is null ? string.Empty : Parent.Name);
            builder.AppendLine("=================================================");
        }
        builder.AppendFormat(formatProvider, "{0}\r\n", Path);
        foreach (DataBase item in this)
            switch (item) {
                case Folder fd: builder.AppendFormat(formatProvider, "{0}", recursive ? fd.ToString(recursive, false, formatProvider) : $"{fd.Path}\r\n"); break;
                case Archive ac: builder.AppendFormat(formatProvider, "{0}\r\n", ac.Path); break;
            }
        return builder.ToString();
    }

    private void ReorderList() {
        if (datas is null) return;
        DataBase[] result = new DataBase[ArrayManipulation.ArrayLength(datas)];
        int folderCount = 0;
        int folderIndex = 0;
        int fileIndex = 0;

        for (int I = 0; I < result.Length; I++)
            if (datas[I] is Folder) folderCount++;

        for (int I = 0; I < result.Length; I++)
            switch (datas[I]) {
                case Folder fd:
                    result[folderIndex++] = fd;
                    break;
                case Archive ac:
                    result[folderCount + fileIndex] = ac;
                    ++fileIndex;
                    break;
            }
        datas = result;
    }

    private static Folder CreateRecursiveFolder(Folder root, int index, string[] names) {
        if (index >= names.Length) return @null;
        string name = names[index];

        if (root.Attributes.HasFlag(ArchiveAttributes.ReadOnly))
            throw new System.InvalidOperationException("Is ReadOnly");
        else if (GodotPath.IsInvalidFileName(name, out char ic))
            throw new System.InvalidOperationException($"The name '{name}' has the invalid character '{ic.EscapeSequenceToString()}'.");

        using Directory directory = new();
        switch (directory.Open(root.Path)) {
            case Error.Ok:
                switch (directory.MakeDir(name)) {
                    case Error.Ok:
                        Folder folder = new(root, GodotPath.Combine(root.Path ?? string.Empty, name), ArchiveAttributes.Directory);
                        ArrayManipulation.Add((DataBase)folder, ref root.datas);
                        Folder temp = CreateRecursiveFolder(folder, index + 1, names);
                        if (temp != @null) return temp;
                        return folder;
                    default: return @null;
                }
            case Error.FileCantRead: throw new System.InvalidOperationException("Error.FileCantRead");
            case Error.FileCantWrite: throw new System.InvalidOperationException("Error.FileCantWrite");
            case Error.FileNotFound: throw new System.IO.FileNotFoundException("File Not Found", root.Path);
            default: return @null;
        }
    }
    /// <summary>Creates a new instance containing a representation of the <c>res://</c> folder.</summary>
    /// <inheritdoc cref="Create(string?)"/>
    public static Folder CreateRes() => FolderBuilder.CreateRes();
    /// <summary>Creates a new instance containing a representation of the <c>user://</c> folder.</summary>
    /// <inheritdoc cref="Create(string?)"/>
    public static Folder CreateUser() => FolderBuilder.CreateUser();
    /// <summary>Creates a new instance containing a specified directory.</summary>
    /// <param name="path">The path that will be instantiated.</param>
    /// <returns>Returns the representation of a folder.</returns>
    /// <exception cref="ArgumentNullException">Occurs if the <paramref name="path"/> parameter is null.</exception>
    public static Folder Create(string? path) => FolderBuilder.Create(path);

    internal static void SetDataList(Folder folder, DataBase[]? datas) => folder.datas = datas;
}
