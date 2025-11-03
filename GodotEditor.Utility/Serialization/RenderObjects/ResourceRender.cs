using Godot;
using System;
using System.Reflection;
using Cobilas.Collections;
using System.Collections.Generic;
using Cobilas.GodotEditor.Utility.Serialization.Properties;
using Cobilas.GodotEditor.Utility.Serialization.Interfaces;

namespace Cobilas.GodotEditor.Utility.Serialization.RenderObjects;
/// <summary>Represents a renderable resource object for inspector serialization.</summary>
public class ResourceRender : IObjectRender {
	private Resource? resource;
	private readonly KeyValuePair<string, PropertyRender[]>[]? renders;
	/// <inheritdoc/>
	public string Name { get; } = string.Empty;
	/// <inheritdoc/>
	public IPropertyRender? Parent { get; set; } = null;
	/// <inheritdoc/>
	public PropertyCustom Custom { get; set; } = SPCNull.Null;
	/// <inheritdoc/>
	public uint ID {
		get {
			if (resource is null) throw new ArgumentNullException(nameof(resource));
			return resource.ResourcePath.Hash();
		}
	}
	string IPropertyRender.Path => throw new NotImplementedException();
	MemberItem IPropertyRender.Member { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	/// <summary>Initializes a new instance of the ResourceRender class.</summary>
	/// <param name="resource">The resource to render in the inspector.</param>
	/// <exception cref="ArgumentNullException">Thrown when resource is null.</exception>
	public ResourceRender(Resource? resource) {
		if (resource is null) throw new ArgumentNullException(nameof(resource));
		Parent = null;

		foreach (KeyValuePair<string, MemberInfo[]> item in PropertyRender.GetPropertyRender(resource.GetType())!) {
			PropertyRender[]? renders = [];
			foreach (MemberInfo item1 in item.Value)
				ArrayManipulation.Add(new PropertyRender(this, new() {
					Parent = resource,
					Menber = item1
				}), ref renders);
			ArrayManipulation.Add(new KeyValuePair<string, PropertyRender[]>(item.Key, renders ?? []), ref this.renders);
		}
	}
	/// <inheritdoc/>
	public object? Get(string? propertyName) {
		foreach (KeyValuePair<string, PropertyRender[]> item in renders ?? [])
			foreach (PropertyRender item1 in item.Value) {
				object? result = item1.Get(propertyName);
				if (result is not null) return result;
			}
		return null;
	}
	/// <inheritdoc/>
	public PropertyItem[] GetPropertyList() {
		PropertyItem[]? items = [];
		foreach (KeyValuePair<string, PropertyRender[]> item in renders ?? []) {
			if (item.Value.Length == 0) continue;
			PropertyItem property = new(item.Key, Variant.Type.Nil,
				usage: PropertyUsageFlags.Category | PropertyUsageFlags.Editor);
			ArrayManipulation.Add(property, ref items);
			foreach (PropertyRender item1 in item.Value)
				ArrayManipulation.Add(item1.GetPropertyList(), ref items);
		}
		return items ?? [];
	}
	/// <inheritdoc/>
	public bool Set(string? propertyName, object? value) {
		foreach (KeyValuePair<string, PropertyRender[]> item in renders ?? [])
			foreach (PropertyRender item1 in item.Value)
				if (item1.Set(propertyName, value))
					return true;
		return false;
	}
	/// <inheritdoc/>
	public void Pop(object? obj) {
		if (obj is null) 
			throw new ArgumentNullException(nameof(obj), $"name:{Name}");
		resource = (Resource?)obj;
		if (renders is not null)
			foreach (KeyValuePair<string, PropertyRender[]> item in renders)
				foreach (PropertyRender item2 in item.Value) {
					if (item2.Member is null)
						throw new NullReferenceException("The 'Member' property of the child member is null!");
					item2.Member.Parent = obj;
					item2.Pop(item2.Member.Value);
				}
	}
}
