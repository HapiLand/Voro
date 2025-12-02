using System;
using UnityEngine;
using VoroSystem.Voro.Compute.DiagramSystem;

namespace VoroSystem.Voro.Compute.Components {
[ExecuteAlways]
[RequireComponent(typeof(VoroDiagram))]
public class VoroCompute : MonoBehaviour {
    public static Action OnCompute;
    public static Action OnChanged;

    #region Serialized Fields
    public VoroDiagram voroDiagram;
    #endregion

    public Diagram Diagram => voroDiagram.diagram;

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