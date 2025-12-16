using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VoroSystem.Voro.Compute.DiagramSystem.DTOs;
using VoroSystem.Voro.Compute.EffectSystem.Core;
using VoroSystem.Voro.Compute.EffectSystem.EffectDefinitions;
using VoroSystem.Voro.Core.Events;

namespace VoroSystem.Voro.Compute.DiagramSystem {
[Serializable]
public class Diagram {
  #region Serialized Fields
  [SerializeField] public string name;
  [SerializeField] public List<Layer> layers;
  [SerializeField] string newLayerName;
  #endregion

  Diagram(string name, List<Layer> layers) {
    this.name = name;
    this.layers = layers;
    DiagramEvents.GetInstance().OnCreateLayer += CreateLayer;
    DiagramEvents.GetInstance().OnRemoveLayer += RemoveLayer;
    DiagramEvents.GetInstance().OnMoveLayer += MoveLayer;
  }

  public Dictionary<string, List<BaseEffect>> GetEffectDictionary(out bool allow) {
    var dict = new Dictionary<string, List<BaseEffect>>();
    allow = false;

    if (layers.Count == 0) {
      allow = false;
      return dict;
    }

    foreach (var layer in layers) {
      dict.TryAdd(layer.name, new List<BaseEffect>());
      var effectList = dict[layer.name];

      effectList.AddRange(layer.nodes.Select(CreateEffect).Where(effect => effect != null));
    }

    allow = true;
    return dict;

    BaseEffect? CreateEffect(Node node) {
      switch (node.Type) {
      case EffectName.Slope: {
        var slope = new Effect<SlopeParameters>(EffectName.Slope)
        {
          Parameters =
          {
            Mode = node.Mode,
            Direction = node.GetParameter<float>("Direction"),
            Steepness = node.GetParameter<float>("Steepness"),
            Reverse = node.GetParameter<bool>("Reverse")
          }
        };
        slope.Init();
        return slope;
      }
      case EffectName.Noise: {
        var noise = new Effect<NoiseParameters>(EffectName.Noise)
        {
          Parameters =
          {
            Mode = node.Mode,
            Size = node.GetParameter<float>("Size"),
            Steepness = node.GetParameter<float>("Steepness")
          }
        };
        noise.Init();
        return noise;
      }
      case EffectName.Flat:
      case EffectName.Terrace:
      default:
        return null;
      }
    }
  }


  void MoveLayer(Layer layer, int direction) {
    var index = layers.IndexOf(layer);
    if (index == -1) {
      return;
    }

    var newIndex = index + direction;
    if (newIndex < 0 || newIndex >= layers.Count) {
      return;
    }

    layers.RemoveAt(index);
    layers.Insert(newIndex, layer);
  }

  void RemoveLayer(Layer layer) {
    layers.Remove(layer);
  }

  public static Diagram CreateFromDataTransferObject(DiagramDTO dto) {
    var layers = dto.layers.Select(layerDto => layerDto.ToLayer()).ToList();
    return new Diagram(dto.name, layers);
  }

  public void CreateLayer(string layerName) {
    if (string.IsNullOrWhiteSpace(layerName)) {
      return;
    }

    var layer = Layer.CreateNewInstance(layerName);
    layers.Add(layer);
  }
}
}