using Godot;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Cobilas.Collections;
using Cobilas.GodotEngine.Utility.IO;
using Cobilas.GodotEngine.Utility.IO.Interfaces;

namespace Cobilas.GodotEngine.Utility.Runtime;
/// <summary>
/// An editor plugin that automatically deploys and builds custom plugins for the Godot engine.
/// Monitors the debug directory for changes and generates plugin configuration and script files.
/// </summary>
public class PlugInDeployer : EditorPlugin {

	private byte statusBuild = 0;
	private string[]? pluginList = null;
	private FolderInfo? debugFolder = null;
	private DateTime lastWriteTime = DateTime.MinValue;
	private const string debugPath = "res://.mono/temp/bin/Debug";

	private const string addonsPath = "res://addons";
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
	public override void _EnterTree() {
		statusBuild = 0;
		pluginList = null;
	}
	/// <inheritdoc/>
	public override void _Process(float delta) {

		if (Folder.Exists(debugPath))
			if (lastWriteTime != (lastWriteTime = (debugFolder ??= new(debugPath, true)).GetLastWriteTime))
				statusBuild = 0;

		switch (statusBuild) {
			case 0:
				statusBuild++;
				if (Deploy(out pluginList))
					_ = DotNetBuild(out _);
				break;
			case 1:
				if (pluginList is not null) {
					bool pluginEnabled = false;
					foreach (string item in pluginList)
						if (!GetEditorInterface().IsPluginEnabled(item))
							GetEditorInterface().SetPluginEnabled(item, pluginEnabled = true);
					if (!pluginEnabled) {
						++statusBuild;
						ArrayManipulation.ClearArraySafe(ref pluginList);
					}
				}
				break;
		}
	}

	private bool Deploy(out string[]? pluginList) {
		bool result = false;
		string[]? _pluginList = [];
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

			if (plugIn.InternalPlugIn) {
				EditorPlugin eplugin = type.Activator<EditorPlugin>();
				eplugin.Name = description.PlugInName;
				AddChild(eplugin);
				continue;
			}

			string plugInPath = GodotPath.Combine(addonsPath, description.PlugInName);

			ArrayManipulation.Add(description.PlugInName, ref _pluginList);
			if (!Folder.Exists(plugInPath)) {
				result = true;
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
		pluginList = _pluginList;
		return result;
	}
	/// <summary>Executes a .NET build command and captures the output.</summary>
	/// <param name="output">An array that will contain the output lines from the build command.</param>
	/// <returns>
	/// Returns 0 if the build was successful; otherwise, returns the exit code from the build process.
	/// </returns>
	/// <remarks>
	/// This method runs the "dotnet build" command and captures its standard output.
	/// The output is returned as an array of strings (boxed as objects).
	/// Successful execution returns 0, while any other exit code indicates failure.
	/// </remarks>
	public static int DotNetBuild(out object[] output) {
		string[] args1 = { "build" };
		Godot.Collections.Array _output = [];
		int ExecuteCode = OS.Execute("dotnet", args1, output:_output);

		switch (ExecuteCode) {
			case 0:
				GD.Print($"[{nameof(PlugInDeployer)}]");
				GD.Print("\tdotnet build executed successfully!");
				output = (object[])_output.Cast<object>();
				return 0;
			default:
				GD.Print($"[{nameof(PlugInDeployer)}]");
				GD.Print($"\tdotnet build failed!({nameof(ExecuteCode)}:{ExecuteCode})");
				output = (object[])_output.Cast<object>();
				return ExecuteCode;
		}
	}
}