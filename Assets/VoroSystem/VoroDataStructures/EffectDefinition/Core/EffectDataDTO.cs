using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using VoroSystem.VoroDataStructures.EffectDefinition.ParameterDefinition.Core;

namespace VoroSystem.VoroDataStructures.EffectDefinition.Core {
// ReSharper disable InconsistentNaming
[Serializable]
public class EffectDataDTO {
  #region Serialized Fields
  [JsonProperty("EffectVariant")] public EffectVariants effectType;
  [JsonProperty("OperationVariant")] public OperationVariants operationType;

  [JsonProperty("Parameters")] [SerializeReference]
  public List<ParameterDataDTO> parameterDTOList;
  #endregion
}
// ReSharper restore InconsistentNaming
}