using System;
using UnityEngine;

namespace VoroSystem.Voro.Core.Events {
[Serializable]
public class ButtonEvents {
  #region Delegates

  public delegate void ButtonAction();

  #endregion

  static ButtonEvents _instance;
  ButtonEvents() { }
  public event ButtonAction OnClick = delegate { Debug.Log("[Button Event] click"); };

  public void RaiseClick() {
    OnClick?.Invoke();
  }

  public static ButtonEvents GetInstance() {
    _instance ??= new ButtonEvents();
    return _instance;
  }
}
}