using System;
using Cobilas.Collections;

namespace Cobilas.GodotEngine.Utility.IO;

public sealed class FolderNode : DataBase
{
    public override string? Name { get; protected set; }

    public override string Path => GetDataPath(this);

    public override DataBase? Parent { get; protected set; }
    public override IDataInfo DataInfo { get; protected set; }
    public override ArchiveAttributes Attributes { get; protected set; }

    public FolderNode(DataBase? parent, string? dataName, ArchiveAttributes attributes) : base(parent, dataName, attributes)
    {
        DataInfo = new FolderInfo(Path);
    }

    public override void Dispose()
    {
        throw new NotImplementedException();
    }

    public override string ToString(string format, IFormatProvider formatProvider)
    {
        throw new NotImplementedException();
    }

    internal static DataBase GetFolderNodeRoot(in string? path, DataBase root) {
        FolderNode? node = null;
        FolderNode? num1 = null;
        string[]? names = GetNames(path);
        if (ArrayManipulation.ArrayLength(names) <= 1) return root;
        for (int I = 0; I < ArrayManipulation.ArrayLength(names); I++) {
            if (I == 0)
                node = num1 = new(root, names![I], ArchiveAttributes.Directory | ArchiveAttributes.ReadOnly);
            else
                num1 = new(num1, names![I], ArchiveAttributes.Directory | ArchiveAttributes.ReadOnly);
        }
        return node!;
    }

    private static string[]? GetNames(in string? path) {
        if (path is null) return [];
        string root = GodotPath.GetPathRoot(path);
        string sPath = path.Replace(root, string.Empty);
        return ArrayManipulation.Add(sPath.Split('/', '\\'), new string[] { root });
    }
}
