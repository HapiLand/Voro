using System;
using Newtonsoft.Json;
using UnityEngine;
using VoroSystem.Voro.Compute.Graphs;

namespace VoroSystem.Voro.Compute {
[ExecuteAlways]
public class VoroCompute : MonoBehaviour {
    public static Action OnCompute;
    public static Action OnChanged;
    

    #region Event Functions
    void Awake() {
        name = "Voro Compute";
        jsonSource = Resources.Load<TextAsset>("Template");
        LoadGraph();
    }
    [SerializeField] public Graph graph;
    [SerializeField] public TextAsset jsonSource;
    void LoadGraph() {
        graph = new Graph();
        graph = JsonConvert.DeserializeObject<Graph>(jsonSource.text);
        foreach (var layer in graph.layers) {
            foreach (var node in layer.nodes) {
                node.ConvertFields();
            }
        }
    }

    void Start() {
      OnCompute?.Invoke();
    }
    #endregion
}
}