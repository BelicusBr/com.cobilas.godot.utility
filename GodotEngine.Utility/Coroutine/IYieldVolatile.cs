namespace Cobilas.GodotEngine.Utility; 
/// <summary>The IyieldVolatile interface allows the Yield class to change the type of process.
/// <para>This interface allows you to change the type of update if the object will use the <see cref="Godot.Node._Process(float)"/> or <see cref="Godot.Node._PhysicsProcess(float)"/> <see cref="Coroutine"/> process.</para>
/// </summary>
public interface IYieldVolatile : IYieldCoroutine {
    /// <summary>Indicates which process of updating the <see cref="Coroutine"/> is using.</summary>
    /// <returns>
    /// <para><c>true</c> when the corrotine is using <see cref="Godot.Node._PhysicsProcess(float)"/>.</para>
    /// <para><c>false</c> when the corrotine is using <see cref="Godot.Node._Process(float)"/>.</para>
    /// </returns>
    bool IsPhysicsProcess { get; }
}