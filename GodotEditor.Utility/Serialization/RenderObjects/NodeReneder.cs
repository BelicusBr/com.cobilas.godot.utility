using Godot;
using System;
using System.Reflection;
using Cobilas.Collections;
using System.Collections.Generic;
using Cobilas.GodotEngine.Utility;
using Cobilas.GodotEditor.Utility.Serialization.Properties;
using Cobilas.GodotEditor.Utility.Serialization.Interfaces;

namespace Cobilas.GodotEditor.Utility.Serialization.RenderObjects;
/// <summary>Represents a renderable node object for inspector serialization.</summary>
public class NodeReneder : IObjectRender {
	private Node? node;
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
			if (node is null) throw new ArgumentNullException(nameof(node));
			string path = node.IsInsideTree() ? node.GetPath() : string.Empty;
			if (GDFeature.HasEditor && node.IsInsideTree()) {
				SceneTree? sct = node.IsInsideTree() ? node.GetTree() : null;
				if (sct is not null)
					if (sct.EditedSceneRoot is not null) {
						string editPath = sct.EditedSceneRoot.GetPath();
						editPath = editPath.Remove(editPath.LastIndexOf('/'));
						path = $"/root{path.Replace(editPath, string.Empty)}";
					}
			}
			return path.Hash();
		}
	}
	string IPropertyRender.Path => throw new NotImplementedException();
	MemberItem IPropertyRender.Member { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	/// <summary>Initializes a new instance of the NodeReneder class.</summary>
	/// <param name="node">The node to render in the inspector.</param>
	/// <exception cref="ArgumentNullException">Thrown when node is null.</exception>
	public NodeReneder(Node? node) {
		if (node is null) 
			throw new ArgumentNullException(nameof(node));
		Parent = null;

		foreach (KeyValuePair<string, MemberInfo[]> item in PropertyRender.GetPropertyRender(node.GetType())!) {
			PropertyRender[]? renders = [];
			foreach (MemberInfo item1 in item.Value)
				ArrayManipulation.Add(new PropertyRender(this, new() {
					Parent = node,
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
				if (result is not null)
					return result;
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
		if (obj is null) throw new ArgumentNullException(nameof(obj), $"name:{Name}");
		node = (Node?)obj;
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
