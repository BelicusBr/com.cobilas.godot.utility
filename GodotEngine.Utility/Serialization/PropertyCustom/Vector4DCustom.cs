using Godot;
using System.Globalization;
using Cobilas.GodotEngine.Utility.Numerics;

namespace Cobilas.GodotEngine.Utility.Serialization.PropertyCustom {
    // [SerializedPropertyCustom(typeof(Vector4D))]
    // [SerializedPropertyCustom(typeof(Quaternion))]
    public sealed class Vector4DCustom : SerializedPropertyCustom {

        public Vector4DCustom() {
            Member = new(
                 "{0}",
                 "{0}/x",
                 "{0}/y",
                 "{0}/z",
                 "{0}/w"
             );
        }

        public override void InitPropMark() {
            propMark.Add($"{PropertyPath}/x", false);
            propMark.Add($"{PropertyPath}/y", false);
            propMark.Add($"{PropertyPath}/z", false);
            propMark.Add($"{PropertyPath}/w", false);
        }

        public override object Get(string propertyPath) {
            if (IsPropertyPath(propertyPath) && Member.IsRead) {
                if (propertyPath == $"{PropertyPath}/x") {
                    if (Member.Value is Vector4D vec4d) return vec4d.x;
                    else if (Member.Value is Quaternion quat) return quat.x;
                } else if (propertyPath == $"{PropertyPath}/y") {
                    if (Member.Value is Vector4D vec4d) return vec4d.y;
                    else if (Member.Value is Quaternion quat) return quat.y;
                } else if (propertyPath == $"{PropertyPath}/z") {
                    if (Member.Value is Vector4D vec4d) return vec4d.z;
                    else if (Member.Value is Quaternion quat) return quat.z;
                } else if (propertyPath == $"{PropertyPath}/w") {
                    if (Member.Value is Vector4D vec4d) return vec4d.w;
                    else if (Member.Value is Quaternion quat) return quat.w;
                }
            }
            return null;
        }

        public override bool Set(string propertyPath, object Value) {
            if (IsPropertyPath(propertyPath) && Member.IsWrite) {
                if (propertyPath == $"{PropertyPath}/x") {
                    if (Member.Value is Vector4D vec4d) {
                        vec4d.x = (float)Value;
                        Member.Value = vec4d;
                    } else if (Member.Value is Quaternion quat) {
                        quat.x = (float)Value;
                        Member.Value = quat;
                    }
                } else if (propertyPath == $"{PropertyPath}/y") {
                    if (Member.Value is Vector4D vec4d) {
                        vec4d.y = (float)Value;
                        Member.Value = vec4d;
                    } else if (Member.Value is Quaternion quat) {
                        quat.y = (float)Value;
                        Member.Value = quat;
                    }
                } else if (propertyPath == $"{PropertyPath}/z") {
                    if (Member.Value is Vector4D vec4d) {
                        vec4d.z = (float)Value;
                        Member.Value = vec4d;
                    } else if (Member.Value is Quaternion quat) {
                        quat.z = (float)Value;
                        Member.Value = quat;
                    }
                } else if (propertyPath == $"{PropertyPath}/w") {
                    if (Member.Value is Vector4D vec4d) {
                        vec4d.w = (float)Value;
                        Member.Value = vec4d;
                    } else if (Member.Value is Quaternion quat) {
                        quat.w = (float)Value;
                        Member.Value = quat;
                    }
                }
                return true;
            }
            return false;
        }

        public override object Convert(string propertyPath, object Value) {
            if (string.IsNullOrEmpty(Value as string)) return Value;
            return System.Convert.ToSingle(Value, CultureInfo.CurrentCulture);
        }

        public override PropertyItem[] GetPropertyList() {
            PropertyItem[] properties = new PropertyItem[] {
                new($"{PropertyPath}/x", Variant.Type.Real, usage:GetPropertyUsageFlags()),
                new($"{PropertyPath}/y", Variant.Type.Real, usage:GetPropertyUsageFlags()),
                new($"{PropertyPath}/z", Variant.Type.Real, usage:GetPropertyUsageFlags()),
                new($"{PropertyPath}/w", Variant.Type.Real, usage:GetPropertyUsageFlags())
            };
            return properties;
        }
    }
}
