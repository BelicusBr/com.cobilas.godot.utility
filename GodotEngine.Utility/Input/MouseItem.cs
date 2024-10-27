using System;

namespace Cobilas.GodotEngine.Utility.Input; 
/// <summary>This structure is a representation of the mouse trigger states.</summary>
[Obsolete($"Use {nameof(PeripheralItem)} struct")]
public struct MouseItem : IEquatable<MouseItem> {
    /// <summary>Represents the index of the mouse trigger that was pressed.</summary>
    public int Index;
    /// <summary>This field signals to the InputKeyBoard that this item should be discarded.</summary>
    public bool onDestroy;
    /// <summary>This field signals to the InputKeyBoard that there will be a delay in changing states.</summary>
    public bool pressDelay;
    /// <summary>Represents the current state of the mouse trigger.</summary>
    public KeyStatus status;
    /// <summary>An empty <seealso cref="MouseItem"/>.</summary>
    /// <returns>Returns an empty representation of the <seealso cref="MouseItem"/>.</returns>
    public static MouseItem Empyt => new() {
        Index = -1,
        status = KeyStatus.None
    };
    /// <inheritdoc/>
    public readonly bool Equals(MouseItem other)
        => other.status == status && other.Index == Index;
    /// <inheritdoc/>
    public override readonly bool Equals(object obj)
        => obj is MouseItem key && Equals(key);
    /// <inheritdoc/>
    public override readonly int GetHashCode()
        => base.GetHashCode();
    /// <summary>Indicates whether this instance is equal to another instance of the same type.</summary>
    /// <param name="A">Object to be compared.</param>
    /// <param name="B">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator ==(MouseItem A, MouseItem B) => A.Equals(B);
    /// <summary>Indicates whether this instance is different from another instance of the same type.</summary>
    /// <param name="A">Object to be compared.</param>
    /// <param name="B">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator !=(MouseItem A, MouseItem B) => !A.Equals(B);
}