using System;

namespace Cobilas.GodotEngine.Utility.Runtime;
public readonly struct PluginManifest(
	string? plugInName, 
	string? plugInDescription, 
	string? plugInAuthor, 
	string? plugInVersion, 
	string? plugInScript)
	: IEquatable<PluginManifest> {
	private readonly string? plugInName = plugInName;
	private readonly string? plugInDescription = plugInDescription;
	private readonly string? plugInAuthor = plugInAuthor;
	private readonly string? plugInVersion = plugInVersion;
	private readonly string? plugInScript = plugInScript;

	private static PluginManifest _Empty => new(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

	public string? PlugInName => plugInName;
	public string? PlugInDescription => plugInDescription;
	public string? PlugInAuthor => plugInAuthor;
	public string? PlugInVersion => plugInVersion;
	public string? PlugInScript => plugInScript;

	public static PluginManifest Empty => _Empty;

	public bool Equals(PluginManifest other)
		=> other.plugInName == plugInName &&
		other.plugInAuthor == plugInAuthor &&
		other.plugInDescription == plugInDescription &&
		other.plugInVersion == plugInVersion &&
		other.plugInScript == plugInScript;

	public override bool Equals(object obj)
		=> obj is PluginManifest other && Equals(other);

	public override int GetHashCode() => base.GetHashCode();

	public static bool operator ==(PluginManifest A, PluginManifest B) => A.Equals(B);
	public static bool operator !=(PluginManifest A, PluginManifest B) => !A.Equals(B);
}
