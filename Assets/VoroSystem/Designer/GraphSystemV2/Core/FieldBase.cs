using System;
using UnityEngine;

namespace VoroSystem.Designer.GraphSystemV2.Core {
[Serializable]
public abstract class FieldBase {
    #region Serialized Fields

    [SerializeReference] public string name;
    [SerializeReference] public object defaultValue;
    [SerializeReference] public FieldType type;

    #endregion

    protected FieldBase(string fieldName, object defaultValue, FieldType type) {
        name = fieldName;
        this.defaultValue = defaultValue;
        this.type = type;
    }

    public abstract void DrawGUI();
}
}