using System.IO;
using Microsoft.Build.Framework;

namespace Cobilas.GodotEditor.Utility.Tasks;

public class GodotPluginTask : UtilityTask {
	private const string ProjectPathIsNull = "The 'ProjectPath' property is null!";
	private const string InitCopyTask = "Running the GamePlugInDeployer class copy task!";
	private const string ProjectFileNotExist = "The Godot project file could not be found!";
	private const string PlugInCodeFileExist = "The PlugInDeployer file was not created because it already exists!";
	private const string PlugInNotDefined = "The GamePlugInDeployer class is already defined in the Godot project file!";
	private const string PlugInCFGFileExist = "The PlugInDeployer.config file was not created because it already exists!";
	private const string CopyingTaskCompleted = "The process of copying the GamePlugInDeployer class was completed successfully!";

	private const string PlugIn = "res://addons/PlugInDeployer/plugin.cfg";
	private const string PlugInGD = "res://addons/PlugInDeployerStart/plugin.cfg";

	[Required]
	public string? ProjectPath { get; set; }

	public override bool Execute() {
		if (ProjectPath is null)
			return LogError(ProjectPathIsNull);
		try {

			_ = LogMessage(InitCopyTask);
			_ = LogMessage(ProjectPath);

			string projectPath = Path.Combine(ProjectPath, "project.godot");
			string dir_addons = Path.Combine(ProjectPath, "addons");
			string dir_path = Path.Combine(ProjectPath, "addons\\PlugInDeployer");
			string file_cfg = Path.Combine(dir_path, "plugin.cfg");
			string file_cs = Path.Combine(dir_path, "GamePlugInDeployer.cs");

			string dir_gd_path = Path.Combine(ProjectPath, "addons\\PlugInDeployerStart");
			string file_gd_cfg = Path.Combine(dir_gd_path, "plugin.cfg");
			string file_gd = Path.Combine(dir_gd_path, "GamePlugInDeployer.gd");

			string old_pluginPool = string.Empty;

			if (!File.Exists(projectPath))
				return LogMessage(ProjectFileNotExist);

			using (StreamReader reader = new(projectPath))
				while (!reader.EndOfStream) {
					string line = reader.ReadLine().Trim();
					if (line.Contains("enabled=PoolStringArray")) {
						old_pluginPool = line;
						break;
					}
				}

			if (old_pluginPool.Contains(PlugInGD)) return LogMessage(PlugInNotDefined);

			if (!Directory.Exists(dir_addons))
				Directory.CreateDirectory(dir_addons);
			if (!Directory.Exists(dir_path))
				Directory.CreateDirectory(dir_path);

			if (!File.Exists(file_cfg)) {
				using StreamWriter writer = new(File.Create(file_cfg));
				writer.Write(ContainerCodeTask.PlugInDeployer_CFG);
			} else _ = LogMessage(PlugInCFGFileExist);
			if (!File.Exists(file_cs)) {
				using StreamWriter writer = new(File.Create(file_cs));
				writer.Write(ContainerCodeTask.PlugInDeployerCode);
			} else _ = LogMessage(PlugInCodeFileExist);

			if (!Directory.Exists(dir_gd_path))
				Directory.CreateDirectory(dir_gd_path);

			if (!File.Exists(file_gd_cfg)) {
				using StreamWriter writer = new(File.Create(file_gd_cfg));
				writer.Write(ContainerCodeTask.PlugInDeployer_GD_CFG);
			} else _ = LogMessage(PlugInCFGFileExist);
			if (!File.Exists(file_gd)) {
				using StreamWriter writer = new(File.Create(file_gd));
				writer.Write(ContainerCodeTask.PlugInDeployerGDCode);
			} else _ = LogMessage(PlugInCodeFileExist);

			using (StreamWriter writer = new(projectPath, true)) {
				writer.AutoFlush = true;
				writer.WriteLine();
				writer.WriteLine("[editor_plugins]");
				writer.WriteLine();
				if (old_pluginPool == string.Empty)
					writer.WriteLine($"enabled=PoolStringArray( \"{PlugInGD}\" )");
				else {
					string cont = old_pluginPool.Replace("enabled=PoolStringArray(", string.Empty).Trim(')');
					if (string.IsNullOrWhiteSpace(cont))
						writer.WriteLine($"{old_pluginPool.TrimEnd(')', ' ')} \"{PlugInGD}\" )");
					else writer.WriteLine($"{old_pluginPool.TrimEnd(')')}, \"{PlugInGD}\" )");
				}
			}
			return LogMessage(CopyingTaskCompleted);

		} catch (System.Exception ex) {
			return LogError(ex);
		}
	}
}
