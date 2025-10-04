using System.Collections.Generic;

namespace Cobilas.GodotEngine.Utility.Input;

internal static class PeripheralItemListExtension {
    internal static PeripheralItem GetKeyCode(this List<PeripheralItem>? pl, KeyCode scancode) {
        if (pl is not null) {
            PeripheralItem result = pl.Find(p => p.KeyCode == scancode);
            if (result != default(PeripheralItem)) return result;
        }
        return PeripheralItem.Empty;
    }

    internal static PeripheralItem GetKeyCode(this List<PeripheralItem>? pl, ulong scancode)
        => GetKeyCode(pl, (KeyCode)scancode);

    internal static void SetKeyCode(this List<PeripheralItem>? pl, KeyCode scancode, PeripheralItem peripheral) {
        if (pl is null || scancode == KeyCode.None) return;
        int index = pl.FindIndex(p => p.KeyCode == scancode);
        if (index != -1) {
            pl[index] = peripheral;
            return;
        }
        pl.Add(peripheral);
    }

    internal static void SetKeyCode(this List<PeripheralItem>? pl, ulong scancode, PeripheralItem peripheral)
        => SetKeyCode(pl, (KeyCode)scancode, peripheral);
}
