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
  #region Serialized Fields

  [SerializeReference] ControlBase control;
  [SerializeReference] VisualElement controlElement;

  #endregion

  public ControlPanel(string title, ControlBase control) : base("control", control.name) {
    this.control = control;
    style.flexDirection = FlexDirection.Row;

    switch (this.control) {
    case FloatInputControl floatInput: {
      var element = new TextField
      {
        value = $"{floatInput.Value:F2}"
      };
      element.RegisterValueChangedCallback(evt => {
        if (float.TryParse(evt.newValue, out var newValue)) {
          floatInput.Value = newValue;
          ControlEvents.GetInstance().RaiseChangeValue(newValue);
        }
      });
      controlElement = element;
    }
      break;
    case ToggleControl toggle: {
      var element = new Toggle("")
      {
        value = toggle.Value
      };
      element.RegisterValueChangedCallback(evt => {
          toggle.Value = evt.newValue;
          ControlEvents.GetInstance().RaiseChangeValue(evt.newValue);
      });
      controlElement = element;
    }
      break;
    }

    Add(controlElement);
  }
}
}