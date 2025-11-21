namespace Cobilas.GodotEngine.Utility;
/// <summary>A null representation of the <see cref="Godot.Node"/> class.</summary>
public class NullNode : Godot.Node {
    private static readonly NullNode @null = new();
    /// <summary>Null <see cref="Godot.Node"/> object.</summary>
    /// <returns>This property will return a null representation of <see cref="Godot.Node"/>.</returns>
    public static NullNode Null => @null;
}
