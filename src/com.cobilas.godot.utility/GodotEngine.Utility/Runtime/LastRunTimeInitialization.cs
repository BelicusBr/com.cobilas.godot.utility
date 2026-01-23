using Godot;
using System;

namespace Cobilas.GodotEngine.Utility.Runtime;
/// <summary>A runtime initialization node that executes after other autoload scripts and manages its position in the scene tree.</summary>
/// <remarks>
/// This class is automatically loaded as an autoload singleton with priority 7, ensuring it runs after most other autoloads.
/// It performs runtime initialization and ensures it remains at the bottom of the scene tree hierarchy.
/// </remarks>
/// <seealso cref="AutoLoadScriptAttribute"/>
[AutoLoadScript(7, autoLoadName:"LastGameRuntime")]
public class LastRunTimeInitialization : Node {
	/// <summary>
	/// Standby = 0,<br/>
	/// Process = 1,<br/>
	/// PhyProcess = 2
	/// </summary>
	private byte status = 0;
    private int myIndex = 0;
    /// <inheritdoc/>
    public override void _Ready() {
        Type[] components = TypeUtilitarian.GetTypes();
        RunTimeInitialization.StartRunTimeInitializationClass(this, components, true);
    }
	/// <inheritdoc/>
	public override void _EnterTree() => RunTimeInitialization._closePlayModeStateChanged = true;
	/// <inheritdoc/>
	public override void _Process(float delta) {
        if (status == 0) status = 1;
        if (status == 1) {
            ChangeMyPosition();
            status = 0;
        }
    }
	/// <inheritdoc/>
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
