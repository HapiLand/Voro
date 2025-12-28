using UnityEngine;
using Voro.Internal.World;
using VoroSystem.VoroWorldGeneration.CubicChunks.World;
using VoroSystem.VoroWorldGeneration.CubicChunks.World.Core;
using VoroSystem.VoroWorldGeneration.CubicChunks.World.Core.States;
using VoroSystem.VoroWorldGeneration.Map;

namespace VoroSystem.VoroWorldGeneration {
[ExecuteAlways]
[RequireComponent(typeof(ChunkWorld))]
public class WorldGenerator : MonoBehaviour {
  public ChunkWorld ChunkWorld;
  void Awake() {
    ChunkWorld = GetComponent<ChunkWorld>();
  }
  public void StartGeneration() {
    // notify cube world that all its cubes should begin generation
    ChunkWorld.NotifyStartGeneration();
  }

  /*public WorldState.GenerationState GetCurrentState() {
    return CubeWorld.WorldState.currentState;
  }*/
}
}