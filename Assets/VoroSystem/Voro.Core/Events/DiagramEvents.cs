using System;
using UnityEngine;
using VoroSystem.Voro.Compute;
using VoroSystem.Voro.Compute.DiagramSystem;

namespace VoroSystem.Voro.Core.Events {
[Serializable]
public class DiagramEvents {
  #region Delegates

  public delegate void ChangeModeAction(OperationMode mode);

  public delegate void CreateLayerAction(string name);

  public delegate void CreateNodeAction(EffectName type);

  public delegate void DiagramAction(Diagram diagram);

  public delegate void MoveLayerAction(Layer layer, int direction);

  public delegate void MoveNodeAction(Node node, int direction);

  public delegate void RemoveLayerAction(Layer layer);

  public delegate void RemoveNodeAction(Node node);

  #endregion

  static DiagramEvents _instance;
  DiagramEvents() { }

  public event DiagramAction OnCreated = delegate { Debug.Log("[Diagram Event] Diagram Created"); };
  public event CreateLayerAction OnCreateLayer = delegate { Debug.Log("[Diagram Event] Create Layer"); };
  public event RemoveLayerAction OnRemoveLayer = delegate { Debug.Log("[Diagram Event] Remove Layer"); };
  public event CreateNodeAction OnCreateNode = delegate { Debug.Log("[Diagram Event] Create Node"); };
  public event MoveLayerAction OnMoveLayer = delegate { Debug.Log("[Diagram Event] Move Layer"); };
  public event Action OnDiagramChanged;
  public event ChangeModeAction OnChangeMode = delegate { Debug.Log("[Diagram Event] Changed Mode"); };

  public event MoveNodeAction OnMoveNode = delegate { Debug.Log("[Diagram Event] Move Node"); };
  public event RemoveNodeAction OnRemoveNode = delegate { Debug.Log("[Diagram Event] Remove Node"); };


  public void RaiseChangeMode(OperationMode mode) {
    OnChangeMode?.Invoke(mode);
    OnDiagramChanged?.Invoke();
  }

  public void RaiseCreateNode(EffectName type) {
    OnCreateNode?.Invoke(type);
    OnDiagramChanged?.Invoke();
  }

  public void RaiseCreateLayer(string layerName) {
    OnCreateLayer?.Invoke(layerName);
    OnDiagramChanged?.Invoke();
  }

  public void RaiseOnCreated(Diagram diagram) {
    OnCreated?.Invoke(diagram);
  }

  public static DiagramEvents GetInstance() {
    _instance ??= new DiagramEvents();
    return _instance;
  }

  public void RaiseRemoveLayer(Layer layer) {
    OnRemoveLayer?.Invoke(layer);
    OnDiagramChanged?.Invoke();
  }

  public void MoveLayer(Layer layer, int direction) {
    OnMoveLayer?.Invoke(layer, direction);
    OnDiagramChanged?.Invoke();
  }

  public void RaiseRemoveNode(Node node) {
    OnRemoveNode?.Invoke(node);
    OnDiagramChanged?.Invoke();
  }

  public void MoveNode(Node mode, int direction) {
    OnMoveNode?.Invoke(mode, direction);
    OnDiagramChanged?.Invoke();
  }
}
}