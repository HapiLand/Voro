namespace VoroSystem.Voro.World.ChunkStructure {
/// <summary>
/// implementation for how a Tile exists in
/// <see cref="Tilemap{T}" />
/// </summary>
public interface IMapTile {
    int Index { get; }
    float Size { get; }
}
}