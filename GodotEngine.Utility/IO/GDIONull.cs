namespace Cobilas.GodotEngine.Utility;
/// <summary>This class is a representation of a null file.</summary>
public sealed class GDIONull : GDFileBase {
    /// <inheritdoc/>
    public override string Name => string.Empty;
    /// <inheritdoc/>
    public override string NameWithoutExtension => string.Empty;
    /// <inheritdoc/>
    public override string Path { get; protected set; } = string.Empty;
    /// <inheritdoc/>
    public override GDFileBase Parent { get; protected set; } = null!;
    /// <inheritdoc/>
    public override GDFileAttributes Attribute { get; protected set; } = GDFileAttributes.Null;
    /// <summary>Represents a null file.</summary>
    /// <returns>Returns a representation of a null file.</returns>
    public static GDFile FileNull => new(Null, string.Empty, GDFileAttributes.Null);
    /// <summary>Represents a null directory file.</summary>
    /// <returns>Returns a representation of a null directory file.</returns>
    public static GDDirectory DirectoryNull => new(Null, string.Empty, GDFileAttributes.Null);
    /// <inheritdoc/>
    public override void Dispose() {}
}