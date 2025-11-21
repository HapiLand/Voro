using System;
using UnityEngine;
using VoroSystem.Voro.Designer.Canvas.Core;
using VoroSystem.Voro.Designer.Canvas.UI.Drawers;

namespace VoroSystem.Voro.Designer.Canvas.Fields {
[Serializable]
public class FloatField : TypedFieldBase<float> {
    #region Serialized Fields
    [SerializeField] FloatFieldDrawer drawer;
    #endregion

    public FloatField(string name, float defaultValue)
        : base(name, defaultValue, FieldType.FloatField) {
        drawer = new FloatFieldDrawer();
    }

    protected override IFieldDrawer<float> Drawer => drawer;
}
}