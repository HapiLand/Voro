using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using VoroSystem.VoroDataStructures.EffectDefinition.Core;

// ReSharper disable InconsistentNaming

namespace VoroSystem.VoroGraphEditor.Data {
[Serializable]
public class LayerDataDTO {
  #region Serialized Fields
  [JsonProperty("Name")] public string name;
  [JsonProperty("Effects")] public List<EffectDataDTO> effectDTOList;
  #endregion
}
}