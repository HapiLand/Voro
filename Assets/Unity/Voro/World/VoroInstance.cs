using System.Linq;
using UnityEngine;
using Voro.Generation;
using Voro.World.Internal;

namespace Voro.World {
[ExecuteAlways]
public class VoroInstance : MonoBehaviour {
    TileMap _tileMap;

    void Awake() {
        _tileMap = new TileMap(2, 2);
    }

    /// <summary>
    ///     instance the chunk for every tile in the map
    /// </summary>
    /// <param name="chunk"></param>
    public void InstanceMap(Chunk chunk) {
        var instances =
            from tile in _tileMap.AsEnumerable()
            from point in chunk.Points
            let pos = tile.TilePosition
            select new { point, tilePos = pos };

        foreach (var item in instances) {
            var instance = item.point.GetMeshObject();
            instance.transform.position += item.tilePos;
            instance.transform.SetParent(transform);
        }
    }
}
}