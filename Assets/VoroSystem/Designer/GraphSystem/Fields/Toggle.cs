using System;
using UnityEngine;
using VoroSystem.Designer.GraphSystem.Core;
using VoroSystem.Designer.GraphSystem.UI.Drawers;

namespace VoroSystem.Designer.GraphSystem.Fields {
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