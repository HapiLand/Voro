using System;
using UnityEngine;

namespace VoroSystem.Voro.Compute.EditorSystem {
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