using System;
using UnityEngine;

namespace VoroSystem.Voro.DataStructures.EffectDefinition.ParameterDefinition.Core {
[Serializable]
public class ParameterData {
  #region Serialized Fields
  [SerializeReference] public ParameterValue defaultValue;
  public string parameterName;
  public ParameterVariants parameterType;
  #endregion

  public ParameterData(string parameterName, ParameterVariants parameterType, ParameterValue defaultValue) {
    this.parameterName = parameterName;
    this.parameterType = parameterType;
    this.defaultValue = defaultValue;
  }
}
}