using System;
using UnityEngine;

namespace VoroSystem.Voro.Core.Events {
[Serializable]
public class ControlEvents {
  #region Delegates
  public delegate void ValueChangeAction<T>(T value);
  #endregion

  static ControlEvents _instance;
  ControlEvents() { }
  public event ValueChangeAction<object> OnChangeValue = delegate { Debug.Log("[Control Event] Changed Value"); };

  public static ControlEvents GetInstance() {
    _instance ??= new ControlEvents();
    return _instance;
  }

  public void RaiseChangeValue<T>(T value) {
    OnChangeValue?.Invoke(value);
  }
}
}