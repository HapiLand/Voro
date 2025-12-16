using System;
using UnityEngine;

namespace VoroSystem.Voro.Compute.EditorSystem {
[Serializable]
public abstract class ControlBase {
  #region Serialized Fields
  [SerializeReference] public string name;
  [SerializeReference] public object defaultValue;
  [SerializeReference] public ControlType type;
  #endregion

  protected ControlBase(string name, object defaultValue, ControlType type) {
    this.name = name;
    this.defaultValue = defaultValue;
    this.type = type;
  }
}
}