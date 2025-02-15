using Godot;
using System;
using System.Text;
using Cobilas.Collections;
using System.Globalization;

using IOFile = System.IO.File;

namespace Cobilas.GodotEngine.Utility.IO.Test;

public class Archive : DataBase {
    private byte[] buffer = [];
    private bool discarded;

    public long bufferLength => buffer.LongLength;
    /// <inheritdoc/>
    public override string Path => GetDataPath(this);
    /// <inheritdoc/>
    public override DataBase? Parent { get; protected set; }
    /// <inheritdoc/>
    public override ArchiveAttributes Attributes { get; protected set; }
    /// <inheritdoc/>
    public override string? Name { get; protected set; }
    public string ArchiveExtension => GodotPath.GetExtension(Path);
    public string NameWithoutExtension => GodotPath.GetFileNameWithoutExtension(Path);
    public Guid GetGuid {
        get {
            byte[] bytes = new byte[16];
            for (long I = 0; I < bufferLength; I++)
                bytes[I % 16] ^= buffer[I];
            return new(bytes);
        }
    }

    private static readonly Archive @null = new(null, string.Empty, ArchiveAttributes.Null);
    /// <summary>A null representation of the <seealso cref="Archive"/> object.</summary>
    /// <returns>Returns a null representation of the <seealso cref="Archive"/> object.</returns>
    public static Archive Null => @null;
    /// <summary>Creates a new instance of this object.</summary>
    public Archive(DataBase? parent, string? dataName, ArchiveAttributes attributes) : base(parent, dataName, attributes) {
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

    public override string ToString() => ToString("PP", CultureInfo.CurrentCulture);
    
    public override string ToString(string format, IFormatProvider formatProvider)
            => format switch {
            "PP" => string.Format(formatProvider, "path:'{0}'", Path),
            "PA" => string.Format(formatProvider, "attributes:'{0}'", Attributes),
            "PDN" => string.Format(formatProvider, "name:'{0}'", Name),
            "PDN2" => string.Format(formatProvider, "name:'{0}'", NameWithoutExtension),
            "PBL" => string.Format(formatProvider, "buffer_length:'{0}'", bufferLength),
            "PPN" => string.Format(formatProvider, "parent_name:'{0}'", Parent?.Name),
            _ => throw new FormatException($"The format '{format}' is not recognized!"),
        };

    public override void Dispose() {
        CheckDisposal();
        discarded = true;
        Attributes = ArchiveAttributes.Null;
        ArrayManipulation.ClearArraySafe(ref buffer);
    }

    private void CheckDisposal() {
        if (discarded)  throw new ObjectDisposedException(nameof(Archive));
    }


    public static bool RenameArchive(Archive? archive, string? newName) {
        if (archive is null) throw new ArgumentNullException(nameof(archive));
        else if (archive == @null) throw new ArgumentNullException(nameof(archive));
        else if (newName is null) throw new ArgumentNullException(nameof(newName));
        else if (archive.Attributes.HasFlag(ArchiveAttributes.ReadOnly))
            throw new System.InvalidOperationException("Is ReadOnly");
        else if (GodotPath.IsInvalidFileName(newName, out char ic))
            throw new System.InvalidOperationException($"The name '{newName}' has the invalid character '{ic.EscapeSequenceToString()}'.");
        
        if (archive.Name == newName || !IOFile.Exists(GodotPath.GlobalizePath(archive.Path))) return false;
        string oldName = GodotPath.GlobalizePath(archive.Path);
        archive.Name = newName;

        IOFile.Copy(oldName, GodotPath.GlobalizePath(archive.Path));
        IOFile.Delete(oldName);
        
        return true;
    }
}
