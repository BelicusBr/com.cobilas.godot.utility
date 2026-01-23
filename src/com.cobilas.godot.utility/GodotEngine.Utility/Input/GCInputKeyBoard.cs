using Godot;
using System;
using Cobilas.GodotEngine.Utility.Runtime;

namespace Cobilas.GodotEngine.Utility.Input;
/// <summary>Provides global keyboard input event handling with priority 9 in the autoload order.</summary>
/// <remarks>
/// This class ensures that global keyboard input events are processed during both regular and physics process frames.
/// It uses a status flag to alternate between processing modes to avoid duplicate event invocations.
/// </remarks>
/// <seealso cref="AutoLoadScriptAttribute"/>
[AutoLoadScript(9)]
public class GCInputKeyBoard : Node {
	/// <summary>
	/// Defines the processing status for the global keyboard input handler.
	/// </summary>
	internal enum GCStatus : byte {
		/// <summary>The handler is in standby mode.</summary>
		Standby = 0,
		/// <summary>The handler is processing during the regular process frame.</summary>
		Process = 1,
		/// <summary>The handler is processing during the physics process frame.</summary>
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
