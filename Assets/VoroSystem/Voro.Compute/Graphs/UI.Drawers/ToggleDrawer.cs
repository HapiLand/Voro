using System;
using UnityEngine;
using VoroSystem.Voro.Compute.Graphs.Core;

namespace VoroSystem.Voro.Compute.Graphs.UI.Drawers {
[Serializable]
public class ToggleDrawer : FieldDrawerBase, IFieldDrawer<bool> {
    #region IFieldDrawer<bool> Members
    public void Draw(ref bool value, string name) {
        GUILayout.BeginHorizontal();
        GUILayout.Label($"{name}", GUILayout.Width(LabelWidth));
        GUILayout.Label($"{value}", GUILayout.Width(ValueWidth));
        value = GUILayout.Toggle(value, "", GUILayout.Width(InputWidth));
        GUILayout.EndHorizontal();
    }
    #endregion
}
}