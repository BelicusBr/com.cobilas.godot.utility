namespace Cobilas.GodotEngine.Utility; 

public interface IYieldVolatile : IYieldCoroutine {
    bool IsPhysicsProcess { get; }
}