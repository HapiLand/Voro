using System;
using UnityEngine;

namespace VoroSystem.VoroWorldGeneration.CubicChunks.World.Core.States {
[Serializable]
public class GenerationCompleteState : IWorldState {
  #region IWorldState Members
  public string Name => "Generation Complete";

  public void EnterState(WorldState world) {
    Debug.Log("[State: GenerationComplete] World fully generated");
  }

  public void UpdateState(WorldState world) {
    world.SetState(new EndPointState());
  }

  public void ExitState(WorldState world) { }
  #endregion
}
}