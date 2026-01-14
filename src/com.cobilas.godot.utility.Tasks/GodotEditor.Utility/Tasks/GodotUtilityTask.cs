using System.IO;
using System.Text;
using Microsoft.Build.Framework;

namespace Cobilas.GodotEditor.Utility.Tasks;

public class GodotUtilityTask : UtilityTask {

	private const string ProjectPathIsNull = "The 'ProjectPath' property is null!";
	private const string InitCopyTask = "Running the GameRuntime class copy task!";
	private const string ProjectFileNotExist = "The Godot project file could not be found!";
	private const string GameRuntimeAlreadyExists = "The GameRuntime.cs file already exists in the godot project!";
	private const string GameRuntimeNotDefined = "The GameRuntime class is already defined in the Godot project file!";
	private const string CopyingTaskCompleted = "The process of copying the GameRuntime class was completed successfully!";

	private const string gameRuntime = "GameRuntime=\"*res://Godot.Runtime/GameRuntime.cs\"";

	[Required]
	public string? ProjectPath { get; set; }

	public override bool Execute() {
		if (ProjectPath is null)
			return LogError(ProjectPathIsNull);
		try {
			_ = LogMessage(InitCopyTask);

			string projectPath = Path.Combine(ProjectPath, "project.godot");
			string rt_dir = Path.Combine(ProjectPath, "Godot.Runtime");
			string rt_file = Path.Combine(rt_dir, "GameRuntime.cs");
			if (!File.Exists(projectPath))
				return LogMessage(ProjectFileNotExist);

			using (StreamReader reader = new(projectPath)) {
				while (!reader.EndOfStream)
					if (reader.ReadLine().Trim() == gameRuntime)
						return LogMessage(GameRuntimeNotDefined);
			}
			
			if (!Directory.Exists(rt_dir))
				Directory.CreateDirectory(rt_dir);
			
			if (File.Exists(rt_file))
				return LogMessage(GameRuntimeAlreadyExists);

			using FileStream stream = File.OpenWrite(rt_file);
			byte[] bytes = Encoding.UTF8.GetBytes(ContainerCodeTask.GodotUtilityTaskCode);
			stream.Write(bytes, 0, bytes.Length);

			using StreamWriter writer = new(projectPath, true);
			writer.AutoFlush = true;
			writer.WriteLine();
			writer.WriteLine("[autoload]");
			writer.WriteLine();
			writer.WriteLine(gameRuntime);

			return LogMessage(CopyingTaskCompleted);

		} catch (System.Exception ex) {
			return LogError(ex);
		}
	}
}
