using System;

namespace Cobilas.GodotEngine.Utility.IO.Test;

public abstract class DataBase : IDisposable {
    public abstract string Name { get; }
    public abstract string? Path { get; protected set; }
    public abstract DataBase? Parent { get; protected set; }
    public abstract ArchiveAttributes Attributes { get; protected set; }

    protected DataBase(DataBase? parent, string? path, ArchiveAttributes attributes) {
        Path = path;
        Parent = parent;
        Attributes = attributes;
    }

    public abstract void Dispose();

    public static string GetDataBaseName(string path) {
        if (path == "res://" || path == "user://") return path;
        return GodotPath.GetFileName(path);
    }
}
