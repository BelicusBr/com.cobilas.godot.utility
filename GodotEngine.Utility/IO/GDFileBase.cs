using System;

namespace Cobilas.GodotEngine.Utility; 

public abstract class GDFileBase : IDisposable {
    public abstract string Name { get; }
    public abstract string Path { get; protected set; }
    public abstract string NameWithoutExtension { get; }
    public abstract GDFileBase Parent { get; protected set; }
    public abstract GDFileAttributes Attribute { get; protected set; }

    public abstract void Dispose();
}