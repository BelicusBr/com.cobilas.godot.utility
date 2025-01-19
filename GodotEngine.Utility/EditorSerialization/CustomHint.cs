using Godot;

namespace Cobilas.GodotEngine.Utility.EditorSerialization;

public abstract class CustomHint {
    public abstract PropertyHint Hint { get; protected set; }
    public abstract string HintString { get; protected set; }

    protected CustomHint(PropertyHint hint, string hintString) {
        Hint = hint;
        HintString = hintString;
    }
}
