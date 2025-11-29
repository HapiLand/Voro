using System;
using UnityEngine;
using VoroSystem.Voro.Compute.Graphs;

namespace VoroSystem.Voro.Compute {
[ExecuteAlways]
[RequireComponent(typeof(VoroDiagram))]
public class VoroCompute : MonoBehaviour {
  public static Action OnCompute;

  #region Serialized Fields

  [SerializeField] VoroDiagram voroDiagram;

  #endregion

  public Graph Graph => voroDiagram.graph;

  #region Event Functions

  void Awake() {
    name = "Voro Compute";
    voroDiagram = GetComponent<VoroDiagram>();
  }

  void Start() {
    OnCompute?.Invoke();
  }

  #endregion
}
}