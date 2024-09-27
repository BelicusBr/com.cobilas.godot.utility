using System;

namespace Cobilas.GodotEngine.Utility.Numerics;
public interface IVectorGeneric<TVector> : IEquatable<TVector>, IVector where TVector : IVector {
    new TVector Normalized { get; }
    new TVector floor { get; }
    new TVector ceil { get; }

    new TVector Round();
}