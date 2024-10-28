using Godot;
using System.Collections.Generic;
using Cobilas.GodotEngine.Utility.Runtime;

namespace Cobilas.GodotEngine.Utility.Input;
/// <summary>This class has methods and properties that get information from keyboard and mouse inputs.</summary>
[RunTimeInitializationClass(nameof(InputKeyBoard))]
public class InputKeyBoard : Node {

    private readonly GCInputKeyBoard gCInputKeyBoard = new();
    private readonly List<PeripheralItem> periferics = [];
    private static MouseInfo mouseInfo = new();
    private static InputKeyBoard? keyBoard = null;
    /// <summary>The mouse trigger index.</summary>
    /// <returns>Returns an <see cref="System.Int32"/> containing the index of the mouse trigger.</returns>
    public static int MouseIndex => mouseInfo.mouseIndex;
    /// <summary>Detects a double mouse click.</summary>
    /// <returns>Returns true when a double mouse click is detected.</returns>
    public static bool DoubleClick => mouseInfo.doubleClick;
    /// <summary>Indicates a change in mouse scroll.</summary>
    /// <returns>Returns a floating-point value when the mouse scroll is changed.</returns>
    public static float DeltaScroll => mouseInfo.deltaScroll;
    /// <summary>The current mouse position.</summary>
    /// <returns>Returns the mouse position based on the defined <see cref="Godot.Viewport"/>.</returns>
    public static Vector2 MousePosition => mouseInfo.mousePosition;
    /// <summary>The current global mouse position.</summary>
    /// <returns>Returns the mouse position based on a root <see cref="Godot.Viewport"/>.</returns>
    public static Vector2 MouseGlobalPosition => mouseInfo.mouseGlobalPosition;
    /// <inheritdoc/>
    public override void _Ready() {
        keyBoard ??= this;
        if (keyBoard == this) {
            gCInputKeyBoard.GCEvent += ChangePeripheralSwitchingStatus;
            GetTree().Root.CallDeferred("add_child", gCInputKeyBoard);
        }
    }
    /// <inheritdoc/>
    public override void _Input(InputEvent @event) {
        if (@event is InputEventKey input) {
            PeripheralItem key = GetKeyItem(input.Scancode);
            if (input.Pressed) {
                if (key.KeyCode == KeyCode.None)
                    key = new((KeyCode)input.Scancode, KeyStatus.Down | KeyStatus.Press) {
                        PeripheralState = ChangeState.Delay | ChangeState.Press
                    };
            } else {
                key.Status = KeyStatus.Up;
                key.PeripheralState = ChangeState.Delay | ChangeState.Destroy;
            }
            SetKeyItem(input.Scancode, key);
        } else if (@event is InputEventMouseMotion mouseMotion) {
            mouseInfo.mousePosition = mouseMotion.Position;
            mouseInfo.mouseGlobalPosition = mouseMotion.GlobalPosition;
        } else if (@event is InputEventMouseButton mouseButton) {
            mouseInfo.doubleClick = mouseButton.Doubleclick;
            int index = mouseButton.ButtonIndex;
            switch ((MouseButton)index) {
                case MouseButton.MouseLeft: case MouseButton.MouseMiddle: case MouseButton.MouseRight:
                case MouseButton.MouseXB1: case MouseButton.MouseXB2: case MouseButton.MouseXB3:
                case MouseButton.MouseXB4: case MouseButton.MouseXB5: case MouseButton.MouseXB6:
                    mouseInfo.mouseIndex = mouseButton.ButtonIndex;
                    if (mouseInfo.changeState.HasFlag(ChangeState.C_Index)) {
                        mouseInfo.changeState ^= ChangeState.C_Index;
                        mouseInfo.changeState |= ChangeState.D_Index;
                    } else mouseInfo.changeState |= ChangeState.D_Index;
                    break;
                case MouseButton.MouseWheelUp: case MouseButton.MouseWheelDown:
                    mouseInfo.deltaScroll = mouseButton.Factor;
                    if (mouseInfo.changeState.HasFlag(ChangeState.C_Scroll)) {
                        mouseInfo.changeState ^= ChangeState.C_Scroll;
                        mouseInfo.changeState |= ChangeState.D_Scroll;
                    } else mouseInfo.changeState |= ChangeState.D_Scroll;
                    break;
            }
            if (MouseIndex == 0) return;
            PeripheralItem key = GetKeyItem((ulong)MouseIndex);
            if (mouseButton.Pressed) {
                if (key.KeyCode == KeyCode.None)
                    key = new((KeyCode)MouseIndex, KeyStatus.Down | KeyStatus.Press) {
                        PeripheralState = ChangeState.Delay | ChangeState.Press
                    };
            } else {
                key.Status = KeyStatus.Up;
                key.PeripheralState = ChangeState.Delay | ChangeState.Destroy;
            }
            SetKeyItem((ulong)MouseIndex, key);
        }
    }

    private void ChangePeripheralSwitchingStatus() {
        if (mouseInfo.changeState.HasFlag(ChangeState.D_Index)) {
            mouseInfo.changeState ^= ChangeState.D_Index;
            mouseInfo.changeState |= ChangeState.C_Index;
        } else if (mouseInfo.changeState.HasFlag(ChangeState.C_Index)) {
            mouseInfo.changeState ^= ChangeState.C_Index;
            mouseInfo.mouseIndex = 0;
        } else if (mouseInfo.changeState.HasFlag(ChangeState.D_Scroll)) {
            mouseInfo.changeState ^= ChangeState.D_Scroll;
            mouseInfo.changeState |= ChangeState.C_Scroll;
        } else if (mouseInfo.changeState.HasFlag(ChangeState.C_Scroll)) {
            mouseInfo.changeState ^= ChangeState.C_Scroll;
            mouseInfo.deltaScroll = 0;
        }
        for (int I = 0; I < periferics.Count; I++) {
            PeripheralItem item = periferics[I];
            if (item.PeripheralState.HasFlag(ChangeState.Delay)) {
                if (item.PeripheralState.HasFlag(ChangeState.Press))
                    item.PeripheralState = ChangeState.Changed | ChangeState.Press;
                else if (item.PeripheralState.HasFlag(ChangeState.Destroy))
                    item.PeripheralState = ChangeState.Destroy;
            } else if (item.PeripheralState.HasFlag(ChangeState.Changed)) {
                if (item.PeripheralState.HasFlag(ChangeState.Press)) {
                    item.PeripheralState = ChangeState.None;
                    item.Status = KeyStatus.Press;
                }
            } else if (item.PeripheralState.HasFlag(ChangeState.Destroy)) {
                periferics.RemoveAt(I--);
                continue;
            }
            periferics[I] = item;
        }
    }

    private void SetKeyItem(ulong scancode, PeripheralItem value)
        => SetKeyItem((KeyCode)scancode, value);

    private void SetKeyItem(KeyCode scancode, PeripheralItem value) {
        if (scancode == KeyCode.None) return;
        for (int I = 0; I < periferics.Count; I++)
            if (periferics[I].KeyCode == scancode) {
                periferics[I] = value;
                return;
            }
        periferics.Add(value);
    }

    private PeripheralItem GetKeyItem(ulong scancode)
        => GetKeyItem((KeyCode)scancode);

    private PeripheralItem GetKeyItem(KeyCode scancode) {
        for (int I = 0; I < periferics.Count; I++)
            if (periferics[I].KeyCode == scancode)
                return periferics[I];
        return PeripheralItem.Empty;
    }
    /// <summary>Determines whether the key was pressed.</summary>
    /// <param name="key">The target key to be verified.</param>
    /// <returns>Returns <c>true</c> if the key was pressed.</returns>
    /// <example>
    /// <code>
    /// using Godot;
    /// using Cobilas.GodotEngine.Utility.Input;
    /// 
    /// public override void _Process(float delta) {
    ///     if (InputKeyBoard.GetKeyDown(KeyCode.A))
    ///         GD.Print("This instruction will only be called each time the key is pressed.");
    /// }
    /// </code>
    /// </example>
    /// <remarks>The method has overloads that allow the use of the <seealso cref="KeyList"/>, <seealso cref="MouseButton"/> and <seealso cref="ButtonList"/> enumerators.</remarks>
    public static bool GetKeyDown(KeyCode key)
        => GetKeyStatus(key, KeyStatus.Down);
    /// <summary>Determines whether the key is being pressed.</summary>
    /// <param name="key">The target key to be verified.</param>
    /// <returns>Returns <c>true</c> if the key is being pressed.</returns>
    /// <example>
    /// <code>
    /// using Godot;
    /// using Cobilas.GodotEngine.Utility.Input;
    /// 
    /// public override void _Process(float delta) {
    ///     if (InputKeyBoard.GetKeyPress(KeyCode.A))
    ///         GD.Print("The instruction will be called constantly as long as the key is pressed.");
    /// }
    /// </code>
    /// </example>
    /// <remarks>The method has overloads that allow the use of the <seealso cref="KeyList"/>, <seealso cref="MouseButton"/> and <seealso cref="ButtonList"/> enumerators.</remarks>
    public static bool GetKeyPress(KeyCode key)
        => GetKeyStatus(key, KeyStatus.Press);
    /// <summary>Determines whether the key has been released.</summary>
    /// <param name="key">The target key to be verified.</param>
    /// <returns>Returns <c>true</c> if the key was released.</returns>
    /// <example>
    /// <code>
    /// using Godot;
    /// using Cobilas.GodotEngine.Utility.Input;
    /// 
    /// public override void _Process(float delta) {
    ///     if (InputKeyBoard.GetKeyUp(KeyCode.A))
    ///         GD.Print("This instruction will only be called whenever the key is released.");
    /// }
    /// </code>
    /// </example>
    /// <remarks>The method has overloads that allow the use of the <seealso cref="KeyList"/>, <seealso cref="MouseButton"/> and <seealso cref="ButtonList"/> enumerators.</remarks>
    public static bool GetKeyUp(KeyCode key)
        => GetKeyStatus(key, KeyStatus.Up);
    /// <inheritdoc cref="GetKeyDown(KeyCode)"/>
    public static bool GetKeyDown(KeyList key) => GetKeyDown((KeyCode)key);
    /// <inheritdoc cref="GetKeyPress(KeyCode)"/>
    public static bool GetKeyPress(KeyList key) => GetKeyPress((KeyCode)key);
    /// <inheritdoc cref="GetKeyUp(KeyCode)"/>
    public static bool GetKeyUp(KeyList key) => GetKeyUp((KeyCode)key);
    /// <inheritdoc cref="GetKeyDown(KeyCode)"/>
    public static bool GetKeyDown(MouseButton key) => GetKeyDown((KeyCode)key);
    /// <inheritdoc cref="GetKeyPress(KeyCode)"/>
    public static bool GetKeyPress(MouseButton key) => GetKeyPress((KeyCode)key);
    /// <inheritdoc cref="GetKeyUp(KeyCode)"/>
    public static bool GetKeyUp(MouseButton key) => GetKeyUp((KeyCode)key);
    /// <inheritdoc cref="GetKeyDown(KeyCode)"/>
    public static bool GetKeyDown(ButtonList key) => GetKeyDown((KeyCode)key);
    /// <inheritdoc cref="GetKeyPress(KeyCode)"/>
    public static bool GetKeyPress(ButtonList key) => GetKeyPress((KeyCode)key);
    /// <inheritdoc cref="GetKeyUp(KeyCode)"/>
    public static bool GetKeyUp(ButtonList key) => GetKeyUp((KeyCode)key);
    /// <summary>Determines whether the mouse trigger was pressed.</summary>
    /// <param name="buttonIndex">The target index to be checked.</param>
    /// <returns>Returns <c>true</c> if the mouse trigger was pressed.</returns>
    public static bool GetMouseDown(int buttonIndex)
        => GetMouseDown((MouseButton)buttonIndex);
    /// <summary>Determines whether the mouse trigger is being pressed.</summary>
    /// <param name="buttonIndex">The target index to be checked.</param>
    /// <returns>Returns <c>true</c> if the mouse trigger is being pressed.</returns>
    public static bool GetMousePress(int buttonIndex)
        => GetMousePress((MouseButton)buttonIndex);
    /// <summary>Determines whether the mouse trigger has been drop by the user.</summary>
    /// <param name="buttonIndex">The target index to be checked.</param>
    /// <returns>Returns <c>true</c> if the mouse trigger was released by the user.</returns>
    public static bool GetMouseUp(int buttonIndex)
        => GetMouseUp((MouseButton)buttonIndex);
    /// <inheritdoc cref="GetMouseDown(int)"/>
    /// <param name="button">The target mouse trigger to be checked.</param>
    public static bool GetMouseDown(MouseButton button)
        => GetKeyStatus((KeyCode)button, KeyStatus.Down);
    /// <inheritdoc cref="GetMousePress(int)"/>
    /// <param name="button">The target mouse trigger to be checked.</param>
    public static bool GetMousePress(MouseButton button)
        => GetKeyStatus((KeyCode)button, KeyStatus.Press);
    /// <inheritdoc cref="GetMouseUp(int)"/>
    /// <param name="button">The target mouse trigger to be checked.</param>
    public static bool GetMouseUp(MouseButton button)
        => GetKeyStatus((KeyCode)button, KeyStatus.Up);

    private static bool GetKeyStatus(KeyCode key, KeyStatus status) 
        => keyBoard!.GetKeyItem(key).Status.HasFlag(status);
}