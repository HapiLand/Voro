using UnityEngine;
using Voro.UI;

namespace Voro.World {
[ExecuteAlways]
public class VoroWorldMaster : MonoBehaviour {
    GenerationLayers _generationLayers;
    TileMap _tileMap;

    void Awake() {
        _tileMap = new TileMap();
        _generationLayers = new GenerationLayers();
    }

    void OnEnable() {
    }
}
}