using Cobilas.GodotEngine.Utility;
using Cobilas.GodotEngine.Utility.Input;
using Cobilas.GodotEngine.Utility.IO;
using Cobilas.GodotEngine.Utility.IO.Interfaces;
using Cobilas.GodotEngine.Utility.Runtime;
using Cobilas.GodotEngine.Utility.Scene;
using Godot;
using System;
using System.IO;
using System.Text;

[Tool]
public class Node_Test : Node2D {

    [Export] private NodePath nodePath;
	[Export] private NodePath n_camera;
	[Export] private NodePath[] n_sprite;

    Camera2D camera;
	Label label;
    StringBuilder builder_Ready = new StringBuilder();

	public override void _Ready() {
        if (RunTime.ExecutionMode == ExecutionMode.EditorMode) return;
		string name = "Global";
		string path = "res://Scripts/Global.cs";

		try {
            label = nodePath.GetNode<Label>();
            camera = n_camera.GetNode<Camera2D>();

            IStream stream = Archive.Open(GodotPath.GlobalizePath("res://TextFile1.txt"), FileAccess.Read, StreamType.IOStream);
            stream.Read(out string stg);

            label.SelfModulate = Color.Color8(255, 255, 255);

            builder_Ready.AppendLine(stg);
            builder_Ready.AppendLine(RunTime.ExecutionMode.ToString());

			SceneManager.LoadedScene += (s) => {
                GD.Print($"{nameof(_Ready)}.LoadedScene:{s}");
            };

            //_ = Coroutine.StartCoroutine(GPO());
            label.Append(builder_Ready);
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
        try {
			//GD.Print("autoload/Auto_InternalInputKeyBoard", ":", ProjectSettings.HasSetting("autoload/Auto_InternalInputKeyBoard"));
			//GD.Print("Auto_InternalInputKeyBoard", ":", ProjectSettings.HasSetting("Auto_InternalInputKeyBoard"));
			label.ClearText();
			label.Append(builder_Ready);

			foreach (NodePath item in n_sprite) {
                Node node = item.GetNode();
                if (node is Sprite spt)
			        label.AppendLine(spt.GetGlobalRect2D().HasPoint(camera.ScreenToWorldPoint(InputKeyBoard.MousePosition)));
                else if (node is Control ctl)
					label.AppendLine(ctl.GetGlobalRect2D().HasPoint(camera.ScreenToWorldPoint(InputKeyBoard.MousePosition)));
			}

        } catch (Exception ex) {
			label = GetNode<Label>(nodePath);
			label.SelfModulate = Color.Color8(255, 0, 0);
			label.AppendLine(ex.ToString());
		}
	}
}
