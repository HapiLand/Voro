using System;
using UnityEngine;

namespace VoroSystem.Voro.Core.Events {
[Serializable]
public class ComputeEvents {
  #region Delegates
  public delegate void ComputeAction();
  #endregion

  static ComputeEvents _instance;
  ComputeEvents() { }
  public event ComputeAction OnCompute = delegate { Debug.Log("[Compute Event] Start Compute"); };

  public void RaiseCompute() {
    OnCompute?.Invoke();
  }

  public static ComputeEvents GetInstance() {
    _instance ??= new ComputeEvents();
    return _instance;
  }
}
}