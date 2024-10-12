using System;

namespace Cobilas.GodotEngine.Utility.Numerics;
/// <inheritdoc cref="IVector"/>
public interface IVectorGeneric<TVector> : IEquatable<TVector>, IVector where TVector : IVector {
    /// <inheritdoc cref="IVector.Normalized"/>
    new TVector Normalized { get; }
    /// <inheritdoc cref="IVector.floor"/>
    new TVector floor { get; }
    /// <inheritdoc cref="IVector.ceil"/>
    new TVector ceil { get; }
    /// <inheritdoc cref="IVector.Round"/>
    new TVector Round();
}