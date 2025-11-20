using UnityEngine;

namespace VoroSystem.Voro.Designer {
public class GraphComponent : MonoBehaviour {
  #region Serialized Fields

  [SerializeField] public Graph.Graph graph;

  #endregion

  #region Event Functions

  void Awake() {
    graph = new Graph.Graph();
  }

  void Start() {
    graph.LoadDefaults();
  }

  #endregion
}
}