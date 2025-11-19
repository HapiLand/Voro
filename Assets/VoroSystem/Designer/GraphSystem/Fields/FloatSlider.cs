using System;
using UnityEngine;
using VoroSystem.Designer.GraphSystem.Core;
using VoroSystem.Designer.GraphSystem.UI.Drawers;

namespace VoroSystem.Designer.GraphSystem.Fields {
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