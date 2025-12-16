using System.Collections.Generic;
using UnityEngine;
using VoroSystem.Voro.DataStructures.EffectDefinition;
using VoroSystem.Voro.DataStructures.EffectDefinition.Core;

namespace VoroSystem.VoroGraphEditor.Data {
public class GraphScriptableObject : ScriptableObject {
  #region Serialized Fields
  public string graphName = "";
  public List<LayerData> layers = new();
  #endregion

  #region Event Functions
  void OnEnable() {
    graphName = "Graph Name";
    layers = new List<LayerData>
    {
      new()
      {
        layerName = "Layer",
        effects = new List<EffectData>
        {
          EffectFactory.Create(EffectVariants.Slope)
        }
      }
    };
  }
  #endregion
}
}