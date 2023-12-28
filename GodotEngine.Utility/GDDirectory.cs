using Godot;
using System;
using System.Text;
using Cobilas.Collections;

using SYSPath = System.IO.Path;

namespace Cobilas.GodotEngine.Utility {
    public sealed class GDDirectory : GDFileBase {

        private bool disposedValue;
        private GDFileBase[] subDir;

        public override string Path { get; protected set; }
        public override GDFileBase Parent { get; protected set; }
        public override string Name => SYSPath.GetFileName(Path);
        public int Count => ArrayManipulation.ArrayLength(subDir);
        public override GDFileAttributes Attribute { get; protected set; }
        public override string NameWithoutExtension => SYSPath.GetFileNameWithoutExtension(Path);

        internal GDDirectory(GDFileBase parent, string path, GDFileAttributes attributes) {
            Path = path;
            Parent = parent;
            this.Attribute = attributes;
        }

        internal GDDirectory(GDFileBase parent, string path) : this(parent, path, GDFileAttributes.Directory) {}

        ~GDDirectory() => Dispose(disposing: false);

        public override void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public GDDirectory GetDirectory(string relativePath, bool isSubdirectory = false) {
            for (int I = 0; I < ArrayManipulation.ArrayLength(subDir); I++) {
                if (subDir[I].Attribute == GDFileAttributes.Directory &&
                (subDir[I].Path == relativePath || subDir[I].Path.TrimEnd('/') == relativePath))
                    return subDir[I] as GDDirectory;
                else if (subDir[I].Attribute == GDFileAttributes.Directory && isSubdirectory) {
                    GDDirectory res = (subDir[I] as GDDirectory).GetDirectory(relativePath, isSubdirectory);
                    if (res != null)
                        return res;
                }
            }
            return null;
        }

        public bool CreateDirectory(string directoryName) {
            using (Directory directory = new Directory())
                if (directory.Open(Path) == Error.Ok)
                    if (directory.MakeDir(directoryName) == Error.Ok) {
                        ArrayManipulation.Add(new GDDirectory(this, $"{Path}{directoryName}/"), ref subDir);
                        return true;
                    }
            return false;
        } 

        public bool RemoveDirectory(string directoryName) {
            using (Directory directory = new Directory())
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
        
        public bool RemoveFile(string fileName) {
            using (Directory directory = new Directory())
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

        public GDDirectory[] GetDirectories() {
            GDDirectory[] res = null;
            for (int I = 0; I < ArrayManipulation.ArrayLength(subDir); I++)
                if (subDir[I].Attribute == GDFileAttributes.Directory)
                    ArrayManipulation.Add(subDir[I] as GDDirectory, ref res);
            return res;
        }
        
        public GDFile[] GetFiles(bool isSubdirectory = false) {
            GDFile[] res = null;
            for (int I = 0; I < ArrayManipulation.ArrayLength(subDir); I++)
                switch (subDir[I].Attribute) {
                    case GDFileAttributes.File:
                        ArrayManipulation.Add(subDir[I] as GDFile, ref res);
                        break;
                    case GDFileAttributes.Directory  when isSubdirectory:
                        ArrayManipulation.Add((subDir[I] as GDDirectory).GetFiles(isSubdirectory), ref res);
                        break;
                }
            return res;
        }

        public GDFile GetFile(string name, bool isSubdirectory = false) {
            for (int I = 0; I < ArrayManipulation.ArrayLength(subDir); I++)
                switch (subDir[I].Attribute) {
                    case GDFileAttributes.File:
                        if (subDir[I].Name == name || subDir[I].NameWithoutExtension == name)
                            return subDir[I] as GDFile;
                        break;
                    case GDFileAttributes.Directory when isSubdirectory:
                        GDFile directory = (subDir[I] as GDDirectory).GetFile(name, isSubdirectory);
                        if (directory != null) 
                            return directory as GDFile;
                        break;
                }
            return null;
        }

        public override string ToString() {
            StringBuilder builder = new StringBuilder();
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
                    Path = null;
                    Attribute = default;
                    Parent = null;
                    ArrayManipulation.ClearArraySafe(ref subDir);
                }
                disposedValue = true;
            }
        }

        public static GDDirectory GetGDDirectory()
            => GetGDDirectory("res://", null);

        /// <summary>
        /// <br>Opens an existing directory of the filesystem. The path argument can be within</br>
        /// <br>the project tree (<c>res://folder</c>), the user directory (<c>user://folder</c>) or an absolute</br>
        /// <br>path of the user filesystem (e.g. <c>/tmp/folder</c> or <c>C:\tmp\folder</c>).</br>
        /// </summary>
        public static GDDirectory GetGDDirectory(string path)
            => GetGDDirectory(path, null);

        private static GDDirectory GetGDDirectory(string relativePath, GDDirectory parent) {
            using (Directory directory = new Directory()) {
                if (directory.Open(relativePath) == Error.Ok) {
                    GDDirectory gDDirectory = new GDDirectory(parent, relativePath);
                    directory.ListDirBegin(false, true);
                    string filename = directory.GetNext();
                    while (!string.IsNullOrEmpty(filename)) {
                        if (filename == "." || filename == "..") {
                            filename = directory.GetNext();
                            continue;
                        }
                        if (directory.CurrentIsDir())
                            ArrayManipulation.Add(GetGDDirectory($"{relativePath}{filename}/", gDDirectory), ref gDDirectory.subDir);
                        else ArrayManipulation.Add(new GDFile(gDDirectory, $"{relativePath}{filename}"), ref gDDirectory.subDir);
                        filename = directory.GetNext();
                    }
                    directory.ListDirEnd();
                    return gDDirectory;
                }
            }
            return null;
        }
    }
}