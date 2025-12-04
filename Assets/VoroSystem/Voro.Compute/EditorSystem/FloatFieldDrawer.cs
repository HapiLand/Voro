using System;
using UnityEngine;
using UnityEngine.UIElements;
using VoroSystem.Voro.Core;
using VoroSystem.Voro.Utilities.Extensions;

namespace VoroSystem.Voro.Compute.EditorSystem {
[Serializable]
public class FloatFieldDrawer : FieldDrawerBase, IFieldDrawer<float> {
  #region IFieldDrawer<float> Members



  public VisualElement DrawUI(ref float v, string name) {
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
    var element = new TextField(name)
    {
      value = $"{v:F2}"
    };
    row.Add(element);

    // value
    var valueLabel = new Label($"{v:F2}");
    row.Add(valueLabel);

    // event
    var newValue = v;
    element.RegisterValueChangedCallback(evt => {
      if (float.TryParse(evt.newValue, out var newVal)) {
        newValue = newVal;
        valueLabel.text = $"{newValue:F2}";
      }
    });
    v = newValue;

    return row;
  }

  #endregion
}
}