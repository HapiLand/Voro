namespace VoroSystem.Landscape.Tilemap {
interface ITilemap<T> where T : struct, ITile {
    int Width { get; }
    int Height { get; }
    float TileSize { get; }
    T? GetTile(int x, int y);
    void SetTile(int x, int y, T tile);

    /// <summary>
    /// Tells, if given coordinates are in tilemap's bounds.
    /// </summary>
    bool InBounds(int x, int y);
}
}