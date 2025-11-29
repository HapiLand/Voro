using System;
using UnityEngine;
using VoroSystem.Voro.Compute.V2.ScriptableObjects.Base;
using VoroSystem.Voro.Utilities;

namespace VoroSystem.Voro.Compute.V2.ScriptableObjects.Definitions {
[CreateAssetMenu(menuName = "Voro/Node/Data/new Position3D")]
[Serializable]
public class Position3D : NodeDataDefinition {
  #region Serialized Fields

  [SerializeField] Vector3 defaultValue;

  #endregion
  void OnEnable() {
    DataType = DataType.Position3D;
  }
  public override SerializableDictionary<string, object> CreateDefaultProperties() {
    var dict = new SerializableDictionary<string, object>
    {
      ["CurrentValue"] = defaultValue
    };
    return dict;
  }

  public override void DrawGUI(SerializableDictionary<string, object> properties) {
    var current = (Vector3)properties["CurrentValue"];
    var x = current.x;
    var y = current.y;
    var z = current.z;
    GUILayout.BeginHorizontal();
    GUILayout.Label($"{DisplayName}", GUILayout.Width(LabelWidth));
    GUILayout.Label($"({x:F2},{y:F2})", GUILayout.Width(ValueWidth));
    var tX = GUILayout.TextField($"{x}", GUILayout.Width(InputWidth / 3f));
    var tY = GUILayout.TextField($"{y}", GUILayout.Width(InputWidth / 3f));
    var tZ = GUILayout.TextField($"{z}", GUILayout.Width(InputWidth / 3f));
    var updated = new Vector3(float.Parse(tX), float.Parse(tY), float.Parse(tZ));
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