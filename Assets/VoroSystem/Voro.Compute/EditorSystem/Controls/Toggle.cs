using System;
using UnityEngine;

namespace VoroSystem.Voro.Compute.EditorSystem.Controls {
[Serializable]
public class Toggle : TypedFieldBase<bool> {
  #region Serialized Fields

  [SerializeField] ToggleDrawer drawer;

  #endregion

  public Toggle(string name, bool defaultValue)
    : base(name, defaultValue, FieldType.Toggle) { }

  protected override IFieldDrawer<bool> Drawer => drawer ??= new ToggleDrawer();
}
}