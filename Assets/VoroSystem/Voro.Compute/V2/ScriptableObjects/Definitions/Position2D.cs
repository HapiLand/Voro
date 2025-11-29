using System;
using UnityEngine;
using VoroSystem.Voro.Compute.V2.ScriptableObjects.Base;
using VoroSystem.Voro.Utilities;

namespace VoroSystem.Voro.Compute.V2.ScriptableObjects.Definitions {
[CreateAssetMenu(menuName = "Voro/Node/Data/new Position2D")]
[Serializable]
public class Position2D : NodeDataDefinition {
  #region Serialized Fields

  [SerializeField] Vector2 defaultValue;

  #endregion
  void OnEnable() {
    DataType = DataType.Position2D;
  }
  public override SerializableDictionary<string, object> CreateDefaultProperties() {
    var dict = new SerializableDictionary<string, object>
    {
      ["CurrentValue"] = defaultValue
    };
    return dict;
  }

  public override void DrawGUI(SerializableDictionary<string, object> properties) {
    var current = (Vector2)properties["CurrentValue"];
    var x = current.x;
    var y = current.y;
    GUILayout.BeginHorizontal();
    GUILayout.Label($"{DisplayName}", GUILayout.Width(LabelWidth));
    GUILayout.Label($"({x:F2},{y:F2})", GUILayout.Width(ValueWidth));
    var tX = GUILayout.TextField($"{x}", GUILayout.Width(InputWidth / 2f));
    var tY = GUILayout.TextField($"{y}", GUILayout.Width(InputWidth / 2f));
    var updated = new Vector2(float.Parse(tX), float.Parse(tY));
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