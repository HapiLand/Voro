using UnityEngine;

namespace Voro.Internal.World {
[ExecuteAlways]
public class ChunkGizmos : MonoBehaviour {
  #region Event Functions
  void OnDrawGizmos() {
    if (ChunkMonitor.Chunks.Count == 0) {
      return;
    }

    foreach (var chunk in ChunkMonitor.Chunks) {
      chunk.GetVisualState(out var col, out var size);
      Gizmos.color = col;
      Gizmos.DrawWireCube(chunk.transform.position, new Vector3(size, 0, size));
    }
  }
  #endregion
}
}