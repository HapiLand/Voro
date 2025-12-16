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
      Debug.Log("[Compute] Create dictionary");
      var dict = Diagram.GetEffectDictionary(out var allow);

      if (!allow) {
        return Texture2D.blackTexture;
      }

      // create buffer if it does not exist
      chunk.TryCreateBuffer();

      // Debug.Log("Compute Begin:");
      foreach (var (layerName, list) in dict) {
        Debug.Log($"Computing Layer: {layerName}");
        list.ForEach(effect => {
          Debug.Log($"Computing Effect {effect.Name.ToString()} in Layer {layerName}");
          effect.RunEffect(chunk);
        });
      }

      chunk.ApplyBuffer();
      return Texture2D.blackTexture;
    });
  }
}
}