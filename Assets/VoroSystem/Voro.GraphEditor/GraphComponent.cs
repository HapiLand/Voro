using UnityEngine;
using VoroSystem.Voro.GraphEditor.Data;

namespace VoroSystem.Voro.GraphEditor {
[ExecuteAlways]
public class GraphComponent : MonoBehaviour {
  #region Serialized Fields

  [SerializeField] GraphScriptableObject graphData;

  #endregion

  #region Event Functions

  void Start() {
    Debug.Log($"Name: {graphData.Name}");
    foreach (var layerData in graphData.Foo)
    {
      Debug.Log($"Number: {layerData.Number}, Toggle: {layerData.Toggle}");
    }
  }

  #endregion
}
}