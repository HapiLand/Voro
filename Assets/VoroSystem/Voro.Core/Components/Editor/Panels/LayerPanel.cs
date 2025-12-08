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
  #region Serialized Fields

  [SerializeReference] Button createNodeButton;
  [SerializeReference] Button removeLayerButton;
  [SerializeField] Layer layer;

  #endregion

  public LayerPanel(string title, Layer layer) : base("layer", title) {
    this.layer = layer;
    Add(new Label($"Name: {this.layer.name}"));

    NodeCreation();
    DeleteLayer();
    MoveLayer();

    if (this.layer.nodes.Count > 0) {
      this.layer.nodes.ForEach(n => { Add(new NodePanel("Node", n)); });
    }
  }

  void DeleteLayer() {
    Add(new Button(() => { DiagramEvents.GetInstance().RaiseRemoveLayer(layer); })
    {
      text = "Delete"
    });
  }

  void MoveLayer() {
    var element = new VisualElement
    {
      style =
      {
        flexDirection = FlexDirection.Row
      }
    };

    element.Add(new Button(() => { DiagramEvents.GetInstance().MoveLayer(layer, -1); })
    {
      text = "Move Up"
    });
    element.Add(new Button(() => { DiagramEvents.GetInstance().MoveLayer(layer, 1); })
    {
      text = "Move Down"
    });
    Add(element);
  }

  void NodeCreation() {
    var element = new VisualElement
    {
      style =
      {
        flexDirection = FlexDirection.Row
      }
    };

    var names = Enum.GetNames(typeof(EffectName)).ToList();
    var dropdown = new DropdownField("Node: ", names, 0);
    createNodeButton = new Button(() => {
      if (Enum.TryParse(dropdown.value, out EffectName type)) {
        DiagramEvents.GetInstance().RaiseCreateNode(type);
      }
    })
    {
      text = $"Add: {dropdown.value}"
    };
    dropdown.RegisterValueChangedCallback(evt => { createNodeButton.text = $"Add: {evt.newValue}"; });

    element.Add(dropdown);
    element.Add(createNodeButton);
    Add(element);
  }
}
}