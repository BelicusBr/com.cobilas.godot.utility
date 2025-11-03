namespace Cobilas.GodotEditor.Utility.Serialization.Interfaces;
/// <summary>Represents a renderable object with an identifier.</summary>
public interface IObjectRender : IPropertyRender {
	/// <summary>Gets the unique identifier for the object render.</summary>
	/// <returns>Returns the unique identifier of the object render.</returns>
	uint ID { get; }
}