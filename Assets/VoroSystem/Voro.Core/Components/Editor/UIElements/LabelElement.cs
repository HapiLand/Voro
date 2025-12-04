using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace VoroSystem.Voro.Core.Components.Editor.UIElements {
[Serializable]
public abstract class LabelElementBase : VisualElement {
  Label _titleLabel;
  Label _contentLabel;
  protected LabelElementBase(string title, string content) {
    ApplyStyle();
    _titleLabel = new Label($"{title} : ");
    Add(_titleLabel);
    _contentLabel = new Label(content);
    Add(_contentLabel);
  }
  void ApplyStyle() {
    
    // padding
    const int padding = 10;
    style.paddingBottom = padding;
    style.paddingTop = padding;
    style.paddingLeft = padding;
    style.paddingRight = padding;
    
    // alignment
    style.alignItems = Align.FlexStart;
    
    // direction
    style.flexDirection = FlexDirection.Row;
  }
}

[Serializable]
public class LabelElement : LabelElementBase {
  public LabelElement(string elementName, string titleText, string contentText) : base(titleText, contentText) {
    name = elementName;
  }
}
}