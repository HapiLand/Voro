using System;
using UnityEngine;
using VoroSystem.Voro.Compute.V2.ScriptableObjects.Base;
using VoroSystem.Voro.Utilities;

namespace VoroSystem.Voro.Compute.V2.ScriptableObjects.Definitions {
[CreateAssetMenu(menuName = "Voro/Node/Data/new RampColor")]
[Serializable]
public class RampColor : NodeDataDefinition {
  // todo ramp color
  #region Serialized Fields

  [SerializeField] float defaultValue; // (int, float, color)

  #endregion
  void OnEnable() {
    DataType = DataType.RampColor;
  }
  public override SerializableDictionary<string, object> CreateDefaultProperties() {
    var dict = new SerializableDictionary<string, object>
    {
      ["CurrentValue"] = defaultValue
    };
    return dict;
  }

  public override void DrawGUI(SerializableDictionary<string, object> properties) {
    var current = (float)properties["CurrentValue"];
    GUILayout.BeginHorizontal();
    GUILayout.Label($"{DisplayName}", GUILayout.Width(LabelWidth));
    GUILayout.Label($"{current:F2}", GUILayout.Width(ValueWidth));
    var text = GUILayout.TextField($"{current}", GUILayout.Width(InputWidth));
    var updated = float.Parse(text);
    if (!Mathf.Approximately(updated, current)) {
      properties["CurrentValue"] = updated;
      NotifyChange(properties);
    }
    GUILayout.Label(updated.ToString());
    GUILayout.EndHorizontal();
  }

  public override void ResetToDefaults() {
    var dictionary = CreateDefaultProperties();
    NotifyChange(dictionary);
  }
}
}