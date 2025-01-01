using Godot;
using System;
using System.Globalization;

namespace Cobilas.GodotEngine.Utility.Serialization.PropertyCustom {
    [SerializedPropertyCustom(typeof(sbyte))]
    [SerializedPropertyCustom(typeof(short))]
    [SerializedPropertyCustom(typeof(int))]
    [SerializedPropertyCustom(typeof(long))]
    [SerializedPropertyCustom(typeof(float))]
    [SerializedPropertyCustom(typeof(double))]
    [SerializedPropertyCustom(typeof(byte))]
    [SerializedPropertyCustom(typeof(ushort))]
    [SerializedPropertyCustom(typeof(uint))]
    [SerializedPropertyCustom(typeof(ulong))]
    [SerializedPropertyCustom(typeof(string))]
    [SerializedPropertyCustom(typeof(bool))]
    public sealed class MenberCustom : SerializedPropertyCustom {

        public MenberCustom() {
            Member = new();
        }

        public override void InitPropMark() {
            propMark.Add(PropertyPath, false);
        }

        public override object Get(string propertyPath) {
            if (IsPropertyPath(propertyPath) && Member.IsRead) {
                return Member.Value;
            }
            return null;
        }

        public override bool Set(string propertyPath, object Value) {
            if (IsPropertyPath(propertyPath) && Member.IsWrite) {
                Member.Value = Value;
                return true;
            }
            return false;
        }

        public override object Convert(string propertyPath, object Value) {
            if (string.IsNullOrEmpty(Value as string)) return Value;
            if (Member.TypeMenber == typeof(sbyte)) return System.Convert.ToSByte((string)Value);
            else if (Member.TypeMenber == typeof(byte)) return System.Convert.ToByte((string)Value);
            else if (Member.TypeMenber == typeof(ushort)) return System.Convert.ToUInt16((string)Value);
            else if (Member.TypeMenber == typeof(uint)) return System.Convert.ToUInt32((string)Value);
            else if (Member.TypeMenber == typeof(ulong)) return System.Convert.ToUInt64((string)Value);
            else if (Member.TypeMenber == typeof(short)) return System.Convert.ToInt16((string)Value);
            else if (Member.TypeMenber == typeof(int)) return System.Convert.ToInt32((string)Value);
            else if (Member.TypeMenber == typeof(long)) return System.Convert.ToInt64((string)Value);
            else if (Member.TypeMenber == typeof(float)) return System.Convert.ToSingle((string)Value, CultureInfo.CurrentCulture);
            else if (Member.TypeMenber == typeof(double)) return System.Convert.ToDouble((string)Value, CultureInfo.CurrentCulture);
            else if (Member.TypeMenber == typeof(bool)) return System.Convert.ToBoolean((string)Value);
            return System.Convert.ToString(Value);
        }

        public override PropertyItem[] GetPropertyList() {
            Variant.Type type = Variant.Type.Nil;
            if (Member.TypeMenber.CompareType(typeof(sbyte), typeof(short), typeof(int), typeof(long),
                typeof(byte), typeof(ushort), typeof(uint), typeof(ulong))) type = Variant.Type.Int;
            else if (Member.TypeMenber.CompareType(typeof(float), typeof(double))) type = Variant.Type.Real;
            else if (Member.TypeMenber.CompareType(typeof(string))) type = Variant.Type.String;
            else if (Member.TypeMenber.CompareType<bool>()) type = Variant.Type.Bool;
            PropertyItem[] properties = new PropertyItem[] {
                new(PropertyPath, type, usage:GetPropertyUsageFlags())
            };
            return properties;
        }
    }
}
