using System;
using UnityEngine;
using VoroSystem.Voro.Utilities.Cameras;
using VoroSystem.Voro.Utilities.Extensions;
using VoroSystem.Voro.World.ChunkStructure.Interfaces;

namespace VoroSystem.Voro.World.ChunkStructure {
[Serializable]
public class TileState : ITileState {
  #region Serialized Fields

  [SerializeField] bool initialised;
  [SerializeField] bool visible;

  #endregion

  #region ITileState Members

  public bool Initialised {
    get => initialised;
    set => initialised = value;
  }

  public bool Visible => visible;

  public void UpdateVisibility(Vector2 position) {
    var cam = CameraManager.Camera;
    var tileWorldPos = position.ToVector3();
    var viewportPos = cam.WorldToViewportPoint(tileWorldPos);
    var isVisible = viewportPos is { z: > 0, x: >= 0 and <= 1, y: >= 0 and <= 1 };
    visible = isVisible;
  }

  #endregion
}
}