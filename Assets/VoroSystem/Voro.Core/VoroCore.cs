using System;
using UnityEngine;
using VoroSystem.Voro.Compute;
using VoroSystem.Voro.World.Voro.Terrain;

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
    [SerializeField] VoroCompute compute;
    [SerializeField] VoroTerrain terrain;
    
    void Awake() {
        name = "Voro Core";
        compute ??= GetComponentInChildren<VoroCompute>();
        terrain ??= GetComponentInChildren<VoroTerrain>();
    }
}
}