using Godot;

namespace Cobilas.GodotEngine.Utility.EditorSerialization;

public sealed class NoneHint : CustomHint {
    public override PropertyHint Hint { get; protected set; }
    public override string HintString { get; protected set; }

    public NoneHint() : base(PropertyHint.None, string.Empty) { }
}
