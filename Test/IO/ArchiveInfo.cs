using Godot;
using System;
using System.IO;
using System.Globalization;
using Cobilas.Test.IO.Interfaces;
using Cobilas.GodotEngine.Utility;
using Cobilas.GodotEngine.Utility.IO;

using GDFile = Godot.File;
using IOFile = System.IO.File;
using TDSIDataInfo = Cobilas.Test.IO.Interfaces.IDataInfo;

namespace Cobilas.Test.IO;

public sealed class ArchiveInfo(string? path) : IArchiveInfo {
    private string? path = TreatPath(path);
	private bool disposedValue;

    public string FullName => path!;
    public string Name => GodotPath.GetFileName(FullName);
    public TDSIDataInfo Parent => Archive.GetParent(this);
    public string ArchiveExtension => GodotPath.GetExtension(FullName);
    public ArchiveAttributes Attributes => Archive.GetArchiveAttributes(this);
    public string NameWithoutExtension => GodotPath.GetFileNameWithoutExtension(FullName);

    public bool IsInternal =>
        GodotPath.GetPathRoot(FullName) switch {
            GodotPath.ResPath when GDFeature.HasRelease => true,
            _ => false,
        };
    public bool IsGodotRoot => GodotPath.IsGodotRoot(FullName);
    public DateTime GetCreationTime => IsInternal ? DateTime.MinValue : IOFile.GetCreationTime(GodotPath.GlobalizePath(FullName));
    public DateTime GetCreationTimeUtc => IsInternal ? DateTime.MinValue : IOFile.GetCreationTimeUtc(GodotPath.GlobalizePath(FullName));
    public DateTime GetLastAccessTime => IsInternal ? DateTime.MinValue : IOFile.GetLastAccessTime(GodotPath.GlobalizePath(FullName));
    public DateTime GetLastAccessTimeUtc => IsInternal ? DateTime.MinValue : IOFile.GetLastAccessTimeUtc(GodotPath.GlobalizePath(FullName));
    public DateTime GetLastWriteTime => IsInternal ? DateTime.MinValue : IOFile.GetLastAccessTime(GodotPath.GlobalizePath(FullName));
    public DateTime GetLastWriteTimeUtc => IsInternal ? DateTime.MinValue : IOFile.GetLastAccessTimeUtc(GodotPath.GlobalizePath(FullName));

    public override string ToString()
        => ToString(string.Empty, CultureInfo.CurrentCulture);

    public string ToString(string format)
		=> ToString(format, CultureInfo.CurrentCulture);

	public string ToString(string format, IFormatProvider formatProvider) 
        => Archive.builder.Clear()
			.AppendFormat(formatProvider,
			string.IsNullOrEmpty(format) ? "path:'{0}'\r\ninternal:'{2}'\r\nattributes:'{3}'" :
			format.Replace("\\%ph", "{0}").Replace("\\%np", "{1}").Replace("\\%itl", "{2}").Replace("\\%att", "{3}"), 
            path, Name, IsInternal, Attributes).ToString();

	public IStream Open(string? path, FileAccess access) {
        DisposedException();
		return GodotPath.GetPathRoot(path ?? throw new ArgumentNullException(nameof(path))) switch {
			GodotPath.ResPath => GDFeature.HasDebug ? new ArchiveStream(GodotPath.GlobalizePath(path), access) : new GodotArchiveStream(path, access),
			_ => new ArchiveStream(GodotPath.GlobalizePath(path), access),
		};
	}

	/// <inheritdoc/>
	public void Dispose() {
        DisposedException();
		disposedValue = true;
		path = null;
	}

    private static string? TreatPath(string? path) {
        if (path is null) throw new ArgumentNullException(nameof(path));
        using GDFile file = new();
        Error error;
		return (error = file.Open(path, GDFile.ModeFlags.ReadWrite)) switch {
			Error.Ok => path,
			Error.DoesNotExist => throw new FileNotFoundException("The file was not found!", path),
			_ => throw new IOException($"[{error}]{path}"),
		};
	}

	private void DisposedException() {
        if (disposedValue)
            throw new ObjectDisposedException(nameof(ArchiveInfo));
	}
}
