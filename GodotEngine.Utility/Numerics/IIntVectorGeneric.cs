using System;

namespace Cobilas.GodotEngine.Utility.Numerics;
public interface IIntVectorGeneric<TVector> : IEquatable<TVector>, IIntVector where TVector : IIntVector {
    new TVector floorToInt { get; }
    new TVector ceilToInt { get; }
    
    new TVector RoundToInt();
}