using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using VoroSystem.Voro.Compute;
using VoroSystem.Voro.Compute.DiagramSystem;
using VoroSystem.Voro.Core.Components.Editor.Panels.Base;
using VoroSystem.Voro.Core.Events;

namespace VoroSystem.Voro.Core.Components.Editor.Panels {
[Serializable]
public class NodePanel : BasePanel {
  bool _collapsed = true;
  VisualElement _content;

  public NodePanel(string title, Node node) : base("node", title) {
    CreateHeaderRow1(node);
    _content = new VisualElement
    {
      name = "content",
      style =
      {
        display = DisplayStyle.None
      },
      visible = false
    };
    Add(_content);
    if (node.Controls.Count > 0) {
      float maxLabelWidth = node.Controls.Max(c => c.name.Length * 7);
      node.Controls.ForEach(c => { _content.Add(new ControlPanel("Control", c, maxLabelWidth)); });
    }
  }

  void CreateHeaderRow1(Node node) {
    var row = CreateRow("row1");
    Header.Add(row);
    var collapseBtn = CreateButton("▼", () => ToggleCollapse());
    row.Add(collapseBtn);
    row.Add(CreateLabel(node.Name, FontStyle.Bold));
    row.Add(new VisualElement { style = { flexGrow = 1 } }); // spacer
    var names = Enum.GetNames(typeof(OperationMode)).ToList();
    var current = names.IndexOf(node.Mode.ToString());
    current = current < 0 ? 0 : current;
    var dropdown = new DropdownField(names, current)
    {
      name = "modeDropdown",
      style =
      {
        color = new StyleColor(Color.black),
        height = 30
      }
    };
    dropdown.RegisterValueChangedCallback(evt => {
      var newMode = (OperationMode)Enum.Parse(typeof(OperationMode), evt.newValue);
      node.Mode = newMode;
      DiagramEvents.GetInstance().RaiseChangeMode(newMode);
    });
    row.Add(dropdown); // spacer
    row.Add(new VisualElement { style = { flexGrow = 1 } }); // spacer
    row.Add(CreateButton("↑", () => DiagramEvents.GetInstance().MoveNode(node, -1)));
    row.Add(CreateButton("↓", () => DiagramEvents.GetInstance().MoveNode(node, 1)));
    row.Add(CreateButton("✕", () => DiagramEvents.GetInstance().RaiseRemoveNode(node),
      Color.crimson, Color.black));
  }

  void ToggleCollapse() {
    _collapsed = !_collapsed;

    if (_collapsed) {
      _content.style.display = DisplayStyle.None;
      _content.visible = false;
    }
    else {
      _content.style.display = DisplayStyle.Flex;
      _content.visible = true;
    }
  }

  Button CreateButton(
    string text,
    Action onClick,
    Color? background = null,
    Color? textColor = null) {
    var button = new Button(onClick) { text = text };

    if (background.HasValue) {
      button.style.backgroundColor = new StyleColor(background.Value);
    }

    if (textColor.HasValue) {
      button.style.color = new StyleColor(textColor.Value);
    }

    return button;
  }

  Label CreateLabel(string text, FontStyle style = FontStyle.Normal) {
    return new Label
    {
      text = text,
      style =
      {
        color = new StyleColor(Color.black),
        unityFontStyleAndWeight = style
      }
    };
  }


  VisualElement CreateRow(string name) {
    return new VisualElement
    {
      name = name,
      style =
      {
        flexDirection = FlexDirection.Row,
        alignItems = Align.Center
      }
    };
  }
}
}