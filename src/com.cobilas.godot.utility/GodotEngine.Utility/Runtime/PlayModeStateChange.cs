namespace Cobilas.GodotEngine.Utility.Runtime;
/// <summary>Enumeration specifying a change in the Editor's play mode state.</summary>
public enum PlayModeStateChange : byte {
    /// <summary>Occurs during the next update of the Editor application if it is in edit mode and was previously in play mode.</summary>
    EnteredEditMode = 0,
    /// <summary>Occurs when exiting edit mode, before the Editor is in play mode.</summary>
    ExitingEditMode = 1,
    /// <summary>Occurs during the next update of the Editor application if it is in play mode and was previously in edit mode.</summary>
    EnteredPlayMode = 2,
    /// <summary>Occurs when exiting play mode, before the Editor is in edit mode.</summary>
    ExitingPlayMode = 3
}
