using System;
using Newtonsoft.Json;

namespace VoroSystem.Voro.GraphEditor.Data {
[Serializable]
public class LayerDataObject {
  #region Serialized Fields

  [JsonProperty("Number")] public float number;
  [JsonProperty("Toggle")] public bool toggle;

  #endregion
}
}