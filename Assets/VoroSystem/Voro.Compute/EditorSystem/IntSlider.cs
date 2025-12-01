using System;
using UnityEngine;

namespace VoroSystem.Voro.Compute.EditorSystem {
[Serializable]
public class IntSlider : TypedFieldBase<int> {
  #region Serialized Fields

  [SerializeField] IntSliderDrawer drawer;

  #endregion

  public IntSlider(string name, int defaultValue, int min, int max)
    : base(name, defaultValue, FieldType.IntSlider) {
    drawer = new IntSliderDrawer(min, max);
  }

  protected override IFieldDrawer<int> Drawer => drawer;
}
}