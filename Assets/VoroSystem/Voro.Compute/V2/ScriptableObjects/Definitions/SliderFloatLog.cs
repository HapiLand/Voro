using System;
using UnityEngine;
using VoroSystem.Voro.Compute.V2.ScriptableObjects.Base;
using VoroSystem.Voro.Utilities;

// ReSharper disable InconsistentNaming

namespace VoroSystem.Voro.Compute.V2.ScriptableObjects.Definitions {
[CreateAssetMenu(menuName = "Voro/Node/Data/new SliderFloatLog")]
[Serializable]
public class SliderFloatLog : NodeDataDefinition {
  #region Serialized Fields

  [SerializeField] float min;
  [SerializeField] float max;
  [SerializeField] float defaultValue;

  #endregion

  void OnEnable() {
    DataType = DataType.SliderFloatLog;
  }

  public override SerializableDictionary<string, object> CreateDefaultProperties() {
    var dict = new SerializableDictionary<string, object>
    {
      ["Min"] = min,
      ["Max"] = max,
      ["CurrentValue"] = defaultValue
    };
    return dict;
  }

  public override void DrawGUI(SerializableDictionary<string, object> properties) {
    var current = (float)properties["CurrentValue"];
    GUILayout.BeginHorizontal();
    GUILayout.Label($"{DisplayName}", GUILayout.Width(LabelWidth));
    GUILayout.Label($"{current:F2}", GUILayout.Width(ValueWidth));
    // todo log
    var updated = GUILayout.HorizontalSlider(current, min, max, GUILayout.Width(InputWidth));
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