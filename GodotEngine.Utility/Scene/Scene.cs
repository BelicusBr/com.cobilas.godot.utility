using System.IO;

namespace Cobilas.GodotEngine.Utility.Scene; 

public struct Scene {
    public int Index { get; private set; }
    public string ScenePath { get; private set; }
    public readonly string Name => Path.GetFileName(ScenePath);
    public readonly string NameWithoutExtension => Path.GetFileNameWithoutExtension(ScenePath);

    public Scene(string scenePath, int index) {
        Index = index;
        ScenePath = scenePath;
    }
}