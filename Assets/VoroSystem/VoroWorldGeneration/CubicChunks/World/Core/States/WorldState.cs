using System.Collections;
using UnityEngine;
using Voro.Internal.World;

namespace VoroSystem.VoroWorldGeneration.CubicChunks.World.Core.States {
[ExecuteAlways]
public class WorldState : MonoBehaviour {
  #region Serialized Fields
  public ChunkWorld cubeWorld;

  [SerializeReference] IWorldState activeState;
  [SerializeReference] Coroutine updateCoroutine;
  #endregion

  public string CurrentStateName => activeState?.Name ?? "None";

  #region Event Functions
  void Awake() {
    cubeWorld = GetComponent<ChunkWorld>();
    SetState(new NotCreatedState());
  }
  #endregion

  public void SetState(IWorldState newState) {
    activeState?.ExitState(this);
    activeState = newState;
    activeState.EnterState(this);
  }

  /// <summary> called by EditorWindow GUI to start the generation system </summary>
  public void StartGeneration() {
    if (activeState is NotCreatedState && updateCoroutine == null) {
      updateCoroutine = StartCoroutine(GenerationRoutine());
    }
  }

  IEnumerator GenerationRoutine() {
    while (activeState is not EndPointState && activeState is not ErrorState) {
      activeState?.UpdateState(this);
      yield return null;
    }

    var endedState = activeState;
    updateCoroutine = null;
    if (endedState is EndPointState) {
      Debug.Log("World Generation complete");
    }
    else {
      Debug.LogWarning($"Generation ended unexpectedly: [State {endedState?.Name}]");
    }
  }
}
}