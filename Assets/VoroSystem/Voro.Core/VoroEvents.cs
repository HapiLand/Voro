using System;
using UnityEngine;
using VoroSystem.Voro.Compute.DiagramSystem;
using VoroSystem.Voro.Compute.EffectSystem.Core;

namespace VoroSystem.Voro.Core {
[Serializable]
public class VoroEvents {
  #region Delegates

  public delegate void ButtonAction();

  public delegate void ComputeAction();

  public delegate void DiagramAction(Diagram diagram);

  public delegate void DiagramLayerAction(string name);

  public delegate void DiagramNodeAction(EffectBase.EffectType effectType);

  #endregion

  static VoroEvents _instance;
  VoroEvents() { }
  
  public static VoroEvents GetInstance() {
    _instance ??= new VoroEvents();
    return _instance;
  }

  public event ButtonAction OnClickEvent = delegate { Debug.Log("[Button Event] Clicked"); };
  public event ButtonAction OnComputeEvent = delegate { Debug.Log("[Button Event] Compute Diagram"); };

  public event DiagramAction OnDiagramCreatedEvent = delegate { Debug.Log("[Event] Diagram Initialised"); };

  public event DiagramLayerAction OnNewLayerEvent = delegate { Debug.Log("[Event] Created New Layer"); };

  public event ComputeAction OnDiagramUpdatedEvent = delegate { Debug.Log("[Event] Diagram Value Changed"); };

  public event DiagramNodeAction OnNewNodeEvent = delegate { Debug.Log("[Event] Created New Node"); };

  public void RaiseClick() {
    OnClickEvent?.Invoke();
  }
  
  public void RaiseClickCompute() {
    OnComputeEvent?.Invoke();
  }
  public void RaiseDiagramUpdated() {
    OnDiagramUpdatedEvent?.Invoke();
  }

  public void RaiseOnDiagramCreated(Diagram diagram) {
    OnDiagramCreatedEvent?.Invoke(diagram);
    OnDiagramUpdatedEvent?.Invoke();
  }

  public void RaiseCreateNewLayer(string layerName) {
    OnNewLayerEvent?.Invoke(layerName);
    OnDiagramUpdatedEvent?.Invoke();
  }

  public void RaiseCreateNewNode(EffectBase.EffectType effectType) {
    OnNewNodeEvent?.Invoke(effectType);
    OnDiagramUpdatedEvent?.Invoke();
  }
}
}