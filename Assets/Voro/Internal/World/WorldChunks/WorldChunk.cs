using UnityEngine;
using Voro.Internal.World.GridTiles;

namespace Voro.Internal.World.WorldChunks {
/// <summary>
/// chunk object at a grid tile position
/// </summary>
public class WorldChunk : MonoBehaviour {
  WorldChunkBounds _bounds;
  GridTile _gridTile;
}
}