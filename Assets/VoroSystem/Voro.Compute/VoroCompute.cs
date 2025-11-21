using System;
using UnityEngine;
using VoroSystem.Voro.Compute.Elevation;
using VoroSystem.Voro.Designer;
using VoroSystem.Voro.Designer.Graph;
using VoroSystem.Voro.World.TerrainOLD;

namespace VoroSystem.Voro.Compute {
/// <summary>
/// Computes texture
/// </summary>
public class VoroCompute : MonoBehaviour {
  #region Serialized Fields

  [SerializeField] VoroTerrain voroTerrain;
  [SerializeField] VoroDesigner voroDesigner;
  [SerializeField] ChunkReader chunkReader;

  #endregion

  #region Event Functions

  void Awake() {
    /*
     * apply heightmap texture across the tilemap
     * generate mesh for each tile
     * displace vertices using heightmap
     */
    voroTerrain ??= GetComponent<VoroTerrain>();
    voroDesigner ??= GetComponentInChildren<VoroDesigner>();

    chunkReader = new ChunkReader();
    name = "Voro Compute";
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