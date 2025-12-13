using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace VoroSystem.Voro.GraphEditor.Data {
[Serializable]
public class GraphDataObject {
  #region Serialized Fields

  [JsonProperty("Name")] public string name;
  [JsonProperty("Foo")] public List<LayerDataObject> foo = new();

  #endregion
}
}