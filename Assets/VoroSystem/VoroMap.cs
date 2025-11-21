using VoroSystem.Landscape.Tilemap;

namespace VoroSystem {
class VoroMap {
    readonly Voro _voro;
    public WorldGrid Grid;

    public VoroMap(Voro voro) {
        _voro = voro;
    }

    /// <summary>
    /// New grid
    /// </summary>
    public void InitTilemap() {
        // create grid
        CreateGrid(_voro.VoroInputValue.InputValues.TileSize);
    }

    void CreateGrid(float tileSize) {
        Grid = new WorldGrid(tileSize);
    }
}
}