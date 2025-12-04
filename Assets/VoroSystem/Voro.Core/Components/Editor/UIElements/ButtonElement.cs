using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace VoroSystem.Voro.Core.Components.Editor.UIElements {
[Serializable]
public abstract class ButtonElementBase : VisualElement {
  Button _button;
  protected ButtonElementBase(string title, Action onClick) {
    _button = new Button
    {
      text = title
    };
    _button.RegisterCallback<ClickEvent>(evt => {
      onClick?.Invoke();
    });
    Add(_button);
  }
}

[Serializable]
public class ButtonElement : ButtonElementBase {
  public ButtonElement(string elementName, string labelText, Action onClick) : base(labelText, onClick) {
    name = elementName;
  }
}
}