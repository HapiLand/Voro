using UnityEngine;

namespace VoroTileMap {
/// <summary>
///     a prefab object for the unity scene
/// </summary>
[ExecuteAlways]
public class TileChunk : MonoBehaviour {
    bool _isVisible;
    Tile tile;

    void Update() {
        if (!UpdateVisibility(out var cam)) {
            return;
        }

        var viewportPos = cam.WorldToViewportPoint(transform.position);
        var inFront = viewportPos.z > 0;
        var inside = viewportPos.x is >= 0 and <= 1 && viewportPos.y is >= 0 and <= 1;
        _isVisible = inFront && inside;

        return;

        bool UpdateVisibility(out Camera cam) {
            cam = Camera.main;
            if (!cam) {
                _isVisible = false;
                return false;
            }

            return true;
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = _isVisible ? Color.green : Color.red;
        var pos = transform.position;
        var size = new Vector3(0.05f, 0, 0.05f);
        Gizmos.DrawWireCube(pos, size);
    }

    public static TileChunk Create(Tile tile, Transform parent) {
        var obj = new GameObject($"Tile_{tile.Coordinates.x}_{tile.Coordinates.y}");
        obj.transform.SetParent(parent);
        obj.transform.position = new Vector3(tile.Coordinates.x, 0, tile.Coordinates.y);

        var chunk = obj.AddComponent<TileChunk>();
        chunk.Initialize(tile);
        return chunk;
    }

    void Initialize(Tile tile) {
        this.tile = tile;
    }
}
}