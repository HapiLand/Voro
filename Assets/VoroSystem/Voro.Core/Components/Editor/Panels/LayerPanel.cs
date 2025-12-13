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
public class LayerPanel : BasePanel {
  public LayerPanel(string title, Layer layer) : base("layer", title) {
    CreateHeaderRow1(layer);
    CreateHeaderRow2(layer);
    if (layer.nodes.Count == 0) {
      return;
    }

    layer.nodes.ForEach(n => Add(new NodePanel("Node", n)));
  }

  void CreateHeaderRow1(Layer layer) {
    var row = CreateRow("row1");
    Header.Add(row);
    row.Add(CreateLabel(layer.name, FontStyle.Bold));
    row.Add(new VisualElement { style = { flexGrow = 1 } }); // spacer
    row.Add(CreateButton("↑", () => DiagramEvents.GetInstance().MoveLayer(layer, -1)));
    row.Add(CreateButton("↓", () => DiagramEvents.GetInstance().MoveLayer(layer, 1)));
    row.Add(CreateButton("✕",
      () => DiagramEvents.GetInstance().RaiseRemoveLayer(layer),
      Color.crimson,
      Color.black));
  }

  void CreateHeaderRow2(Layer layer) {
    var row = CreateRow("row2");
    Header.Add(row);
    row.Add(CreateLabel("New Node"));
    var names = Enum.GetNames(typeof(EffectName)).ToList();
    var dropdown = new DropdownField(names, 0)
    {
      name = "effectDropdown",
      style =
      {
        color = new StyleColor(Color.black),
        height = 30
      }
    };
    row.Add(dropdown);
    row.Add(CreateButton("Add Node", () => {
      if (Enum.TryParse(dropdown.value, out EffectName type)) {
        DiagramEvents.GetInstance().RaiseCreateNode(type);
      }
    }));
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