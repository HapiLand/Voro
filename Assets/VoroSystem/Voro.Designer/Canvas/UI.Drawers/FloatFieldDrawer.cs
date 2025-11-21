using System;
using UnityEngine;
using VoroSystem.Voro.Designer.Canvas.Core;

namespace VoroSystem.Voro.Designer.Canvas.UI.Drawers {
[Serializable]
public class FloatFieldDrawer : FieldDrawerBase, IFieldDrawer<float> {
    #region IFieldDrawer<float> Members
    public void Draw(ref float value, string name) {
        GUILayout.BeginHorizontal();
        GUILayout.Label($"{name}", GUILayout.Width(LabelWidth));
        GUILayout.Label($"{value:F2}", GUILayout.Width(ValueWidth));
        var text = GUILayout.TextField($"{value}", GUILayout.Width(InputWidth));
        value = float.Parse(text);
        GUILayout.EndHorizontal();
    }
    #endregion
}
}