using System;
using UnityEngine;

namespace VoroSystem.Voro.Compute.EditorSystem.Controls {
[Serializable]
public class IntSlider : TypedFieldBase<int> {
  #region Serialized Fields

  [SerializeField] IntSliderDrawer drawer;

  #endregion

  int max;
  int min;

  public IntSlider(string name, int defaultValue, int min, int max)
    : base(name, defaultValue, FieldType.IntSlider) {
    this.min = min;
    this.max = max;
    drawer = new IntSliderDrawer(min, max);
  }

  protected override IFieldDrawer<int> Drawer => drawer ??= new IntSliderDrawer(min, max);
}
}