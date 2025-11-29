using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using VoroSystem.Voro.Compute.V2.ScriptableObjects.Core;

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
    
    [SerializeField] NodeRegistry registry;

    #endregion

    public Layer() { }

    public Layer(string name) {
      layerName = name;
    }
    
    public void CreateNode(NodeType type, OperationMode mode = OperationMode.Set)
    {
      var def = registry.GetDefinition(type);
      nodes.Add(Node.CreateInstance(def, mode));
    }
    
    /*public void CreateNode(NodeType type) {
      nodes.Add(Node.CreateInstance(type));
    }*/

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


      /*Node(NodeType type, OperationMode mode) {
        this.type = type;
        this.mode = mode;
      }*/

      /*/// <summary>
      /// Factory method is used to construct Node according to the value of NodeType.
      /// </summary>
      /// <param name="type"> </param>
      /// <param name="mode"> </param>
      /// <returns> </returns>
      public static Node CreateInstance(NodeType type, OperationMode mode = OperationMode.Set) {
        switch (type) {
        case NodeType.Debug: {
          {
            var node = new Node(type, mode);
            node.data.Add(new NodeData("Slider Float", DataType.SliderFloat));
            // node.data.Add(new NodeData("", DataType.SliderFloat_Log));
            // node.data.Add(new NodeData("", DataType.SliderInt));
            // node.data.Add(new NodeData("", DataType.SliderInt_Log));
            // node.data.Add(new NodeData("Toggle", DataType.Toggle));
            // node.data.Add(new NodeData("", DataType.InputFloat));
            // node.data.Add(new NodeData("", DataType.InputInt));
            // node.data.Add(new NodeData("Input Text", DataType.InputText));
            // node.data.Add(new NodeData("", DataType.Angle));
            // node.data.Add(new NodeData("", DataType.Button));
            // node.data.Add(new NodeData("", DataType.Color));
            // node.data.Add(new NodeData("", DataType.Position2D));
            // node.data.Add(new NodeData("", DataType.Position3D));
            // node.data.Add(new NodeData("", DataType.RampColor));
            // node.data.Add(new NodeData("", DataType.RampFloat));
            return node;
          }
        }
        case NodeType.Slope:
        {
          var node = new Node(type, mode);
          // node.data.Add(new NodeData("Direction", DataType.Angle));
          // node.data.Add(new NodeData("Steepness", DataType.SliderFloat));
          // node.data.Add(new NodeData("Reverse", DataType.Toggle));
          return node;
        }
        case NodeType.Noise:
        case NodeType.Flat:
        case NodeType.Terrace:
          throw new NotImplementedException(nameof(type));
        default:
          throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }


        return new Node(type, mode);
      }
    }*/

      #endregion
    }

    #endregion
  }
}
}