using System;
using UnityEngine;
using UnityEngine.UIElements;
using VoroSystem.Voro.Compute.DiagramSystem;
using VoroSystem.Voro.Core.Components.Editor.Panels.Base;
using VoroSystem.Voro.Core.Events;

namespace VoroSystem.Voro.Core.Components.Editor.Panels {
[Serializable]
public class DiagramPanel : BasePanel {
  public DiagramPanel(string title, Diagram diagram) : base("diagram", title) {
    Header.Add(new Label
    {
      text = diagram.name,
      style =
      {
        color = new StyleColor(Color.black),
        unityFontStyleAndWeight = FontStyle.Bold,
        marginBottom = 10
      }
    });
    Header.Add(new VisualElement
    {
      name = "row1",
      style =
      {
        flexDirection = FlexDirection.Row,
        alignItems = Align.Center
      }
    });
    Header.Q<VisualElement>("row1").Add(new Label
    {
      text = "New Layer",
      style =
      {
        color = new StyleColor(Color.black)
      }
    });
    Header.Q<VisualElement>("row1").Add(new TextField
    {
      name = "field",
      value = "Default Layer Name",
      style =
      {
        height = 30
      }
    });
    Header.Q<VisualElement>("row1").Add(new Button(() => {
      DiagramEvents.GetInstance().RaiseCreateLayer(Header.Q<VisualElement>("row1").Q<TextField>("field").value);
    })
    {
      name = "button",
      text = "Create Layer"
    });
    if (diagram.layers.Count == 0) {
      return;
    }

    Add(new VisualElement
    {
      name = "row2",
      style =
      {
        flexDirection = FlexDirection.Row
      }
    });
    this.Q<VisualElement>("row2").Add(new ScrollView(ScrollViewMode.Vertical)
    {
      name = "scroll"
    });

    diagram.layers.ForEach(l => { this.Q<VisualElement>("row2").Add(new LayerPanel("Layer", l)); });
  }
}
}