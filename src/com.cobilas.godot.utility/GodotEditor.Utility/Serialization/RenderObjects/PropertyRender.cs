using Godot;
using System;
using System.Reflection;
using Cobilas.Collections;
using System.Collections.Generic;
using Cobilas.GodotEditor.Utility.Serialization.Interfaces;
using Cobilas.GodotEditor.Utility.Serialization.Properties;
using Cobilas.GodotEngine.Utility;

namespace Cobilas.GodotEditor.Utility.Serialization.RenderObjects;
/// <summary>Represents a renderable property for inspector serialization.</summary>
public class PropertyRender : IPropertyRender {
	internal const BindingFlags members = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
	private readonly KeyValuePair<string, PropertyRender[]>[]? renders;
	/// <inheritdoc/>
	public string Path => GetPath(this).Trim('/');
	/// <inheritdoc/>
	public IPropertyRender? Parent { get; set; } = null;
	/// <inheritdoc/>
	public MemberItem Member { get; set; } = MemberItem.Null;
	/// <inheritdoc/>
	public PropertyCustom Custom { get; set; } = SPCNull.Null;
	/// <inheritdoc/>
	public string Name => Member is null ? string.Empty : Member.Name;
	/// <summary>Initializes a new instance of the PropertyRender class.</summary>
	/// <param name="parent">The parent property render.</param>
	/// <param name="member">The member item to render.</param>
	/// <exception cref="ArgumentNullException">Thrown when member is null.</exception>
	public PropertyRender(IPropertyRender? parent, MemberItem? member) {
		if (member is null) throw new ArgumentNullException(nameof(member));
		Parent = parent;
		Member = member;
		Type type = member.TypeMenber;
		if (!IsSpecialType(type, out PropertyCustom? custom)) {
			BuildSerialization.IsPropertyCustom(type, out custom);
			foreach (KeyValuePair<string, MemberInfo[]> item in GetPropertyRender(type)!) {
				PropertyRender[]? renders = [];
				foreach (MemberInfo item1 in item.Value)
					ArrayManipulation.Add(new PropertyRender(this, new() {
						Parent = member.Value,
						Menber = item1
					}), ref renders);
				ArrayManipulation.Add(new KeyValuePair<string, PropertyRender[]>(item.Key, renders ?? []), ref this.renders);
			}
		}
		Custom = custom ?? SPCNull.Null;
	}
	/// <inheritdoc/>
	public object? Get(string? propertyName) {
		if (Custom is not null && Custom is not SPCNull) {
			Custom.Member = Member;
			Custom.PropertyPath = Path;
			Custom.PropertyRender = this;
			if (Member.IsSaveCache && Custom.VerifyPropertyName(propertyName)) {
				object? res = Custom.CacheValueToObject(propertyName,
					PropertyRenderCache.GetValue(GetID(this), propertyName));
				return res;
			}
			if (Custom.VerifyPropertyName(propertyName))
				return Custom.Get(propertyName);
			return null;
		}
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
		if (Custom is null || Custom is SPCNull) {
			foreach (KeyValuePair<string, PropertyRender[]> item in renders ?? []) {
				foreach (PropertyRender item1 in item.Value)
					ArrayManipulation.Add(item1.GetPropertyList(), ref items);
			}
		} else {
			Custom.Member = Member;
			Custom.PropertyPath = Path;
			Custom.PropertyRender = this;
			ArrayManipulation.Add(Custom.GetPropertyList(), ref items);
		}
		return items ?? [];
	}
	/// <inheritdoc/>
	public bool Set(string? propertyName, object? value) {
		if (Custom is not null && Custom is not SPCNull) {
			if (Member.Value is null) 
				Member.Value = Member.TypeMenber.Activator();
			Custom.Member = Member;
			Custom.PropertyPath = Path;
			Custom.PropertyRender = this;
			if (GDFeature.HasEditor) {
				if (Custom.VerifyPropertyName(propertyName)) {
					if (Member.IsSaveCache)
						PropertyRenderCache.SetValue(GetID(this), propertyName,
							Custom.ObjectToCacheValue(propertyName, value));
					return Custom.Set(propertyName, value);
				}
			} else
				if (Custom.VerifyPropertyName(propertyName))
					return Custom.Set(propertyName, value);
			return false;
		}
		foreach (KeyValuePair<string, PropertyRender[]> item in renders ?? [])
			foreach (PropertyRender item1 in item.Value)
				if (item1.Set(propertyName, value))
					return true;
		return false;
	}
	/// <inheritdoc/>
	public void Pop(object? obj) {
		if (Member is null || Member == MemberItem.Null)
			throw new NullReferenceException($"The property '{nameof(Member)}' is null!");
		obj ??= Member.TypeMenber.Activator();
		if (renders is not null)
			foreach (KeyValuePair<string, PropertyRender[]> item in renders)
				foreach (PropertyRender item2 in item.Value) {
					if (item2.Member is null) 
						throw new NullReferenceException($"The '{nameof(Member)}' property of the child member is null!");
					item2.Member.Parent = obj;
					item2.Pop(item2.Member.Value);
				}
	}

	internal static KeyValuePair<string, MemberInfo[]>[]? GetPropertyRender(Type type) {
		string typeName = type.Name;
		MemberInfo[]? r_members = [];
		foreach (MemberInfo? item in type.GetMembers(members))
			if (IsShowPropertyType(item))
				ArrayManipulation.Add(item, ref r_members);
		if (type.BaseType is null) return new KeyValuePair<string, MemberInfo[]>[] { new(typeName, r_members ?? []) };
		return ArrayManipulation.Add(GetPropertyRender(type.BaseType), new KeyValuePair<string, MemberInfo[]>[] { new(typeName, r_members ?? []) });
	}

	internal static string GetPath(IPropertyRender? render) {
		if (render is null) return string.Empty;
		return $"{GetPath(render.Parent)}/{render.Name}";
	}

	internal static uint GetID(IPropertyRender? render) {
		if (render is IObjectRender obj) return obj.ID;
		else if (render is null) throw new ArgumentNullException(nameof(render));
		return GetID(render.Parent);
	}

	private static bool IsShowPropertyType(MemberInfo info)
		=> (IsPrimitiveType(info) || IsEnumType(info) || 
				IsSerializableType(info) || IsGodotType(info)) &&
				IsSerializeFieldType(info);

	private static bool IsSerializableType(MemberInfo info)
		=> info switch {
			FieldInfo fdf => fdf.FieldType.GetAttribute<SerializableAttribute>() is not null,
			PropertyInfo ptf => ptf.PropertyType.GetAttribute<SerializableAttribute>() is not null,
			_ => false
		};

	private static bool IsSerializeFieldType(MemberInfo info)
		=> info switch {
			FieldInfo fdf => fdf.GetCustomAttribute<ShowPropertyAttribute>() is not null,
			PropertyInfo ptf => ptf.GetCustomAttribute<ShowPropertyAttribute>() is not null,
			_ => false
		};

	private static bool IsGodotType(MemberInfo info)
		=> info switch {
			FieldInfo fdf => IsGodotType(fdf.FieldType),
			PropertyInfo ptf => IsGodotType(ptf.PropertyType),
			_ => false
		};

	private static bool IsGodotType(Type type)
		=> type.CompareType(
				typeof(Godot.NodePath),
				typeof(Godot.Resource)
			);

	private static bool IsSpecialType(Type type, out PropertyCustom custom) {
		if (IsPrimitiveType(type)) {
			custom = new PrimitiveTypeCustom();
			return true;
		} else if (IsEnumType(type)) {
			custom = new EnumCustom();
			return true;
		} else if (type.CompareType<Godot.NodePath>()) {
			custom = new NodePathCustom();
			return true;
		} else if (type.CompareTypeAndSubType<Godot.Resource>()) {
			custom = new ResourceCustom();
			return true;
		}
		custom = SPCNull.Null;
		return false;
	}

	private static bool IsEnumType(MemberInfo info)
		=> info switch {
			FieldInfo fdf => fdf.FieldType.CompareTypeAndSubType<Enum>(),
			PropertyInfo ptf => ptf.PropertyType.CompareTypeAndSubType<Enum>(),
			_ => false
		};

	private static bool IsPrimitiveType(MemberInfo info)
		=> info switch {
			FieldInfo fdf => IsPrimitiveType(fdf.FieldType),
			PropertyInfo ptf => IsPrimitiveType(ptf.PropertyType),
			_ => false
		};

	private static bool IsPrimitiveType(Type type)
		=> type.CompareType(typeof(string), typeof(bool), typeof(sbyte), typeof(byte), typeof(ushort), typeof(short),
			typeof(uint), typeof(int), typeof(ulong), typeof(long), typeof(float), typeof(double));
}
