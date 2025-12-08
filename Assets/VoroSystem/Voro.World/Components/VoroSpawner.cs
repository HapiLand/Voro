using UnityEngine;
using VoroSystem.Voro.World.ChunkStructure;

namespace VoroSystem.Voro.World.Components {
[ExecuteAlways]
public class VoroSpawner : MonoBehaviour {
  #region Event Functions

  void Awake() {
    VoroMap.CreatedChunk += HandleChunkCreated;
  }

  void OnDisable() {
    VoroMap.CreatedChunk -= HandleChunkCreated;
  }

  #endregion

  void HandleChunkCreated(Chunk obj) { }
}
}