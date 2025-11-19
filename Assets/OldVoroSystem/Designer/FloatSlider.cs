using System;
using UnityEngine;

namespace OldVoroSystem.Designer {
[Serializable]
public class FloatSlider : EffectFieldBase {
  public FloatSlider(string name, float defaultValue, float min, float max) : base(name, defaultValue,
    FieldType.FloatSlider) {
    Min = min;
    Max = max;
  }

  public float Min { get; }

  public float Max { get; }

  public float Value {
    get => (float)DefaultValue;
    set => DefaultValue = value;
  }

  public override void DrawGUI() {
    GUILayout.BeginHorizontal();
    GUILayout.Label("Float Slider");
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