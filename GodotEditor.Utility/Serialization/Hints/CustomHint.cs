using Godot;

namespace Cobilas.GodotEditor.Utility.Serialization.Hints;
/// <summary>Base class for generating custom hints.</summary>
public abstract class CustomHint {
    /// <summary>The type of hint of the property.</summary>
    /// <returns>Returns the hint type of the property.</returns>
    public abstract PropertyHint Hint { get; protected set; }
    /// <summary>Additional information for the property hint.</summary>
    /// <returns>Returns additional information for the property hint.</returns>
    public abstract string HintString { get; protected set; }
    /// <summary>Creates a new instance of this object.</summary>
    /// <param name="hint">The type of hint of the property.</param>
    /// /// <param name="hintString">Additional information for the property hint.</param>
    protected CustomHint(PropertyHint hint, string hintString) {
        Hint = hint;
        HintString = hintString;
    }
}
