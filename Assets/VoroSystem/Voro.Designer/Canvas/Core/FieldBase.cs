using System;
using UnityEngine;

namespace VoroSystem.Voro.Designer.Canvas.Core {
[Serializable]
public abstract class FieldBase {
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