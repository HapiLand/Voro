using UnityEngine;
using VoroSystem.Voro.Compute;

namespace VoroSystem.Voro.Core {
/*
 * group the components into game objects
 * --Namespaces--
 *
 * Voro.Compute
 * --   Compute.Effects     : ComputeShader
 * --   Compute.Elevation   : Heightmap
 *
 * Voro.Designer
 * --   Designer.Graph    : GUI editor
 *
 * Voro.Landscape
 * --   Landscape.Map     : Bounding box, Tilemap
 *
 * Voro.Terrain
 * --   Terrain.Ground    : Chunked mesh
 */
[ExecuteAlways]
public class VoroCore : MonoBehaviour {
    #region Serialized Fields
    [SerializeField] VoroCompute compute;
    // [SerializeField] VoroTerrain terrain;
    #endregion

    #region Event Functions
    void Awake() {
        name = "Voro Core";
        compute ??= GetComponentInChildren<VoroCompute>();
        // terrain ??= GetComponentInChildren<VoroTerrain>();
    }
    #endregion
}
}