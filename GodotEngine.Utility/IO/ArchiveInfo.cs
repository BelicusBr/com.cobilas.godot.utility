using Godot;
using System;
using System.IO;
using System.Globalization;
using Cobilas.GodotEngine.Utility.IO.Interfaces;

using GDFile = Godot.File;
using IOFile = System.IO.File;

namespace Cobilas.GodotEngine.Utility.IO;

public sealed class ArchiveInfo(string? path) : IArchiveInfo {
    private string? path = TreatPath(path);
	private bool disposedValue;

    public string FullName => path!;
    public string Name => GodotPath.GetFileName(FullName);
    public IDataInfo Parent => Archive.GetParent(this);
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

	public IStream Open(FileAccess access, StreamType type) 
		=> type switch {
			StreamType.IOStream => GodotPath.GetPathRoot(path ?? throw new ArgumentNullException(nameof(path))) switch {
				GodotPath.ResPath when GDFeature.HasDebug => new ArchiveStream(GodotPath.GlobalizePath(path), access),
				GodotPath.ResPath => new GodotArchiveStream(path, access),
				_ => new ArchiveStream(GodotPath.GlobalizePath(path), access),
			},
			StreamType.GDStream => new GodotArchiveStream(path, access),
			_ => GodotPath.GetPathRoot(path ?? throw new ArgumentNullException(nameof(path))) switch {
				GodotPath.ResPath => new GodotArchiveStream(path, access),
				GodotPath.UserPath => new GodotArchiveStream(path, access),
				_ => new ArchiveStream(GodotPath.GlobalizePath(path), access),
			}
		};

	public IStream Open(FileAccess access)
		=> Open(access, StreamType.Auto);

	public bool CopyTo(string? destinationPath, bool overwrite)	{
		DisposedException();
		if (destinationPath is null) throw new ArgumentNullException(nameof(destinationPath));
		else if (GDFeature.HasRelease)
			if (GodotPath.GetPathRoot(destinationPath) == GodotPath.ResPath)
				return false;
		destinationPath = GodotPath.GlobalizePath(destinationPath);
		string npath = GodotPath.Combine(destinationPath, Name);
		if (!overwrite)
			if (IOFile.Exists(npath))
				return false;
		if (IsInternal) {
			using IStream stream = Open(FileAccess.Read);
			using FileStream file = IOFile.Open(destinationPath, FileMode.OpenOrCreate);
			file.Write(stream.Read());
		} else IOFile.Copy(FullName, destinationPath, overwrite);
		return true;
	}

	public bool CopyTo(string? destinationPath) => CopyTo(destinationPath, overwrite:false);

	public bool Move(string? destinationPath)	{
		DisposedException();
		if (destinationPath is null) throw new ArgumentNullException(nameof(destinationPath));
		if (IsInternal) return false;
		string fpath = GodotPath.GlobalizePath(FullName),
			   dPath = GodotPath.GlobalizePath(destinationPath);
		IOFile.Move(fpath, GodotPath.Combine(dPath, Name));
		path = GodotPath.Combine(destinationPath, Name);
		return true;
	}

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
		return (error = file.Open(path, GetModeFlags(path))) switch {
			Error.Ok => path,
			Error.DoesNotExist => throw new FileNotFoundException("The file was not found!", path),
			_ => throw new IOException($"[{error}]{path}"),
		};
	}

	private static GDFile.ModeFlags GetModeFlags(string path)
		=> GodotPath.GetPathRoot(path) switch {
            GodotPath.ResPath when GDFeature.HasRelease => GDFile.ModeFlags.Read,
            _ => GDFile.ModeFlags.ReadWrite,
        };

	private void DisposedException() {
        if (disposedValue)
            throw new ObjectDisposedException(nameof(ArchiveInfo));
	}
}
