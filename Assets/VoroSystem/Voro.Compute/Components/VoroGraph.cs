using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using VoroSystem.Voro.Compute.DiagramSystem;

namespace VoroSystem.Voro.Compute.Components {



[ExecuteAlways]
public class VoroGraph : MonoBehaviour {
  #region Serialized Fields

  [SerializeField] public Graph graph;
  [SerializeField] public TextAsset jsonSource;

  #endregion

  #region Event Functions

  void Awake() {
    jsonSource = Resources.Load<TextAsset>("Template");
    LoadGraph();
  }

  #endregion

  void LoadGraph() {
    graph = new Graph();
    graph = JsonConvert.DeserializeObject<Graph>(jsonSource.text);
    foreach (var node in graph.layers.SelectMany(layer => layer.nodes)) {
      node.ConvertFields();
    }
  }
}
}