using Godot;
using System;

using GDTFile = Godot.File;
using IOPath = System.IO.Path;

namespace Cobilas.GodotEngine.Utility; 
/// <summary>This class is a representation of a file.</summary>
[Obsolete("Use Archive class")]
public class GDFile : GDFileBase {
    private bool disposedValue;
    /// <inheritdoc/>
    public override string Path { get; protected set; }
    /// <inheritdoc/>
    public override string Name => IOPath.GetFileName(Path);
    /// <inheritdoc/>
    public override GDFileBase Parent { get; protected set; }
    /// <inheritdoc/>
    public override GDFileAttributes Attribute { get; protected set; }
    /// <inheritdoc/>
    public override string NameWithoutExtension => IOPath.GetFileNameWithoutExtension(Path);

    internal GDFile(GDFileBase? parent, string? path, GDFileAttributes attributes) {
        if (parent is null) throw new ArgumentNullException(nameof(parent));
        if (path is null) throw new ArgumentNullException(nameof(path));
        this.Path = path;
        this.Parent = parent;
        this.Attribute = attributes;
    }

    internal GDFile(GDFileBase? parent, string? path) : this(parent, path, GDFileAttributes.File) {}
    /// <summary>The finalizer allows the disposal of unmanageable code.</summary>
    ~GDFile()
        => Dispose(disposing: false);
    /// <summary>Allows reading of the file.</summary>
    /// <returns>Returns the list of bytes read from the file converted to a string using the UTF-8 decoder.</returns>
    public string Read() {
        string result = string.Empty;
        using GDTFile file = new();
        if (file.Open(Path, GDTFile.ModeFlags.Read) == Error.Ok) {
            result = file.GetAsText(false);
            file.Close();
        }
        return result;
    }
    /// <summary>Allows you to write data into the file.</summary>
    /// <param name="buffer">The list of bytes that will be written into the file.</param>
    public void Write(byte[]? buffer) {
        if (buffer is null) throw new ArgumentNullException(nameof(buffer));

        using GDTFile file = new();
        if (file.Open(Path, GDTFile.ModeFlags.Write) == Error.Ok) {
            file.StoreBuffer(buffer);
            file.Close();
        }
    }
    /// <summary>Loads a resource at the given <c>path</c>, caching the result for further access.</summary>
    /// <remarks>
    /// The registered <see cref="Godot.ResourceFormatLoader"/> are queried sequentially to find the
    /// first one which can handle the file's extension, and then attempt loading. If
    /// loading fails, the remaining ResourceFormatLoaders are also attempted.
    /// </remarks>
    /// <param name="typeHint">
    /// An optional <c>type_hint</c> can be used to further specify the <see cref="Godot.Resource"/> type
    /// that should be handled by the Godot.ResourceFormatLoader. Anything that inherits
    /// from <see cref="Godot.Resource"/> can be used as a type hint, for example <see cref="Godot.Image"/>.
    /// </param>
    /// <param name="noCache">
    /// If <c>no_cache</c> is <c>true</c>, the resource cache will be bypassed and the resource will
    /// be loaded anew. Otherwise, the cached resource will be returned if it exists.
    /// </param>
    /// <returns>Returns an empty resource if no <see cref="Godot.ResourceFormatLoader"/> could handle the file.</returns>
    public Resource Load(string typeHint = "", bool noCache = false)
        => ResourceLoader.Load(Path, typeHint, noCache);
    /// <inheritdoc cref="GDFile.Load(string, bool)"/>
    public Resource Load()
        => ResourceLoader.Load(Path);
    /// <inheritdoc cref="GDFile.Load(string, bool)"/>
    /// <typeparam name="T">The type to cast to. Should be a descendant of <see cref="Godot.Resource"/>.</typeparam>
    /// <exception cref="System.InvalidCastException">Thrown when the given the loaded resource can't be casted to the given type T.</exception>
    public T Load<T>(string typeHint = "", bool noCache = false) where T : class
        => ResourceLoader.Load<T>(Path, typeHint, noCache);
    /// <inheritdoc cref="GDFile.Load{T}(string, bool)"/>
    public T Load<T>() where T : class
        => ResourceLoader.Load<T>(Path);
    /// <inheritdoc cref="IDisposable.Dispose"/>
    /// <param name="disposing">When the value is true the object discards the manageable resources.</param>
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
    /// <inheritdoc/>
    public override void Dispose() {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}