using Godot;
using System.Globalization;

namespace Cobilas.GodotEditor.Utility.Serialization.Hints;
/// <summary>Represents custom hint range.</summary>
public sealed class RangeHint : CustomHint {
    /// <inheritdoc/>
    public override PropertyHint Hint { get; protected set; } = PropertyHint.None;
    /// <inheritdoc/>
    public override string HintString { get; protected set; } = string.Empty;
    /// <inheritdoc cref="CustomHint(PropertyHint, string)"/>
    /// <param name="min">The minimum value of the range.</param>
    /// <param name="max">The maximum value of the range.</param>
    /// <param name="step">The increment value of the range.</param>
    public RangeHint(int min, int max, int step) : 
        base(PropertyHint.Range, $"{min},{max},{step}"){}
    /// <inheritdoc cref="RangeHint(int, int, int)"/>
    public RangeHint(float min, float max, float step) : 
        base(PropertyHint.ExpRange, 
        $"{min.ToString(CultureInfo.InvariantCulture)},{max.ToString(CultureInfo.InvariantCulture)},{step.ToString(CultureInfo.InvariantCulture)}"){}
}
