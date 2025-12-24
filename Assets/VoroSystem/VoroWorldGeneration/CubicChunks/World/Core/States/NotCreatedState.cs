using System;
using UnityEngine;

namespace VoroSystem.VoroWorldGeneration.CubicChunks.World.Core.States {
[Serializable]
public class NotCreatedState : IWorldState {
  #region IWorldState Members
  public string Name => "Not Created";

  public void EnterState(WorldState world) {
    Debug.Log("[State: NotCreated] World ready to generate");
  }

  public void UpdateState(WorldState world) {
    world.SetState(new CreatingState());
  }

  public void ExitState(WorldState world) { }
  #endregion
}
}