using UnityEngine;
using Voro.Core.VoroSystem;
using Voro.Systems.Landscape;
using Voro.Systems.LifeCycle;

namespace Voro.Systems {
class VoroManager : SystemLifecycle {
    readonly Transform _root;
    VoroSystem _system;
    LandscapeSystem _landscapeSystem;

    public VoroManager(Transform root) {
        _root = root;
    }

    /// <summary> Create a bounding region for the World Map, get configuration.json </summary>
    protected override void Initialize() {
        Debug.Log("[Voro System] Initialize");
        _system = new VoroSystem();
        Debug.Log("[Voro System] Creating Landscape System");
        _landscapeSystem = new LandscapeSystem();
        // _system.CreateEnvironment(_root);
        // _system.CreateWorld();
        // Debug.Log("[Voro System] Loading Configuration");
        // _system.LoadConfiguration();
    }

    protected override void Creation() {
        Debug.Log("[Voro System] Creation");
        Debug.Log("[Voro System] Create TerrainLayers");
        _system.CreateLayers();
    }

    protected override void Production() {
        Debug.Log("[Voro System] Production");
        Debug.Log("[Voro System] Preparing Generation");
        _system.InitializeGeneration();
        Debug.Log("[Voro System] Run Terrain Generation");
        _system.RunGeneration();
    }

    protected override void Construction() {
        Debug.Log("[Voro System] Construction");
        Debug.Log("[Voro System] Create Terrain Mesh");
        _system.ConstructTerrain();
    }
}
}