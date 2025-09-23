using UnityEngine;
using VoroGeneration;

namespace VoroTileMap {
[ExecuteAlways]
public class GameWorld : MonoBehaviour {
    GenerationController _generationController;
    TileMap _tileMap;
    WorldMapController _worldMapController;
    int length => 10;
    int width => 10;

    void Awake() {
        _tileMap = new TileMap(width, length);
        _tileMap.OnTileArrayCompletedGeneration += () => {
            // handle event once every tile has been created
            // GenerationController can only create its Diagram when the tilemap is completed
            _generationController = GenerationController.GetInstance(_worldMapController, _tileMap);
        };

        _worldMapController = new WorldMapController(_tileMap, transform);

        _tileMap.GenerateTiles();
    }
}
}