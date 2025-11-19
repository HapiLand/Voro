using UnityEngine;

namespace VoroSystem.Designer.GraphSystem {
[ExecuteAlways]
public class GraphComponent : MonoBehaviour {
  #region Serialized Fields

  [SerializeField] public Graph graph;

  #endregion

  #region Event Functions

  void Awake() {
    graph = new Graph();
  }

  void Start() {
    graph.LoadDefaults();
  }

  #endregion
}
}