namespace Cobilas.GodotEngine.Utility.Runtime; 

/// <summary>Indicates the boot priority.</summary>
public enum Priority : byte  {
    /// <summary>Starts before everyone else.</summary>
    StartBefore = 0,
    /// <summary>Starts after everyone else.</summary>
    StartLater = 1
}