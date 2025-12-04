using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace VoroSystem.Voro.Compute.EditorSystem {
[Serializable]
public abstract class FieldBase {
  #region FieldType enum

  [Serializable]
  public enum FieldType {
    FloatField,
    Angle,
    FloatSlider,
    Toggle,
    IntSlider
  }

  #endregion

  #region Serialized Fields

  [SerializeReference] public string name;
  [SerializeReference] public object defaultValue;
  [SerializeReference] public FieldType type;

  #endregion

  protected FieldBase(string fieldName, object defaultValue, FieldType type) {
    name = fieldName;
    this.defaultValue = defaultValue;
    this.type = type;
    // VoroComputeEvents.GetInstance().DiagramSystem.Field.RaiseCreated(this);
  }

  public abstract VisualElement FieldUI();
}
}