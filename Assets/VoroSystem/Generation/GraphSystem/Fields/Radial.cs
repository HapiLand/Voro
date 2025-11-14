using UnityEngine;

namespace VoroSystem.Generation.GraphSystem.Fields {
public class Radial : EffectFieldBase {
    public Radial(string name, float defaultValue) : base(name, defaultValue,
        FieldType.FloatSlider) { }

    public float Min => 0f;
    public float Max => 360f;

    public float Value {
        get => (float)DefaultValue;
        set => DefaultValue = value;
    }

    public override void DrawGUI() {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Radial");
        GUILayout.Label($"{Value}");
        Value = GUILayout.HorizontalSlider(Value, Min, Max);
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