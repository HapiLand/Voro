using System;
using UnityEngine;

namespace VoroSystem.VoroWorldGeneration.CubicChunks.World.Core.States {
[Serializable]
public class EndPointState : IWorldState {
  #region IWorldState Members
  public string Name => "End Point";

  public void EnterState(WorldState world) {
    Debug.Log("[State: EndPoint]");
  }

  public void UpdateState(WorldState world) { }
  public void ExitState(WorldState world) { }
  #endregion
}
}