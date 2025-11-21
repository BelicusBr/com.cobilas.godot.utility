using System.IO;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Cobilas.GodotEditor.Utility.Tasks;

public class GodotUtilityTask : Task {

	private readonly string gameRuntime = "GameRuntime=\"*res://Godot.Runtime/GameRuntime.cs\"";
	private readonly string code =
@"using Cobilas.GodotEngine.Utility.Runtime;

namespace Godot.Runtime {
	public class GameRuntime : RunTimeInitialization { }
}";

	[Required]
	public string? ProjectPath { get; set; }

	public override bool Execute() {

		if (ProjectPath is null) {
			Log.LogError("The 'ProjectPath' property is null!");
			return true;
		}
		Log.LogMessage(MessageImportance.High, "Running the GameRuntime class copy task!");

		try {
			string projectPath = Path.Combine(ProjectPath, "project.godot");
			string rt_dir = Path.Combine(ProjectPath, "Godot.Runtime");
			string rt_file = Path.Combine(rt_dir, "GameRuntime.cs");
			if (!File.Exists(projectPath)) return false;
			using (StreamReader reader = new(projectPath)) {
				while (!reader.EndOfStream)
					if (reader.ReadLine().Trim() == gameRuntime) {
						Log.LogMessage("The GameRuntime class is already defined in the Godot project file.");
						return true;
					}
			}
			
			if (!Directory.Exists(rt_dir))
				Directory.CreateDirectory(rt_dir);
			
			if (File.Exists(rt_file)) {
				Log.LogMessage("The GameRuntime.cs file already exists in the godot project.");
				return true;
			}

			using FileStream stream = File.OpenWrite(rt_file);
			byte[] bytes = Encoding.UTF8.GetBytes(code);
			stream.Write(bytes, 0, bytes.Length);

			using StreamWriter writer = new(projectPath, true);
			writer.AutoFlush = true;
			writer.WriteLine();
			writer.WriteLine("[autoload]");
			writer.WriteLine();
			writer.WriteLine(gameRuntime);

			Log.LogMessage("The process of copying the GameRuntime class was completed successfully!");

		} catch (System.Exception ex) {
			Log.LogWarningFromException(ex);
		}

		return !Log.HasLoggedErrors;
	}
}
