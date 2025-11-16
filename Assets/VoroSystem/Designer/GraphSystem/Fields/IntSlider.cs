using System;
using UnityEngine;

namespace VoroSystem.Designer.GraphSystem.Fields {
[Serializable]
public class IntSlider : EffectFieldBase {
    public IntSlider(string name, int defaultValue, int min, int max) : base(name, defaultValue,
        FieldType.IntSlider) {
        Min = min;
        Max = max;
    }

    public int Min { get; }

    public int Max { get; }

    public int Value {
        get => (int)DefaultValue;
        set => DefaultValue = value;
    }

    public override void DrawGUI() {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Float Slider");
        GUILayout.Label($"{Value}");
        Value = (int)GUILayout.HorizontalSlider(Value, Min, Max);
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