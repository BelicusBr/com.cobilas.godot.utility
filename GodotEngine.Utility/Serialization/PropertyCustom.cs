namespace Cobilas.GodotEngine.Utility.Serialization;
/// <summary>Base class for custom properties.</summary>
public abstract class PropertyCustom : ISerializedPropertyManipulation {
    /// <inheritdoc cref="MemberItem.IsHide"/>
    public abstract bool IsHide { get; }
    /// <summary>The path of the property.</summary>
    /// <value>Sets the path of the property.</value>
    /// <returns>Returns the path of the property.</returns>
    public abstract string PropertyPath { get; set; }
    /// <summary>The property to be manipulated.</summary>
    /// <value>Defines a new member.</value>
    /// <returns>Returns the manipulated member.</returns>
    public abstract MemberItem Member { get; set; }
    /// <inheritdoc/>
    public abstract object? Get(string? propertyName);
    /// <inheritdoc/>
    public abstract PropertyItem[] GetPropertyList();
    /// <inheritdoc/>
    public abstract bool Set(string? propertyName, object? value);
    /// <summary>Converts the value obtained from the cache to a specific value.</summary>
    /// <param name="propertyName">The name of the property.</param>
    /// <param name="value">The value to be converted.</param>
    /// <returns>Returns the already converted value.</returns>
    public abstract object? CacheValueToObject(string? propertyName, string? value);
}
