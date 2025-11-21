using System.Collections.Generic;
using UnityEngine;
using VoroSystem.Voro.Designer.Canvas;
using VoroSystem.Voro.Designer.Canvas.Core;

namespace VoroSystem.Voro.Designer {
[ExecuteAlways]
public class VoroDesigner : MonoBehaviour {
    #region Serialized Fields
    [SerializeField] public Graph graph;
    [SerializeField] public CanvasTemplate template;
    #endregion

    #region Event Functions
    void Awake() {
        name = "Voro Designer";      
        graph = BuildGraphFromTemplate(template);
        // graph = new Graph();
    }
    void Start() {
        graph.LoadDefaults();
    }
    #endregion
    
    Graph BuildGraphFromTemplate(CanvasTemplate t) {
        Graph g = new Graph(t.graphName);

        foreach (Layer srcLayer in t.layers) {
            Layer newLayer = new Layer(srcLayer.layerName);

            foreach (Node srcNode in srcLayer.nodes) {
                Node newNode = new Node(srcNode.nodeName) {
                    operation = srcNode.operation,
                    fields = new List<FieldBase>(srcNode.fields)
                };
                newLayer.CreateNode(newNode);
            }

            g.CreateLayer(newLayer.layerName);
            g.layers[g.layers.Count - 1].nodes = newLayer.nodes;
        }

        return g;
    }
}
}