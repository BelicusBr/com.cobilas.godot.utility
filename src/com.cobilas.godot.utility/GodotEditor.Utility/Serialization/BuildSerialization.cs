using Godot;
using System;
using System.Reflection;
using Cobilas.Collections;
using System.Collections.Generic;
using Cobilas.GodotEditor.Utility.Serialization.Interfaces;
using Cobilas.GodotEditor.Utility.Serialization.Properties;
using Cobilas.GodotEditor.Utility.Serialization.RenderObjects;

namespace Cobilas.GodotEditor.Utility.Serialization;
/// <summary>Class allows to build a serialization list of properties of a node class.</summary>
public static class BuildSerialization {
	private const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
	private static readonly Dictionary<Type, IPropertyRender> propris = [];
	private static readonly Dictionary<Type, PropertyCustom> propertyCustom = [];
	/// <summary>Gets the property list for serialization of the specified Godot object.</summary>
	/// <param name="obj">The Godot object to get the property list for.</param>
	/// <returns>Returns an array of property items for serialization, or null if the object is not supported.</returns>
	public static PropertyItem[]? GetPropertyList(Godot.Object? obj) {
		ISerializedPropertyManipulation? manipulation = BuildObjectRender(obj);
		if (manipulation is null) return null;
		return manipulation.GetPropertyList();
	}
	/// <summary>Gets the value of a property from the specified Godot object.</summary>
	/// <param name="obj">The Godot object to get the property value from.</param>
	/// <param name="propertyName">The name of the property to retrieve.</param>
	/// <returns>Returns the property value, or null if the object or property is not found.</returns>
	public static object? GetValue(Godot.Object? obj, string? propertyName) {
		ISerializedPropertyManipulation? manipulation = BuildObjectRender(obj);
		if (manipulation is null) return null;
		return manipulation.Get(propertyName);
	}
	/// <summary>Sets the value of a property on the specified Godot object.</summary>
	/// <param name="obj">The Godot object to set the property value on.</param>
	/// <param name="propertyName">The name of the property to set.</param>
	/// <param name="value">The value to set for the property.</param>
	/// <returns>Returns true if the value was successfully set, false otherwise.</returns>
	/// <remarks>In standalone mode, the value setting is deferred until the next Ready event.</remarks>
	public static bool SetValue(Godot.Object? obj, string? propertyName, object? value) {
		ISerializedPropertyManipulation? manipulation = BuildObjectRender(obj);
		if (manipulation is null) return false;
		return manipulation.Set(propertyName, value);
	}
	/// <summary>Builds a property manipulation interface for the specified Godot object.</summary>
	/// <param name="obj">The Godot object to build the render for.</param>
	/// <returns>An <see cref="ISerializedPropertyManipulation"/> interface for the object, or null if unsupported.</returns>
	/// <remarks>
	/// This method creates and caches property renderers for efficient property manipulation.
	/// Supported object types include <see cref="Node"/> and <see cref="Resource"/>.
	/// </remarks>
	public static ISerializedPropertyManipulation? BuildObjectRender(Godot.Object? obj)
		=> obj switch {
			Node nd => BuildRender(nd, obj => new NodeReneder(obj)),
			Resource rc => BuildRender(rc, obj => new ResourceRender(obj)),
			_ => null
		};

	private static ISerializedPropertyManipulation? BuildRender<T>(T? obj, Func<T, IPropertyRender> renderFactory) where T : Godot.Object {
		if (obj is null) return null;

		Type objectType = obj.GetType();

		if (propris.TryGetValue(objectType, out IPropertyRender render)) {
			render.Pop(obj);
			return render;
		}

		IPropertyRender newRenderer = renderFactory(obj);
		propris.Add(objectType, newRenderer);
		newRenderer.Pop(obj);
		return newRenderer;
	}
	/// <summary>Checks if the type has a custom property handler and retrieves it.</summary>
	/// <param name="type">The type to check.</param>
	/// <param name="custom">The custom property handler if found.</param>
	/// <returns>Returns <c>true</c> when the type has a custom property handler.</returns>
	public static bool IsPropertyCustom(Type type, out PropertyCustom? custom) {
		custom = null;
		if (IsPropertyCustom(type)) {
			custom = propertyCustom[type];
			return true;
		}
		return false;
	}
	/// <summary>Checks if the type has a <seealso cref="PropertyCustom"/>.</summary>
	/// <param name="type">The type to check.</param>
	/// <returns>Returns <c>true</c> when the type has a <seealso cref="PropertyCustom"/>.</returns>
	public static bool IsPropertyCustom(Type type) {
		if (propertyCustom.ContainsKey(type)) return true;
		Type[] types = TypeUtilitarian.GetTypes();
		foreach (Type item in types) {
			PropertyCustomAttribute attribute = item.GetAttribute<PropertyCustomAttribute>();
			if (attribute is null) continue;
			else if (!attribute.UseForChildren && attribute.Target != type) continue;
			else if (attribute.UseForChildren && !type.CompareTypeAndSubType(attribute.Target)) continue;
			propertyCustom.Add(type, item.Activator<PropertyCustom>());
			return true;
		}
		return false;
	}

	private static MemberInfo[]? GetMembers(Type? type) {
		if (type is null) return Array.Empty<MemberInfo>();
		return ArrayManipulation.Add(type.GetMembers(flags), GetMembers(type.BaseType));
	}
}