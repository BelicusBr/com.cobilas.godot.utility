using Godot;
using System;
using System.IO;
using System.Globalization;
using Cobilas.GodotEngine.Utility.IO.Interfaces;

using GDFile = Godot.File;
using IOFile = System.IO.File;

namespace Cobilas.GodotEngine.Utility.IO;
/// <summary>Represents information about an archive file and provides operations for working with archive contents.</summary>
/// <remarks>
/// This class implements the <see cref="IArchiveInfo"/> interface and provides
/// a unified way to work with files in both Godot's virtual file system and the system file system.
/// </remarks>
/// <param name="path">The path to the archive file. If null, an exception will be thrown.</param>
/// <exception cref="ArgumentNullException">Thrown when path is null.</exception>
/// <exception cref="FileNotFoundException">Thrown when the file does not exist.</exception>
/// <exception cref="IOException">Thrown when an I/O error occurs while opening the file.</exception>
public sealed class ArchiveInfo(string? path) : IArchiveInfo {
	private string? path = TreatPath(path);
	private bool disposedValue;
	/// <inheritdoc/>
	public string FullName => path!;
	/// <inheritdoc/>
	public string Name => GodotPath.GetFileName(FullName);
	/// <inheritdoc/>
	public IDataInfo Parent => Archive.GetParent(this);
	/// <inheritdoc/>
	public string ArchiveExtension => GodotPath.GetExtension(FullName);
	/// <inheritdoc/>
	public ArchiveAttributes Attributes => Archive.GetArchiveAttributes(this);
	/// <inheritdoc/>
	public string NameWithoutExtension => GodotPath.GetFileNameWithoutExtension(FullName);
	/// <inheritdoc/>
	public bool IsInternal =>
		GodotPath.GetPathRoot(FullName) switch {
			GodotPath.ResPath when GDFeature.HasStandalone => true,
			_ => false,
		};
	/// <inheritdoc/>
	public bool IsGodotRoot => GodotPath.IsGodotRoot(FullName);
	/// <inheritdoc/>
	public DateTime GetCreationTime => IsInternal ? DateTime.MinValue : IOFile.GetCreationTime(GodotPath.GlobalizePath(FullName));
	/// <inheritdoc/>
	public DateTime GetCreationTimeUtc => IsInternal ? DateTime.MinValue : IOFile.GetCreationTimeUtc(GodotPath.GlobalizePath(FullName));
	/// <inheritdoc/>
	public DateTime GetLastAccessTime => IsInternal ? DateTime.MinValue : IOFile.GetLastAccessTime(GodotPath.GlobalizePath(FullName));
	/// <inheritdoc/>
	public DateTime GetLastAccessTimeUtc => IsInternal ? DateTime.MinValue : IOFile.GetLastAccessTimeUtc(GodotPath.GlobalizePath(FullName));
	/// <inheritdoc/>
	public DateTime GetLastWriteTime => IsInternal ? DateTime.MinValue : IOFile.GetLastAccessTime(GodotPath.GlobalizePath(FullName));
	/// <inheritdoc/>
	public DateTime GetLastWriteTimeUtc => IsInternal ? DateTime.MinValue : IOFile.GetLastAccessTimeUtc(GodotPath.GlobalizePath(FullName));
	/// <inheritdoc/>
	public IStream Open(FileAccess access, StreamType type)
		=> type switch {
			StreamType.IOStream => GodotPath.GetPathRoot(path ?? throw new ArgumentNullException(nameof(path))) switch {
				GodotPath.ResPath when GDFeature.HasEditor => new ArchiveStream(GodotPath.GlobalizePath(path), access),
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
	/// <inheritdoc/>
	public IStream Open(FileAccess access)
		=> Open(access, StreamType.Auto);
	/// <inheritdoc/>
	public bool CopyTo(string? destinationPath, bool overwrite) {
		DisposedException();
		if (destinationPath is null) throw new ArgumentNullException(nameof(destinationPath));
		else if (GDFeature.HasStandalone)
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
		}
		else IOFile.Copy(FullName, destinationPath, overwrite);
		return true;
	}
	/// <inheritdoc/>
	public bool CopyTo(string? destinationPath) => CopyTo(destinationPath, overwrite: false);
	/// <inheritdoc/>
	public bool Move(string? destinationPath) {
		DisposedException();
		if (destinationPath is null) throw new ArgumentNullException(nameof(destinationPath));
		if (IsInternal) return false;
		string fpath = GodotPath.GlobalizePath(FullName),
			   dPath = GodotPath.GlobalizePath(destinationPath);
		IOFile.Move(fpath, GodotPath.Combine(dPath, Name));
		path = GodotPath.Combine(destinationPath, Name);
		return true;
	}
	/// <summary>Returns a string representation of the archive information using the current culture.</summary>
	/// <returns>A string containing the archive information.</returns>
	public override string ToString()
		=> ToString(string.Empty, CultureInfo.CurrentCulture);
	/// <summary>Returns a formatted string representation of the archive information using the current culture.</summary>
	/// <param name="format">The format string. Use placeholders: \\%ph for path, \\%np for name, \\%itl for internal flag, \\%att for attributes.</param>
	/// <returns>A formatted string containing the archive information.</returns>
	public string ToString(string format)
		=> ToString(format, CultureInfo.CurrentCulture);
	/// <summary>
	/// Returns a formatted string representation of the archive information using the 
	/// specified format and format provider.
	/// </summary>
	/// <remarks>
	/// The default format when empty is: "path:'{0}'\r\ninternal:'{2}'\r\nattributes:'{3}'"
	/// <br>Placeholders in the format string are replaced as follows:</br>
	/// <br>- <c>\\%ph:</c> Full path</br>
	/// <br>- <c>\\%np:</c> File name</br>
	/// <br>- <c>\\%itl:</c> Internal flag</br>
	/// <br>- <c>\\%att:</c> Archive attributes</br>
	/// </remarks>
	/// <param name="format">The format string. Use placeholders: \\%ph for path, \\%np for name, \\%itl for internal flag, \\%att for attributes.</param>
	/// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
	/// <returns>A formatted string containing the archive information.</returns>
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
			GodotPath.ResPath when GDFeature.HasStandalone => GDFile.ModeFlags.Read,
			_ => GDFile.ModeFlags.ReadWrite,
		};

	private void DisposedException() {
		if (disposedValue)
			throw new ObjectDisposedException(nameof(ArchiveInfo));
	}
}