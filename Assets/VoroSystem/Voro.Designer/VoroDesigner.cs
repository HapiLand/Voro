using System;
using UnityEngine;
using VoroSystem.Voro.Designer.Canvas;

namespace VoroSystem.Voro.Designer {
[ExecuteAlways]
public class VoroDesigner : MonoBehaviour {
  public static Action OnChanged;

  #region Serialized Fields

  [SerializeField] public Graph graph;

  #endregion

  #region Event Functions

  void Awake() {
    name = "Voro Designer";
    graph = new Graph();
  }

  #endregion
}
}