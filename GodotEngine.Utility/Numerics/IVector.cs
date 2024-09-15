using System;

namespace Cobilas.GodotEngine.Utility.Numerics;
public interface IVector : IFormattable {
#pragma warning disable IDE1006 // Estilos de Nomenclatura
    float magnitude { get; }
    float sqrMagnitude { get; }
    IVector floor { get; }
    IVector ceil { get; }
    float aspect { get; }
    int AxisCount { get; }
#pragma warning restore IDE1006 // Estilos de Nomenclatura
    IVector Normalized { get; }

    float this[int index] { get; set; }

    string ToString(string format);
}

public interface IVector<TVector> : IEquatable<TVector>, IVector where TVector : IVector {
    new TVector Normalized { get; }
#pragma warning disable IDE1006 // Estilos de Nomenclatura
    new TVector floor { get; }
    new TVector ceil { get; }
#pragma warning restore IDE1006 // Estilos de Nomenclatura
}