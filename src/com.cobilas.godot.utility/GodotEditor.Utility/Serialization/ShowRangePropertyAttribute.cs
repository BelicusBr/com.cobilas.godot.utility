using Cobilas.GodotEditor.Utility.Serialization.Hints;

namespace Cobilas.GodotEditor.Utility.Serialization;
/// <summary>The attribute allows you to display a field or property in the editor in range form.</summary>
public class ShowRangePropertyAttribute : ShowPropertyAttribute {
#pragma warning disable CS1573 // O parâmetro não tem nenhuma tag param correspondente no comentário XML (mas outros parâmetros têm)
    /// <inheritdoc cref="ShowPropertyAttribute.ShowPropertyAttribute(bool)"/>
    /// <param name="min">The minimum value of the range.</param>
    /// <param name="max">The maximum value of the range.</param>
    /// <param name="step">The increment value of the range.</param>
    public ShowRangePropertyAttribute(bool saveInCache, int min, int max, int step) :
        base(saveInCache, new RangeHint(min, max, step)) {}
#pragma warning restore CS1573 // O parâmetro não tem nenhuma tag param correspondente no comentário XML (mas outros parâmetros têm)
    /// <inheritdoc cref="ShowRangePropertyAttribute.ShowRangePropertyAttribute(bool, int, int, int)"/>
    public ShowRangePropertyAttribute(bool saveInCache, float min, float max, float step) : 
        base(saveInCache, new RangeHint(min, max, step)) {}
    /// <inheritdoc cref="ShowRangePropertyAttribute.ShowRangePropertyAttribute(bool, int, int, int)"/>
    public ShowRangePropertyAttribute(bool saveInCache, int min, int max) :
        base(saveInCache, new RangeHint(min, max, 1)) {}
    /// <inheritdoc cref="ShowRangePropertyAttribute.ShowRangePropertyAttribute(bool, int, int, int)"/>
    public ShowRangePropertyAttribute(bool saveInCache, float min, float max) : 
        base(saveInCache, new RangeHint(min, max, .01f)) {}
    /// <inheritdoc cref="ShowRangePropertyAttribute.ShowRangePropertyAttribute(bool, int, int, int)"/>
    public ShowRangePropertyAttribute(int min, int max, int step) : 
        base(false, new RangeHint(min, max, step)) {}
    /// <inheritdoc cref="ShowRangePropertyAttribute.ShowRangePropertyAttribute(bool, int, int, int)"/>
    public ShowRangePropertyAttribute(float min, float max, float step) : 
        base(false, new RangeHint(min, max, step)) {}
    /// <inheritdoc cref="ShowRangePropertyAttribute.ShowRangePropertyAttribute(bool, int, int, int)"/>
    public ShowRangePropertyAttribute(int min, int max) : 
        base(false, new RangeHint(min, max, 1)) {}
    /// <inheritdoc cref="ShowRangePropertyAttribute.ShowRangePropertyAttribute(bool, int, int, int)"/>
    public ShowRangePropertyAttribute(float min, float max) : 
        base(false, new RangeHint(min, max, .01f)) {}
}
