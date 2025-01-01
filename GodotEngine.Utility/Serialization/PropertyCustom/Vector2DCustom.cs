using Godot;
using System;
using System.Globalization;
using Cobilas.GodotEngine.Utility.Numerics;

namespace Cobilas.GodotEngine.Utility.Serialization.PropertyCustom {
    // [SerializedPropertyCustom(typeof(Vector2D))]
    // [SerializedPropertyCustom(typeof(Vector2DInt))]
    public sealed class Vector2DCustom : SerializedPropertyCustom {

        public Vector2DCustom() {
            Member = new(
                 "{0}",
                 "{0}/x",
                 "{0}/y"
             );
        }

        public override void InitPropMark() {
            propMark.Add($"{PropertyPath}/x", false);
            propMark.Add($"{PropertyPath}/y", false);
        }

        public override object Get(string propertyPath) {
            if (IsPropertyPath(propertyPath) && Member.IsRead) {
                if (propertyPath == $"{PropertyPath}/x") {
                    if (Member.Value is Vector2D vec2d) return vec2d.x;
                    else if (Member.Value is Vector2DInt vec2di) return vec2di.x;
                } else if (propertyPath == $"{PropertyPath}/y") {
                    if (Member.Value is Vector2D vec2d) return vec2d.y;
                    else if (Member.Value is Vector2DInt vec2di) return vec2di.y;
                }
            }
            return null;
        }

        public override bool Set(string propertyPath, object Value) {
            if (IsPropertyPath(propertyPath) && Member.IsWrite) {
                if (propertyPath == $"{PropertyPath}/x") {
                    if (Member.Value is Vector2D vec2d) {
                        vec2d.x = (float)Value;
                        Member.Value = vec2d;
                    } else if (Member.Value is Vector2DInt vec2di) {
                        vec2di.x = (int)Value;
                        Member.Value = vec2di;
                    }
                } else if (propertyPath == $"{PropertyPath}/y") {
                    if (Member.Value is Vector2D vec2d) {
                        vec2d.y = (float)Value;
                        Member.Value = vec2d;
                    } else if (Member.Value is Vector2DInt vec2di) {
                        vec2di.y = (int)Value;
                        Member.Value = vec2di;
                    }
                }
                return true;
            }
            return false;
        }

        public override object Convert(string propertyPath, object Value) {
            if (string.IsNullOrEmpty(Value as string)) return Value;
            if (Member.TypeMenber == typeof(Vector2D)) return System.Convert.ToSingle(Value, CultureInfo.CurrentCulture);
            else return System.Convert.ToInt32(Value);
        }

        public override PropertyItem[] GetPropertyList() {
            Variant.Type type = Member.TypeMenber.CompareType<Vector2D>() ? Variant.Type.Real : Variant.Type.Int;

            PropertyItem[] properties = new PropertyItem[] {
                new($"{PropertyPath}/x", type, usage:GetPropertyUsageFlags()),
                new($"{PropertyPath}/y", type, usage:GetPropertyUsageFlags())
            };
            return properties;
        }
    }
}
