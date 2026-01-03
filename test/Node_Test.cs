using Cobilas.GodotEditor.Utility.Serialization;
using Cobilas.GodotEditor.Utility.Serialization.RenderObjects;
using Cobilas.GodotEngine.Utility;
using Cobilas.GodotEngine.Utility.IO;
using Cobilas.GodotEngine.Utility.Scene;
using Cobilas.GodotEngine.Utility.IO.Interfaces;
using Cobilas.GodotEngine.Utility.Runtime;
using Godot;
using Godot.Collections;
using System;
using System.IO;
using System.Xml.Linq;

[Tool]
public class Node_Test : Node2D {

    [Export] private NodePath nodePath;

    public override void _Ready()
    {
        if (RunTime.ExecutionMode == ExecutionMode.EditorMode) return;
		string name = "Global";
		string path = "res://Scripts/Global.cs";
        //GD.Print(ProjectSettings.get("autoload"));
      //  if (ProjectSettings.HasSetting($"autoload/{name}")) {
      //      DebugLog.Log($"ex:autoload/{name}");
      //  } else {
      //      ProjectSettings.SetSetting($"autoload/{name}", "*" + path);
		    //ProjectSettings.Save();
      //  }
		Label label;
		try {
            label = nodePath.GetNode<Label>();
            IStream stream = Archive.Open(GodotPath.GlobalizePath("res://TextFile1.txt"), FileAccess.Read, StreamType.IOStream);
            stream.Read(out string stg);

            label.SelfModulate = Color.Color8(255, 255, 255);

            label.AppendLine(stg);
            label.AppendLine(RunTime.ExecutionMode);

            SceneManager.LoadedScene += (s) => {
                GD.Print($"{nameof(_Ready)}.LoadedScene:{s}");
            };

            _ = Coroutine.StartCoroutine(GPO());
        }
        catch (Exception ex) {
            label = GetNode<Label>(nodePath);
			label.SelfModulate = Color.Color8(255, 0, 0);
            label.AppendLine(ex.ToString());
		}
    }

    private System.Collections.IEnumerator GPO() {
        yield return new RunTimeSecond(1f);
        SceneManager.LoadScene("Rect2D_Test");
    }

	public override void _Process(float delta) {
        //GD.Print(GetTree().Root.GetType());
		//GD.Print(GetTree().EditedSceneRoot.GetType());
	}
}
