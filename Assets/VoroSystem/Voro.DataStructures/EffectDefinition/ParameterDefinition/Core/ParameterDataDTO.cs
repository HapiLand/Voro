using System;
using Newtonsoft.Json;
using UnityEngine;

namespace VoroSystem.Voro.DataStructures.EffectDefinition.ParameterDefinition.Core {
// ReSharper disable InconsistentNaming
[Serializable]
public class ParameterDataDTO {
  #region Serialized Fields
  [JsonProperty("Name")] public string name;
  [JsonProperty("ParameterVariant")] public ParameterVariants parameterType;

  [JsonProperty("Value")] [SerializeReference]
  public object defaultValue;
  #endregion
}
// ReSharper restore InconsistentNaming
}