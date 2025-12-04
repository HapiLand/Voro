using System;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.UIElements;
using VoroSystem.Voro.Utilities.Extensions;

namespace VoroSystem.Voro.Core.Components.Editor.UIElements {
[Serializable]
public abstract class ContainerBase : VisualElement {
  Label _headingLabel;

  protected ContainerBase(string title) {
    ApplyStyle();
    _headingLabel = new Label(title)
    {
      text = title
    };
    Add(_headingLabel);
  }

  void ApplyStyle() {
    var bgColor = Color.darkSlateGray;
    var strokeColor = Color.black;
    
    style.backgroundColor = new StyleColor(bgColor);
    
    // padding
    const int padding = 10;
    style.paddingBottom = padding;
    style.paddingTop = padding;
    style.paddingLeft = padding;
    style.paddingRight = padding;
    
    // alignment
    style.alignItems = Align.FlexStart;
    
    // outline
    const int outlineWidth = 2;
    style.borderTopWidth = outlineWidth;
    style.borderBottomWidth = outlineWidth;
    style.borderLeftWidth = outlineWidth;
    style.borderRightWidth = outlineWidth;
    style.borderTopColor = strokeColor;
    style.borderBottomColor = strokeColor;
    style.borderLeftColor = strokeColor;
    style.borderRightColor = strokeColor;
  }
}

[Serializable]
public class Container : ContainerBase {
  public Container(string elementName, string labelText) : base(labelText) {
    name = elementName;
  }
}
}