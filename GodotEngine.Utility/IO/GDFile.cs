using Godot;
using System;

using GDTFile = Godot.File;
using IOPath = System.IO.Path;

namespace Cobilas.GodotEngine.Utility; 

public class GDFile : GDFileBase {
    private bool disposedValue;

    public override string Path { get; protected set; }
    public override string Name => IOPath.GetFileName(Path);
    public override GDFileBase Parent { get; protected set; }
    public override GDFileAttributes Attribute { get; protected set; }
    public override string NameWithoutExtension => IOPath.GetFileNameWithoutExtension(Path);

    internal GDFile(GDFileBase parent, string path, GDFileAttributes attributes) {
        this.Path = path;
        this.Parent = parent;
        this.Attribute = attributes;
    }

    internal GDFile(GDFileBase parent, string path) : this(parent, path, GDFileAttributes.File) {}

    ~GDFile()
        => Dispose(disposing: false);

    public string Read() {
        string result = string.Empty;
        using (GDTFile file = new())
            if (file.Open(Path, GDTFile.ModeFlags.Read) == Error.Ok) {
                result = file.GetAsText();
                file.Close();
            }
        return result;
    }

    public void Write(byte[] buffer) {
        using GDTFile file = new();
        if (file.Open(Path, GDTFile.ModeFlags.Write) == Error.Ok) {
            file.StoreBuffer(buffer);
            file.Close();
        }
    }

    public Resource Load()
        => ResourceLoader.Load(Path);

    public T Load<T>() where T : class
        => ResourceLoader.Load<T>(Path);

    protected virtual void Dispose(bool disposing) {
        if (!disposedValue) {
            if (disposing) {
                Path = null!;
                Parent = null!;
                Attribute = default;
            }
            disposedValue = true;
        }
    }

    public override void Dispose() {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}