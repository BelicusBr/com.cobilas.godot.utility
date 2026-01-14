using System;

namespace Cobilas.GodotEngine.Utility.Runtime;
/// <summary>Contains metadata for a Godot editor plugin.</summary>
/// <remarks>
/// This structure is used to define the plugin's name, description, author, version, and script filename.
/// </remarks>
/// <param name="plugInName">The name of the plugin.</param>
/// <param name="plugInDescription">A brief description of the plugin.</param>
/// <param name="plugInAuthor">The author of the plugin.</param>
/// <param name="plugInVersion">The version of the plugin.</param>
/// <param name="plugInScript">The filename of the plugin script.</param>
public readonly struct PlugInManifest(
	string? plugInName,
	string? plugInDescription,
	string? plugInAuthor,
	string? plugInVersion,
	string? plugInScript)
	: IEquatable<PlugInManifest> {
	private readonly string? plugInName = plugInName;
	private readonly string? plugInDescription = plugInDescription;
	private readonly string? plugInAuthor = plugInAuthor;
	private readonly string? plugInVersion = plugInVersion;
	private readonly string? plugInScript = plugInScript;

	private static PlugInManifest _Empty => new(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
	/// <summary>Gets the name of the plugin.</summary>
	/// <returns>The plugin name.</returns>
	public string? PlugInName => plugInName;
	/// <summary>Gets the description of the plugin.</summary>
	/// <returns>The plugin description.</returns>
	public string? PlugInDescription => plugInDescription;
	/// <summary>Gets the author of the plugin.</summary>
	/// <returns>The plugin author.</returns>
	public string? PlugInAuthor => plugInAuthor;
	/// <summary>Gets the version of the plugin.</summary>
	/// <returns>The plugin version.</returns>
	public string? PlugInVersion => plugInVersion;
	/// <summary>Gets the script filename of the plugin.</summary>
	/// <returns>The plugin script filename.</returns>
	public string? PlugInScript => plugInScript;
	/// <summary>Gets an empty <see cref="PlugInManifest"/>.</summary>
	/// <returns>An empty plugin manifest.</returns>
	public static PlugInManifest Empty => _Empty;
	/// <inheritdoc/>
	public bool Equals(PlugInManifest other)
		=> other.plugInName == plugInName &&
		other.plugInAuthor == plugInAuthor &&
		other.plugInDescription == plugInDescription &&
		other.plugInVersion == plugInVersion &&
		other.plugInScript == plugInScript;
	/// <inheritdoc/>
	public override bool Equals(object obj)
		=> obj is PlugInManifest other && Equals(other);
	/// <inheritdoc/>
	public override int GetHashCode() => base.GetHashCode();
	/// <summary>
	/// Determines whether two <see cref="PlugInManifest"/> instances are equal.
	/// </summary>
	/// <param name="A">The first manifest to compare.</param>
	/// <param name="B">The second manifest to compare.</param>
	/// <returns>True if the manifests are equal; otherwise, false.</returns>
	public static bool operator ==(PlugInManifest A, PlugInManifest B) => A.Equals(B);
	/// <summary>
	/// Determines whether two <see cref="PlugInManifest"/> instances are not equal.
	/// </summary>
	/// <param name="A">The first manifest to compare.</param>
	/// <param name="B">The second manifest to compare.</param>
	/// <returns>True if the manifests are not equal; otherwise, false.</returns>
	public static bool operator !=(PlugInManifest A, PlugInManifest B) => !A.Equals(B);
}