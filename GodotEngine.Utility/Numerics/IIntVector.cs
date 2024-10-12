using System;

namespace Cobilas.GodotEngine.Utility.Numerics;
/// <inheritdoc cref="IVector"/>
public interface IIntVector : IFormattable {
    /// <inheritdoc cref="IVector.magnitude"/>
    float magnitude { get; }
    /// <inheritdoc cref="IVector.sqrMagnitude"/>
    int sqrMagnitude { get; }
    /// <inheritdoc cref="IVector.aspect"/>
    float aspect { get; }
    /// <inheritdoc cref="IVector.AxisCount"/>
    int AxisCount { get; }
    /// <inheritdoc cref="IVector.this"/>
    int this[int index] { get; set; }

    /// <inheritdoc/>
    string ToString(string format);
}