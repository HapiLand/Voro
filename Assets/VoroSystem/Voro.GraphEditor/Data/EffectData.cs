using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace VoroSystem.Voro.GraphEditor.Data {
[Serializable]
public class EffectData {
  #region Serialized Fields
  public string variantType;
  public string operation;
  public List<ControlData> controls;
  #endregion
  
  [Serializable]
  // ReSharper disable InconsistentNaming
  public class DTO {
    // ReSharper restore InconsistentNaming
    #region Serialized Fields
    [JsonProperty("Variant")] public string variantType;
    [JsonProperty("Mode")] public string operation;
    [JsonProperty("Controls")] public List<ControlData.DTO> controls = new();
    #endregion
  }
}
}