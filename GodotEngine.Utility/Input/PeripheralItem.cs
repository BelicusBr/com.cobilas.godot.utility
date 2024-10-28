using Godot;
using System;

namespace Cobilas.GodotEngine.Utility.Input;
/// <summary>Responsible for maintaining the status information of a peripheral.</summary>
public struct PeripheralItem : IEquatable<PeripheralItem>, IEquatable<KeyCode>,
    IEquatable<KeyList>, IEquatable<MouseButton>, IEquatable<KeyStatus>, IEquatable<ulong> {

    internal ChangeState PeripheralState { get; set; }
    /// <summary>Represents the status of a peripheral input.</summary>
    /// <value>Allows you to change the status of the object.</value>
    /// <returns>Returns the status of the peripheral.</returns>
    public KeyStatus Status { get; set; }
    /// <summary>Represents a keyboard or mouse input.</summary>
    /// <returns>Returns a representation of a peripheral input.</returns>
    public KeyCode KeyCode { get; private set; }
    /// <summary>The ScanCode is a representation of the <seealso cref="Input.KeyCode"/>.</summary>
    /// <returns>Returns a code representing a <seealso cref="Input.KeyCode"/> key.</returns>
    public readonly ulong ScanCode => (ulong)KeyCode;
    /// <summary>Indicates whether it is a mouse trigger.</summary>
    /// <returns>Returns <c>true</c> when the object is a mouse trigger.</returns>
    public readonly bool IsMouseButton => KeyCode switch {
        KeyCode.MouseLeft or KeyCode.MouseMiddle or KeyCode.MouseRight or 
        KeyCode.MouseWheelDown or KeyCode.MouseWheelLeft or KeyCode.MouseWheelRight or 
        KeyCode.MouseWheelUp or KeyCode.MouseXB1 or KeyCode.MouseXB2 or KeyCode.MouseXB3 or 
        KeyCode.MouseXB4 or KeyCode.MouseXB5 or KeyCode.MouseXB6 => true,
        _ => false,
    };
    /// <summary>Represents an empty PeripheralItem.</summary>
    /// <returns>Returns an empty representation of PeripheralItem.</returns>
    public static PeripheralItem Empty => new(KeyCode.None);
    /// <summary>Starts a new instance of the object.</summary>
    public PeripheralItem(KeyCode keyCode, KeyStatus keyStatus) {
        KeyCode = keyCode;
        Status = keyStatus;
        PeripheralState = ChangeState.None;
    }
    /// <summary>Starts a new instance of the object.</summary>
    public PeripheralItem(KeyCode keyCode) : this(keyCode, KeyStatus.None) {}
    /// <inheritdoc/>
    public readonly bool Equals(KeyCode other) => IEquals(other);
    /// <inheritdoc/>
    public readonly bool Equals(KeyStatus other) => IEquals(other);
    /// <inheritdoc/>
    public readonly bool Equals(ulong other) => other == this.ScanCode;
    /// <inheritdoc/>
    public readonly bool Equals(PeripheralItem other) => IEquals(other);
    /// <inheritdoc/>
    public readonly bool Equals(KeyList other) => IEquals(other);
    /// <inheritdoc/>
    public readonly bool Equals(MouseButton other) => IEquals(other);
    /// <inheritdoc/>
    public readonly override bool Equals(object obj) => IEquals(obj);
    /// <inheritdoc/>
    public readonly override int GetHashCode() => base.GetHashCode();
    /// <inheritdoc/>
    public readonly override string ToString() => $"Key[{KeyCode}] Code[{ScanCode}] Status[{Status}]";

    private readonly bool IEquals(object obj)
        => obj switch {
            PeripheralItem per => per.KeyCode == this.KeyCode && per.Status == this.Status,
            KeyCode kc => kc == this.KeyCode,
            KeyStatus ks => ks == this.Status,
            KeyList kl => !this.IsMouseButton && kl == (KeyList)this.KeyCode,
            MouseButton mb => this.IsMouseButton && mb == (MouseButton)this.KeyCode,
            ulong ul => ul == this.ScanCode,
            _ => false
        };
    /// <summary>Indicates whether this instance is equal to another instance of the same type.</summary>
    /// <param name="A">Object to be compared.</param>
    /// <param name="B">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator ==(PeripheralItem A, PeripheralItem B) => A.Equals(B);
    /// <summary>Indicates whether this instance is different from another instance of the same type.</summary>
    /// <param name="A">Object to be compared.</param>
    /// <param name="B">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator !=(PeripheralItem A, PeripheralItem B) => !A.Equals(B);
    /// <summary>Indicates whether this instance is equal to another instance of the same type.</summary>
    /// <param name="A">Object to be compared.</param>
    /// <param name="B">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator ==(PeripheralItem A, KeyCode B) => A.Equals(B);
    /// <summary>Indicates whether this instance is different from another instance of the same type.</summary>
    /// <param name="A">Object to be compared.</param>
    /// <param name="B">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator !=(PeripheralItem A, KeyCode B) => !A.Equals(B);
    /// <summary>Indicates whether this instance is equal to another instance of the same type.</summary>
    /// <param name="A">Object to be compared.</param>
    /// <param name="B">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator ==(PeripheralItem A, KeyList B) => A.Equals(B);
    /// <summary>Indicates whether this instance is different from another instance of the same type.</summary>
    /// <param name="A">Object to be compared.</param>
    /// <param name="B">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator !=(PeripheralItem A, KeyList B) => !A.Equals(B);
    /// <summary>Indicates whether this instance is equal to another instance of the same type.</summary>
    /// <param name="A">Object to be compared.</param>
    /// <param name="B">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator ==(PeripheralItem A, MouseButton B) => A.Equals(B);
    /// <summary>Indicates whether this instance is different from another instance of the same type.</summary>
    /// <param name="A">Object to be compared.</param>
    /// <param name="B">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator !=(PeripheralItem A, MouseButton B) => !A.Equals(B);
    /// <summary>Indicates whether this instance is equal to another instance of the same type.</summary>
    /// <param name="A">Object to be compared.</param>
    /// <param name="B">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator ==(PeripheralItem A, KeyStatus B) => A.Equals(B);
    /// <summary>Indicates whether this instance is different from another instance of the same type.</summary>
    /// <param name="A">Object to be compared.</param>
    /// <param name="B">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator !=(PeripheralItem A, KeyStatus B) => !A.Equals(B);
    /// <summary>Indicates whether this instance is equal to another instance of the same type.</summary>
    /// <param name="A">Object to be compared.</param>
    /// <param name="B">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator ==(PeripheralItem A, ulong B) => A.Equals(B);
    /// <summary>Indicates whether this instance is different from another instance of the same type.</summary>
    /// <param name="A">Object to be compared.</param>
    /// <param name="B">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator !=(PeripheralItem A, ulong B) => !A.Equals(B);
    /// <summary>Explicit conversion operator.(<seealso cref="PeripheralItem"/> to <seealso cref="Input.KeyCode"/>)</summary>
    /// <param name="A">Object to be converted.</param>
    public static explicit operator KeyCode(PeripheralItem A) => A.KeyCode;
    /// <summary>Explicit conversion operator.(<seealso cref="PeripheralItem"/> to <seealso cref="KeyList"/>)</summary>
    /// <param name="A">Object to be converted.</param>
    public static explicit operator KeyList(PeripheralItem A) => A.IsMouseButton ? KeyList.Unknown : (KeyList)A.KeyCode;
    /// <summary>Explicit conversion operator.(<seealso cref="PeripheralItem"/> to <seealso cref="MouseButton"/>)</summary>
    /// <param name="A">Object to be converted.</param>
    public static explicit operator MouseButton(PeripheralItem A) => A.IsMouseButton ? (MouseButton)A.KeyCode : MouseButton.Unknown;
    /// <summary>Explicit conversion operator.(<seealso cref="PeripheralItem"/> to <seealso cref="KeyStatus"/>)</summary>
    /// <param name="A">Object to be converted.</param>
    public static explicit operator KeyStatus(PeripheralItem A) => A.Status;
    /// <summary>Explicit conversion operator.(<seealso cref="PeripheralItem"/> to <seealso cref="UInt64"/>)</summary>
    /// <param name="A">Object to be converted.</param>
    public static explicit operator ulong(PeripheralItem A) => A.ScanCode;
}
