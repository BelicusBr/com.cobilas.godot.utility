namespace Cobilas.GodotEngine.Utility.IO.Test;

public abstract class DataBase {
    public abstract string? Path { get; protected set; }
    public abstract DataBase? Parent { get; protected set; }
    public abstract ArchiveAttributes Attributes { get; protected set; }

    protected DataBase(DataBase? parent, string? path, ArchiveAttributes attributes) {
        Path = path;
        Parent = parent;
        Attributes = attributes;
    }
}
