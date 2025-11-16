using System;
using UnityEngine;
using VoroSystem.Designer.GraphSystemV2.Core;

namespace VoroSystem.Designer.GraphSystemV2.UI.Drawers {
[Serializable]
public class FloatSliderDrawer : FieldDrawerBase, IFieldDrawer<float> {
    #region Serialized Fields

    [SerializeField] float min;
    [SerializeField] float max;

    #endregion

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
}
}