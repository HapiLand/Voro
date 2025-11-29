using System;
using UnityEngine;
using VoroSystem.Voro.Compute.V2.ScriptableObjects.Base;
using VoroSystem.Voro.Utilities;

namespace VoroSystem.Voro.Compute.V2.ScriptableObjects.Definitions {
[CreateAssetMenu(menuName = "Voro/Node/Data/new InputText")]
[Serializable]
public class InputText : NodeDataDefinition {
  #region Serialized Fields

  [SerializeField] string defaultValue;

  #endregion
  void OnEnable() {
    DataType = DataType.InputText;
  }
  public override SerializableDictionary<string, object> CreateDefaultProperties() {
    var dict = new SerializableDictionary<string, object>
    {
      ["CurrentValue"] = defaultValue
    };
    return dict;
  }

  public override void DrawGUI(SerializableDictionary<string, object> properties) {
    var current = (string)properties["CurrentValue"];
    GUILayout.BeginHorizontal();
    GUILayout.Label($"{DisplayName}", GUILayout.Width(LabelWidth));
    GUILayout.Label($"{current}", GUILayout.Width(ValueWidth));
    var updated = GUILayout.TextField($"{current}", GUILayout.Width(InputWidth));
    if (updated != current) {
      properties["CurrentValue"] = updated;
      NotifyChange(properties);
    }
    GUILayout.Label(updated);
    GUILayout.EndHorizontal();
  }

  public override void ResetToDefaults() {
    var dictionary = CreateDefaultProperties();
    NotifyChange(dictionary);
  }
}
}