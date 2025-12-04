using System;
using UnityEngine;

namespace VoroSystem.Voro.Compute.EditorSystem.Controls {
[Serializable]
public class FloatSlider : TypedFieldBase<float> {
  #region Serialized Fields

  [SerializeField] FloatSliderDrawer drawer;

  #endregion

  float max;

  float min;

  public FloatSlider(string name, float defaultValue, float min, float max)
    : base(name, defaultValue, FieldType.FloatSlider) {
    this.min = min;
    this.max = max;
    drawer = new FloatSliderDrawer(min, max);
  }

  protected override IFieldDrawer<float> Drawer => drawer ??= new FloatSliderDrawer(min, max);
}
}