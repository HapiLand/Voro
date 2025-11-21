using UnityEngine;
using VoroSystem.Voro.World.Map;

namespace VoroSystem.Voro.World.Terrain {
[ExecuteAlways]
public class VoroTerrain : MonoBehaviour {
  #region Serialized Fields

  [SerializeField] VoroMap map;
  [SerializeField] public Tilemap<Chunk> chunkMap;

  #endregion

  bool IsDirty => map != null && map.isDirty;

  #region Event Functions

  void Awake() {
    name = "Voro Terrain";
  }

  void Update() {
    RegenerateIfDirty();
  }

  #endregion

  public void Init(VoroMap map) {
    this.map = map;
    CreateTilemap();
    RegenerateIfDirty();
  }

  void CreateTilemap() {
    chunkMap ??= new Tilemap<Chunk>(map.tileSize, map.mapSizeX, map.mapSizeZ,
      (index, pos) => new Chunk(map.GetTile(index)));
  }

  void RegenerateIfDirty() {
    if (!IsDirty) {
      return;
    }

    RegenerateMap();
  }

  void RegenerateMap() {
    chunkMap.mapSizeX = map.mapSizeX;
    chunkMap.mapSizeZ = map.mapSizeZ;
    chunkMap.tileSize = map.tileSize;

    chunkMap.CreateMap();
  }

  public Chunk GetChunk(int index) {
    return chunkMap[index];
  }
}
}