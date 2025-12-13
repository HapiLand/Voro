using System;
using UnityEngine;
using UnityEngine.UIElements;
using VoroSystem.Voro.Compute.EditorSystem;
using VoroSystem.Voro.Compute.EditorSystem.Controls;
using VoroSystem.Voro.Core.Components.Editor.Panels.Base;
using VoroSystem.Voro.Core.Events;

namespace VoroSystem.Voro.Core.Components.Editor.Panels {
[Serializable]
public class ControlPanel : BasePanel {
  /*#region Serialized Fields

  [SerializeReference] ControlBase control;
  [SerializeReference] VisualElement controlElement;

  #endregion*/

  public ControlPanel(string title, ControlBase control, float labelWidth) : base("control", "") {
    Header.Add(new VisualElement
    {
      name = "row1",
      style =
      {
        flexDirection = FlexDirection.Row
      }
    });
    Header.Q<VisualElement>("row1").Add(new Label
    {
      text = control.name,
      style =
      {
        color = new StyleColor(Color.black),
        width = labelWidth,
        marginRight = 10,
        unityFontStyleAndWeight = FontStyle.Bold
      }
    });
    switch (control) {
    case FloatInputControl floatInput: {
      Header.Q<VisualElement>("row1").Add(new TextField
      {
        name = "field",
        value = $"{floatInput.Value:F2}"
      });
      Header.Q<VisualElement>("row1").Q<TextField>("field").RegisterValueChangedCallback(evt => {
        if (float.TryParse(evt.newValue, out var newValue)) {
          floatInput.Value = newValue;
          ControlEvents.GetInstance().RaiseChangeValue(newValue);
        }
      });
    }
      break;
    case ToggleControl toggle: {
      Header.Q<VisualElement>("row1").Add(new Toggle
      {
        name = "field",
        value = toggle.Value
      });
      Header.Q<VisualElement>("row1").Q<Toggle>("field").RegisterValueChangedCallback(evt => {
        toggle.Value = evt.newValue;
        ControlEvents.GetInstance().RaiseChangeValue(evt.newValue);
      });
    }
      break;
    }
  }
}
}