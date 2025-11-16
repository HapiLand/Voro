using System;
using UnityEngine;
using VoroSystem.Designer.GraphSystemV2.Core;
using VoroSystem.Designer.GraphSystemV2.UI.Drawers;

namespace VoroSystem.Designer.GraphSystemV2.Fields {
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