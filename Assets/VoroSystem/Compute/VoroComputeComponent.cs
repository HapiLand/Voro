using System;
using UnityEngine;
using VoroSystem.Designer;
using VoroSystem.Designer.GraphSystem;
using VoroSystem.Terrain;

namespace VoroSystem.Compute {
/// <summary>
/// Computes texture
/// </summary>
[ExecuteAlways]
[RequireComponent(typeof(VoroTerrainComponent))]
[RequireComponent(typeof(ChunkTerrainComponent))]
[RequireComponent(typeof(VoroDesignerComponent))]
public class VoroComputeComponent : MonoBehaviour {
  #region Serialized Fields

  [SerializeField] VoroTerrainComponent voroTerrain;
  [SerializeField] ChunkTerrainComponent chunkTerrain;
  [SerializeField] VoroDesignerComponent voroDesigner;
  [SerializeField] ChunkReader chunkReader;

  #endregion

  #region Event Functions

  void Awake() {
    /*
     * apply heightmap texture across the tilemap
     * generate mesh for each tile
     * displace vertices using heightmap
     */
    voroTerrain ??= GetComponent<VoroTerrainComponent>();
    chunkTerrain ??= GetComponent<ChunkTerrainComponent>();
    voroDesigner ??= GetComponent<VoroDesignerComponent>();

    chunkReader = new ChunkReader();
    name = "VoroCompute";
    chunkReader.Subscribe();
  }

  void OnEnable() {
    chunkReader ??= new ChunkReader();
    chunkReader.Subscribe();
  }

  void OnDisable() {
    chunkReader?.Unsubscribe();
  }

  #endregion

  public void DoCompute() {
    OnDoCompute?.Invoke(voroDesigner.graphComponent.graph);
  }

  public static event Action<Graph> OnDoCompute;
}
}