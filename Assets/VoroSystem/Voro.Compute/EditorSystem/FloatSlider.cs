using System;
using UnityEngine;

namespace VoroSystem.Voro.Compute.EditorSystem {
[Serializable]
public class FloatSlider : TypedFieldBase<float> {
  #region Serialized Fields

  [SerializeField] FloatSliderDrawer drawer;

  #endregion

  public FloatSlider(string name, float defaultValue, float min, float max)
    : base(name, defaultValue, FieldType.FloatSlider) {
    drawer = new FloatSliderDrawer(min, max);
  }

  protected override IFieldDrawer<float> Drawer => drawer;
}
}