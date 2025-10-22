using UnityEngine;
using Voro.Core.Map;

namespace Voro.Core.World {
class ResultsFactory {
    readonly Terrain _terrain;
    readonly TileMap _tileMap;

    public ResultsFactory(Terrain terrain, TileMap tileMap) {
        _terrain = terrain;
        _tileMap = tileMap;
    }

    public void GenerateChunkedTerrain(Transform parent) {
        // get the Results that has the quad for each Chunk
        var endResults = _terrain.Results;

        // instantiate the quad where each result is placed at its Tile position
        for (var i = 0; i < endResults.Count; i++) {
            var result = endResults[i];
            var tile = _tileMap[i];
            (int x, int y) coordinate = (i % _tileMap.Size.x, i / _tileMap.Size.y);
            coordinate = (0, 0);
            var quad = result.Quad;

            var gameObj = new GameObject($"[{coordinate.x}x{coordinate.y}]")
            {
                transform =
                {
                    position = new Vector3(coordinate.x, 0f, coordinate.y)
                }
            };
            var meshFilter = gameObj.AddComponent<MeshFilter>();
            meshFilter.sharedMesh = quad;
            var meshRenderer = gameObj.AddComponent<MeshRenderer>();
            var originalMat = Resources.Load<Material>("FbxMat");
            var mat = new Material(originalMat)
            {
                color = Color.seaGreen
            };
            meshRenderer.material = mat;

            gameObj.transform.SetParent(parent, true);
        }
    }
}
}