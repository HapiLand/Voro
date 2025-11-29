using System;
using Newtonsoft.Json;
using UnityEngine;
using VoroSystem.Voro.Designer.Canvas;

namespace VoroSystem.Voro.Designer {
[ExecuteAlways]
public class VoroDesigner : MonoBehaviour {
    public static Action OnChanged;

    #region Event Functions
    void Awake() {
        name = "Voro Designer";
        jsonSource = Resources.Load<TextAsset>("Template");
        LoadGraph();
    }
    #endregion

    void LoadGraph() {
        graph = new Graph();
        graph = JsonConvert.DeserializeObject<Graph>(jsonSource.text);
        foreach (var layer in graph.layers) {
            foreach (var node in layer.nodes) {
                node.ConvertFields();
            }
        }
    }

    #region Serialized Fields
    [SerializeField] public Graph graph;
    [SerializeField] public TextAsset jsonSource;
    #endregion
}
}