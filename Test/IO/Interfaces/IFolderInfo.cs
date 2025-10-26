using System.Collections.Generic;

namespace Cobilas.Test.IO.Interfaces;
public interface IFolderInfo : IDataInfo, IEnumerable<IDataInfo> {
    long DataCount { get; }
    IFolderInfo[] GetFolders();
    IArchiveInfo[] GetArchives();
    IFolderInfo CreateFolder(string? name);
    IArchiveInfo CreateArchive(string? name);
    bool RenameFolder(string? oldName, string? newName);
    bool RenameArchive(string? oldName, string? newName);
    bool RemoveFolder(string? name);
    bool RemoveArchive(string? name);
    bool Existe(string? name);
    bool MoveTo(string? name, string? path);
    void Refresh();
}
