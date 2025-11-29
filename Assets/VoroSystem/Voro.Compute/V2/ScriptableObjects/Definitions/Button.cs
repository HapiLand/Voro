using System;
using UnityEngine;
using VoroSystem.Voro.Compute.V2.ScriptableObjects.Base;
using VoroSystem.Voro.Utilities;

namespace VoroSystem.Voro.Compute.V2.ScriptableObjects.Definitions {
[CreateAssetMenu(menuName = "Voro/Node/Data/new Button")]
[Serializable]
public class Button : NodeDataDefinition {
  #region Serialized Fields

  public event Action OnClick;
  [SerializeField] string label;

  #endregion
  void OnEnable() {
    DataType = DataType.Button;
  }
  public override SerializableDictionary<string, object> CreateDefaultProperties() {
    var dict = new SerializableDictionary<string, object>
    {
      ["Label"] = label
    };
    return dict;
  }

  public override void DrawGUI(SerializableDictionary<string, object> properties) {
    GUILayout.BeginHorizontal();
    GUILayout.Label($"{DisplayName}", GUILayout.Width(LabelWidth));
    if (GUILayout.Button((string)properties["Label"], GUILayout.Width(InputWidth))) {
      OnClick?.Invoke();
      NotifyChange(properties);
    }
    GUILayout.EndHorizontal();
  }

  public override void ResetToDefaults() {
    var dictionary = CreateDefaultProperties();
    NotifyChange(dictionary);
  }
}
}