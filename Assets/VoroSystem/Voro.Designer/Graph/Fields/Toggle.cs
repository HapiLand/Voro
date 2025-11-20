using System;
using UnityEngine;
using VoroSystem.Voro.Designer.Graph.Core;
using VoroSystem.Voro.Designer.Graph.UI.Drawers;

namespace VoroSystem.Voro.Designer.Graph.Fields {
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