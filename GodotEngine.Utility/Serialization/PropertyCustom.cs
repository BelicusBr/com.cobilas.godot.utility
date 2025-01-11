namespace Cobilas.GodotEngine.Utility.Serialization;

public abstract class PropertyCustom : ISerializedPropertyManipulation {
    public abstract bool IsHide { get; set; }
    public abstract string PropertyPath { get; set; }
    public abstract MemberItem Member { get; set; }

    public abstract object? Get(string? propertyName);
    public abstract PropertyItem[] GetPropertyList();
    public abstract bool Set(string? propertyName, object? value);
    public abstract object? CacheValueToObject(string? propertyName, string? value);
}
