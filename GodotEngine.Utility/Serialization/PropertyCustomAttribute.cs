using System;

namespace Cobilas.GodotEngine.Utility.Serialization;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
public sealed class PropertyCustomAttribute : Attribute {
    public Type Target { get; private set; }
    
    public PropertyCustomAttribute(Type target)
    {
        Target = target;
    }
}
