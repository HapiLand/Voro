using System;
using UnityEngine;
using VoroSystem.Voro.Designer.Canvas.Core;
using VoroSystem.Voro.Designer.Canvas.UI.Drawers;

namespace VoroSystem.Voro.Designer.Canvas.Fields {
[Serializable]
public class Radial : TypedFieldBase<float> {
    #region Serialized Fields
    [SerializeField] RadialDrawer drawer;
    #endregion

    public Radial(string name, float defaultValue)
        : base(name, defaultValue, FieldType.Radial) {
        drawer = new RadialDrawer(0, 360f);
    }

    protected override IFieldDrawer<float> Drawer => drawer;
}
}