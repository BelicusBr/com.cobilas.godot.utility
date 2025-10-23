using System;

namespace Cobilas.GodotEngine.Utility.IO;
/// <summary>Provides information about data properties and timestamps.</summary>
public interface IDataInfo {
    /// <summary>Gets a value indicating whether the data is internal.</summary>
    /// <remarks>This property determines whether <seealso cref="IDataInfo"/> is embedded into the compiled godot project.</remarks>
    /// <returns>true if the data is internal; otherwise, false.</returns>
    bool IsInternal { get; }
    /// <summary>Gets a value indicating whether this data is at the root of the Godot project.</summary>
    /// <remarks>
    /// The root of a Godot project is typically the directory containing the project.godot file.
    /// This property helps identify resources that are directly in the project's root folder.
    /// </remarks>
    /// <returns><c>true</c> if this data is at the root of the Godot project; otherwise, <c>false</c>.</returns>
    bool IsGodotRoot { get; }
    /// <summary>Gets the creation time of the data.</summary>
    /// <returns>A <see cref="DateTime"/> representing the creation time.</returns>
    DateTime GetCreationTime { get; }
    /// <summary>Gets the creation time in UTC of the data.</summary>
    /// <returns>A <see cref="DateTime"/> representing the UTC creation time.</returns>
    DateTime GetCreationTimeUtc { get; }
    /// <summary>Gets the last access time of the data.</summary>
    /// <returns>A <see cref="DateTime"/> representing the last access time.</returns>
    DateTime GetLastAccessTime { get; }
    /// <summary>Gets the last access time in UTC of the data.</summary>
    /// <returns>A <see cref="DateTime"/> representing the UTC last access time.</returns>
    DateTime GetLastAccessTimeUtc { get; }
    /// <summary>Gets the last write time of the data.</summary>
    /// <returns>A <see cref="DateTime"/> representing the last write time.</returns>
    DateTime GetLastWriteTime { get; }
    /// <summary>Gets the last write time in UTC of the data.</summary>
    /// <returns>A <see cref="DateTime"/> representing the UTC last write time.</returns>
    DateTime GetLastWriteTimeUtc { get; }
}
