using System;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using VoroSystem.Voro.Compute.Graphs;
using VoroSystem.Voro.Compute.V2;
using VoroSystem.Voro.Compute.V2.ScriptableObjects.Base;

// ReSharper disable UnassignedField.Global

namespace VoroSystem.Voro.Compute {
[ExecuteAlways]
public class VoroDiagram : MonoBehaviour {
  public static Action OnChanged;

  #region Serialized Fields

  [SerializeField] public Diagram diagram;
  [SerializeField] public Graph graph;

  #endregion

  #region Event Functions

  void Awake() {
    LoadGraph(Resources.Load<TextAsset>("Template"));
    LoadDiagram(Resources.Load<TextAsset>("Diagram"));
  }

  void OnDestroy() {
    if (diagram == null) {
      return;
    }
    foreach (var nodeData in diagram.layers.SelectMany(layer => layer.nodes.SelectMany(node => node.data))) {
      var dataDef = nodeData.dataDefinition;
      // ReSharper disable once EventUnsubscriptionViaAnonymousDelegate
      dataDef.OnValueChanged -= properties => { OnChanged?.Invoke(); };
    }

  }

  #endregion

  public void CreateLayer(string layerName) {
    diagram.CreateLayer(layerName);
    OnChanged?.Invoke();
  }

  public void CreateNode(Diagram.Layer layer, NodeType type, OperationMode mode = OperationMode.Set) {
    var registry = Resources.Load<NodeRegistry>("Registry");
    var nodeDefinition = registry.GetDefinition(type);
    var node = layer.CreateNode(nodeDefinition, mode);

    // subscribe to detect changes
    foreach (var dataDef in nodeDefinition.DataDefinitions) {
      dataDef.OnValueChanged += properties => { OnChanged?.Invoke(); };
    }
  }

  public void SetNodeMode(Diagram.Layer.Node node, OperationMode mode) {
    if (node.mode == mode) {
      return;
    }

    node.mode = mode;
    OnChanged?.Invoke();
  }

  public void LoadDiagram(TextAsset asset) {
    var dto = JsonConvert.DeserializeObject<DiagramDto>(asset.text);
    var registry = Resources.Load<NodeRegistry>("Registry");
    diagram = new Diagram(dto.diagramName);

    foreach (var layerDto in dto.layers) {
      var layer = new Diagram.Layer(layerDto.layerName);

      foreach (var dataDef in from nodeDto in layerDto.nodes
               let nodeDefinition = registry.GetDefinition(nodeDto.nodeType)
               let node = layer.CreateNode(nodeDefinition, nodeDto.mode)
               from dataDef in nodeDefinition.DataDefinitions
               select dataDef) {
        dataDef.OnValueChanged += properties => { OnChanged?.Invoke(); };
      }

      diagram.layers.Add(layer);
    }

    OnChanged?.Invoke();
  }

  void LoadGraph(TextAsset asset) {
    graph = new Graph();
    graph = JsonConvert.DeserializeObject<Graph>(asset.text);
    foreach (var node in graph.layers.SelectMany(layer => layer.nodes)) {
      node.ConvertFields();
    }
  }
}
}