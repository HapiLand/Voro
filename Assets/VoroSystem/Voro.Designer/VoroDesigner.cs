using UnityEngine;

namespace VoroSystem.Voro.Designer {
[RequireComponent(typeof(GraphComponent))]
public class VoroDesigner : MonoBehaviour {
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