using Cobilas.GodotEditor.Utility.Serialization;
using Cobilas.GodotEditor.Utility.Serialization.RenderObjects;
using Cobilas.GodotEngine.Utility;
using Cobilas.GodotEngine.Utility.IO;
using Cobilas.GodotEngine.Utility.IO.Interfaces;
using Godot;
using Godot.Collections;
using System;
using System.IO;
using System.Xml.Linq;

public class Node_Test : Node2D {

    [Export] private NodePath nodePath;

    public override void _Ready()
    {
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
        }
        catch (Exception ex) {
            label = GetNode<Label>(nodePath);
			label.SelfModulate = Color.Color8(255, 0, 0);
            label.AppendLine(ex.ToString());
		}

    }
}
