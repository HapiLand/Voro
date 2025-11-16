using System;
using UnityEngine;
using VoroSystem.Designer.GraphSystemV2.Core;
using VoroSystem.Designer.GraphSystemV2.UI.Drawers;

namespace VoroSystem.Designer.GraphSystemV2.Fields {
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