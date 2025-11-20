using System;
using System.Collections.Generic;
using UnityEngine;

namespace VoroSystem.Voro.Designer.Graph {
[Serializable]
public class Layer {
  #region Serialized Fields

  [SerializeField] public string layerName;
  [SerializeField] public List<Node> nodes = new();

  #endregion

  public Layer(string layerName) {
    this.layerName = layerName;
  }

  public void CreateNode(Node def) {
    AddNode(def);
  }

  void AddNode(Node node) {
    nodes.Add(node);
  }

  public void LoadDefaults() {
    var def = NodeLookup.Get("Flat");
    CreateNode(def);
  }
}
}