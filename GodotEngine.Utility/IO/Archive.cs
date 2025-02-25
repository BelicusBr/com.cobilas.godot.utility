using Godot;
using System;
using System.Text;
using System.Data;
using Cobilas.Collections;
using System.Globalization;

using IOFile = System.IO.File;

namespace Cobilas.GodotEngine.Utility.IO;
/// <summary>Represents a system file.</summary>
public class Archive : DataBase {
    private byte[] buffer = [];
    private bool discarded;
    /// <summary>Allows you to check the length of the allocated buffer.</summary>
    /// <returns>Returns the length of the allocated buffer.</returns>
    public long bufferLength => buffer.LongLength;
    /// <inheritdoc/>
    public override string Path => GetDataPath(this);
    /// <inheritdoc/>
    public override DataBase? Parent { get; protected set; }
    /// <inheritdoc/>
    public override ArchiveAttributes Attributes { get; protected set; }
    /// <inheritdoc/>
    public override string? Name { get; protected set; }
    /// <summary>Determines whether the object is a null representation.</summary>
    /// <returns>Returns <c>true</c> if the object is a null representation.</returns>
    public bool IsNull => this == @null;
    /// <summary>Allows you to get the system file extension.</summary>
    /// <returns>Returns a string containing the system file extension.</returns>
    public string ArchiveExtension => GodotPath.GetExtension(Path);
    /// <summary>Allows you to get the name of the system file without its extension.</summary>
    /// <returns>Returns a string with the name of the system file without its extension.</returns>
    public string NameWithoutExtension => GodotPath.GetFileNameWithoutExtension(Path);
    /// <summary>Allows you to generate a guid from the allocated buffer.</summary>
    /// <returns>Returns a guid generated from the allocated buffer.</returns>
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
            case Error.FileCantRead: throw new ReadOnlyException("Error.FileCantRead");
            case Error.FileNotFound: throw new System.IO.FileNotFoundException("File Not Found", Path);
        }
    }
    /// <summary>This method allows reading the system file loaded in this object.</summary>
    /// <returns>Returns a copy of the buffer loaded into this object.</returns>
    public byte[] Read() {
        CheckDisposal();
        byte[] bufferCopy = new byte[bufferLength];
        ArrayManipulation.CopyTo(buffer, bufferCopy);
        return bufferCopy;
    }
    /// <summary>This method allows reading the system file loaded in this object.</summary>
    /// <param name="encoding">The <seealso cref="Encoding"/> that will be used to convert the object buffer to a <seealso cref="string"/>.</param>
    /// <param name="text">Returns the buffer already converted to <seealso cref="string"/>.</param>
    /// <exception cref="ObjectDisposedException">Will occur when the method is called after the object has been disposed.</exception>
    public void Read(Encoding encoding, out string text) {
        CheckDisposal();
        text = encoding.GetString(buffer);
    }
    /// <inheritdoc cref="Read(Encoding, out string)"/>
    public void Read(out string text) => Read(Encoding.UTF8, out text);
    /// <param name="encoding">The <seealso cref="Encoding"/> that will be used to convert the object buffer to a <seealso cref="StringBuilder"/>.</param>
    /// <inheritdoc cref="Read(Encoding, out string)"/>
    /// <param name="builder">Returns the buffer already converted to <seealso cref="StringBuilder"/>.</param>
    public void Read(Encoding encoding, out StringBuilder builder) {
        CheckDisposal();
        builder = new(encoding.GetString(buffer));
    }
    /// <inheritdoc cref="Read(Encoding, out StringBuilder)"/>
    public void Read(out StringBuilder builder) => Read(Encoding.UTF8, out builder);
    /// <param name="encoding">The <seealso cref="Encoding"/> that will be used to convert the object buffer to a <seealso cref="char"/>[].</param>
    /// <inheritdoc cref="Read(Encoding, out string)"/>
    /// <param name="chars">Returns the buffer already converted to <seealso cref="char"/>[].</param>
    public void Read(Encoding encoding, out char[] chars) {
        CheckDisposal();
        chars = encoding.GetChars(buffer);
    }
    /// <inheritdoc cref="Read(Encoding, out char[])"/>
    public void Read(out char[] chars) => Read(Encoding.UTF8, out chars);
    /// <summary>The method allows replacing the object's current buffer with another one.</summary>
    /// <param name="newBuffer">The new buffer that will be allocated in this object.</param>
    /// <exception cref="ReadOnlyException">Will occur if the method is called on an object that is marked as read-only.</exception>
    /// <exception cref="ArgumentNullException">Occurs if the <paramref name="newBuffer"/> parameter is null.</exception>
    /// <exception cref="ObjectDisposedException">Will occur when the method is called after the object has been disposed.</exception>
    public void ReplaceBuffer(byte[]? newBuffer) {
        if (Attributes.HasFlag(ArchiveAttributes.ReadOnly)) throw new ReadOnlyException("Error.FileCantWrite");
        CheckDisposal();
        if (newBuffer is null) throw new ArgumentNullException(nameof(newBuffer));
        byte[] bufferCopy = new byte[newBuffer.LongLength];
        ArrayManipulation.CopyTo(newBuffer, bufferCopy);
        this.buffer = bufferCopy;
    }
    /// <summary>The method allows writing to the object's buffer.</summary>
    /// <param name="buffer">The value that will be written to the object buffer.</param>
    /// <param name="encoding">The Encoding that will be used to write to the object buffer.</param>
    /// <exception cref="ReadOnlyException">Will occur if the method is called on an object that is marked as read-only.</exception>
    /// <exception cref="ObjectDisposedException">Will occur when the method is called after the object has been disposed.</exception>
    public void Write(byte[] buffer, Encoding encoding) {
        if (Attributes.HasFlag(ArchiveAttributes.ReadOnly)) throw new ReadOnlyException("Error.FileCantWrite");
        CheckDisposal();

        Read(encoding, out StringBuilder builder);
        ReplaceBuffer(encoding.GetBytes(builder.Append(encoding.GetString(buffer)).ToString()));
    }
    /// <inheritdoc cref="Write(byte[], Encoding)"/>
    public void Write(byte[] buffer) => Write(buffer, Encoding.UTF8);
    /// <inheritdoc cref="Write(byte[], Encoding)"/>
    /// <param name="text">The value that will be written to the object buffer.</param>
    /// <param name="encoding"></param>
    public void Write(string text, Encoding encoding) {
        if (Attributes.HasFlag(ArchiveAttributes.ReadOnly)) throw new ReadOnlyException("Error.FileCantWrite");
        CheckDisposal();

        Read(encoding, out StringBuilder builder);
        ReplaceBuffer(encoding.GetBytes(builder.Append(text).ToString()));
    }
    /// <inheritdoc cref="Write(string, Encoding)"/>
    public void Write(string text) => Write(text, Encoding.UTF8);
    /// <inheritdoc cref="Write(byte[], Encoding)"/>
    /// <param name="chars">The value that will be written to the object buffer.</param>
    /// <param name="encoding"></param>
    public void Write(char[] chars, Encoding encoding) {
        if (Attributes.HasFlag(ArchiveAttributes.ReadOnly)) throw new ReadOnlyException("Error.FileCantWrite");
        CheckDisposal();

        Read(encoding, out StringBuilder builder);
        ReplaceBuffer(encoding.GetBytes(builder.Append(chars).ToString()));
    }
    /// <inheritdoc cref="Write(char[], Encoding)"/>
    public void Write(char[] chars) => Write(chars, Encoding.UTF8);
    /// <summary>Allows you to flush the object's buffer to the system file that this object represents.</summary>
    /// <exception cref="ReadOnlyException">Will occur if the method is called on an object that is marked as read-only.</exception>
    /// <exception cref="InvalidOperationException">It will occur when there is another invalid operation.</exception>
    /// <exception cref="ObjectDisposedException">Will occur when the method is called after the object has been disposed.</exception>
    /// <exception cref="System.IO.FileNotFoundException">It will occur when the path of the file that this object represents does not exist.</exception>
    public void Flush() {
        CheckDisposal();
        using File file = new();
        Error error;
        if (Attributes.HasFlag(ArchiveAttributes.ReadOnly)) error = Error.FileCantWrite;
        else error = file.Open(Path, File.ModeFlags.Write);

        switch (error) {
            case Error.Ok: file.StoreBuffer(buffer); break;
            case Error.FileCantWrite: throw new ReadOnlyException("Error.FileCantWrite");
            case Error.FileNotFound: throw new System.IO.FileNotFoundException("File Not Found", Path);
            default: throw new InvalidOperationException(error.ToString());
        }
    }
    /// <summary>The method allows you to refresh the buffer if the file is changed.</summary>
    /// <exception cref="InvalidOperationException">It will occur when there is another invalid operation.</exception>
    /// <exception cref="ObjectDisposedException">Will occur when the method is called after the object has been disposed.</exception>
    /// <exception cref="System.IO.FileNotFoundException">It will occur when the path of the file that this object represents does not exist.</exception>
    public void RefreshBuffer() {
        CheckDisposal();
        using File file = new();
        Error error;
        buffer = (error = file.Open(Path, File.ModeFlags.Read)) switch {
            Error.Ok => file.GetBuffer((long)file.GetLen()),
            Error.FileNotFound => throw new System.IO.FileNotFoundException("File Not Found", Path),
            _ => throw new InvalidOperationException(error.ToString()),
        };
    }
    /// <inheritdoc/>
    public override string ToString() => ToString("PP", CultureInfo.CurrentCulture);
    /// <inheritdoc/>
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
    /// <inheritdoc/>
    public override void Dispose() {
        CheckDisposal();
        Attributes = ArchiveAttributes.Null;
        ArrayManipulation.ClearArraySafe(ref buffer);
        discarded = true;
    }

    private void CheckDisposal() {
        if (discarded) throw new ObjectDisposedException(nameof(Archive));
    }
    /// <summary>Allows you to rename the system file.</summary>
    /// <param name="archive">The representation of the file that will be renamed.</param>
    /// <param name="newName">The new name that will be given to the object.</param>
    /// <returns>Returns <c>true</c> when the operation is performed successfully.</returns>
    /// <exception cref="System.InvalidOperationException">Occurs when the name of the new file has an invalid character.</exception>
    /// <exception cref="ReadOnlyException">Will occur if the method is called on an object that is marked as read-only.</exception>
    public static bool RenameArchive(Archive? archive, string? newName) {
        if (archive is null) throw new ArgumentNullException(nameof(archive));
        else if (archive == @null) throw new ArgumentNullException(nameof(archive));
        else if (newName is null) throw new ArgumentNullException(nameof(newName));
        else if (archive.Attributes.HasFlag(ArchiveAttributes.ReadOnly))
            throw new ReadOnlyException("Is ReadOnly");
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
