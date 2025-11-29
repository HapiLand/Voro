using System;
using UnityEngine;
using VoroSystem.Voro.Compute.Graphs.Core;
using VoroSystem.Voro.Compute.Graphs.UI.Drawers;

namespace VoroSystem.Voro.Compute.Graphs.Fields {
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