namespace Cobilas.GodotEngine.Utility.Serialization;
/// <summary>The interface allows property manipulation.</summary>
public interface ISerializedPropertyManipulation {
    /// <summary>The method allows you to get the value of the property.</summary>
    /// <param name="propertyName">The name of the property.</param>
    /// <returns>Returns the value of the property.</returns>
    object? Get(string? propertyName);
    /// <summary>The method allows you to set the property value.</summary>
    /// <param name="propertyName">The name of the property.</param>
    /// <param name="value">The value that will be set in the property.</param>
    /// <returns>Returns <c>true</c> when the property is changed.</returns>
    bool Set(string? propertyName, object? value);
    /// <summary>The method allows you to create a property serialization list.</summary>
    /// <returns>Returns a property serialization list.</returns>
    PropertyItem[] GetPropertyList();
}
