using Godot;
using System;

namespace Cobilas.GodotEngine.Utility.Runtime;

internal class LastRunTimeInitialization : Node {
    //Standby = 0,
    //Process = 1,
    //PhyProcess = 2
    private byte status = 0;
    private int myIndex = 0;

    public override void _Ready() {
        Type[] components = TypeUtilitarian.GetTypes();
        RunTimeInitialization.StartRunTimeInitializationClass(this, components, true);
    }

    public override void _Process(float delta) {
        if (status == 0) status = 1;
        if (status == 1) {
            ChangeMyPosition();
            status = 0;
        }
    }

    public override void _PhysicsProcess(float delta) {
        if (status == 0) status = 2;
        if (status == 2) {
            ChangeMyPosition();
            status = 0;
        }
    }

    private void ChangeMyPosition() {
        if (myIndex == GetIndex()) return;
        Viewport root = GetTree().Root;
        root.CallDeferred("remove_child", this);
        root.CallDeferred("add_child", this);
        myIndex = GetIndex();
    }
}
