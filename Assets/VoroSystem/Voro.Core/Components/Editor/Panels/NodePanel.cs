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
  #region Serialized Fields

  [SerializeField] Node node;

  #endregion

  public NodePanel(string title, Node node) : base("node", title) {
    this.node = node;
    Add(new Label($"Name: {this.node.Name}"));

    ModeChange();
    DeleteNode();
    MoveNode();

    this.node.Controls.ForEach(c => { Add(new ControlPanel("Control", c)); });
  }

  void DeleteNode() {
    Add(new Button(() => { DiagramEvents.GetInstance().RaiseRemoveNode(node); })
    {
      text = "Delete"
    });
  }

  void MoveNode() {
    var element = new VisualElement
    {
      style =
      {
        flexDirection = FlexDirection.Row
      }
    };
    element.Add(new Button(() => { DiagramEvents.GetInstance().MoveNode(node, -1); })
    {
      text = "Move Up"
    });
    element.Add(new Button(() => { DiagramEvents.GetInstance().MoveNode(node, 1); })
    {
      text = "Move Down"
    });
    Add(element);
  }

  void ModeChange() {
    var names = Enum.GetNames(typeof(OperationMode)).ToList();
    var current = names.IndexOf(node.Mode.ToString());
    current = current < 0 ? 0 : current;
    var dropdown = new DropdownField("Mode: ", names, current);
    dropdown.RegisterValueChangedCallback(evt => {
      DiagramEvents.GetInstance()
        .RaiseChangeMode((OperationMode)Enum.Parse(typeof(OperationMode), evt.newValue));
      node.Mode = (OperationMode)Enum.Parse(typeof(OperationMode), evt.newValue);
    });
    Add(dropdown);
  }
}
}