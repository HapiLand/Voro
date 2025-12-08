using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VoroSystem.Voro.Compute.DiagramSystem.DTOs;
using VoroSystem.Voro.Compute.EditorSystem;
using VoroSystem.Voro.Compute.EditorSystem.Controls;

namespace VoroSystem.Voro.Compute.DiagramSystem {
[Serializable]
public class Node {
  #region Serialized Fields

  [SerializeField] EffectName type;
  [SerializeField] OperationMode mode;
  [SerializeReference] List<ControlBase> controls;

  #endregion

  Node(EffectName type, OperationMode mode, List<ControlBase> controls) {
    this.type = type;
    this.mode = mode;
    this.controls = controls;
  }


  public EffectName Type => type;

  public OperationMode Mode {
    get => mode;
    set => mode = value;
  }

  public List<ControlBase> Controls => controls;
  public string Name => Type.ToString();

  public static Node CreateFromDataTransferObject(NodeDTO dto) {
    var fields = dto.fields.Select(fieldDto => fieldDto.ToFieldBase()).ToList();
    return new Node(dto.type, dto.mode, fields);
  }

  public static Node CreateNewInstance(EffectName type) {
    var fieldDtos = FieldsLookup.LoadFields(type);
    var fields = fieldDtos.Select(dto => dto.ToFieldBase()).ToList();
    return new Node(type, OperationMode.Set, fields);
  }

  public T GetParameter<T>(string name) {
    foreach (var c in controls) {
      if (c.name == name && c is FloatInputControl floatInput) {
        return (T)(object)floatInput.Value;
      }
      if (c.name == name && c is ToggleControl toggle) {
        return (T)(object)toggle.Value;
      }
    }
    throw new InvalidOperationException();
  }
}
}