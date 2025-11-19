using System;
using UnityEngine;
using VoroSystem.Designer.GraphSystem.Core;
using VoroSystem.Designer.GraphSystem.UI.Drawers;

namespace VoroSystem.Designer.GraphSystem.Fields {
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