using System.Collections.Generic;
using System.Text;
using UnityEngine;
using VoroSystem.VoroDataStructures.EffectDefinition;
using VoroSystem.VoroDataStructures.EffectDefinition.Core;

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
          // EffectFactory.Create(EffectVariants.Slope),
          EffectFactory.Create(EffectVariants.Noise),
        }
      }
    };
  }
  #endregion

  public override string ToString() {
    var sb = new StringBuilder();
    sb.AppendLine($"Graph Name: {graphName}");
    sb.AppendLine("Layers:");

    for (var i = 0; i < layers.Count; i++) {
      sb.AppendLine($"  Layer {i}: {layers[i]}");
    }

    return sb.ToString();
  }
}
}