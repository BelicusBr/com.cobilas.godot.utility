using System.Reflection;
using Cobilas.GodotEditor.Utility.Serialization.Interfaces;

namespace Cobilas.GodotEditor.Utility.Serialization.Properties;
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
	/// <summary>Gets or sets the property render associated with this custom property.</summary>
	/// <returns>Returns the property render associated with this custom property.</returns>
	/// <value>Receives the property render to associate with this custom property.</value>
	public abstract IPropertyRender? PropertyRender { get; set; }
    /// <summary>The serialization attribute of the property.</summary>
    /// <returns>Returns the serialization attribute that tells the editor how to display the property.</returns>
    public SerializeFieldAttribute Attribute => Member.Menber.GetCustomAttribute<SerializeFieldAttribute>();
	/// <summary>Gets or sets the value of the property and propagates changes through the render hierarchy.</summary>
	/// <returns>Returns the current value of the property.</returns>
	/// <value>Receives the new value for the property and propagates it through the render hierarchy.</value>
	/// <remarks>
	/// When setting a value, it automatically propagates the change to parent property renders
	/// to maintain consistency in the serialization hierarchy.
	/// </remarks>
	public object? Value { 
        get => Member.Value; 
        set {
            Member.Value = value;
            if (PropertyRender is not null)
                SpreadValue(PropertyRender.Parent, Member.Parent);
        } 
    }
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
	/// <summary>Converts an object value to a string representation for caching.</summary>
	/// <param name="propertyName">The name of the property.</param>
	/// <param name="value">The object value to be converted.</param>
	/// <returns>Returns the string representation of the value for caching.</returns>
	public abstract string? ObjectToCacheValue(string? propertyName, object? value);
	/// <summary>Verifies if the given property name is valid for this custom property.</summary>
	/// <param name="propertyName">The property name to verify.</param>
	/// <returns>Returns <c>true</c> when the property name is valid.</returns>
	public abstract bool VerifyPropertyName(string? propertyName);

    private static void SpreadValue(IPropertyRender? PropertyRender, object? value) {
        if (PropertyRender is null) return;
        else if (PropertyRender is IObjectRender) return;
        PropertyRender.Member.Value = value;
        SpreadValue(PropertyRender.Parent, PropertyRender.Member.Parent);
    }
}
