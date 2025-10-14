using VoroSystem.WorldGrid.Grids;

namespace VoroSystem.WorldGrid {
public interface IWorld : IMap<ITile> {
    bool HasMap { get; set; }
    void GenerateMapArray();
    void SetMapSize(int x, int y);
    void InstantiateMap();
}
}