using System.Collections.Generic;
using UnityEngine;
using VoroSystem.Voro.Persistence;
using VoroSystem.VoroDataStructures.LayerDef;

namespace VoroSystem.VoroDataStructures.DiagramDef {
public static class DiagramFactory {
  public static Diagram LoadPreset(int index = 0) {
    var jsonPresets = PresetUtility.DiagramPresets;
    Debug.Log($"Found {jsonPresets.Length} presets");
    var dto = PresetUtility.ParseJsonToDiagramDTO(jsonPresets, index);
    var diagram = ConvertToDiagram(dto);
    return diagram;
  }

  static Diagram ConvertToDiagram(DiagramDataTransferObject dto) {
    var layers = new List<ILayer>();
    foreach (var layerDTO in dto.Layers) {
      layers.Add(LayerFactory.ConvertToLayer(layerDTO));
    }

    var diagram = new Diagram(dto.Name, layers);
    return diagram;
  }
}
}