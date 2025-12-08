using System;
using UnityEngine;
using UnityEngine.UIElements;
using VoroSystem.Voro.Compute.DiagramSystem;
using VoroSystem.Voro.Core.Components.Editor.Panels.Base;
using VoroSystem.Voro.Core.Events;

namespace VoroSystem.Voro.Core.Components.Editor.Panels {
[Serializable]
public class DiagramPanel : BasePanel {
  #region Serialized Fields

  [SerializeField] Button createLayerButton;
  [SerializeField] Diagram diagram;

  #endregion

  public DiagramPanel(string title, Diagram diagram) : base("diagram", title) {
    this.diagram = diagram;
    Add(new Label($"Name: {this.diagram.name}"));

    LayerCreation();

    if (this.diagram.layers.Count > 0) {
      this.diagram.layers.ForEach(l => { Add(new LayerPanel("Layer", l)); });
    }
  }

  void LayerCreation() {
    var element = new VisualElement
    {
      style =
      {
        flexDirection = FlexDirection.Row
      }
    };

    var layerNameField = new TextField("New Layer: ")
    {
      value = "DefaultName"
    };
    createLayerButton = new Button(() => { DiagramEvents.GetInstance().RaiseCreateLayer(layerNameField.value); })
    {
      text = $"Create '{layerNameField.value}'"
    };
    layerNameField.RegisterValueChangedCallback(evt => { createLayerButton.text = $"Create '{evt.newValue}'"; });

    element.Add(layerNameField);
    element.Add(createLayerButton);
    Add(element);
  }
}
}