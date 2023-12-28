using Godot;
using System.Collections.Generic;
using Cobilas.GodotEngine.Utility.Runtime;

namespace Cobilas.GodotEngine.Utility.Input {
    [RunTimeInitializationClass(nameof(InputKeyBoard))]
    public class InputKeyBoard : Node {

        private List<KeyItem> pairs;
        private List<MouseItem> pairs2;
        private static bool mousePressed;
        private static InputKeyBoard keyBoard;

        public static int MouseIndex { get; private set;}
        public static bool DoubleClick { get; private set;}
        public static float DeltaScroll { get; private set;}
        public static Vector2 MousePosition { get; private set;}
        public static Vector2 MouseGlobalPosition { get; private set;}

        public override void _Ready() {
            if (keyBoard == null) {
                pairs = new List<KeyItem>();
                pairs2 = new List<MouseItem>();
                keyBoard = this;
            }
        }

        public override void _Input(InputEvent @event) {
            mousePressed = false;
            if (@event is InputEventKey input) {
                KeyItem key = GetKeyItem(input.Scancode);
                if (input.Pressed) {
                    if (key.status == KeyStatus.None) {
                        pairs.Add(key = new KeyItem {
                            status = KeyStatus.Down,
                            key = (KeyList)input.Scancode
                        });
                    }
                } else if (key.status == KeyStatus.Down || key.status == KeyStatus.Press)
                    key.status = KeyStatus.Up;
                SetKeyItem(input.Scancode, key);
            } else if (@event is InputEventMouseMotion mouseMotion) {
                MousePosition = mouseMotion.Position;
                MouseGlobalPosition = mouseMotion.GlobalPosition;
            } else if (@event is InputEventMouseButton mouseButton) {
                mousePressed = mouseButton.Pressed;
                DeltaScroll = mouseButton.Factor;
                DoubleClick = mouseButton.Doubleclick;
                MouseIndex = mouseButton.ButtonIndex;
                if (MouseIndex == 0) return;
                MouseItem key = GetMouseItem(MouseIndex);
                if (mousePressed) {
                    if (key.status == KeyStatus.None)
                        pairs2.Add(key = new MouseItem {
                            status = KeyStatus.Down,
                            Index = MouseIndex
                        });
                } else if (key.status == KeyStatus.Down || key.status == KeyStatus.Press)
                    key.status = KeyStatus.Up;
                SetMouseItem(MouseIndex, key);
            }
        }

        public override void _PhysicsProcess(float delta) {
            if (!mousePressed)
                MouseIndex = 0;
            for (int I = 0; I < pairs.Count; I++) {
                if (pairs[I].status == KeyStatus.Down) {
                    KeyItem temp = pairs[I];
                    if (!temp.pressDelay)
                        temp.pressDelay = true;
                    else temp.status = KeyStatus.Press;
                    pairs[I] = temp;
                    continue;
                } else if (pairs[I].status != KeyStatus.Up)
                    continue;
                
                if (pairs[I].onDestroy) {
                    pairs.RemoveAt(I);
                    I = -1;
                } else if (!pairs[I].onDestroy) {
                    KeyItem key = pairs[I];
                    key.onDestroy = true;
                    pairs[I] = key;
                }
            }
            for (int I = 0; I < pairs2.Count; I++) {
                if (pairs2[I].status == KeyStatus.Down) {
                    MouseItem temp = pairs2[I];
                    if (!temp.pressDelay)
                        temp.pressDelay = true;
                    else temp.status = KeyStatus.Press;
                    pairs2[I] = temp;
                    continue;
                } else if (pairs2[I].status != KeyStatus.Up)
                    continue;
                
                if (pairs2[I].onDestroy) {
                    pairs2.RemoveAt(I);
                    I = -1;
                } else if (!pairs2[I].onDestroy) {
                    MouseItem key = pairs2[I];
                    key.onDestroy = true;
                    pairs2[I] = key;
                }
            }
        }

        private void SetKeyItem(ulong scancode, KeyItem value)
            => SetKeyItem((KeyList)scancode, value);

        private void SetKeyItem(KeyList scancode, KeyItem value) {
            for (int I = 0; I < pairs.Count; I++)
                if (pairs[I].key == scancode) {
                    pairs[I] = value;
                    break;
                }
        }

        private KeyItem GetKeyItem(ulong scancode)
            => GetKeyItem((KeyList)scancode);

        private KeyItem GetKeyItem(KeyList scancode) {
            for (int I = 0; I < pairs.Count; I++)
                if (pairs[I].key == scancode)
                    return pairs[I];
            return KeyItem.Empyt;
        }

        private void SetMouseItem(int index, MouseItem value) {
            for (int I = 0; I < pairs2.Count; I++)
                if (pairs2[I].Index == index) {
                    pairs2[I] = value;
                    break;
                }
        }

        private MouseItem GetMouseItem(int index) {
            for (int I = 0; I < pairs2.Count; I++)
                if (pairs2[I].Index == index)
                    return pairs2[I];
            return default;
        }

        public static bool GetKeyDown(KeyList key)
            => GetKeyStatus(key, KeyStatus.Down);

        public static bool GetKeyPress(KeyList key)
            => GetKeyStatus(key, KeyStatus.Press);

        public static bool GetKeyUp(KeyList key)
            => GetKeyStatus(key, KeyStatus.Up);

        public static bool GetMouseDown(int buttonIndex)
            => GetMouseStatus(buttonIndex, KeyStatus.Down);

        public static bool GetMousePress(int buttonIndex)
            => GetMouseStatus(buttonIndex, KeyStatus.Press);

        public static bool GetMouseUp(int buttonIndex)
            => GetMouseStatus(buttonIndex, KeyStatus.Up);

        public static bool GetMouseDown(MouseButton button)
            => GetMouseDown((int)button);

        public static bool GetMousePress(MouseButton button)
            => GetMousePress((int)button);

        public static bool GetMouseUp(MouseButton button)
            => GetMouseUp((int)button);

        private static bool GetKeyStatus(KeyList key, KeyStatus status) {
            KeyItem keyItem = keyBoard.GetKeyItem(key);
            return keyItem.status != KeyStatus.None && keyItem.status == status;
        }

        private static bool GetMouseStatus(int index, KeyStatus status) {
            MouseItem keyItem = keyBoard.GetMouseItem(index);
            return keyItem.status != KeyStatus.None && keyItem.status == status;
        }
    }
}