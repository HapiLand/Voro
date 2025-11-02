namespace VoroSystem.Grids {
public interface ITilemap<T> where T : class, ITile {
    int TileSize { get; }
    int SizeX { get; }
    int SizeZ { get; }
    T GetTile(int x, int y);
    T GetTile(int index);
    void SetTile(int x, int y, T tile);
    void SetTile(int index, T tile);

    /// <summary> Tells, if given coordinates are in tilemap's bounds. </summary>
    bool InBounds(int x, int y);
}
}