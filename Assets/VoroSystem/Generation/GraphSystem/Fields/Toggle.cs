using UnityEngine;

namespace VoroSystem.Generation.GraphSystem.Fields {
public class Toggle : EffectFieldBase {
    public Toggle(string name, bool defaultValue) : base(name, defaultValue, FieldType.Toggle) { }

    public bool Value {
        get => (bool)DefaultValue;
        set => DefaultValue = value;
    }

    public override void DrawGUI() {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Toggle");
        GUILayout.Label($"{Value}");
        Value = GUILayout.Toggle(Value, Name);
        GUILayout.EndHorizontal();
    }

    /// <summary>
    /// detect if anything in the field changes
    /// </summary>
    public override int ComputeHash() {
        unchecked {
            // prevent overflow exceptions
            var hash = Name.GetHashCode();
            hash = hash * 31 + Value.GetHashCode();
            return hash;
        }
    }
}
}