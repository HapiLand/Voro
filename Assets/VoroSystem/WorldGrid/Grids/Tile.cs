using UnityEngine;

namespace VoroSystem.WorldGrid.Grids {
/// <summary>
///     represents a value inside a map which stores a chunk value
/// </summary>
public readonly struct Tile : ITile {
    public Tile(int x, int y, Vector3 worldPosition, ITile.TileChunk chunk) {
        Debug.Log($"new Tile at '{worldPosition.x:F2} x {worldPosition.y:F2}'");
        WorldPosition = worldPosition;
        Chunk = chunk;
        Coord = new Vector2Int(x, y);
    }

    public Vector3 WorldPosition { get; }
    public Vector2Int Coord { get; }

    /// <summary>
    ///     When computing the Tile, the Chunk is altered as the chunk is mutalb
    /// </summary>
    public ITile.TileChunk Chunk { get; }

    public void InstantiateTile(Transform parent) {
        // create the object that stores the contents of the Tile
        var instance = new GameObject("Tile")
        {
            transform =
            {
                position = WorldPosition
            }
        };
        instance.transform.SetParent(parent);

        Chunk.ForEach(cell => { cell.InstantiateCell(instance.transform); });
    }
}
}