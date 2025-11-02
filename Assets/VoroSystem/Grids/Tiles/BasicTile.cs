using UnityEngine;

namespace VoroSystem.Grids.Tiles {
/// <summary> Tile that exists in the grid </summary>
public class BasicTile : ITile {
    readonly TileCoordinate _tileCoordinate;
    readonly TileState _tileState;

    public BasicTile(Vector2 position) {
        _tileCoordinate = new TileCoordinate(position);
        _tileState = new TileState(false, false);
        TileMeshResult = new TileMeshResult(this);
    }

    public Vector2 Position => _tileCoordinate.Position;
    public bool Visible => _tileState.Visible;
    public bool Dirty => _tileState.Dirty;
    public StateType StateType => _tileState.StateType;
    public TileMeshResult TileMeshResult { get; }

    public void Update() {
        _tileState.UpdateVisibility(Position);
    }
}
}