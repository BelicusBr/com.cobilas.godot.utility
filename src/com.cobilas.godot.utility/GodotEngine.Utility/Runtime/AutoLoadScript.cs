using Godot;
using System;
using System.Linq;
using Cobilas.Collections;
using System.Collections.Generic;
using Cobilas.GodotEngine.Utility.IO;
using Cobilas.GodotEngine.Utility.IO.Interfaces;

using SYSDictionary = System.Collections.Generic.Dictionary<int, System.Type[]?>;

namespace Cobilas.GodotEngine.Utility.Runtime;
/// <summary>An editor plugin that automatically generates and manages autoload scripts for the Godot engine.</summary>
/// <remarks>
/// This plugin monitors the debug directory for changes and automatically creates C# scripts
/// for types marked with <see cref="AutoLoadScriptAttribute"/>. These scripts are then registered
/// as autoload singletons in the Godot project.
/// </remarks>
/// <seealso cref="PlugInDeployerAttribute"/>
/// <seealso cref="AutoLoadScriptAttribute"/>
[PlugInDeployer(true)]
public class AutoLoadScript : EditorPlugin {
	private byte statusBuild = 0;
	private string[]? autoLoadList = null;
	private FolderInfo? debugFolder = null;
	private DateTime lastWriteTime = DateTime.MinValue;
	private const string runTimePath = "res://Godot.Runtime";
	private const string debugPath = "res://.mono/temp/bin/Debug";

	private const string AutoLoadScriptCode =
@"
#pragma warning disable IDE0130
namespace Godot.Runtime {{
#pragma warning restore IDE0130
	/// <inheritdoc/>
	internal class {0} : {1} {{ }}
}}
";
	/// <inheritdoc/>
	public override void _EnterTree() {
		statusBuild = 0;
		autoLoadList = null;
	}
	/// <inheritdoc/>
	public override void _Process(float delta) {
		switch (statusBuild) {
			default:
				if (Folder.Exists(debugPath)) {
					debugFolder ??= new(debugPath, true);
					if (lastWriteTime != (lastWriteTime = debugFolder.GetLastWriteTime))
						statusBuild = 0;
				}
				break;
			case 0:
				GD.Print(nameof(AutoLoadScript));
				statusBuild++;
				FolderInfo datas;
				if (!Folder.Exists(runTimePath))
					datas = (FolderInfo)Folder.Create(runTimePath);
				else datas = new(runTimePath, true);

				using (datas)
					foreach (var item1 in GetAutoLoadScript())
						if (item1.Value is not null)
							foreach (Type item2 in item1.Value) {
								AutoLoadScriptAttribute auto = item2.GetAttribute<AutoLoadScriptAttribute>();
								string className = auto.AutoLoadName ?? $"Auto_{item2.Name}";
								if (datas.Existe($"{className}.cs")) {
									ArrayManipulation.Add(className, ref autoLoadList);
									continue;
								}
								using ArchiveInfo archive = (ArchiveInfo)datas.CreateArchive($"{className}.cs");
								using IStream stream = archive.Open(System.IO.FileAccess.Write, StreamType.GDStream);
								stream.AutoFlush = true;
								stream.Write(string.Format(AutoLoadScriptCode, item2.Name, item2.FullName));
								ArrayManipulation.Add(className, ref autoLoadList);
							}
				break;
			case 1:
				statusBuild++;
				if (autoLoadList is not null)
					foreach (string item in autoLoadList)
						if (ProjectSettings.HasSetting($"autoload/{item}"))
							RemoveAutoloadSingleton(item);
				break;
			case 2:
				bool addAutoload = false;
				if (autoLoadList is not null)
					foreach (string item in autoLoadList)
						if (Archive.Exists($"{runTimePath}/{item}.cs"))
							if (!ProjectSettings.HasSetting($"autoload/{item}")) {
								AddAutoloadSingleton(item, $"{runTimePath}/{item}.cs");
								addAutoload = true;
							}
				if (!addAutoload) {
					statusBuild++;
					ArrayManipulation.ClearArraySafe(ref autoLoadList);
				}
				break;
		}
	}
	/// <summary>
	/// Retrieves all types marked with <see cref="AutoLoadScriptAttribute"/> and groups them by priority.
	/// </summary>
	/// <returns>
	/// An array of key-value pairs where the key is the priority and the value is an array of types
	/// with that priority, sorted in ascending order by priority.
	/// </returns>
	/// <remarks>
	/// This method scans all loaded types for the <see cref="AutoLoadScriptAttribute"/> and organizes
	/// them into a dictionary keyed by the priority specified in the attribute. The result is then
	/// sorted by priority and returned as an array of key-value pairs.
	/// </remarks>
	/// <seealso cref="AutoLoadScriptAttribute"/>
	/// <seealso cref="TypeUtilitarian.GetTypes(string?)"/>
	private static KeyValuePair<int, Type[]?>[] GetAutoLoadScript() {
		SYSDictionary result = [];
		foreach (Type item in TypeUtilitarian.GetTypes()) {
			AutoLoadScriptAttribute? auto = item.GetAttribute<AutoLoadScriptAttribute>();
			if (auto is null) continue;
			if (result.ContainsKey(auto.Priority))
				result[auto.Priority] = ArrayManipulation.Add(item, result[auto.Priority]);
			else result.Add(auto.Priority, new Type[] { item });
		}
		return result.OrderBy((v) => v.Key).ToArray();
	}
	[PlugInDeployerDescription]
	private static PlugInManifest GetPlugInManifest()
		=> new(
			nameof(AutoLoadScript),
			string.Empty,
			"Cobilas",
			"1.0",
			$"PlugIn_{nameof(AutoLoadScript)}.cs"
			);
}