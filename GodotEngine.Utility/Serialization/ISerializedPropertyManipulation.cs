namespace Cobilas.GodotEngine.Utility.Serialization;

public interface ISerializedPropertyManipulation {
    object? Get(string? propertyName);
    bool Set(string? propertyName, object? value);
    PropertyItem[] GetPropertyList();
}
