using Godot;
using System.Text;

namespace Cobilas.GodotEngine.Utility.IO.Test;

public class Archive : DataBase {
    public override string? Path { get; protected set; }
    public override DataBase? Parent { get; protected set; }
    public override ArchiveAttributes Attributes { get; protected set; }

    private static readonly Archive @null = new(null, string.Empty, ArchiveAttributes.Null);

    public static Archive Null => @null;

    public Archive(DataBase? parent, string? path, ArchiveAttributes attributes) : base(parent, path, attributes) {}

    public string Read() {
        using File file = new();
        Error error = file.Open(Path, File.ModeFlags.Read);
        return error switch {
            Error.Ok => file.GetAsText(),
            Error.FileCantRead => throw new System.InvalidOperationException("Error.FileCantRead"),
            Error.FileNotFound => throw new System.IO.FileNotFoundException("File Not Found", Path),
            _ => throw new System.InvalidOperationException(),
        };
    }

    public void Write(byte[] buffer) {
        using File file = new();
        Error error;
        if (Attributes.HasFlag(ArchiveAttributes.ReadOnly)) error = Error.FileCantWrite;
        else error = file.Open(Path, File.ModeFlags.Write);

        switch (error) {
            case Error.Ok: file.StoreBuffer(buffer); break;
            case Error.FileCantWrite: throw new System.InvalidOperationException("Error.FileCantWrite");
            case Error.FileNotFound: throw new System.IO.FileNotFoundException("File Not Found", Path);
            default: throw new System.InvalidOperationException(error.ToString());
        }
    }

    public void Write(string text) => Write(Encoding.UTF8.GetBytes(text));
}
