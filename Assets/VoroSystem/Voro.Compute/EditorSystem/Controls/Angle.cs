using System;
using UnityEngine;

namespace VoroSystem.Voro.Compute.EditorSystem.Controls {
[Serializable]
public class Angle : TypedFieldBase<float> {
  #region Serialized Fields

  [SerializeField] AngleDrawer drawer;

  #endregion

  public Angle(string name, float defaultValue)
    : base(name, defaultValue, FieldType.Angle) {
    drawer = new AngleDrawer(0, 360f);
  }

  protected override IFieldDrawer<float> Drawer => drawer ??= new AngleDrawer(0f, 360f);
}
}