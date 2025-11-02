using UnityEngine;
using VoroSystem.Core;

namespace VoroSystem.World.Generator {
class LandscapeMesh {
    readonly VoroLandscapeGenerator _voroLandscapeGenerator;

    public LandscapeMesh(VoroLandscapeGenerator voroLandscapeGenerator) {
        _voroLandscapeGenerator = voroLandscapeGenerator;
    }

    /// <summary> create terrain mesh </summary>
    public void GenerateMesh() {
        // get the Results that has the quad for each Chunk
        var endResults = _voroLandscapeGenerator.ComputedVoroTerrain.Results;

        // instantiate the quad where each result is placed at its Tile position
        for (var i = 0; i < endResults.Count; i++) {
            var result = endResults[i];
            var tile = _voroLandscapeGenerator.TilemapComponent.CompMap.Tilemap.GetTile(i);
            (int x, int y) coordinate = (i % _voroLandscapeGenerator.TilemapComponent.TilemapParameters.mapSizeX,
                i / _voroLandscapeGenerator.TilemapComponent.TilemapParameters.mapSizeY);
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

            gameObj.transform.SetParent(VoroComponent.Instance, true);
        }
    }
}
}