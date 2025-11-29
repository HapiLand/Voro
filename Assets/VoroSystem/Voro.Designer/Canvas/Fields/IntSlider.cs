using System;
using UnityEngine;
using VoroSystem.Voro.Designer.Canvas.Core;
using VoroSystem.Voro.Designer.Canvas.UI.Drawers;

namespace VoroSystem.Voro.Designer.Canvas.Fields {
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