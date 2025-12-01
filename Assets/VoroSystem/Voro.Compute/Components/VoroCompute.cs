using System;
using UnityEngine;
using VoroSystem.Voro.Compute.DiagramSystem;

namespace VoroSystem.Voro.Compute.Components {
[ExecuteAlways]
[RequireComponent(typeof(VoroGraph))]
public class VoroCompute : MonoBehaviour {
  public static Action OnCompute;
  public static Action OnChanged;

  #region Serialized Fields

  public VoroGraph voroGraph;

  #endregion

  public Graph Graph => voroGraph.graph;

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