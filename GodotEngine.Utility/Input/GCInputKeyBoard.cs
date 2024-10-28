using Godot;
using System;

namespace Cobilas.GodotEngine.Utility.Input;

internal class GCInputKeyBoard : Node {
    internal enum GCStatus : byte {
        Standby = 0,
        Process = 1,
        PhyProcess = 2
    }

    internal event Action? GCEvent;
    internal GCStatus status = GCStatus.Standby;
    private int myIndex = 0;

    public override void _Process(float delta) {
        if (status == GCStatus.Standby) status = GCStatus.Process;
        if (status == GCStatus.Process) {
            GCEvent?.Invoke();
            ChangeMyPosition();
            status = GCStatus.Standby;
        }
    }

    public override void _PhysicsProcess(float delta) {
        if (status == GCStatus.Standby) status = GCStatus.PhyProcess;
        if (status == GCStatus.PhyProcess) {
            GCEvent?.Invoke();
            ChangeMyPosition();
            status = GCStatus.Standby;
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
