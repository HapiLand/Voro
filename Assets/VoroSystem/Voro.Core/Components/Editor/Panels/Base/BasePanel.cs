using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace VoroSystem.Voro.Core.Components.Editor.Panels.Base {
[Serializable]
public abstract class BasePanel : VisualElement {
  #region Serialized Fields

  [SerializeReference] Label titleLabel;

  #endregion

  protected BasePanel(string id, string title) {
    name = id;

    style.backgroundColor = new StyleColor(Color.darkSlateBlue);

    const int padding = 10;
    style.paddingBottom = padding;
    style.paddingTop = padding;
    style.paddingLeft = padding;
    style.paddingRight = padding;
    style.marginBottom = padding;
    style.marginTop = padding;
    style.marginLeft = padding;
    style.marginRight = padding;
    style.alignItems = Align.FlexStart;

    const int strokeWidth = 2;
    var strokeColor = Color.black;
    style.borderTopWidth = strokeWidth;
    style.borderBottomWidth = strokeWidth;
    style.borderLeftWidth = strokeWidth;
    style.borderRightWidth = strokeWidth;
    style.borderTopColor = strokeColor;
    style.borderBottomColor = strokeColor;
    style.borderLeftColor = strokeColor;
    style.borderRightColor = strokeColor;

    titleLabel = new Label(title);
    Add(titleLabel);
  }
}
}