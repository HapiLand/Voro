using UnityEngine;
using VoroSystem.Landscape;

namespace VoroSystem.Terrain {
/// <summary>
/// Makes the mesh
/// </summary>
[ExecuteAlways]
[RequireComponent(typeof(VoroLandscapeComponent))]
[RequireComponent(typeof(ChunkTerrainComponent))]
public class VoroTerrainComponent : MonoBehaviour {
  #region Serialized Fields

  [SerializeField] VoroLandscapeComponent voroLandscape;
  [SerializeField] ChunkTerrainComponent chunkTerrain;

  #endregion

  #region Event Functions

  void Awake() {
    /*
     * apply heightmap texture across the tilemap
     * generate mesh for each tile
     * displace vertices using heightmap
     */
    voroLandscape ??= GetComponent<VoroLandscapeComponent>();
    chunkTerrain ??= GetComponent<ChunkTerrainComponent>();
    name = "VoroTerrain";
    chunkTerrain.Initialize(voroLandscape);
  }

  #endregion
}
}