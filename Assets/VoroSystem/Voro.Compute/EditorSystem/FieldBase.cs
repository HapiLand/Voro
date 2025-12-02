using System;
using UnityEngine;

namespace VoroSystem.Voro.Compute.EditorSystem {
[Serializable]
public abstract class FieldBase {
    [Serializable]
    public enum FieldType {
        FloatField,
        Radial,
        FloatSlider,
        Toggle,
        IntSlider
    }

    protected FieldBase(string fieldName, object defaultValue, FieldType type) {
        name = fieldName;
        this.defaultValue = defaultValue;
        this.type = type;
    }

    public abstract void DrawGUI();

    #region Serialized Fields
    [SerializeReference] public string name;
    [SerializeReference] public object defaultValue;
    [SerializeReference] public FieldType type;
    #endregion
}
}