using System;
using UnityEngine;
using VoroSystem.Voro.Compute.Graphs.Core;
using VoroSystem.Voro.Compute.Graphs.UI.Drawers;

namespace VoroSystem.Voro.Compute.Graphs.Fields {
[Serializable]
public class IntSlider : TypedFieldBase<int> {
    #region Serialized Fields
    [SerializeField] IntSliderDrawer drawer;
    #endregion

    public IntSlider(string name, int defaultValue, int min, int max)
        : base(name, defaultValue, FieldType.IntSlider) {
        drawer = new IntSliderDrawer(min, max);
    }

    protected override IFieldDrawer<int> Drawer => drawer;
}
}