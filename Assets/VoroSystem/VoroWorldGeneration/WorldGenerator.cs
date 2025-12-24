using UnityEngine;
using VoroSystem.VoroWorldGeneration.CubicChunks.World;
using VoroSystem.VoroWorldGeneration.CubicChunks.World.Core;
using VoroSystem.VoroWorldGeneration.CubicChunks.World.Core.States;
using VoroSystem.VoroWorldGeneration.Map;

namespace VoroSystem.VoroWorldGeneration {
[ExecuteAlways]
[RequireComponent(typeof(CubeWorld))]
public class WorldGenerator : MonoBehaviour {
  public CubeWorld CubeWorld;
  void Awake() {
    CubeWorld = GetComponent<CubeWorld>();
  }
  public void StartGeneration() {
    // notify cube world that all its cubes should begin generation
    CubeWorld.NotifyStartGeneration();
  }

  /*public WorldState.GenerationState GetCurrentState() {
    return CubeWorld.WorldState.currentState;
  }*/
}
}