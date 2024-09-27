using System;

namespace Cobilas.GodotEngine.Utility.Numerics;
public interface IVector : IFormattable {
    float magnitude { get; }
    float sqrMagnitude { get; }
    IVector floor { get; }
    IVector ceil { get; }
    float aspect { get; }
    int AxisCount { get; }
    IVector Normalized { get; }

    float this[int index] { get; set; }

    IVector Round();
    string ToString(string format);
}