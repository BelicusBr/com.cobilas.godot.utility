using Godot;
using System;
using Cobilas.GodotEngine.Utility.Runtime;

namespace Cobilas.GodotEngine.Utility.Input;
[AutoLoadScript(9)]
public class GCInputKeyBoard : Node {
    internal enum GCStatus : byte {
        Standby = 0,
        Process = 1,
        PhyProcess = 2
    }

    internal static event Action? GCEvent;
    private GCStatus status = GCStatus.Standby;
	/// <inheritdoc/>
	public override void _Process(float delta) {
        if (status == GCStatus.Standby) status = GCStatus.Process;
        if (status == GCStatus.Process) {
            GCEvent?.Invoke();
            status = GCStatus.Standby;
        }
    }
	/// <inheritdoc/>
	public override void _PhysicsProcess(float delta) {
        if (status == GCStatus.Standby) status = GCStatus.PhyProcess;
        if (status == GCStatus.PhyProcess) {
            GCEvent?.Invoke();
            status = GCStatus.Standby;
        }
    }
}
