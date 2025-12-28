using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Voro.Internal.World.GridTiles;
using VoroSystem.VoroWorldGeneration;
using VoroSystem.VoroWorldGeneration.CubicChunks.Player;
using VoroSystem.VoroWorldGeneration.CubicChunks.Player.Core;
using VoroSystem.VoroWorldGeneration.Map;

namespace Voro.Internal.World {
[ExecuteAlways]
[RequireComponent(typeof(WorldGenTilemap), typeof(WorldGenInstancer))]
public class Chunk : MonoBehaviour {
  #region Serialized Fields
  public bool IsPlayerInside;
  public bool NeighborHasPlayer;

  [field: SerializeField] public Vector3Int GridCoord { get; set; }
  [SerializeField] bool lastPlayerInside;
  [field: SerializeField] public PlayerPoint Player { get; set; }
  #endregion

  readonly Vector3Int[] NeighborOffsets = GenerateNeighborOffsets();

  WorldGenTilemap MapGenerator;
  WorldGenInstancer TileInstancer;
  Tilemap<Tile> Tilemap;
  float GizmoBaseSize => GridTileSettings.GridTileSize;
  public Bounds Bounds => new(transform.position, Vector3.one * GridTileSettings.GridTileSize);

  public Vector3 WorldOriginPosition => new(
    GridCoord.x * GridTileSettings.GridTileSize,
    GridCoord.y * GridTileSettings.GridTileSize,
    GridCoord.z * GridTileSettings.GridTileSize);

  public Vector3Int BoundSize => new(
    Mathf.Max(1, Mathf.CeilToInt(Bounds.size.x)),
    Mathf.Max(1, Mathf.CeilToInt(Bounds.size.y)),
    Mathf.Max(1, Mathf.CeilToInt(Bounds.size.z))
  );

  #region Event Functions
  void Awake() {
    TileInstancer = GetComponent<WorldGenInstancer>();
    MapGenerator = GetComponent<WorldGenTilemap>();
    TileInstancer.Init(MapGenerator);
    Player = PlayerLocator.GetOrCreatePlayer();
  }

  void Update() {
    UpdatePlayerDetection();
  }

  void OnEnable() {
    ChunkMonitor.RegisterChunk(this);
  }

  void OnDisable() {
    ChunkMonitor.UnregisterChunk(this);
  }
  #endregion

  void UpdatePlayerDetection() {
    if (!Player) {
      SetPlayerInside(false);
      return;
    }

    var inside = Bounds.Contains(Player.transform.position);
    SetPlayerInside(inside);
  }

  void SetPlayerInside(bool inside) {
    if (inside == lastPlayerInside) {
      return;
    }

    IsPlayerInside = inside;
    lastPlayerInside = inside;
    NotifyNeighbors(inside);
  }

  void NotifyNeighbors(bool playerInside) {
    foreach (var neighbor in GetNeighbors()) {
      neighbor.SetNeighborHasPlayer(playerInside);
    }
  }

  public void SetNeighborHasPlayer(bool value) {
    NeighborHasPlayer = value;
  }

  static Vector3Int[] GenerateNeighborOffsets() {
    return
      (from x in Enumerable.Range(-1, 3)
        from y in Enumerable.Range(-1, 3)
        from z in Enumerable.Range(-1, 3)
        where x != 0 || y != 0 || z != 0
        select new Vector3Int(x, y, z))
      .ToArray();
  }

  IEnumerable<Chunk> GetNeighbors() {
    var world = ChunkWorld.Instance;
    if (!world) {
      yield break;
    }

    foreach (var offset in NeighborOffsets) {
      var coord = GridCoord + offset;
      if (!world.TryGetCube(coord, out var neighbor)) {
        continue;
      }

      yield return neighbor;
    }
  }


  public void GenerateTilemap() {
    MapGenerator.Check(out var allowGeneration);
    if (allowGeneration) {
      Debug.Log("Starting Generation");
      MapGenerator.GenerateWorldGrid(
        this,
        tilemapComplete => {
          Tilemap = tilemapComplete;
          Debug.Log("Tilemap Generation Complete");
        });

      Debug.Log("Generation Complete");
    }
    else {
      Debug.Log("Map Generation not allowed");
    }
  }

  public void GetVisualState(out Color color, out float size) {
    if (IsPlayerInside) {
      size = GizmoBaseSize;
      color = Color.green;
      return;
    }

    if (NeighborHasPlayer) {
      size = GizmoBaseSize * 0.8f;
      color = Color.blue;
      return;
    }

    size = GizmoBaseSize * 0.25f;
    color = Color.red;
  }
}
}