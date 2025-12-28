using System;
using Source;
using UnityEngine;
using VoroSystem.VoroWorldGeneration.CubicChunks.World;
using VoroSystem.VoroWorldGeneration.CubicChunks.World.Core;
using VoroSystem.VoroWorldGeneration.CubicChunks.World.Core.States;

namespace Voro.Internal.World {
[ExecuteAlways]
[RequireComponent(typeof(ChunkGizmos))]
public class ChunkWorld : MonoBehaviour {
  #region Serialized Fields
  public WorldState worldState;
  [field: SerializeField] protected GridTracker GridTracker { get; private set; }
  [SerializeField] CubeStorage cubes;
  #endregion

  public static ChunkWorld Instance { get; private set; }
  public SerializableDictionary<Vector3Int, Chunk> CubeDictionary => cubes.cubeDictionary;

  #region Event Functions
  void Awake() {
    if (Instance != null && Instance != this) {
      Destroy(gameObject);
      return;
    }

    Instance = this;
    cubes = new CubeStorage(transform);
    worldState = gameObject.AddComponent<WorldState>();
    GridTracker = new GridTracker();
  }

  void Update() {
    if (GridTracker.TryUpdateCoordinate()) {
      OnCoordinateChanged(GridTracker.ActiveCoordinate);
    }
  }
  #endregion

  public bool TryGetCube(Vector3Int coord, out Chunk cube) {
    return cubes.TryGetCube(coord, out cube);
  }

  void OnCoordinateChanged(Vector3Int newCoord) {
    var cube = cubes.GetOrCreateCube(newCoord);
    CoordinateChanged?.Invoke(cube);
  }

  public event Action<Chunk> CoordinateChanged;

  public void NotifyStartGeneration() {
    worldState.StartGeneration();
  }
}
}