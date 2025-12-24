using System;
using UnityEngine;

namespace VoroSystem.VoroWorldGeneration.CubicChunks.World.Core.States {
[Serializable]
public class ErrorState : IWorldState {
  readonly string _errorMessage;

  public ErrorState(string errorMessage) {
    _errorMessage = errorMessage;
  }

  #region IWorldState Members
  public string Name => "Error";

  public void EnterState(WorldState world) {
    Debug.Log($"[State: Error] World State failed to continue: {_errorMessage}");
  }

  public void UpdateState(WorldState world) { }
  public void ExitState(WorldState world) { }
  #endregion
}
}