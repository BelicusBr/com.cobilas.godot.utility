using Cobilas.GodotEditor.Utility.Serialization.Properties;

namespace Cobilas.GodotEditor.Utility.Serialization.Interfaces;
/// <summary>Represents a renderable property with serialization capabilities.</summary>
public interface IPropertyRender : ISerializedPropertyManipulation {
	/// <summary>Gets the name of the property.</summary>
	/// <returns>Returns the name of the property.</returns>
	string Name { get; }
	/// <summary>Gets the path of the property.</summary>
	/// <returns>Returns the path of the property.</returns>
	string Path { get; }
	/// <summary>Gets or sets the member item associated with this property.</summary>
	/// <returns>Returns the member item associated with this property.</returns>
	/// <value>Receives the member item to associate with this property.</value>
	MemberItem Member { get; set; }
	/// <summary>Gets or sets the custom property implementation.</summary>
	/// <returns>Returns the custom property implementation.</returns>
	/// <value>Receives the custom property implementation.</value>
	PropertyCustom Custom { get; set; }
	/// <summary>Gets or sets the parent property render.</summary>
	/// <returns>Returns the parent property render, or null if this is a root property.</returns>
	/// <value>Receives the parent property render.</value>
	IPropertyRender? Parent { get; set; }
	/// <summary>Pops the property value from the render stack.</summary>
	/// <param name="obj">The object to pop, or null to pop without setting a value.</param>
	public void Pop(object? obj);
}