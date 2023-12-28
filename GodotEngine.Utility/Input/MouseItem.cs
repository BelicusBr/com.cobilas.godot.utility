using System;

namespace Cobilas.GodotEngine.Utility.Input {
    public struct MouseItem : IEquatable<MouseItem> {
        public int Index;
        public bool onDestroy;
        public bool pressDelay;
        public KeyStatus status;

        public static MouseItem Empyt => new MouseItem {
            Index = -1,
            status = KeyStatus.None
        };
        
        public bool Equals(MouseItem other)
            => other.status == status && other.Index == Index;

        public override bool Equals(object obj)
            => obj is MouseItem key && Equals(key);

        public override int GetHashCode()
            => base.GetHashCode();

        public static bool operator ==(MouseItem A, MouseItem B) => A.Equals(B);
        public static bool operator !=(MouseItem A, MouseItem B) => !A.Equals(B);
    }
}