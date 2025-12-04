using System;
using UnityEngine;
using UnityEngine.UIElements;
using VoroSystem.Voro.Core;
using VoroSystem.Voro.Utilities.Extensions;

namespace VoroSystem.Voro.Compute.EditorSystem {
[Serializable]
public class ToggleDrawer : FieldDrawerBase, IFieldDrawer<bool> {
  #region IFieldDrawer<bool> Members



  public VisualElement DrawUI(ref bool v, string name) {
    var row = new VisualElement
    {
      style =
      {
        backgroundColor = new StyleColor(EditorBackgroundColor.Bg.ToRGB()),
        alignItems = Align.Center,
        flexDirection = FlexDirection.Row
      }
    };

    // heading
    row.Add(new Label($"{name}"));

    // element
    var element = new Toggle
    {
      value = v
    };
    row.Add(element);

    // value
    var valueLabel = new Label($"{v}");
    row.Add(valueLabel);

    // event
    var newValue = v;
    element.RegisterValueChangedCallback(evt => {
      newValue = evt.newValue;
      valueLabel.text = $"{newValue}";
    });
    v = newValue;

    return row;
  }

  #endregion
}
}