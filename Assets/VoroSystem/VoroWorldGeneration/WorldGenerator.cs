using UnityEngine;
using VoroSystem.VoroWorldGeneration.Map;

namespace VoroSystem.VoroWorldGeneration {
[ExecuteAlways]
[RequireComponent(typeof(WorldGenState), typeof(WorldGenTilemap), typeof(WorldGenInstancer))]
public class WorldGenerator : MonoBehaviour {
  #region Serialized Fields
  public WorldGenState stateMachine;
  public WorldGenTilemap tilemap;
  public WorldGenInstancer instancer;
  #endregion

  Tilemap<Tile> _worldGrid;

  #region Event Functions
  void Awake() {
    stateMachine = GetComponent<WorldGenState>();
    instancer = GetComponent<WorldGenInstancer>();
    tilemap = GetComponent<WorldGenTilemap>();
  }
  #endregion

  public void StartGeneration() {
    if (!stateMachine.CanStartGeneration()) {
      return;
    }

    stateMachine.StartGeneration();

    // generate grid, tiles are instanced into scene
    // _worldGrid = WorldGenTilemap.GenerateWorldGrid();
    tilemap.GenerateWorldGrid(worldGrid =>
    {
      // Debug.Log("Tilemap ready");
      _worldGrid = worldGrid;
    });
    

    stateMachine.CompleteGeneration();
  }
}
}