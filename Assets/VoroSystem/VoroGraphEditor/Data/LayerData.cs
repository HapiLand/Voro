using System;
using System.Collections.Generic;
using UnityEngine;
using VoroSystem.Voro.DataStructures.EffectDefinition.Core;

namespace VoroSystem.VoroGraphEditor.Data {
[Serializable]
public class LayerData {
  #region Serialized Fields
  public string layerName = "";

  [SerializeReference] public List<EffectData> effects = new();
  #endregion
}
}