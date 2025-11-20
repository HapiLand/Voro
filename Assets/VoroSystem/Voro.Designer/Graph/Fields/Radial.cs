using System;
using UnityEngine;
using VoroSystem.Voro.Designer.Graph.Core;
using VoroSystem.Voro.Designer.Graph.UI.Drawers;

namespace VoroSystem.Voro.Designer.Graph.Fields {
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