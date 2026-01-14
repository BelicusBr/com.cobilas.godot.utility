using Godot;
using System;
using System.IO;
using System.Reflection;
using Cobilas.GodotEngine.Utility.IO;
using Cobilas.GodotEngine.Utility.IO.Interfaces;

namespace Cobilas.GodotEngine.Utility.Runtime;

/// <summary>
/// An editor plugin that automatically deploys and builds custom plugins for the Godot engine.
/// Monitors the debug directory for changes and generates plugin configuration and script files.
/// </summary>
public class PlugInDeployer : EditorPlugin {

	private DateTime time;
	private FolderInfo? dataInfo = null;
	private readonly string[] args = { "build" };

	private const string addonsPath = "res://addons";
	private const string debugPath = "res://.mono/temp/bin/Debug";
	private const BindingFlags staticMethodFlag = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
	private const string plugInCFG =
@"[plugin]

name=""{0}""
description=""{1}""
author=""{2}""
version=""{3}""
script=""{4}""
";
	private const string PlugInCode =
@"#if TOOLS
#pragma warning disable IDE0130
namespace Godot.PlugIn {{
#pragma warning restore IDE0130
	/// <inheritdoc/>
	[Tool]
	public class PlugIn_{0} : {1} {{ }}
}}
#endif
";
	/// <inheritdoc/>
	public override void DisablePlugin() {
		if (dataInfo is null) return;
		dataInfo.Dispose();
		dataInfo = null;
	}
	/// <inheritdoc/>
	public override void _Process(float delta) {
		if (dataInfo is null) {
			if (!Folder.Exists(debugPath)) return;
			dataInfo = new(debugPath, false);
			time = DateTime.MinValue;
		}
		if (time == (time = dataInfo.GetLastWriteTime)) return;

		Deploy();

		int ExecuteCode = OS.Execute("dotnet", args);
		switch (ExecuteCode) {
			case 1:
				GD.Print($"[{nameof(PlugInDeployer)}]");
				GD.Print("\tdotnet build executed successfully!");
				break;
			default:
				GD.Print($"[{nameof(PlugInDeployer)}]");
				GD.Print($"\tdotnet build failed!({nameof(ExecuteCode)}:{ExecuteCode})");
				break;
		}
	}

	private void Deploy() {
		if (!Folder.Exists(addonsPath))
			Folder.Create(addonsPath).Dispose();
		Type[] types = TypeUtilitarian.GetTypes();
		foreach (Type type in types) {
			PlugInDeployerAttribute? plugIn = type.GetAttribute<PlugInDeployerAttribute>(true);
			if (plugIn is null) continue;
			PlugInManifest description = PlugInManifest.Empty;
			foreach (MethodInfo? item in type.GetMethods(staticMethodFlag)) {
				PlugInDeployerDescriptionAttribute plugIn1 = item.GetCustomAttribute<PlugInDeployerDescriptionAttribute>();
				if (plugIn1 is null) continue;
				description = (PlugInManifest)item.Invoke(null, null);
				break;
			}

			if (description == PlugInManifest.Empty)
				description = new(type.Name, string.Empty, string.Empty, "1.0", $"PlugIn_{type.Name}.cs");

			ExceptionMessages.ThrowIfNullOrEmpty(description.PlugInName);
			ExceptionMessages.ThrowIfNullOrEmpty(description.PlugInScript);

			string plugInPath = GodotPath.Combine(addonsPath, description.PlugInName);

			if (!Folder.Exists(plugInPath)) {
				using FolderInfo folder = (FolderInfo)Folder.Create(plugInPath);
				using ArchiveInfo archive = (ArchiveInfo)folder.CreateArchive("plugin.cfg");
				using IArchiveStream stream = (IArchiveStream)archive.Open(FileAccess.Write, StreamType.IOStream);
				stream.Write(string.Format(plugInCFG,
					description.PlugInName,
					description.PlugInDescription,
					description.PlugInAuthor,
					description.PlugInVersion,
					description.PlugInScript
					));
				using ArchiveInfo archive2 = (ArchiveInfo)folder.CreateArchive(description.PlugInScript);
				using IArchiveStream stream2 = (IArchiveStream)archive2.Open(FileAccess.Write, StreamType.IOStream);
				stream2.Write(string.Format(PlugInCode, type.Name, type.FullName));
			}
		}
	}
}