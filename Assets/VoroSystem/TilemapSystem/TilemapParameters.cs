using UnityEngine;

namespace VoroSystem.TilemapSystem {
public class TilemapParameters {
    [Range(1, 10)] public int mapSizeX = 5;
    [Range(1, 10)] public int mapSizeY = 5;
    [SerializeField] [Range(0, 10)] int padding;
    [Range(0.5f, 1f)] public float tileSize = 1f;
}
}