using Godot;

namespace Cobilas.GodotEngine.Utility.EditorSerialization;

public sealed class RangeHint : CustomHint {
    public override PropertyHint Hint { get; protected set; }
    public override string HintString { get; protected set; }

    public RangeHint(int min, int max) : 
        base( PropertyHint.Range, $"{min},{max}"){}

    public RangeHint(float min, float max) : 
        base( PropertyHint.ExpRange, $"{min},{max}"){}
}
