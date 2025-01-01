using System;

namespace Cobilas.GodotEngine.Utility.Serialization {
    /// <summary>The attribute signals which types are targeted for serialization.</summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class SerializedPropertyCustomAttribute : Attribute {
        /// <summary>Types that are serialized.</summary>
        /// <returns>Returns a list of types that are serialized.</returns>
        public Type[] TargetType { get; private set; }
        /// <summary>Creates a new instance of this object.</summary>
        public SerializedPropertyCustomAttribute(params Type[] targetType) {
            TargetType = targetType;
        }
        /// <summary>Creates a new instance of this object.</summary>
        public SerializedPropertyCustomAttribute(Type targetType1, Type targetType2, Type targetType3) {
            TargetType = new Type[] {
                targetType1,
                targetType2,
                targetType3
            };
        }
        /// <summary>Creates a new instance of this object.</summary>
        public SerializedPropertyCustomAttribute(Type targetType1, Type targetType2) {
            TargetType = new Type[] {
                targetType1,
                targetType2
            };
        }
        /// <summary>Creates a new instance of this object.</summary>
        public SerializedPropertyCustomAttribute(Type targetType1) {
            TargetType = new Type[] {
                targetType1
            };
        }
        
        /// <summary>Check if it is target type.</summary>
        /// <param name="type">The type to be checked.</param>
        /// <returns>Returns <c>true</c> if the type is the target of this attribute.</returns>
        public bool IsTargetType(Type type) {
            foreach (Type item in TargetType)
                if (item == type)
                    return true;
            return false;
        }
    }
}
