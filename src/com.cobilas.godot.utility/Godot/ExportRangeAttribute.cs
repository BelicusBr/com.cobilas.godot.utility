namespace Godot;
/// <summary>Allows you to serialize <seealso cref="int"/> and <seealso cref="float"/> values ​​as a 
/// sliderbar with a minimum and maximum range.</summary>
public class ExportRangeAttribute : ExportAttribute {
    /// <summary>Creates a new instance of this object.</summary>
    public ExportRangeAttribute(int min, int max) : base(PropertyHint.Range, $"{min},{max}") { }
    /// <summary>Creates a new instance of this object.</summary>
    public ExportRangeAttribute(float min, float max) : base(PropertyHint.ExpRange, $"{min},{max}") { }
}
