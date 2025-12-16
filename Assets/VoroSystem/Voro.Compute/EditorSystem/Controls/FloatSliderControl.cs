using System;
using UnityEngine;

namespace VoroSystem.Voro.Compute.EditorSystem.Controls {
[Serializable]
public class FloatSliderControl : TypedControlBase<float> {
  #region Serialized Fields
  [SerializeField] float rangeMax;

  [SerializeField] float rangeMin;
  #endregion

  public FloatSliderControl(string name, float defaultValue, float rangeMin, float rangeMax)
    : base(name, defaultValue, ControlType.FloatSlider) {
    this.rangeMin = rangeMin;
    this.rangeMax = rangeMax;
  }
}
}