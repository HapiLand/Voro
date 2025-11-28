using System;
using System.Collections.Generic;
using UnityEngine;
using VoroSystem.Voro.World.TileEntities;
using VoroSystem.Voro.World.TileStructure;

namespace VoroSystem.Voro.World.Map {
[ExecuteAlways]
public class VoroMap : MonoBehaviour {
  #region Serialized Fields

  [SerializeField] public bool isDirty;
  [SerializeField] public Tilemap<Tile> tilemap;
  [SerializeField] [Range(0.1f, 1f)] public float tileSize = 1f;
  [SerializeField] public int mapSizeX = 10;
  [SerializeField] public int mapSizeZ = 10;

  #endregion

  public (Vector3 A, Vector3 B) Corner { get; private set; }

  #region Event Functions

  void Awake() {
    SetCorners(Vector3.zero, new Vector3(mapSizeX, 0, mapSizeZ));
    name = "Voro Map";
    CreateTilemap();
    RegenerateIfDirty();
  }

  void Start() {
    isDirty = true;
  }

  void Update() {
    RegenerateIfDirty();
    tilemap?.ForEach(t => t.Update());
  }

  void OnValidate() {
    isDirty = true;
    CreateTilemap();
    RegenerateIfDirty();
  }

  #endregion

  void CreateTilemap() {
    tilemap ??= new Tilemap<Tile>(tileSize, mapSizeX, mapSizeZ, (index, pos) => new Tile(index, pos, tileSize));
  }


  
  void RegenerateIfDirty() {
    if (!isDirty) {
      return;
    }

    RegenerateMap();
    isDirty = false;
  }

  void RegenerateMap() {
    tilemap.mapSizeX = mapSizeX;
    tilemap.mapSizeZ = mapSizeZ;
    tilemap.tileSize = tileSize;

    Initialize();
    tilemap.CreateMap();
  }

  void Initialize() {
    tilemap = new Tilemap<Tile>(tileSize, mapSizeX, mapSizeZ, (index, pos) => new Tile(index, pos, tileSize));
  }

  public Tile GetTile(int index) {
    return tilemap[index];
  }

  public void SetCorners(Vector3 a, Vector3 b) {
    mapSizeX = Mathf.RoundToInt(Mathf.Abs(b.x - a.x));
    mapSizeZ = Mathf.RoundToInt(Mathf.Abs(b.z - a.z));
    Corner = (a, b);
    isDirty = true;
  }

}
}