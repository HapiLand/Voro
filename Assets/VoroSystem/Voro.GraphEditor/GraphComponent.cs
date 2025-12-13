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
    Debug.Log($"[Graph] {graphData.graphName}");

    for (var i = 0; i < graphData.layers.Count; i++) {
      var layer = graphData.layers[i];
      Debug.Log($"  Layer[{i}] {layer.layerName}");

      for (var j = 0; j < layer.effects.Count; j++) {
        var effect = layer.effects[j];
        Debug.Log($"    Effect[{j}] {effect.variantType}");

        for (var k = 0; k < effect.controls.Count; k++) {
          var control = effect.controls[k];
          Debug.Log($"      Control[{k}] {control.controlName} {control.variantType}");
        }
      }
    }
  }

  #endregion
}
}