using Godot;

namespace Cobilas.GodotEngine.Utility.Serialization.PropertyCustom {
    public class IDCustom : SerializedPropertyCustom {

        public IDCustom() {
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
            return System.Convert.ToString(Value);
        }

        public override PropertyItem[] GetPropertyList() {
            PropertyItem[] properties = new PropertyItem[] {
                new(PropertyPath, Variant.Type.String, usage:PropertyUsageFlags.ScriptVariable | PropertyUsageFlags.Storage)
            };
            return properties;
        }
    }
}