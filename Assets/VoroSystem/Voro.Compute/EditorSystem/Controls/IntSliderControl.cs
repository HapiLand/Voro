using System;
using UnityEngine;

namespace VoroSystem.Voro.Compute.EditorSystem.Controls {
[Serializable]
public class IntSliderControl : TypedControlBase<int> {
  #region Serialized Fields

  [SerializeField] int rangeMax;
  [SerializeField] int rangeMin;

  #endregion

  public IntSliderControl(string name, int defaultValue, int rangeMin, int rangeMax)
    : base(name, defaultValue, ControlType.IntSlider) {
    this.rangeMin = rangeMin;
    this.rangeMax = rangeMax;
  }
}
}