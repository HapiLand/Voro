using System;
using UnityEngine;
using UnityEngine.UIElements;
using VoroSystem.Voro.Core;
using VoroSystem.Voro.Utilities.Extensions;

namespace VoroSystem.Voro.Compute.EditorSystem {
[Serializable]
public class AngleDrawer : FieldDrawerBase, IFieldDrawer<float> {
  #region Serialized Fields

  [SerializeField] float min;
  [SerializeField] float max;

  #endregion

  public AngleDrawer(float min, float max) {
    this.min = min;
    this.max = max;
  }

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
    var element = new Slider(min, max)
    {
      value = v
    };
    row.Add(element);

    // value
    var valueLabel = new Label($"{v:F2}");
    row.Add(valueLabel);

    // event
    var newValue = v;
    element.RegisterValueChangedCallback(evt => {
      newValue = evt.newValue;
      valueLabel.text = $"{newValue:F2}";
    });
    v = newValue;

    return row;
  }

  #endregion
}
}