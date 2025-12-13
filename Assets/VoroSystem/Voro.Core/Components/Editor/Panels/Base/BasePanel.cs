using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace VoroSystem.Voro.Core.Components.Editor.Panels.Base {
[Serializable]
public abstract class BasePanel : VisualElement {
  public VisualElement Header;

  protected BasePanel(string id, string title) {
    name = id;
    Header = CreateHeader(title);
    Add(Header);

    style.backgroundColor = new StyleColor(Color.slateGray);
    // style.alignItems = Align.FlexStart;

    var borderWidth = 0;
    var borderColor = Color.black;
    var margin = 0;

    style.borderTopWidth = borderWidth;
    style.borderTopColor = borderColor;
    style.marginTop = margin;

    style.borderBottomWidth = borderWidth;
    style.borderBottomColor = borderColor;
    style.marginBottom = margin;

    style.borderLeftWidth = borderWidth;
    style.borderLeftColor = borderColor;
    style.marginLeft = margin;

    style.borderRightWidth = borderWidth;
    style.borderRightColor = borderColor;
    style.marginRight = margin;
  }

  static VisualElement CreateHeader(string headerText) {
    var header = new VisualElement
    {
      style =
      {
        backgroundColor = new StyleColor(Color.lightSteelBlue),

        paddingLeft = 10,

        paddingRight = 10,

        paddingTop = 10,

        paddingBottom = 10,
        borderBottomWidth = 2,
        borderBottomColor = Color.black
      }
    };

    var label = new Label(headerText)
    {
      style =
      {
        color = new StyleColor(Color.black)
      }
    };
    header.Add(label);

    return header;
  }
}
}