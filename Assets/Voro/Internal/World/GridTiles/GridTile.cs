using UnityEngine;

namespace Voro.Internal.World.GridTiles {
/// <summary>
/// space within the grid that the player can be inside
/// </summary>
public class GridTile {
  /// <summary> grid coordinate </summary>
  public Vector3Int Coordinate;

  /// <summary> player is inside this </summary>
  public bool IsActive;

  /// <summary> player is in the neighbour </summary>
  public bool IsNeighbourActive;
}
}