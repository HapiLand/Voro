using UnityEngine;
using VoroSystem.Voro.Compute.DiagramSystem;
using VoroSystem.Voro.Core.Events;
using VoroSystem.Voro.World.Components;

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
    ComputeEvents.GetInstance().OnCompute += HandleOnCompute;
  }

  void OnDisable() {
    ComputeEvents.GetInstance().OnCompute -= HandleOnCompute;
  }

  #endregion

  void HandleOnCompute() {
    VoroWorld.Instance.SetChunkTextures(chunk => {
      var dict = Diagram.GetEffectDictionary(out var allow);
      var resultTexture = Texture2D.blackTexture;

      if (!allow) {
        return resultTexture;
      }

      foreach (var (layerName, list) in dict) {
        Debug.Log($"Layer: {layerName}");
        list.ForEach(effect => {
          Debug.Log($"Effect: {effect.Name.ToString()}");
          resultTexture = effect.RunEffect(chunk);
        });
      }
      
      // get buffer data
      chunk.ReadBuffer();
      // release buffers
      chunk.ReleasePointBuffer();

      return resultTexture;
    });
  }
}
}