using System;
using UnityEngine;

namespace VoroSystem.Voro.Compute.EditorSystem.Controls {
[Serializable]
public class AngleControl : TypedControlBase<float> {
  #region Serialized Fields

  [SerializeField] public float rangeMin;
  [SerializeField] public float rangeMax;

  #endregion

  public AngleControl(string name, float defaultValue)
    : base(name, defaultValue, ControlType.Angle) {
    rangeMin = 0;
    rangeMax = 360;
  }
}
}