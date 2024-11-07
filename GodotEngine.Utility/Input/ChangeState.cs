namespace Cobilas.GodotEngine.Utility.Input;
[System.Flags]
internal enum ChangeState : ushort {
    None = 0,
    Changed = 2,
    Destroy = 4,
    Delay = 8,
    Press = 16,
    C_Index = 32,
    C_Scroll = 64,
    D_Index = 128,
    D_Scroll = 256
}
