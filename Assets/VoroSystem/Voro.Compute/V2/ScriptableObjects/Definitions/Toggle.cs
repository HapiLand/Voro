using System;
using UnityEngine;
using VoroSystem.Voro.Compute.V2.ScriptableObjects.Base;
using VoroSystem.Voro.Utilities;

namespace VoroSystem.Voro.Compute.V2.ScriptableObjects.Definitions {
[CreateAssetMenu(menuName = "Voro/Node/Data/new Toggle")]
[Serializable]
public class Toggle : NodeDataDefinition {
  #region Serialized Fields

  [SerializeField] bool defaultValue;

  #endregion
  void OnEnable() {
    DataType = DataType.Toggle;
  }
  public override SerializableDictionary<string, object> CreateDefaultProperties() {
    var dict = new SerializableDictionary<string, object>
    {
      ["CurrentValue"] = defaultValue
    };
    return dict;
  }

  public override void DrawGUI(SerializableDictionary<string, object> properties) {
    var current = (bool)properties["CurrentValue"];
    GUILayout.BeginHorizontal();
    GUILayout.Label($"{DisplayName}", GUILayout.Width(LabelWidth));
    GUILayout.Label($"{current}", GUILayout.Width(ValueWidth));
    var updated = GUILayout.Toggle(current, "", GUILayout.Width(InputWidth));
    if (updated != current) {
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