using UnityEngine;
using VoroSystem.Generation.TerrainSystem;
using VoroSystem.Landscape;

namespace VoroSystem.Generation {
/// <summary>
/// Makes the mesh
/// </summary>
[ExecuteAlways]
[RequireComponent(typeof(VoroLandscapeComponent))]
[RequireComponent(typeof(ChunkTerrainComponent))]
[RequireComponent(typeof(VoroComputeComponent))]
public class VoroTerrainComponent : MonoBehaviour {
    #region Serialized Fields

    [SerializeField] VoroLandscapeComponent voroLandscape;
    [SerializeField] ChunkTerrainComponent chunkTerrain;
    [SerializeField] VoroComputeComponent compute;

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
        compute ??= GetComponent<VoroComputeComponent>();
        name = "VoroTerrain";
        chunkTerrain.Initialize(voroLandscape);
    }

    #endregion
}
}