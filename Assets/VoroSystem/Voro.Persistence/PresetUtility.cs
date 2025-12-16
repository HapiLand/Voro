using Newtonsoft.Json.Linq;
using UnityEngine;
using VoroSystem.Voro.DataStructures.DiagramDef;

namespace VoroSystem.Voro.Persistence {
public static class PresetUtility {
  /// <summary>
  /// TerrainPreset
  /// {
  /// "Name": "Preset Zero",
  /// "Layers": [ ... ]
  /// }
  /// TerrainLayer
  /// {
  /// "Name": "Simple Layer",
  /// "Nodes": [ ... ]
  /// }
  /// TerrainNode
  /// {
  /// "Name": "Slope",
  /// "Mode": "Set",
  /// "Controls": [ ... ]
  /// }
  /// </summary>

  public static TextAsset[] DiagramPresets {
    get
    {
      var jsonFiles = Resources.LoadAll<TextAsset>("JSON/Diagram");
      return jsonFiles;
    }
  }

  public static DiagramDataTransferObject ParseJsonToDiagramDTO(TextAsset[] jsonFiles, int index) {
    var json = jsonFiles[index];
    var parsedObject = JObject.Parse(json.text);

    // parse diagram
    var diagram = parsedObject.ToObject<DiagramDataTransferObject>();
    Debug.Log($"[Diagram {diagram.Name}] Layers: {diagram.Layers.Count}");

    // parse layers
    foreach (var layer in diagram.Layers) {
      Debug.Log($"[Layer {layer.Name}] Nodes: {layer.Nodes.Count}");

      // parse nodes
      foreach (var node in layer.Nodes) {
        Debug.Log($"[Node {node.Name}] Mode: {node.Mode}, Controls: {node.Controls.Count}");

        // parse controls
        foreach (var control in node.Controls) {
          Debug.Log($"[Control {control.Name}] Type: {control.Type}, Value: {control.Value}");
        }
      }
    }

    return diagram;
  }
}
}