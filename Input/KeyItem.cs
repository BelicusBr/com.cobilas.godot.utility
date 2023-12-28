using Godot;
using System;

namespace Cobilas.GodotEngine.Utility.Input {
    public struct KeyItem : IEquatable<KeyItem> {
        public KeyList key;
        public bool onDestroy;
        public bool pressDelay;
        public KeyStatus status;

        public static KeyItem Empyt => new KeyItem {
            key = KeyList.Space,
            status = KeyStatus.None
        };

        public bool Equals(KeyItem other)
            => other.status == status && other.key == key;

        public override bool Equals(object obj)
            => obj is KeyItem key && Equals(key);

        public override int GetHashCode()
            => base.GetHashCode();

        public static bool operator ==(KeyItem A, KeyItem B) => A.Equals(B);
        public static bool operator !=(KeyItem A, KeyItem B) => !A.Equals(B);
    }
}