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
  void Awake() {
    BoundingBox = new GridCubeBoundingBox(transform);
    CubePlayerDetection = new CubePlayerDetection(this)
    {
      Player = PlayerLocator.GetOrCreatePlayer()
    };
  }

  void Update() {
    CubePlayerDetection.Update();
  }
  #endregion

  /// <summary>
  /// generates tiles within the bounds of the cube
  /// </summary>
  public void GenerateTilemap() {
    // todo update WorldGenState to indicate the GridCube is generating tiles
  }
}
}