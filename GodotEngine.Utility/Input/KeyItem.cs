using Godot;
using System;

namespace Cobilas.GodotEngine.Utility.Input; 
/// <summary>This structure is a representation of the states of a keyboard key.</summary>
[Obsolete($"Use {nameof(PeripheralItem)} struct")]
public struct KeyItem : IEquatable<KeyItem> {
    /// <summary>Represents the keyboard key that was pressed.</summary>
    public KeyList key;
    /// <inheritdoc cref="MouseItem.onDestroy"/>
    public bool onDestroy;
    /// <inheritdoc cref="MouseItem.pressDelay"/>
    public bool pressDelay;
    /// <summary>Represents the current state of the keyboard key.</summary>
    public KeyStatus status;
    /// <summary>An empty <seealso cref="KeyItem"/>.</summary>
    /// <returns>Returns an empty representation of <seealso cref="KeyItem"/>.</returns>
    public static KeyItem Empyt => new() {
        key = KeyList.Space,
        status = KeyStatus.None
    };
    /// <inheritdoc/>
    public readonly bool Equals(KeyItem other)
        => other.status == status && other.key == key;
    /// <inheritdoc/>
    public override readonly bool Equals(object obj)
        => obj is KeyItem key && Equals(key);
    /// <inheritdoc/>
    public override readonly int GetHashCode()
        => base.GetHashCode();
    /// <summary>Indicates whether this instance is equal to another instance of the same type.</summary>
    /// <param name="A">Object to be compared.</param>
    /// <param name="B">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator ==(KeyItem A, KeyItem B) => A.Equals(B);
    /// <summary>Indicates whether this instance is different from another instance of the same type.</summary>
    /// <param name="A">Object to be compared.</param>
    /// <param name="B">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator !=(KeyItem A, KeyItem B) => !A.Equals(B);
}