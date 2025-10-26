using System;
using System.IO;
using System.Text;
using Cobilas.GodotEngine.Utility.IO;
using Cobilas.Test.IO.Interfaces;
using TDSIDataInfo = Cobilas.Test.IO.Interfaces.IDataInfo;

namespace Cobilas.Test.IO;

public static class Archive {

    [ThreadStatic] internal readonly static StringBuilder builder = new();

    public static ArchiveAttributes GetArchiveAttributes(TDSIDataInfo? data) {
        if (data is null) throw new ArgumentNullException(nameof(data));
        else if (data.IsInternal) return ArchiveAttributes.Archive | ArchiveAttributes.ReadOnly | ArchiveAttributes.Hidden;
        return (ArchiveAttributes)File.GetAttributes(GodotPath.GlobalizePath(data.FullName));
    }

    public static ArchiveAttributes GetArchiveAttributes(string? path) {
        using ArchiveInfo ark = new(path);
        return GetArchiveAttributes(ark);
    }

	public static TDSIDataInfo GetParent(IArchiveInfo? data) => Folder.GetParent(data);
}
