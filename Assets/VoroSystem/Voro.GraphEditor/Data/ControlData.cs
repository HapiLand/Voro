using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace VoroSystem.Voro.GraphEditor.Data {
[Serializable]
public class ControlData {
  #region Serialized Fields
  public string controlName;
  public string variantType;
  // ReSharper disable InconsistentNaming
  public object defaultValue;
  // ReSharper restore InconsistentNaming
  #endregion
  
  [Serializable]
  // ReSharper disable InconsistentNaming
  public class DTO {
    // ReSharper restore InconsistentNaming
    #region Serialized Fields
    [JsonProperty("Name")] public string name;
    [JsonProperty("Variant")] public string variantType;
    #endregion
    // ReSharper disable InconsistentNaming
    [JsonProperty("Value")] public object defaultValue;
    // ReSharper restore InconsistentNaming
  }
}
}