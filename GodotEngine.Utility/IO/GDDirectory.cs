using Godot;
using System;
using System.Text;
using Cobilas.Collections;

using SYSPath = System.IO.Path;

namespace Cobilas.GodotEngine.Utility; 
/// <summary>Represents a directory file.</summary>
public sealed class GDDirectory : GDFileBase {

    private bool disposedValue;
    private GDFileBase[] subDir = Array.Empty<GDFileBase>();
    /// <inheritdoc/>
    public override string Path { get; protected set; }
    /// <inheritdoc/>
    public override string NameWithoutExtension => Name;
    /// <inheritdoc/>
    public override string Name => SYSPath.GetFileName(Path);
    /// <summary>Subdirectory count.</summary>
    /// <returns>Returns the number of subdirectories present in this directory.</returns>
    public int Count => ArrayManipulation.ArrayLength(subDir);
    /// <inheritdoc/>
    public override GDFileBase Parent { get; protected set; }
    /// <inheritdoc/>
    public override GDFileAttributes Attribute { get; protected set; }

    internal GDDirectory(GDFileBase? parent, string? path, GDFileAttributes attributes) {
        if (parent is null) throw new ArgumentNullException(nameof(parent));
        if (path is null) throw new ArgumentNullException(nameof(path));

        Path = path;
        Parent = parent;
        this.Attribute = attributes;
    }

    internal GDDirectory(GDFileBase? parent, string? path) : this(parent, path, GDFileAttributes.Directory) {}
    /// <summary>The finalizer allows the disposal of unmanageable code.</summary>
    ~GDDirectory() => Dispose(disposing: false);
    /// <inheritdoc/>
    public override void Dispose() {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    /// <summary>Gets a specific directory.</summary>
    /// <param name="relativePath">The name of the directory to search.</param>
    /// <param name="isSubdirectory">Allows the method to search subdirectories.</param>
    /// <exception cref="ArgumentNullException">The exception is thrown when <seealso cref="string"/> object is null.</exception>
    /// <returns></returns>
    public GDDirectory? GetDirectory(string? relativePath, bool isSubdirectory = false) {
        if (relativePath is null) throw new ArgumentNullException(nameof(relativePath));

        for (int I = 0; I < ArrayManipulation.ArrayLength(subDir); I++) {
            if (subDir[I].Attribute == GDFileAttributes.Directory &&
            (subDir[I].Path == relativePath || subDir[I].Path.TrimEnd('/') == relativePath))
                return subDir[I] as GDDirectory;
            else if (subDir[I].Attribute == GDFileAttributes.Directory && isSubdirectory) {
                GDDirectory res = (subDir[I] as GDDirectory)!.GetDirectory(relativePath, isSubdirectory)!;
                if (res != null)
                    return res;
            }
        }
        return GDIONull.DirectoryNull;
    }
    /// <summary>Get all directories.</summary>
    /// <remarks>Subdirectories of directories already obtained will not be included.</remarks>
    /// <returns>If no directory is found an empty list will be returned.</returns>
    public GDDirectory[] GetDirectories() {
        GDDirectory[] res = Array.Empty<GDDirectory>();
        for (int I = 0; I < ArrayManipulation.ArrayLength(subDir); I++)
            if (subDir[I].Attribute == GDFileAttributes.Directory)
                ArrayManipulation.Add((subDir[I] as GDDirectory)!, ref res);
        return res;
    }
    /// <summary>Allows you to create a new directory.</summary>
    /// <param name="directoryName">The name of the new directory.</param>
    /// <exception cref="ArgumentNullException">The exception is thrown when <seealso cref="string"/> object is null.</exception>
    /// <returns>Returns <c>true</c> if the directory was created successfully.</returns>
    public bool CreateDirectory(string? directoryName) {
        if (directoryName is null) throw new ArgumentNullException(nameof(directoryName));
        using Directory directory = new();
        if (directory.Open(Path) == Error.Ok)
            if (directory.MakeDir(directoryName) == Error.Ok) {
                ArrayManipulation.Add(new GDDirectory(this, $"{Path}{directoryName}/"), ref subDir);
                return true;
            }
        return false;
    } 
    /// <summary>Allows you to remove a specific directory.</summary>
    /// <param name="directoryName">The directory to be removed.</param>
    /// <exception cref="ArgumentNullException">The exception is thrown when <seealso cref="string"/> object is null.</exception>
    /// <returns>Returns <c>true</c> if the directory was deleted successfully.</returns>
    public bool RemoveDirectory(string? directoryName) {
        if (directoryName is null) throw new ArgumentNullException(nameof(directoryName));
        using Directory directory = new();
        if (directory.Open(Path) == Error.Ok)
            if (directory.Remove(directoryName) == Error.Ok) {
                for (int I = 0; I < ArrayManipulation.ArrayLength(subDir); I++)
                    if (subDir[I].Attribute == GDFileAttributes.Directory && subDir[I].Name.TrimEnd('/') == directoryName) {
                        ArrayManipulation.Remove(I, ref subDir);
                        break;
                    }
                return true;
            }
        return false;
    }
    /// <summary>Allows the removal of a specific file.</summary>
    /// <param name="fileName">The name of the file to be deleted.</param>
    /// <exception cref="ArgumentNullException">The exception is thrown when <seealso cref="string"/> object is null.</exception>
    /// <returns>Returns <c>true</c> if the file was deleted successfully.</returns>
    public bool RemoveFile(string? fileName) {
        if (fileName is null) throw new ArgumentNullException(nameof(fileName));
        using Directory directory = new();
        if (directory.Open(Path) == Error.Ok)
            if (directory.Remove(fileName) == Error.Ok) {
                for (int I = 0; I < ArrayManipulation.ArrayLength(subDir); I++)
                    if (subDir[I].Attribute == GDFileAttributes.File &&
                    (subDir[I].Name == fileName || subDir[I].NameWithoutExtension == fileName)) {
                        ArrayManipulation.Remove(I, ref subDir);
                        break;
                    }
                return true;
            }
        return false;
    }
    /// <summary>Gets multiple files in the same directory.</summary>
    /// <param name="isSubdirectory">Allows the method to search subdirectories.</param>
    /// <returns>If no files are found, an empty list will be returned.</returns>
    public GDFile[] GetFiles(bool isSubdirectory = false) {
        GDFile[] res = Array.Empty<GDFile>();
        for (int I = 0; I < ArrayManipulation.ArrayLength(subDir); I++)
            switch (subDir[I].Attribute) {
                case GDFileAttributes.File:
                    ArrayManipulation.Add((subDir[I] as GDFile)!, ref res);
                    break;
                case GDFileAttributes.Directory  when isSubdirectory:
                    ArrayManipulation.Add((subDir[I] as GDDirectory)!.GetFiles(isSubdirectory), ref res);
                    break;
            }
        return res;
    }
    /// <summary>Gets a specific file.</summary>
    /// <param name="name">The name of the file to search for.</param>
    /// <param name="isSubdirectory">Allows the method to search subdirectories.</param>
    /// <exception cref="ArgumentNullException">The exception is thrown when <seealso cref="string"/> object is null.</exception>
    /// <returns>If the directory is not found, a <seealso cref="GDDirectory"/> marked as <seealso cref="GDFileAttributes.Null"/> will be returned.</returns>
    public GDFile? GetFile(string? name, bool isSubdirectory = false) {
        if (name is null) throw new ArgumentNullException(nameof(name));
        for (int I = 0; I < ArrayManipulation.ArrayLength(subDir); I++)
            switch (subDir[I].Attribute) {
                case GDFileAttributes.File:
                    if (subDir[I].Name == name || subDir[I].NameWithoutExtension == name)
                        return subDir[I] as GDFile;
                    break;
                case GDFileAttributes.Directory when isSubdirectory:
                    GDFile directory = (subDir[I] as GDDirectory)!.GetFile(name, isSubdirectory)!;
                    if (directory != null) 
                        return directory;
                    break;
            }
        return GDIONull.FileNull;
    }
    /// <inheritdoc/>
    public override string ToString() {
        StringBuilder builder = new();
        builder.AppendFormat("[{0}]{1}\r\n", Attribute, Path);
        for (int I = 0; I < ArrayManipulation.ArrayLength(subDir); I++)
            builder.Append(subDir[I].ToString());
        return builder.ToString();
    }

    private void Dispose(bool disposing) {
        if (!disposedValue) {
            if (disposing) {
                for (int I = 0; I < Count; I++)
                    subDir[I].Dispose();
                Path = null!;
                Attribute = default;
                Parent = null!;
                ArrayManipulation.ClearArraySafe(ref subDir);
            }
            disposedValue = true;
        }
    }
    /// <inheritdoc cref="GDDirectory.GetGDDirectory(string?)"/>
    public static GDDirectory? GetGDDirectory()
        => GetGDDirectory("res://");
    /// <summary>Opens an existing directory of the filesystem.</summary>
    /// <param name="path">
    /// The path argument can be within the project tree (<c>res://folder</c>), 
    /// the user directory (<c>user://folder</c>) or an absolute
    /// path of the user filesystem (e.g. <c>/tmp/folder</c> or <c>C:\tmp\folder</c>).
    /// </param>
    public static GDDirectory? GetGDDirectory(string? path)
        => GetGDDirectory(SYSPath.IsPathRooted(path) ? $"{path}/" : path, GDFileBase.Null);

    private static GDDirectory? GetGDDirectory(string? relativePath, GDFileBase? parent) {
        if (relativePath is null) throw new ArgumentNullException(nameof(relativePath));

        using Directory directory = new();
        if (directory.Open(relativePath) == Error.Ok) {
            GDDirectory gDDirectory = new(parent, relativePath);
            directory.ListDirBegin(false, true);
            string filename = directory.GetNext();
            while (!string.IsNullOrEmpty(filename)) {
                if (filename == "." || filename == "..") {
                    filename = directory.GetNext();
                    continue;
                }
                if (directory.CurrentIsDir())
                    ArrayManipulation.Add(GetGDDirectory($"{relativePath}{filename}/", gDDirectory)!, ref gDDirectory.subDir);
                else ArrayManipulation.Add(new GDFile(gDDirectory, $"{relativePath}{filename}"), ref gDDirectory.subDir);
                filename = directory.GetNext();
            }
            directory.ListDirEnd();
            return gDDirectory;
        }
        return null;
    }
}