using System;

namespace Cobilas.GodotEngine.Utility.Serialization;
/// <summary>This attribute indicates to <seealso cref="BuildSerialization"/> 
/// that the <seealso cref="PropertyCustom"/> class belongs to a certain type.</summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
public sealed class PropertyCustomAttribute : Attribute {
    /// <summary>The target type to be customized.</summary>
    /// <returns>Returns the target type.</returns>
    public Type Target { get; private set; }
    /// <summary>Creates a new instance of this object.</summary>
    public PropertyCustomAttribute(Type target)
    {
        Target = target;
    }
}
