using Godot;
using System;
using System.IO;
using System.Reflection;
using Cobilas.GodotEngine.Utility.IO;
using Cobilas.GodotEngine.Utility.IO.Interfaces;

namespace Cobilas.GodotEngine.Utility.Runtime;

public class PlugInDeployer : EditorPlugin {

	private const string addonsPath = "res://addons";
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
	public class PlugIn_{0} : {1} {{ }}
}}
#endif
";

	/// <inheritdoc/>
	public override void EnablePlugin() {
		//addons
		if (!Folder.Exists(addonsPath))
			Folder.Create(addonsPath).Dispose();
		Type[] types = TypeUtilitarian.GetTypes();
		foreach (Type type in types) {
			PlugInDeployerAttribute? plugIn = type.GetAttribute<PlugInDeployerAttribute>(true);
			if (plugIn is null) continue;
			PluginManifest description = PluginManifest.Empty;
			foreach (MethodInfo? item in type.GetMethods(BindingFlags.Static)) {
				PlugInDeployerDescriptionAttribute plugIn1 = item.GetCustomAttribute<PlugInDeployerDescriptionAttribute>();
				if (plugIn1 is null) continue;
				description = (PluginManifest)item.Invoke(null, null);
				break;
			}

			if (description == PluginManifest.Empty)
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
				using IArchiveStream stream2 = (IArchiveStream)archive.Open(FileAccess.Write, StreamType.IOStream);
				stream.Write(string.Format(PlugInCode, type.Name, type.FullName));
			}
		}
	}
}
