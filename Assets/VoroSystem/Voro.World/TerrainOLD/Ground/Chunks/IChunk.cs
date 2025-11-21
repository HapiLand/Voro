using VoroSystem.Voro.World.Map;

namespace VoroSystem.Voro.World.TerrainOLD.Ground.Chunks {
public interface IChunk {
  ITile Tile { get; }
  ChunkInstance Instance { get; set; }
}
}