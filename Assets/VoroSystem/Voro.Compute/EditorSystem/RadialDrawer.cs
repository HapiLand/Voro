using System;
using UnityEngine;

namespace VoroSystem.Voro.Compute.EditorSystem {
[Serializable]
public class RadialDrawer : FieldDrawerBase, IFieldDrawer<float> {
  #region Serialized Fields

  [SerializeField] float min;
  [SerializeField] float max;

  #endregion

  public RadialDrawer(float min, float max) {
    this.min = min;
    this.max = max;
  }

  #region IFieldDrawer<float> Members

  public void Draw(ref float value, string name) {
    GUILayout.BeginHorizontal();
    GUILayout.Label($"{name}", GUILayout.Width(LabelWidth));
    GUILayout.Label($"{value:F2}", GUILayout.Width(ValueWidth));
    value = GUILayout.HorizontalSlider(value, min, max, GUILayout.Width(InputWidth));
    GUILayout.EndHorizontal();
  }

  #endregion
}
}