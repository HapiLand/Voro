using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using VoroSystem.Voro.Compute.V2.ScriptableObjects.Base;

namespace VoroSystem.Voro.Compute.V2 {
[Serializable]
public class Diagram {
  #region Serialized Fields

  [SerializeField] [JsonProperty("DiagramName")]
  public string diagramName;

  [SerializeField] [JsonProperty("Layers")]
  public List<Layer> layers = new();

  #endregion

  public Diagram() { }

  public Diagram(string name) {
    diagramName = name;
  }

  public void CreateLayer(string name) {
    layers.Add(new Layer(name));
  }

  #region Nested type: ${0}

  [Serializable]
  public class Layer {
    #region Serialized Fields

    [SerializeField] [JsonProperty("LayerName")]
    public string layerName;

    [SerializeField] [JsonProperty("Nodes")]
    public List<Node> nodes = new();
    
    #endregion

    public Layer() { }

    public Layer(string name) {
      layerName = name;
    }
    
    public Node CreateNode(NodeDefinition def, OperationMode mode = OperationMode.Set) {
      var node = Node.CreateInstance(def, mode);
      nodes.Add(node);
      return node;
    }

    #region Nested type: ${0}

    [Serializable]
    public class Node {
      #region Serialized Fields

      [SerializeField] public NodeType type;

      [SerializeField] public OperationMode mode;

      public List<NodeData> data = new();

      #endregion

      public static Node CreateInstance(NodeDefinition def, OperationMode mode = OperationMode.Set) {
        var node = new Node
        {
          type = def.Type,
          mode = mode
        };

        foreach (var dataDef in def.DataDefinitions) {
          node.data.Add(new NodeData(dataDef));
        }

        return node;
      }
      #endregion
    }

    #endregion
  }
}
}