using System;
using UnityEngine;

namespace VoroSystem.VoroWorldGeneration.CubicChunks.World.Core.States {
[Serializable]
public class CreatingState : IWorldState {
  #region IWorldState Members
  public string Name => "Creating";

  public void EnterState(WorldState world) {
    Debug.Log("[State: Creating] Starting world generation");
    var cubeWorld = world.cubeWorld;
    var cubeDictionary = cubeWorld.CubeDictionary;
    if (cubeDictionary.Count == 0) {
      world.SetState(new ErrorState("CubeDictionary empty"));
    }
  }

  public void UpdateState(WorldState world) {
    var cubeWorld = world.cubeWorld;
    var cubeDictionary = cubeWorld.CubeDictionary;
    foreach (var (coord, cube) in cubeDictionary) {
      cube.GenerateTilemap();
    }

    world.SetState(new GenerationCompleteState());
  }

  public void ExitState(WorldState world) {
    Debug.Log("[State: Creating] World generation complete");
  }
  #endregion
}
}