using UnityEngine;
using VoroSystem.Voro.Compute.Components;
using VoroSystem.Voro.World.Components;

namespace VoroSystem.Voro.Core.Components {
[ExecuteAlways]
public class VoroCore : MonoBehaviour {
  #region Serialized Fields

  [SerializeField] VoroWorld voroWorld;
  [SerializeField] VoroCompute voroCompute;

  #endregion

  #region Event Functions

  void Awake() {
    name = "Voro Core";
    voroWorld = CreateChild(voroWorld);
    voroCompute = CreateChild(voroCompute);
  }

  #endregion

  T CreateChild<T>(T existing, string childName = "") where T : Component {
    if (existing != null) {
      return existing;
    }

    var child = new GameObject(childName);
    child.transform.SetParent(transform);
    return child.AddComponent<T>();
  }

  #region Compute handlers

  /*static void HandleComputeCalled(object sender, VoroComputeEvents.ComputeEventArgs e) {
    Debug.Log("[VoroCore] Compute called");
  }

  void HandleComputeBegin(object sender, VoroComputeEvents.ComputeEventArgs e) {
    Debug.Log("[VoroCore] Compute begin");
    HandleOnCompute();
  }

  static void HandleComputeComplete(object sender, VoroComputeEvents.ComputeEventArgs e) {
    Debug.Log("[VoroCore] Compute complete");
  }*/

  #endregion
}
}