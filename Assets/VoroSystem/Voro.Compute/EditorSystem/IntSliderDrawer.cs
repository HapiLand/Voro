using System;
using UnityEngine;

namespace VoroSystem.Voro.Compute.EditorSystem {
[Serializable]
public class IntSliderDrawer : FieldDrawerBase, IFieldDrawer<int> {
    public IntSliderDrawer(int min, int max) {
        this.min = min;
        this.max = max;
    }

    #region IFieldDrawer<int> Members
    public void Draw(ref int value, string name) {
        GUILayout.BeginHorizontal();
        GUILayout.Label($"{name}", GUILayout.Width(LabelWidth));
        GUILayout.Label($"{value:F2}", GUILayout.Width(ValueWidth));
        value = (int)GUILayout.HorizontalSlider(value, min, max, GUILayout.Width(InputWidth));
        GUILayout.EndHorizontal();
    }
    #endregion

    #region Serialized Fields
    [SerializeField] int min;
    [SerializeField] int max;
    #endregion
}
}