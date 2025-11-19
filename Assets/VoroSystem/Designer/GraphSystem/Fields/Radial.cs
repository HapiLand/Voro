using System;
using UnityEngine;
using VoroSystem.Designer.GraphSystem.Core;
using VoroSystem.Designer.GraphSystem.UI.Drawers;

namespace VoroSystem.Designer.GraphSystem.Fields {
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