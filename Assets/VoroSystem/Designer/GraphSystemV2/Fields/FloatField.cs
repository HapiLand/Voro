using System;
using UnityEngine;
using VoroSystem.Designer.GraphSystemV2.Core;
using VoroSystem.Designer.GraphSystemV2.UI.Drawers;

namespace VoroSystem.Designer.GraphSystemV2.Fields {
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