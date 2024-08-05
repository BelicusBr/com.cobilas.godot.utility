namespace Cobilas.GodotEngine.Utility;

public sealed class GDIONull : GDFileBase {
    public override string Name => string.Empty;
    public override string NameWithoutExtension => string.Empty;
    public override string Path { get; protected set; } = string.Empty;

    public override GDFileBase Parent { get; protected set; } = null!;
    public override GDFileAttributes Attribute { get; protected set; } = GDFileAttributes.Null;

    public override void Dispose() {}
}