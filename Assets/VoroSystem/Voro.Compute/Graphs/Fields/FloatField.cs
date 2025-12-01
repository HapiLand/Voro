using System;
using UnityEngine;
using VoroSystem.Voro.Compute.Graphs.Core;
using VoroSystem.Voro.Compute.Graphs.UI.Drawers;

namespace VoroSystem.Voro.Compute.Graphs.Fields {
[Serializable]
public class FloatField : TypedFieldBase<float> {
  #region Serialized Fields

  [SerializeField] FloatFieldDrawer drawer;

  #endregion

  public FloatField(string name, float defaultValue)
    : base(name, defaultValue, FieldType.FloatField) {
    drawer = new FloatFieldDrawer();
  }

  protected override IFieldDrawer<float> Drawer => drawer;
}
}