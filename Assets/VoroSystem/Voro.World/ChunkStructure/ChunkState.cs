using System;
using UnityEngine;
using VoroSystem.Voro.Utilities.Cameras;
using VoroSystem.Voro.World.ChunkStructure.Interfaces;

namespace VoroSystem.Voro.World.ChunkStructure {
[Serializable]
public class ChunkState : IChunkState {
  #region Serialized Fields
  [SerializeField] bool visible;
  #endregion

  #region IChunkState Members
  public bool Visible => visible;

  public void UpdateVisibility(Vector3 position) {
    var cam = CameraManager.Camera;
    var viewportPos = cam.WorldToViewportPoint(position);
    var isVisible = viewportPos is { z: > 0, x: >= 0 and <= 1, y: >= 0 and <= 1 };
    visible = isVisible;
  }
  #endregion
}
}