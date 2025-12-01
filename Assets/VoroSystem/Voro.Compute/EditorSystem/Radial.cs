using System;
using UnityEngine;

namespace VoroSystem.Voro.Compute.EditorSystem {
[Serializable]
public class Radial : TypedFieldBase<float> {
  #region Serialized Fields

  [SerializeField] RadialDrawer drawer;

  #endregion

  public Radial(string name, float defaultValue)
    : base(name, defaultValue, FieldType.Radial) {
    drawer = new RadialDrawer(0, 360f);
  }

  protected override IFieldDrawer<float> Drawer => drawer;
}
}