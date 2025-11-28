using System;
using UnityEngine;
using VoroSystem.Voro.Designer.Canvas.Core;
using VoroSystem.Voro.Designer.Canvas.UI.Drawers;

namespace VoroSystem.Voro.Designer.Canvas.Fields {
[Serializable]
public class Toggle : TypedFieldBase<bool> {
  #region Serialized Fields

  [SerializeField] ToggleDrawer drawer;

  #endregion

  public Toggle(string name, bool defaultValue)
    : base(name, defaultValue, FieldType.Toggle) { }

  protected override IFieldDrawer<bool> Drawer => drawer;
}
}