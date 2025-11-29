using System;
using UnityEngine;
using VoroSystem.Voro.Compute.V2.ScriptableObjects.Core.Base;
using VoroSystem.Voro.Utilities;

// ReSharper disable SpecifyACultureInStringConversionExplicitly

namespace VoroSystem.Voro.Compute.V2.ScriptableObjects.Core {
[CreateAssetMenu(menuName = "Voro/Node/Data/new SliderFloat")]
[Serializable]
public class SliderFloatDefinition : NodeDataDefinition {
  #region Serialized Fields

  [SerializeField] float min;
  [SerializeField] float max;
  [SerializeField] float defaultValue;

  #endregion

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
    var updated = GUILayout.HorizontalSlider(current, min, max, GUILayout.Width(InputWidth));
    properties["CurrentValue"] = updated;
    GUILayout.Label(updated.ToString());
    GUILayout.EndHorizontal();
  }

  public override void ResetToDefaults() {
    throw new NotImplementedException();
  }
}
}