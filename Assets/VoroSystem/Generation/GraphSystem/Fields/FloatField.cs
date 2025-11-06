using System;
using UnityEngine;

namespace VoroSystem.Generation.GraphSystem.Fields {
[Serializable]
public class FloatField : EffectFieldBase {
    public FloatField(string name, float defaultValue) : base(name, defaultValue, FieldType.FloatField) { }

    public float Value {
        get => (float)DefaultValue;
        set => DefaultValue = value;
    }

    public override void DrawGUI() {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Float Field");
        GUILayout.Label($"{Value}");
        Value = float.Parse(GUILayout.TextField(Value.ToString()));
        GUILayout.EndHorizontal();
    }

    /// <summary>
    /// detect if anything in the field changes
    /// </summary>
    public override int ComputeHash() {
        unchecked { // prevent overflow exceptions
            var hash = Name.GetHashCode();
            hash = hash * 31 + Value.GetHashCode();
            return hash;
        }
    }
}
}