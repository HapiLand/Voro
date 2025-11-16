using UnityEngine;
using VoroSystem.Designer.GraphSystemV2;

namespace VoroSystem.Designer {
[ExecuteAlways]
[RequireComponent(typeof(GraphComponent))]
public class VoroDesignerComponent : MonoBehaviour {
    #region Serialized Fields

    [SerializeField] GraphComponent graphComponent;

    #endregion

    #region Event Functions

    void Awake() {
        graphComponent ??= GetComponent<GraphComponent>();
        name = "VoroDesigner";
    }

    #endregion
}
}