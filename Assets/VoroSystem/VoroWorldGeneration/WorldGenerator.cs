using UnityEngine;
using VoroSystem.VoroWorldGeneration.CubicChunks.World;

namespace VoroSystem.VoroWorldGeneration {
[ExecuteAlways]
[RequireComponent(typeof(CubeWorld))]
public class WorldGenerator : MonoBehaviour {
  #region Serialized Fields
  public CubeWorld CubeWorld;
  #endregion

  #region Event Functions
  void Awake() {
    CubeWorld = GetComponent<CubeWorld>();
  }
  #endregion

  public void StartGeneration() {
    // notify cube world that all its cubes should begin generation
    CubeWorld.NotifyStartGeneration();
  }

  /*public WorldState.GenerationState GetCurrentState() {
    return CubeWorld.WorldState.currentState;
  }*/
}
}