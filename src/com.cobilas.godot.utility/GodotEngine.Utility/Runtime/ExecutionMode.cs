namespace Cobilas.GodotEngine.Utility.Runtime;
/// <summary>Represents the editor mode states.</summary>
public enum ExecutionMode : byte {
    /// <summary>When the editor is running the project.</summary>
    PlayerMode = 0,
    /// <summary>When the editor is not running the project.</summary>
    EditorMode = 1,
}
