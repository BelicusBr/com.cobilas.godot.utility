using Godot;
using System;
using System.Linq;
using Cobilas.Collections;
using Cobilas.GodotEngine.Utility.IO;
using Cobilas.GodotEngine.Utility.IO.Interfaces;

using SYSDictionary = System.Collections.Generic.Dictionary<int, System.Type[]?>;

namespace Cobilas.GodotEngine.Utility.Runtime;
[PlugInDeployer]
public class AutoLoadScript : EditorPlugin {
	private byte statusBuild = 0;
	private string[]? autoLoadList = null;
	private const string runTimePath = "res://Godot.Runtime";

	private const string AutoLoadScriptCode =
@"
#pragma warning disable IDE0130
namespace Godot.Runtime {{
#pragma warning restore IDE0130
	/// <inheritdoc/>
	public class Auto_{0} : {1} {{ }}
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
			case 0:
				GD.Print(nameof(AutoLoadScript));
				statusBuild++;
				bool build = false;
				FolderInfo datas;
				if (!Folder.Exists(runTimePath))
					datas = (FolderInfo)Folder.Create(runTimePath);
				else datas = new(runTimePath, true);
				using (datas)
					foreach (var item1 in GetAutoLoadScript())
						if (item1.Value is not null)
							foreach (Type item2 in item1.Value) {
								if (datas.Existe($"Auto_{item2.Name}.cs")) {
									if (!ProjectSettings.HasSetting($"autoload/Auto_{item2.Name}"))
										ArrayManipulation.Add(item2.Name, ref autoLoadList);
									continue;
								}
								build = true;
								using ArchiveInfo archive = (ArchiveInfo)datas.CreateArchive($"Auto_{item2.Name}.cs");
								using IStream stream = archive.Open(System.IO.FileAccess.Write, StreamType.GDStream);
								stream.AutoFlush = true;
								stream.Write(string.Format(AutoLoadScriptCode, item2.Name, item2.FullName));
								ArrayManipulation.Add(item2.Name, ref autoLoadList);
							}
				if (build) 
					_ = PlugInDeployer.DotNetBuild(out _);
				break;
			case 1:
				bool addAutoload = false;
				if (autoLoadList is not null)
					foreach (string item in autoLoadList)
						if (Archive.Exists($"{runTimePath}/Auto_{item}.cs"))
							if (!ProjectSettings.HasSetting($"autoload/Auto_{item}")) {
								AddAutoloadSingleton($"Auto_{item}", $"{runTimePath}/Auto_{item}.cs");
								addAutoload = true;
							}
				if (!addAutoload) {
					statusBuild++;
					ArrayManipulation.ClearArraySafe(ref autoLoadList);
				}
				break;
		}
	}

	private static SYSDictionary GetAutoLoadScript() {
		SYSDictionary result = [];
		foreach (Type item in TypeUtilitarian.GetTypes()) {
			AutoLoadScriptAttribute? auto = item.GetAttribute<AutoLoadScriptAttribute>();
			if (auto is null) continue;
			if (result.ContainsKey(auto.Priority))
				result[auto.Priority] = ArrayManipulation.Add(item, result[auto.Priority]);
			else result.Add(auto.Priority, new Type[] { item });
		}
		result.OrderBy((v) => v.Key);
		return result;
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
