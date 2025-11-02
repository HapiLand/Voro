using VoroSystem.World.Generate;

namespace VoroSystem.Grids.Tiles {
public class TileMeshResult {
    readonly BasicTile _basicTile;
    BaseResult _baseResult;

    public TileMeshResult(BasicTile basicTile) {
        _basicTile = basicTile;
    }

    public BaseResult BaseResult {
        get
        {
            _baseResult ??= new BaseResult(_basicTile);
            return _baseResult;
        }
    }
}
}