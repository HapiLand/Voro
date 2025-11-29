using System;
using UnityEngine;
using VoroSystem.Voro.Compute.Graphs.Core;
using VoroSystem.Voro.Compute.Graphs.UI.Drawers;

namespace VoroSystem.Voro.Compute.Graphs.Fields {
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