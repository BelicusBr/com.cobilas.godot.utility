using Godot;
using System;
using System.Text;
using System.Reflection;
using Cobilas.Collections;
using System.Collections.Generic;
using Cobilas.IO.Serialization.Json;

using IOPath = System.IO.Path;
using IOFile = System.IO.File;
using IODirectory = System.IO.Directory;

namespace Cobilas.GodotEngine.Utility.Serialization {
    /// <summary>Allows a custom serializer for a specific type.</summary>
    public abstract class SerializedPropertyCustom {
        /// <summary>Allows you to mark the properties that were loaded from the cache.</summary>
        protected Dictionary<string, bool> propMark = [];
        /// <summary>The custom class member.</summary>
        /// <returns>Returns the custom class member.</returns>
        /// <value>Receives the custom class member.</value>
        public MemberItem Member { get; set; } = MemberItem.Null;
        /// <summary>The custom property path.</summary>
        /// <returns>Returns the path of the custom property.</returns>
        /// <value>Receives the custom property path.</value>
        public string PropertyPath { get; set; } = string.Empty;
        /// <summary>Checks if the field or property is cached.</summary>
        /// <returns>Returns <c>true</c> if the field or property is cached.</returns>
        public bool IsSaveCache {
            get {
                SerializeFieldAttribute res = Member.Menber.GetCustomAttribute<SerializeFieldAttribute>();
                if (res is null) return false;
                return res.SaveInCache;
            }
        }
        /// <summary>Checks if the field or property was loaded from the cache.</summary>
        /// <returns>Returns <c>true</c> if the field or property was loaded from the cache.</returns>
        public bool LoadCache {
            get {
                foreach (KeyValuePair<string, bool> item in propMark)
                    if (!item.Value)
                        return false;
                return true;
            }
        }

        private static string cacheFolderPath => IOPath.Combine(AppDomain.CurrentDomain.BaseDirectory, "cache");

        public abstract void InitPropMark();
        public abstract PropertyItem[] GetPropertyList();
        public abstract object Get(string propertyPath);
        public abstract bool Set(string propertyPath, object Value);
        public abstract object Convert(string propertyPath, object Value);
        public virtual bool IsPropertyPath(string propertyPath) => IsPropertyPath(propertyPath, out _);

        protected PropertyUsageFlags GetPropertyUsageFlags() 
            => Member.Menber.GetCustomAttribute<SerializeFieldAttribute>().Flags;

        protected bool IsPropertyPath(string propertyPath, out int index) {
            for (int I = 0; I < ArrayManipulation.ArrayLength(Member.PropertyNames); I++)
                if (string.Format(Member.PropertyNames[I], PropertyPath) == propertyPath) {
                    index = I;
                    return true;
                }
            index = -1;
            return false;
        }

        private void SetPropMark(string propertyPath) {
            if (propMark.ContainsKey(propertyPath))
                propMark[propertyPath] = true;
        }

        public static bool LoadInCache(string id, SerializedPropertyCustom spc, string propertyPath, out object? value) {
            value = null;
            if (!spc.IsSaveCache || spc.LoadCache) return false;
            spc.SetPropMark(propertyPath);
            InitCacheFolder();
            using GDDirectory directory = GDDirectory.GetGDDirectory("res://cache");
            GDFile file = directory.GetFile($"id_{id}.cache")!;
            Dictionary<string, string> props = Json.Deserialize<Dictionary<string, string>>(file.Read())!;
            props = props is null ? [] : props;
            if (props.TryGetValue(propertyPath, out string result))
                return spc.Set(propertyPath, value = spc.Convert(propertyPath, result));
            return false;
        }

        public static bool UnloadInCache(string id, SerializedPropertyCustom spc, string propertyPath, object value) {
            if (!spc.IsSaveCache) return false;
            InitCacheFolder();
            InitFileCache($"id_{id}.cache");
            using (GDDirectory directory = GDDirectory.GetGDDirectory("res://cache")) {
                GDFile file = directory.GetFile($"id_{id}.cache");
                Dictionary<string, string> props = Json.Deserialize<Dictionary<string, string>>(file.Read());
                props = props is null ? [] : props;
                if (!props.ContainsKey(propertyPath))
                    props.Add(propertyPath, string.Empty);
                props[propertyPath] = value.ToString();
                file.Write(Encoding.UTF8.GetBytes(Json.Serialize(props)));
            }
            return spc.Set(propertyPath, value);
        }

        private static void InitCacheFolder() {
            if (GDFeature.HasEditor)
                if (!IODirectory.Exists(cacheFolderPath))
                    IODirectory.CreateDirectory(cacheFolderPath);
        }

        private static void InitFileCache(string fileName) {
            if (GDFeature.HasEditor)
                if (!IOFile.Exists(IOPath.Combine(cacheFolderPath, fileName)))
                    IOFile.Create(IOPath.Combine(cacheFolderPath, fileName)).Dispose();
        }
    }
}
