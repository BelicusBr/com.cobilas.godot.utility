using Godot;

namespace Cobilas.GodotEditor.Utility.Serialization.Hints;
/// <summary>The <seealso cref="NoneHint"/> class is an empty representation of a <seealso cref="CustomHint"/>.</summary>
public sealed class NoneHint : CustomHint {
    /// <inheritdoc/>
    public override PropertyHint Hint { get; protected set; } = PropertyHint.None;
    /// <inheritdoc/>
    public override string HintString { get; protected set; } = string.Empty;
    /// <inheritdoc cref="CustomHint(PropertyHint, string)"/>
    public NoneHint() : base(PropertyHint.None, string.Empty) { }
}
