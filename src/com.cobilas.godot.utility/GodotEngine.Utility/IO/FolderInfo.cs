using Godot;
using System;
using System.IO;
using System.Data;
using System.Collections;
using Cobilas.Collections;
using System.Globalization;
using System.Collections.Generic;
using Cobilas.GodotEngine.Utility.IO.Interfaces;

using IOFile = System.IO.File;
using GDFolder = Godot.Directory;
using IOFolder = System.IO.Directory;

namespace Cobilas.GodotEngine.Utility.IO;
/// <summary>Represents information about a folder/directory and provides operations for folder management.</summary>
/// <remarks>
/// This class implements the <see cref="IFolderInfo"/> interface and provides
/// a unified way to work with folders in both Godot's virtual file system and the system file system.
/// It maintains internal caches of folder contents for efficient enumeration and manipulation.
/// </remarks>
/// <param name="path">The path to the folder. If null, an exception will be thrown.</param>
/// <param name="skipHidden">Whether to skip hidden files and folders in the enumeration.</param>
/// <exception cref="ArgumentNullException">Thrown when path is null.</exception>
/// <exception cref="DirectoryNotFoundException">Thrown when the folder does not exist.</exception>
/// <exception cref="IOException">Thrown when an I/O error occurs while opening the folder.</exception>
public sealed class FolderInfo(string? path, bool skipHidden) : IFolderInfo {
	private bool disposedValue;
	private string? path = TreatPath(path);
	private readonly bool skipHidden = skipHidden;
	private KeyValuePair<string, string>[][] datas = GetDatas(path, skipHidden);
	/// <inheritdoc/>
	public string FullName => path!;
	/// <inheritdoc/>
	public IDataInfo Parent => Folder.GetParent(this);
	/// <inheritdoc/>
	public string Name => GodotPath.GetFileName(FullName);
	/// <inheritdoc/>
	public long DataCount => ArrayManipulation.ArrayLongLength(datas);
	/// <inheritdoc/>
	public ArchiveAttributes Attributes => Archive.GetArchiveAttributes(this);
	/// <inheritdoc/>
	public bool IsInternal =>
		GodotPath.GetPathRoot(FullName) switch {
			GodotPath.ResPath when GDFeature.HasStandalone => true,
			_ => false,
		};
	/// <inheritdoc/>
	public bool IsGodotRoot => GodotPath.IsGodotRoot(FullName);
	/// <inheritdoc/>
	public DateTime GetCreationTime => IsInternal ? DateTime.MinValue : IOFolder.GetCreationTime(GodotPath.GlobalizePath(FullName));
	/// <inheritdoc/>
	public DateTime GetCreationTimeUtc => IsInternal ? DateTime.MinValue : IOFolder.GetCreationTimeUtc(GodotPath.GlobalizePath(FullName));
	/// <inheritdoc/>
	public DateTime GetLastAccessTime => IsInternal ? DateTime.MinValue : IOFolder.GetLastAccessTime(GodotPath.GlobalizePath(FullName));
	/// <inheritdoc/>
	public DateTime GetLastAccessTimeUtc => IsInternal ? DateTime.MinValue : IOFolder.GetLastAccessTimeUtc(GodotPath.GlobalizePath(FullName));
	/// <inheritdoc/>
	public DateTime GetLastWriteTime => IsInternal ? DateTime.MinValue : IOFolder.GetLastWriteTime(GodotPath.GlobalizePath(FullName));
	/// <inheritdoc/>
	public DateTime GetLastWriteTimeUtc => IsInternal ? DateTime.MinValue : IOFolder.GetLastWriteTimeUtc(GodotPath.GlobalizePath(FullName));

	private KeyValuePair<string, string>[] _Folder { get => datas[0]; set => datas[0] = value; }
	private KeyValuePair<string, string>[] _Archives { get => datas[1]; set => datas[1] = value; }
	/// <inheritdoc/>
	public IDataInfo CreateArchive(string? name) {
		_ReadOnlyException();
		if (name is null) throw new ArgumentNullException(nameof(name));
		else if (IsInternal) return DataNull.Null;
		string path = GodotPath.Combine(GodotPath.GlobalizePath(FullName), name);
		IOFile.Create(path).Dispose();
		_Archives = ArrayManipulation.Add(new KeyValuePair<string, string>("ark", name), _Archives) ?? [];
		return new ArchiveInfo(path);
	}
	/// <inheritdoc/>
	public IDataInfo CreateFolder(string? name) {
		_ReadOnlyException();
		if (name is null) throw new ArgumentNullException(nameof(name));
		else if (IsInternal) return DataNull.Null;
		string path = GodotPath.Combine(GodotPath.GlobalizePath(FullName), name);
		IOFolder.CreateDirectory(path);
		_Folder = ArrayManipulation.Add(new KeyValuePair<string, string>("dir", name), _Folder) ?? [];
		return new FolderInfo(path, skipHidden);
	}
	/// <inheritdoc/>
	public bool Existe(string? name) {
		DisposedException();
		if (name is null) throw new ArgumentNullException(nameof(name));
		for (byte I = 0; I < 2; I++)
			foreach (KeyValuePair<string, string> item in datas[I])
				if (item.Value == name)
					return true;
		return false;
	}
	/// <inheritdoc/>
	public IArchiveInfo[] GetArchives() {
		DisposedException();
		IArchiveInfo[] result = new IArchiveInfo[ArrayManipulation.ArrayLongLength(_Archives)];
		for (long I = 0; I < result.Length; I++)
			result[I] = new ArchiveInfo(GodotPath.Combine(FullName, _Archives[I].Value));
		return result;
	}
	/// <inheritdoc/>
	public IFolderInfo[] GetFolders() {
		DisposedException();
		IFolderInfo[] result = new IFolderInfo[ArrayManipulation.ArrayLongLength(_Folder)];
		for (long I = 0; I < result.Length; I++)
			result[I] = new FolderInfo(GodotPath.Combine(FullName, _Folder[I].Value), skipHidden);
		return result;
	}
	/// <inheritdoc/>
	public bool RemoveArchive(string? name) {
		DisposedException();
		if (name is null) throw new ArgumentNullException(nameof(name));
		else if (!IsInternal)
			for (long I = 0; I < ArrayManipulation.ArrayLongLength(_Archives); I++)
				if (_Archives[I].Value == name) {
					_Archives = ArrayManipulation.Remove(I, _Archives);
					string path = GodotPath.Combine(GodotPath.GlobalizePath(FullName), name);
					IOFile.Delete(path);
					return true;
				}
		return false;
	}
	/// <inheritdoc/>
	public bool RemoveFolder(string? name) {
		DisposedException();
		if (name is null) throw new ArgumentNullException(nameof(name));
		else if (!IsInternal)
			for (long I = 0; I < ArrayManipulation.ArrayLongLength(_Folder); I++)
				if (_Folder[I].Value == name) {
					_Folder = ArrayManipulation.Remove(I, _Folder);
					string path = GodotPath.Combine(GodotPath.GlobalizePath(FullName), name);
					IOFolder.Delete(path, true);
					return true;
				}
		return false;
	}
	/// <inheritdoc/>
	public bool RenameArchive(string? oldName, string? newName) {
		if (oldName is null) throw new ArgumentNullException(nameof(oldName));
		else if (newName is null) throw new ArgumentNullException(nameof(newName));
		else if (!IsInternal)
			for (long I = 0; I < ArrayManipulation.ArrayLongLength(_Archives); I++)
				if (_Archives[I].Value == oldName) {
					_Archives = ArrayManipulation.Remove(I, _Archives);
					string oldPath = GodotPath.Combine(GodotPath.GlobalizePath(FullName), oldName);
					string newPath = GodotPath.Combine(GodotPath.GlobalizePath(FullName), newName);
					string BackUpOfFileToReplace = GodotPath.Combine(GodotPath.GlobalizePath(FullName), $"{newName}.bac");
					IOFile.Replace(oldPath, newPath, BackUpOfFileToReplace);
					_Archives[I] = new(_Archives[I].Key, newName);
					return true;
				}
		return false;
	}
	/// <inheritdoc/>
	public bool RenameFolder(string? oldName, string? newName) {
		if (oldName is null) throw new ArgumentNullException(nameof(oldName));
		else if (newName is null) throw new ArgumentNullException(nameof(newName));
		else if (!IsInternal)
			for (long I = 0; I < ArrayManipulation.ArrayLongLength(_Folder); I++)
				if (_Folder[I].Value == oldName) {
					_Folder = ArrayManipulation.Remove(I, _Folder);
					string oldPath = GodotPath.Combine(GodotPath.GlobalizePath(FullName), oldName);
					string newPath = GodotPath.Combine(GodotPath.GlobalizePath(FullName), newName);
					IOFile.Move(oldPath, newPath);
					_Folder[I] = new(_Folder[I].Key, newName);
					return true;
				}
		return false;
	}
	/// <inheritdoc/>
	public bool MoveTo(string? name, string? path) {
		if (name is null) throw new ArgumentNullException(nameof(name));
		else if (path is null) throw new ArgumentNullException(nameof(path));
		else if (!IsInternal)
			for (long J = 0; J < ArrayManipulation.ArrayLongLength(_Folder); J++)
				if (_Folder[J].Value == name) {
					string oldPath = GodotPath.Combine(GodotPath.GlobalizePath(FullName), name);
					string newPath = GodotPath.Combine(GodotPath.GlobalizePath(path), name);
					if (oldPath == newPath) return false;
					IOFolder.Move(oldPath, newPath);
					_Folder = ArrayManipulation.Remove(J, _Folder);
					return true;
				}
		return false;
	}
	/// <inheritdoc/>
	public bool Move(string? destinationPath) {
		if (destinationPath is null) throw new ArgumentNullException(nameof(destinationPath));
		else if (!IsInternal) {
			string oldPath = GodotPath.GlobalizePath(FullName),
				   newPath = GodotPath.Combine(GodotPath.GlobalizePath(destinationPath), Name);
			if (oldPath == newPath) return false;
			IOFolder.Move(oldPath, newPath);
			path = newPath;
			return true;
		}
		return false;
	}
	/// <summary>Returns a string representation of the folder information using the current culture.</summary>
	/// <returns>A string containing the folder information.</returns>
	public override string ToString()
		=> ToString(string.Empty, CultureInfo.CurrentCulture);
	/// <summary>Returns a formatted string representation of the folder information using the current culture.</summary>
	/// <param name="format">The format string. Use placeholders: 
	/// <br>- <c>\\%ph</c> for path</br>
	/// <br>- <c>\\%np</c> for name</br>
	/// <br>- <c>\\%itl</c> for internal flag</br>
	/// <br>- <c>\\%att</c> for attributes</br>
	/// <br>- <c>\\%dtc</c> for data count.</br>
	/// </param>
	/// <returns>A formatted string containing the folder information.</returns>
	public string ToString(string format)
		=> ToString(format, CultureInfo.CurrentCulture);
	/// <summary>
	/// Returns a formatted string representation of the folder information using 
	/// the specified format and format provider.
	/// </summary>
	/// <remarks>
	/// The default format when empty is: "path:'{0}'\r\ninternal:'{2}'\r\nattributes:'{3}'\r\ndata count:'{4}'"
	/// <br>Placeholders in the format string are replaced as follows:</br>
	/// <br>- <c>\\%ph:</c> Full path</br>
	/// <br>- <c>\\%np:</c> Folder name</br>
	/// <br>- <c>\\%itl:</c> Internal flag</br>
	/// <br>- <c>\\%att:</c> Archive attributes</br>
	/// <br>- <c>\\%dtc:</c> Data count (number of items in folder)</br>
	/// </remarks>
	/// <param name="format">The format string. Use placeholders: 
	/// <br>- <c>\\%ph</c> for path</br>
	/// <br>- <c>\\%np</c> for name</br>
	/// <br>- <c>\\%itl</c> for internal flag</br>
	/// <br>- <c>\\%att</c> for attributes</br>
	/// <br>- <c>\\%dtc</c> for data count.</br>
	/// </param>
	/// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
	/// <returns>A formatted string containing the folder information.</returns>
	public string ToString(string format, IFormatProvider formatProvider)
		=> Archive.builder.Clear()
			.AppendFormat(
				formatProvider,
				string.IsNullOrEmpty(format) ? "path:'{0}'\r\ninternal:'{2}'\r\nattributes:'{3}'\r\ndata count:'{4}'" :
				format.Replace("\\%ph", "{0}").Replace("\\%np", "{1}").Replace("\\%itl", "{2}")
					  .Replace("\\%att", "{3}").Replace("\\%dtc", "{4}"),
				path, Name, IsInternal, Attributes, DataCount
			).ToString();
	/// <inheritdoc/>
	public void Refresh() {
		ArrayManipulation.ClearArraySafe(datas);
		datas = GetDatas(FullName, skipHidden);
	}
	/// <inheritdoc/>
	public IEnumerator<IDataInfo> GetEnumerator() {
		for (byte I = 0; I < 2; I++)
			foreach (KeyValuePair<string, string> item in datas[I]) {
				string path = GodotPath.Combine(FullName, item.Value);
				if (item.Key == "dir") yield return new FolderInfo(path, skipHidden);
				else yield return new ArchiveInfo(path);
			}
	}
	/// <inheritdoc/>
	public void Dispose() {
		DisposedException();
		disposedValue = true;
		path = null;
	}

	IEnumerator IEnumerable.GetEnumerator()
		=> ((IEnumerable<IDataInfo>)this).GetEnumerator();

	private void DisposedException() {
		if (disposedValue)
			throw new ObjectDisposedException(nameof(ArchiveInfo));
	}

	private void _ReadOnlyException() {
		DisposedException();
		if (IsInternal) throw new ReadOnlyException("Data classified as internal cannot be modified!");
	}

	private static KeyValuePair<string, string>[][] GetDatas(string? path, bool skipHidden) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		using GDFolder folder = new();
		Error error;
		switch (error = folder.Open(path)) {
			case Error.Ok:
				folder.ListDirBegin(true, skipHidden);
				string dataName = folder.GetNext();
				KeyValuePair<string, string>[]? dir = [];
				KeyValuePair<string, string>[]? ark = [];
				while (!string.IsNullOrEmpty(dataName)) {
					if (folder.CurrentIsDir()) ArrayManipulation.Add(new KeyValuePair<string, string>("dir", dataName), ref dir);
					else ArrayManipulation.Add(new KeyValuePair<string, string>("ark", dataName), ref ark);
					dataName = folder.GetNext();
				}
				folder.ListDirEnd();
				return new KeyValuePair<string, string>[][] { dir ?? [], ark ?? [] } ?? new KeyValuePair<string, string>[0][];
			case Error.DoesNotExist:
				throw new DirectoryNotFoundException($"The folder '{path}' was not found!");
			default:
				throw new IOException($"[{error}]{path}");
		}
	}

	private static string? TreatPath(string? path) {
		if (path is null) throw new ArgumentNullException(nameof(path));
		using GDFolder folder = new();
		Error error;
		return (error = folder.Open(path)) switch {
			Error.Ok => path,
			Error.DoesNotExist => throw new DirectoryNotFoundException($"The folder '{path}' was not found!"),
			_ => throw new IOException($"[{error}]{path}"),
		};
	}
}