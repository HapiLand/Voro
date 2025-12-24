using System;
using UnityEngine;

namespace VoroSystem.VoroWorldGeneration.Map {
[Serializable]
public class Tile {
  #region Serialized Fields
  [field: SerializeField] public int Index { get; private set; }
  [field: SerializeField] public Vector2 Position { get; private set; }
  [field: SerializeField] public TileEntity Entity { get; private set; }
  #endregion

  bool _isVisible;

  public Tile(int index, Vector2 position) {
    Position = position;
    Index = index;
  }

  public void CreateEntity(Transform parentTransform) {
    var gameObject = new GameObject($"Tile_{Index}")
    {
      transform =
      {
        parent = parentTransform
      }
    };

    Entity = gameObject.AddComponent<TileEntity>();
    Entity.SetTile(this);
    Debug.Log("Tile Created Entity");
  }

  public void Update() {
    if (Entity == null) {
      return;
    }

    var currentlyVisible = CheckVisibility();
    Entity.gameObject.SetActive(_isVisible);
    
    if (currentlyVisible != _isVisible) {
      _isVisible = currentlyVisible;
      if (_isVisible) {
        Entity.MarkDirty();
      }
    }
    if (_isVisible && Entity.IsDirty) {
      Entity.UpdateTileEntity();
    }
  }

  bool CheckVisibility() {
    var viewportPos = Camera.main.WorldToViewportPoint(new Vector3(Position.x, 0f, Position.y));
    return viewportPos is { z: > 0, x: >= 0 and <= 1, y: >= 0 and <= 1 };
  }

  public void MarkEntityDirty() {
    Entity?.MarkDirty();
  }
}
}