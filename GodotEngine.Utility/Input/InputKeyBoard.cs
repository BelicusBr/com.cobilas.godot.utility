using Godot;
using System.Collections.Generic;
using Cobilas.GodotEngine.Utility.Runtime;

namespace Cobilas.GodotEngine.Utility.Input; 

[RunTimeInitializationClass(nameof(InputKeyBoard))]
public class InputKeyBoard : Node {

    private readonly List<PeripheralItem> periferics = [];
    private readonly GCInputKeyBoard gCInputKeyBoard = new();
    private static InputKeyBoard? keyBoard = null;
    private static ulong lastPressed = 0UL;

    public static int MouseIndex { get; private set;}
    public static bool DoubleClick { get; private set;}
    public static float DeltaScroll { get; private set;}
    public static Vector2 MousePosition { get; private set;}
    public static Vector2 MouseGlobalPosition { get; private set;}
    /// <inheritdoc/>
    public override void _Ready() {
        keyBoard ??= this;
        if (keyBoard == this) {
            gCInputKeyBoard.Name = nameof(GCInputKeyBoard);
            gCInputKeyBoard.periferics = this.periferics;
            Viewport root = GetTree().Root;
            root.CallDeferred("add_child", gCInputKeyBoard);
            int gcIndex = gCInputKeyBoard.GetIndex();
            //move_child
            gCInputKeyBoard.GCEvent += () => {
                //MouseIndex = 0;
                if (gcIndex != gCInputKeyBoard.GetIndex())
                    root.CallDeferred("move_child", gCInputKeyBoard, gcIndex = root.GetChildCount() - 1);
            };
        }
    }
    /// <inheritdoc/>
    public override void _Input(InputEvent @event) {
        if (@event is InputEventKey input) {
            if (lastPressed == input.Scancode) return;
            PeripheralItem key = GetKeyItem(lastPressed = input.Scancode);
            if (input.Pressed) {
                if (key.KeyCode == KeyCode.None)
                    key = new((KeyCode)input.Scancode, KeyStatus.Down | KeyStatus.Press);
                else key.Status = KeyStatus.Down | KeyStatus.Press;
            } else {
                key.Status = KeyStatus.Up;
                key.Dispose();
            }
            SetKeyItem(input.Scancode, key);
        } else if (@event is InputEventMouseMotion mouseMotion) {
            MousePosition = mouseMotion.Position;
            MouseGlobalPosition = mouseMotion.GlobalPosition;
        } else if (@event is InputEventMouseButton mouseButton) {
            DeltaScroll = mouseButton.Factor;
            DoubleClick = mouseButton.Doubleclick;
            MouseIndex = mouseButton.ButtonIndex;
            if (MouseIndex == 0) return;
            PeripheralItem key = GetKeyItem((ulong)MouseIndex);
            if (mouseButton.Pressed) {
                if (key.KeyCode == KeyCode.None)
                    key = new((KeyCode)MouseIndex, KeyStatus.Down | KeyStatus.Press);
                else key.Status = KeyStatus.Down | KeyStatus.Press;
            } else {
                key.Status = KeyStatus.Up;
                key.Dispose();
            }
            SetKeyItem((ulong)MouseIndex, key);
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

    public static bool GetKeyDown(KeyCode key)
        => GetKeyStatus(key, KeyStatus.Down);

    public static bool GetKeyPress(KeyCode key)
        => GetKeyStatus(key, KeyStatus.Press);

    public static bool GetKeyUp(KeyCode key)
        => GetKeyStatus(key, KeyStatus.Up);

    public static bool GetMouseDown(int buttonIndex)
        => GetKeyStatus((KeyCode)buttonIndex, KeyStatus.Down);

    public static bool GetMousePress(int buttonIndex)
        => GetKeyStatus((KeyCode)buttonIndex, KeyStatus.Press);

    public static bool GetMouseUp(int buttonIndex)
        => GetKeyStatus((KeyCode)buttonIndex, KeyStatus.Up);

    public static bool GetMouseDown(MouseButton button)
        => GetMouseDown((int)button);

    public static bool GetMousePress(MouseButton button)
        => GetMousePress((int)button);

    public static bool GetMouseUp(MouseButton button)
        => GetMouseUp((int)button);

    private static bool GetKeyStatus(KeyCode key, KeyStatus status) {
        PeripheralItem keyItem = keyBoard!.GetKeyItem(key);
        bool result = keyItem.Status != KeyStatus.None && keyItem.Status.HasFlag(status);
        if (keyItem.Status.HasFlag(KeyStatus.Down)) {
            keyItem.Status = KeyStatus.Press;
            keyBoard.SetKeyItem(key, keyItem);
        } else if (keyItem.Status == KeyStatus.Up) {
            keyItem.Status = KeyStatus.None;
            keyBoard.SetKeyItem(key, keyItem);
        }
        return result;
    }
}