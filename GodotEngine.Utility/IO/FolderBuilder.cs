using System;
using System.IO;
using Cobilas.Collections;

using GDDir = Godot.Directory;

namespace Cobilas.GodotEngine.Utility.IO;
/// <summary>Provides utility methods for building folder structures in Godot.</summary>
/// <remarks>This class handles the creation of folder hierarchies for both resource (res://) and user (user://) paths.</remarks>
internal static class FolderBuilder {
    /// <summary>Creates a folder structure starting from the Godot resource root (res://).</summary>
    /// <returns>A <see cref="Folder"/> object representing the resource root folder structure.</returns>
    internal static Folder CreateRes()
        => CreateRes(GodotPath.ResPath, Folder.Null);
    /// <summary>Creates a folder structure starting from the Godot user data root (user://).</summary>
    /// <returns>A <see cref="Folder"/> object representing the user data root folder structure.</returns>
    internal static Folder CreateUser()
        => CreateRes(GodotPath.UserPath, Folder.Null);
    /// <summary>Creates a folder structure for the specified path.</summary>
    /// <param name="path">The path to create the folder structure for.</param>
    /// <returns>A <see cref="Folder"/> object representing the folder structure at the specified path.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="path"/> is null.</exception>
    internal static Folder Create(string? path) {
        if (path is null) throw new ArgumentNullException(nameof(path));
        return GodotPath.GetPathRoot(path) switch {
            GodotPath.ResPath => CreateRes(path, FolderNode.GetFolderNodeRoot(GodotPath.GetDirectoryName(path), Folder.Null)),
            GodotPath.UserPath => CreateRes(path, FolderNode.GetFolderNodeRoot(GodotPath.GetDirectoryName(path), Folder.Null)),
            _ => Create(path, FolderNode.GetFolderNodeRoot(GodotPath.GetDirectoryName(path), Folder.Null))
        };
    }

    private static Folder Create(string path, DataBase? root) {
        Folder result = Folder.Null;
        if (!Directory.Exists(path)) return result;
        ArchiveAttributes attributes = ArchiveAttributes.Directory;
        DirectoryInfo info = new(path);
        if (info.Attributes.HasFlag(FileAttributes.ReadOnly))
            attributes |= ArchiveAttributes.ReadOnly;
        if (info.Attributes.HasFlag(FileAttributes.Hidden))
            attributes |= ArchiveAttributes.Hidden;

        DirectoryInfo[] dirs = info.GetDirectories();
        FileInfo[] arks = info.GetFiles();
        DataBase[] list = new DataBase[
            ArrayManipulation.ArrayLongLength(arks) +
            ArrayManipulation.ArrayLongLength(dirs)
        ];
        result = new Folder(root, GodotPath.GetFileName(path), attributes);

        for (long I = 0; I < list.LongLength; I++) {
            if (I < ArrayManipulation.ArrayLongLength(dirs))
                list[I] = Create(dirs[I].FullName, result);
            else {
                long index = I - ArrayManipulation.ArrayLongLength(dirs);
                if (arks[index].Attributes.HasFlag(FileAttributes.ReadOnly))
                    attributes |= ArchiveAttributes.ReadOnly;
                if (arks[index].Attributes.HasFlag(FileAttributes.Hidden))
                    attributes |= ArchiveAttributes.Hidden;
                list[I] = new Archive(result, GodotPath.GetFileName(arks[index].FullName), attributes);
            }
        }
        Folder.SetDataList(result, list);
        return result;
    }

    private static Folder CreateRes(string? path, DataBase? root) {
        if (path is null) throw new ArgumentNullException(nameof(path));
        Folder result = Folder.Null;

        using GDDir directory = new();
        if (directory.Open(path) == Godot.Error.Ok)
        {
            ArchiveAttributes attributes = ArchiveAttributes.Directory;
            if (GDFeature.HasRelease)
                if (GodotPath.GetPathRoot(path) == GodotPath.ResPath)
                    attributes |= ArchiveAttributes.ReadOnly;
            string npath = GodotPath.GetFileName(path);
            result = new(root, string.IsNullOrEmpty(npath) ? path : npath, attributes);

            directory.ListDirBegin(true, true);
            string fileName = directory.GetNext();
            DataBase[]? dirs = null;
            DataBase[]? arks = null;

            while (!string.IsNullOrEmpty(fileName)) {
                if (directory.CurrentIsDir())
                    dirs = ArrayManipulation.Add((DataBase)Create(GodotPath.Combine(path, fileName), result), dirs);
                else {
                    attributes = ArchiveAttributes.File;
                    if (GDFeature.HasRelease)
                        if (GodotPath.GetPathRoot(path) == GodotPath.ResPath)
                            attributes |= ArchiveAttributes.ReadOnly;
                    Archive archive = new(result, fileName, attributes);
                    arks = ArrayManipulation.Add(archive, arks);
                }
                fileName = directory.GetNext();
            }

            directory.ListDirEnd();
            Folder.SetDataList(result, ArrayManipulation.Add(arks, dirs));
        }
        return result;
    }
}
