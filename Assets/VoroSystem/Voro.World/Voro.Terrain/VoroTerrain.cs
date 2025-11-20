using UnityEngine;
using VoroSystem.Voro.World.Landscape;

namespace VoroSystem.Voro.World.Voro.Terrain {
/// <summary>
/// Makes the mesh
/// </summary>
[RequireComponent(typeof(ChunkTerrain))]
public class VoroTerrain : MonoBehaviour {
  #region Serialized Fields

  [SerializeField] VoroLandscape voroLandscape;
  [SerializeField] ChunkTerrain chunkTerrain;

  #endregion

  #region Event Functions

  void Awake() {
    /*
     * apply heightmap texture across the tilemap
     * generate mesh for each tile
     * displace vertices using heightmap
     */
    voroLandscape ??= GetComponentInChildren<VoroLandscape>();
    chunkTerrain ??= GetComponent<ChunkTerrain>();
    name = "VoroTerrain";
    chunkTerrain.Initialize(voroLandscape);
  }

  #endregion
}
}