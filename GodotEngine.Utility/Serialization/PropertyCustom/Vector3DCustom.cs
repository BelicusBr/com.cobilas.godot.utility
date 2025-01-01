using Godot;
using System;
using System.Globalization;
using Cobilas.GodotEngine.Utility.Numerics;

namespace Cobilas.GodotEngine.Utility.Serialization.PropertyCustom {
    // [SerializedPropertyCustom(typeof(Vector3D))]
    // [SerializedPropertyCustom(typeof(Vector3DInt))]
    public sealed class Vector3DCustom : SerializedPropertyCustom {

        public Vector3DCustom() {
            Member = new(
                 "{0}",
                 "{0}/x",
                 "{0}/y",
                 "{0}/z"
             );
        }

        public override void InitPropMark() {
            propMark.Add($"{PropertyPath}/x", false);
            propMark.Add($"{PropertyPath}/y", false);
            propMark.Add($"{PropertyPath}/z", false);
        }

        public override object Get(string propertyPath) {
            if (IsPropertyPath(propertyPath) && Member.IsRead) {
                if (propertyPath == $"{PropertyPath}/x") {
                    if (Member.Value is Vector3D vec3d) return vec3d.x;
                    else if (Member.Value is Vector3DInt vec3di) return vec3di.x;
                } else if (propertyPath == $"{PropertyPath}/y") {
                    if (Member.Value is Vector3D vec3d) return vec3d.y;
                    else if (Member.Value is Vector3DInt vec3di) return vec3di.y;
                } else if (propertyPath == $"{PropertyPath}/z") {
                    if (Member.Value is Vector3D vec3d) return vec3d.z;
                    else if (Member.Value is Vector3DInt vec3di) return vec3di.z;
                }
            }
            return null;
        }

        public override bool Set(string propertyPath, object Value) {
            if (IsPropertyPath(propertyPath) && Member.IsWrite) {
                if (propertyPath == $"{PropertyPath}/x") {
                    if (Member.Value is Vector3D vec3d) {
                        vec3d.x = (float)Value;
                        Member.Value = vec3d;
                    } else if (Member.Value is Vector3DInt vec3di) {
                        vec3di.x = (int)Value;
                        Member.Value = vec3di;
                    }
                } else if (propertyPath == $"{PropertyPath}/y") {
                    if (Member.Value is Vector3D vec3d) {
                        vec3d.y = (float)Value;
                        Member.Value = vec3d;
                    } else if (Member.Value is Vector3DInt vec3di) {
                        vec3di.y = (int)Value;
                        Member.Value = vec3di;
                    }
                } else if (propertyPath == $"{PropertyPath}/z") {
                    if (Member.Value is Vector3D vec3d) {
                        vec3d.z = (float)Value;
                        Member.Value = vec3d;
                    } else if (Member.Value is Vector3DInt vec3di) {
                        vec3di.z = (int)Value;
                        Member.Value = vec3di;
                    }
                }
                return true;
            }
            return false;
        }

        public override object Convert(string propertyPath, object Value) {
            if (string.IsNullOrEmpty(Value as string)) return Value;
            if (Member.TypeMenber == typeof(Vector3D)) return System.Convert.ToSingle(Value, CultureInfo.CurrentCulture);
            else return System.Convert.ToInt32(Value);
        }

        public override PropertyItem[] GetPropertyList() {
            Variant.Type type = Member.TypeMenber.CompareType<Vector3D>() ? Variant.Type.Real : Variant.Type.Int;
            
            PropertyItem[] properties = new PropertyItem[] {
                new($"{PropertyPath}/x", type, usage:GetPropertyUsageFlags()),
                new($"{PropertyPath}/y", type, usage:GetPropertyUsageFlags()),
                new($"{PropertyPath}/z", type, usage:GetPropertyUsageFlags())
            };
            return properties;
        }
    }
}
