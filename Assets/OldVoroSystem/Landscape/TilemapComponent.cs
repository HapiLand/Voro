using System;
using OldVoroSystem.Generation;
using UnityEngine;
using VoroSystem.Landscape.WorldGridSystem;

namespace OldVoroSystem.Landscape {
[ExecuteAlways]
[RequireComponent(typeof(MesherComponent))]
public class TilemapComponent : MonoBehaviour {
  Vector2Int _lastDimensions;
  MesherComponent _mesher;
  ChunkTilemap _tilemap;
  WorldGridComponent _worldGrid;
  public static TilemapComponent Instance { get; private set; }

  int SizeX => _worldGrid.Dimensions.xSize;
  int SizeZ => _worldGrid.Dimensions.zSize;

  bool DimensionsChanged => _lastDimensions.x != SizeX || _lastDimensions.y != SizeZ;

  #region Event Functions

  void Awake() {
    if (Instance != null) {
      Destroy(gameObject);
      return;
    }

    Instance = this;
    _worldGrid = gameObject.AddComponent<WorldGridComponent>();
    _mesher = MesherComponent.Instance;
    InitTilemap();
  }


  void Update() {
    if (DimensionsChanged) {
      RegenerateMap();
    }

    _tilemap.ForEach(tile => { tile.Update(); });
  }

  void LateUpdate() {
    _mesher.MakeMesh(_tilemap);
  }

  #endregion

  void InitTilemap() {
    Debug.Log("Initialising the Tilemap");
    _tilemap = new ChunkTilemap(SizeX, SizeZ);
    _lastDimensions = new Vector2Int(SizeX, SizeZ);

    for (var z = 0; z < SizeZ; z++) {
      for (var x = 0; x < SizeX; x++) {
        _tilemap.CreateTile(x, z);
      }
    }
  }

  void RegenerateMap() {
    InitTilemap();
  }

  public void ForEach(Action<ChunkTile> action) {
    _tilemap.ForEach(action);
  }

  public ChunkTile GetTile(int index) {
    return _tilemap.GetTile(index);
  }
}
}