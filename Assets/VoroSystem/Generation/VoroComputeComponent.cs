using UnityEngine;
using VoroSystem.Landscape;

namespace VoroSystem.Generation {
/// <summary>
/// Computes texture
/// </summary>
[ExecuteAlways]
[RequireComponent(typeof(VoroLandscapeComponent))]
public class VoroComputeComponent : MonoBehaviour {
    #region Serialized Fields

    [SerializeField] VoroLandscapeComponent voroLandscape;

    #endregion

    #region Event Functions

    void Awake() {
        /*
         * apply heightmap texture across the tilemap
         * generate mesh for each tile
         * displace vertices using heightmap
         */
        voroLandscape ??= GetComponent<VoroLandscapeComponent>();
        name = "VoroCompute";
    }

    #endregion
}
}