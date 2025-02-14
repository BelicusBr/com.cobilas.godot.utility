using Cobilas.Collections;
using Godot;
using System;
using System.Text;

namespace Cobilas.GodotEngine.Utility.IO.Test;

public class Archive : DataBase {
    private byte[] buffer;
    private bool discarded;

    public long bufferL => buffer.LongLength;
    public override string? Path { get; protected set; }
    public override DataBase? Parent { get; protected set; }
    public override ArchiveAttributes Attributes { get; protected set; }
    public override string Name => GodotPath.GetFileName(Path ?? string.Empty);
    public string ArchiveExtension => GodotPath.GetExtension(Path ?? string.Empty);
    public string NameWithoutExtension => GodotPath.GetFileNameWithoutExtension(Path ?? string.Empty);

    private static readonly Archive @null = new(null, string.Empty, ArchiveAttributes.Null);

    public static Archive Null => @null;

    public Archive(DataBase? parent, string? path, ArchiveAttributes attributes) : base(parent, path, attributes) {
        using File file = new();
        Error error = file.Open(Path, File.ModeFlags.Read);
        switch (error) {
            case Error.Ok: buffer = file.GetBuffer((long)file.GetLen()); break;
            case Error.FileCantRead: throw new System.InvalidOperationException("Error.FileCantRead");
            case Error.FileNotFound: throw new System.IO.FileNotFoundException("File Not Found", Path);
        }
    }

    public byte[] Read() {
        CheckDisposal();
        return buffer;
    }

    public void Read(Encoding encoding, out string text) {
        CheckDisposal();
        text = encoding.GetString(buffer);
    }

    public void Read(out string text) => Read(Encoding.UTF8, out text);

    public void Read(Encoding encoding, out StringBuilder text) {
        CheckDisposal();
        text = new(encoding.GetString(buffer));
    }

    public void Read(out StringBuilder text) => Read(Encoding.UTF8, out text);

    public void Read(Encoding encoding, out char[] chars) {
        CheckDisposal();
        chars = encoding.GetChars(buffer);
    }

    public void Read(out char[] chars) => Read(Encoding.UTF8, out chars);

    public void ReplaceBuffer(byte[] buffer) {
        if (Attributes.HasFlag(ArchiveAttributes.ReadOnly)) throw new System.InvalidOperationException("Error.FileCantWrite");
        CheckDisposal();
        this.buffer = buffer;
    }

    public void Write(byte[] buffer, Encoding encoding) {
        if (Attributes.HasFlag(ArchiveAttributes.ReadOnly)) throw new System.InvalidOperationException("Error.FileCantWrite");
        CheckDisposal();

        Read(encoding, out StringBuilder text);
        StringBuilder builder = text;
        builder.Append(encoding.GetString(buffer));
        ReplaceBuffer(encoding.GetBytes(builder.ToString()));
    }

    public void Write(byte[] buffer) => Write(buffer, Encoding.UTF8);

    public void Write(string buffer, Encoding encoding) {
        if (Attributes.HasFlag(ArchiveAttributes.ReadOnly)) throw new System.InvalidOperationException("Error.FileCantWrite");
        CheckDisposal();

        Read(encoding, out StringBuilder text);
        StringBuilder builder = text;
        builder.Append(buffer);
        ReplaceBuffer(encoding.GetBytes(builder.ToString()));
    }

    public void Write(string buffer) => Write(buffer, Encoding.UTF8);

    public void Write(char[] buffer, Encoding encoding) {
        if (Attributes.HasFlag(ArchiveAttributes.ReadOnly)) throw new System.InvalidOperationException("Error.FileCantWrite");
        CheckDisposal();

        Read(encoding, out StringBuilder text);
        StringBuilder builder = text;
        builder.Append(buffer);
        ReplaceBuffer(encoding.GetBytes(builder.ToString()));
    }

    public void Write(char[] buffer) => Write(buffer, Encoding.UTF8);

    public void Flush() {
        CheckDisposal();
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

    public override void Dispose() {
        CheckDisposal();
        discarded = true;
        Attributes = ArchiveAttributes.Null;
        ArrayManipulation.ClearArraySafe(ref buffer);
    }

    private void CheckDisposal() {
        if (discarded)  throw new ObjectDisposedException(nameof(Archive));
    }
}
