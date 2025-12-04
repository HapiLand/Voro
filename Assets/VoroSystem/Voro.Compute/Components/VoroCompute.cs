using UnityEngine;
using VoroSystem.Voro.Compute.DiagramSystem;
using VoroSystem.Voro.Core;

namespace VoroSystem.Voro.Compute.Components {
[ExecuteAlways]
[RequireComponent(typeof(VoroDiagram))]
public class VoroCompute : MonoBehaviour {
  #region Serialized Fields

  public VoroDiagram voroDiagram;

  #endregion

  public Diagram Diagram => voroDiagram.diagram;

  #region Event Functions

  void Awake() {
    name = "Voro Compute";
    voroDiagram = GetComponent<VoroDiagram>();
  }
  #endregion
}
}