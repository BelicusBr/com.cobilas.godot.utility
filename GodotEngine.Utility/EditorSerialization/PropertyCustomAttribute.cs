using System;

namespace Cobilas.GodotEngine.Utility.EditorSerialization;
/// <summary>This attribute indicates to <seealso cref="BuildSerialization"/> 
/// that the <seealso cref="PropertyCustom"/> class belongs to a certain type.</summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
public sealed class PropertyCustomAttribute : Attribute {
    /// <summary>The target type to be customized.</summary>
    /// <returns>Returns the target type.</returns>
    public Type Target { get; private set; }
    /// <summary>Allows child classes to use this PropertyCustom.</summary>
    /// <returns>Returns <c>true</c> when child classes use this PropertyCustom.</returns>
    public bool UseForChildren { get; private set; }
    /// <summary>Creates a new instance of this object.</summary>
    public PropertyCustomAttribute(Type target, bool useForChildren) {
        Target = target;
        UseForChildren = useForChildren;
    }
    /// <summary>Creates a new instance of this object.</summary>
    public PropertyCustomAttribute(Type target) : this(target, false) {}
}
