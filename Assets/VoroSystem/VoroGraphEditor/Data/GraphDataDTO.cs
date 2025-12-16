using System;
using System.Collections.Generic;
using Newtonsoft.Json;

// ReSharper disable InconsistentNaming

namespace VoroSystem.VoroGraphEditor.Data {
[Serializable]
public class GraphDataDTO {
  #region Serialized Fields
  [JsonProperty("Name")] public string name;
  [JsonProperty("Layers")] public List<LayerDataDTO> layerDTOList;
  #endregion
}
}