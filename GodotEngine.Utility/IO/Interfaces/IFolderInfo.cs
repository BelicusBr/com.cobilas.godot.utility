using System.Collections.Generic;

namespace Cobilas.GodotEngine.Utility.IO.Interfaces;
public interface IFolderInfo : IDataInfo, IEnumerable<IDataInfo> {
    long DataCount { get; }
    IFolderInfo[] GetFolders();
    IArchiveInfo[] GetArchives();
    IDataInfo CreateFolder(string? name);
    IDataInfo CreateArchive(string? name);
    bool RenameFolder(string? oldName, string? newName);
    bool RenameArchive(string? oldName, string? newName);
    bool RemoveFolder(string? name);
    bool RemoveArchive(string? name);
    bool Existe(string? name);
    bool MoveTo(string? name, string? path);
    bool Move(string? destinationPath);
	void Refresh();
}
