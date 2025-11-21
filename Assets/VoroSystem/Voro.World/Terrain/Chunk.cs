using System;
using VoroSystem.Voro.World.Map;
using VoroSystem.Voro.World.TerrainOLD.Ground.Chunks.Geometry;

namespace VoroSystem.Voro.World.Terrain {
[Serializable]
public class Chunk : IChunk {
  
  public Chunk(Tile tile) {
    Tile = tile;
  }

  #region IChunk Members

  public ITile Tile { get; }

  #endregion
}
}