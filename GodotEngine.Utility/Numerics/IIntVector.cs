using System;

namespace Cobilas.GodotEngine.Utility.Numerics;
public interface IIntVector : IFormattable {
    float magnitude { get; }
    int sqrMagnitude { get; }
    IIntVector floorToInt { get; }
    IIntVector ceilToInt { get; }
    float aspect { get; }
    int AxisCount { get; }

    int this[int index] { get; set; }

    IIntVector RoundToInt();
    string ToString(string format);
}