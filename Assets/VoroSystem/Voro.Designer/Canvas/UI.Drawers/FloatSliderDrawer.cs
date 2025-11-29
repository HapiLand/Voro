using System;
using UnityEngine;
using VoroSystem.Voro.Designer.Canvas.Core;

namespace VoroSystem.Voro.Designer.Canvas.UI.Drawers {
[Serializable]
public class FloatSliderDrawer : FieldDrawerBase, IFieldDrawer<float> {
    public FloatSliderDrawer(float min, float max) {
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

    #region Serialized Fields
    [SerializeField] float min;
    [SerializeField] float max;
    #endregion
}
}