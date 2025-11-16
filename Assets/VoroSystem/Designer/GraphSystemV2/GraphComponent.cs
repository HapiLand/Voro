using UnityEngine;

namespace VoroSystem.Designer.GraphSystemV2 {
[ExecuteAlways]
public class GraphComponent : MonoBehaviour {
    #region Serialized Fields

    [SerializeField] public Graph graph;

    #endregion

    #region Event Functions

    void Awake() {
        graph = new Graph();
    }

    #endregion
}
}