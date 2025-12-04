using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VoroSystem.Voro.Compute.DiagramSystem.DTOs;
using VoroSystem.Voro.Core;
using VoroSystem.Voro.Utilities;

namespace VoroSystem.Voro.Compute.DiagramSystem {
[Serializable]
public class Diagram {
  #region Serialized Fields

  [SerializeField] public string name;
  [SerializeField] public List<Layer> layers;
  [SerializeField] string newLayerName;
  [SerializeField] VoroEvents events;

  #endregion

  Diagram(string name, List<Layer> layers) {
    this.name = name;
    this.layers = layers;
    events = VoroEvents.GetInstance();
    events.OnNewLayerEvent += CreateLayer;
  }

  public static Diagram CreateFromDataTransferObject(DiagramDTO dto) {
    var layers = dto.layers.Select(layerDto => layerDto.ToLayer()).ToList();
    return new Diagram(dto.name, layers);
  }

  public void CreateLayer(string layerName) {
    layerName = string.IsNullOrWhiteSpace(layerName) ? "New Layer" : layerName;
    var layer = Layer.CreateNewInstance(layerName);
    layers.Add(layer);
  }

}
}