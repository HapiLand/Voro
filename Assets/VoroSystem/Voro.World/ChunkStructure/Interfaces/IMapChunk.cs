namespace VoroSystem.Voro.World.ChunkStructure.Interfaces {
/// <summary>
/// implementation for how a Tile exists in
/// <see cref="Tilemap{T}" />
/// </summary>
public interface IMapChunk {
  int MapIndex { get; }
  float ChunkSize { get; }
}
}