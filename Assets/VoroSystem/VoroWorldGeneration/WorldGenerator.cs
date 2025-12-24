using UnityEngine;
using VoroSystem.VoroWorldGeneration.Map;

namespace VoroSystem.VoroWorldGeneration {
[ExecuteAlways]
[RequireComponent(typeof(WorldGenState), typeof(WorldGenTilemap), typeof(WorldGenInstancer))]
// todo require component CubeWorld 
public class WorldGenerator : MonoBehaviour {
  #region Serialized Fields
  public WorldGenState stateMachine;

  // todo CubeWorld implements WorldGenState
  //  WorldGenerator notifies the state machine when the entire world should generate
  public WorldGenTilemap tilemap;

  // todo GridCube implements WorldGenTilemap
  public WorldGenInstancer instancer;
  #endregion

  // todo GridCube implements WorldGenInstancer
  Tilemap<Tile> _worldGrid;

  #region Event Functions
  void Awake() {
    stateMachine = GetComponent<WorldGenState>();
    instancer = GetComponent<WorldGenInstancer>();
    tilemap = GetComponent<WorldGenTilemap>();
    instancer.Init(tilemap);
  }
  #endregion

  public void StartGeneration() {
    if (!stateMachine.CanStartGeneration()) {
      return;
    }

    stateMachine.StartGeneration();
    // todo notify CubeWorld that all its cubes should begin generation
    /*// generate grid, tiles are instanced into scene
    Debug.Log("Start Generate World Grid");
    tilemap.GenerateWorldGrid(
      new Vector3Int(10, 1, 10),
      worldGrid => {
      Debug.Log("Tilemap ready");
      _worldGrid = worldGrid;
      stateMachine.CompleteGeneration();
    });*/
  }

  public WorldGenState.GenerationState GetCurrentState() {
    return stateMachine.currentState;
  }
}
}