using UnityEngine;

namespace VoroSystem.VoroWorldGeneration {
[ExecuteAlways]
public class WorldGenState : MonoBehaviour {
  #region GenerationState enum
  public enum GenerationState {
    NotCreated,
    Creating,
    GenerationComplete
  }
  #endregion

  #region Serialized Fields
  [HideInInspector] public GenerationState currentState = GenerationState.NotCreated;
  #endregion

  public bool CanStartGeneration() {
    return currentState == GenerationState.NotCreated;
  }

  public void StartGeneration() {
    if (!CanStartGeneration()) {
      Debug.LogWarning("Generation already in progress");
      return;
    }

    currentState = GenerationState.Creating;
    Debug.Log("World generation started");
  }

  public void CompleteGeneration() {
    if (currentState != GenerationState.Creating) {
      Debug.LogWarning("Cannot complete generation that hasn't started");
      return;
    }

    currentState = GenerationState.GenerationComplete;
    Debug.Log("World generation complete");
  }
}
}