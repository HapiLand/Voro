using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace VoroSystem.Voro.GraphEditor.Data {
public class GraphScriptableObject : ScriptableObject {
  #region Serialized Fields

  public string graphName = "Example Name";
  public List<LayerData> layers = new();

  #endregion
  
  [Serializable]
  // ReSharper disable InconsistentNaming
  public class DTO {
    // ReSharper restore InconsistentNaming
    #region Serialized Fields
    [JsonProperty("Name")] public string name;
    [JsonProperty("Layers")] public List<LayerData.DTO> layers = new();
    #endregion
  }
}
}