using UnityEngine;
using VoroSystem.Designer.GraphSystem;

namespace VoroSystem.Designer {
[ExecuteAlways]
[RequireComponent(typeof(GraphComponent))]
public class VoroDesignerComponent : MonoBehaviour {
  #region Serialized Fields

  [SerializeField] public GraphComponent graphComponent;

  #endregion

  #region Event Functions

  void Awake() {
    graphComponent ??= GetComponent<GraphComponent>();
    name = "VoroDesigner";
  }

  #endregion
}
}