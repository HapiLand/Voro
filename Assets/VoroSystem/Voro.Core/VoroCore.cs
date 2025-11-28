using UnityEngine;
using VoroSystem.Voro.Compute;

namespace VoroSystem.Voro.Core {
[ExecuteAlways]
public class VoroCore : MonoBehaviour {
  #region Serialized Fields

  [SerializeField] VoroCompute compute;

  #endregion

  #region Event Functions

  void Awake() {
    name = "Voro Core";
    compute ??= GetComponentInChildren<VoroCompute>();
    // terrain ??= GetComponentInChildren<VoroTerrain>();
  }

  #endregion

  // [SerializeField] VoroTerrain terrain;
}
}