using System;
using UnityEngine;
using VoroSystem.Voro.Compute.Graphs;

namespace VoroSystem.Voro.Compute {
[ExecuteAlways]
[RequireComponent(typeof(VoroGraph))]
public class VoroCompute : MonoBehaviour {
  public static Action OnCompute;
  public static Action OnChanged;
  public Graph Graph => voroGraph.graph;

  public VoroGraph voroGraph;

  #region Event Functions

  void Awake() {
    name = "Voro Compute";
    voroGraph = GetComponent<VoroGraph>();
  }

  void Start() {
    OnCompute?.Invoke();
  }

  #endregion
}
}