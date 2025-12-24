using UnityEngine;
using VoroSystem.VoroWorldGeneration.CubicChunks.Cubes.Core;
using VoroSystem.VoroWorldGeneration.CubicChunks.Player.Core;

namespace VoroSystem.VoroWorldGeneration.CubicChunks.Cubes {
public class GridCube : BaseCube {
  // todo serialize fields
  public GridCube() {
    CubePlayerDetection = new CubePlayerDetection(this);
  }

  public GridCubeBoundingBox BoundingBox { get; private set; }
  public CubePlayerDetection CubePlayerDetection { get; private set; }
  protected override bool IsPlayerInside => CubePlayerDetection?.IsPlayerInside ?? false;
  protected override bool NeighborHasPlayer => CubePlayerDetection?.NeighborHasPlayer ?? false;

  #region Event Functions
  protected override void Awake() {
    base.Awake();

    BoundingBox = new GridCubeBoundingBox(transform);
    CubePlayerDetection = new CubePlayerDetection(this)
    {
      Player = PlayerLocator.GetOrCreatePlayer()
    };
  }

  void Update() {
    CubePlayerDetection.Update();
    // todo destroy tilemap on condition
    //  destroy when player is not inside this or neighbor
    //  destroy when no tiles are visible
  }
  #endregion

  /// <summary>
  /// generates tiles within the bounds of the cube
  /// </summary>
  public void GenerateTilemap() {
    MapGenerator.Check(out var allowGeneration);
    if (allowGeneration) {
      Debug.Log("Starting Generation");
      // todo notify CubeWorld.WorldGenState to indicate the GridCube has started generating the tilemap
      // todo set internal state - generating

      MapGenerator.GenerateWorldGrid(
        BoundingBox,
        tilemapComplete => {
          Tilemap = tilemapComplete;
          // todo notify CubeWorld.WorldGenState to indicate the GridCube has completed creating the tilemap
          Debug.Log("Tilemap Generation Complete");
        });

      Debug.Log("Generation Complete");
      // todo set internal state - completed generation
    }
    else {
      Debug.Log("Map Generation not allowed");
      // todo set internal state - generation denied
    }
  }
}
}