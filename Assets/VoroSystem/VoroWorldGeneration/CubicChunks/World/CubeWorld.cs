using System;
using Source;
using UnityEngine;
using VoroSystem.VoroWorldGeneration.CubicChunks.Cubes;
using VoroSystem.VoroWorldGeneration.CubicChunks.World.Core;
using VoroSystem.VoroWorldGeneration.CubicChunks.World.Core.States;

namespace VoroSystem.VoroWorldGeneration.CubicChunks.World {
[Serializable]
public class CubeWorld : BaseWorld {
  #region Serialized Fields
  [SerializeField] CubeStorage cubes;
  #endregion

  public static CubeWorld Instance { get; private set; }
  public SerializableDictionary<Vector3Int, GridCube> CubeDictionary => cubes.cubeDictionary;

  #region Event Functions
  protected override void Awake() {
    base.Awake();
    if (Instance != null && Instance != this) {
      Destroy(gameObject);
      return;
    }

    Instance = this;
    cubes = new CubeStorage(transform);
    worldState = gameObject.AddComponent<WorldState>();
  }
  #endregion

  public event Action<GridCube> CoordinateChanged;

  protected override void OnCoordinateChanged(Vector3Int newCoord) {
    base.OnCoordinateChanged(newCoord);
    var cube = cubes.GetOrCreateCube(newCoord);
    CoordinateChanged?.Invoke(cube);
  }

  public bool TryGetCube(Vector3Int coord, out GridCube cube) {
    return cubes.TryGetCube(coord, out cube);
  }

  public void NotifyStartGeneration() {
    worldState.StartGeneration();
  }
}
}