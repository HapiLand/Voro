using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using VoroSystem.Voro.Compute.Graphs;
using VoroSystem.Voro.Compute.V2;

namespace VoroSystem.Voro.Compute {
[ExecuteAlways]
public class VoroDiagram : MonoBehaviour {
  // ReSharper disable once UnassignedField.Global
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

  #endregion

  void LoadDiagram(TextAsset asset) {
    diagram = new Diagram();

    // var settings = new JsonSerializerSettings
    // {
    //   Converters = new List<JsonConverter> { new NodeConverter() }
    // };
    // diagram = JsonConvert.DeserializeObject<Diagram>(asset.text, settings);
    foreach (var node in diagram.layers.SelectMany(layer => layer.nodes)) { }
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