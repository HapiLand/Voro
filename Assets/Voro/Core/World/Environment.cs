using UnityEngine;

namespace Voro.Core.World {
/// <summary> Top-Level class which contains the full hierarchy of terrain system </summary>
class Environment {
    /// <summary> The parent Transform where this Environment is found within the Unity scene </summary>
    readonly Transform _parent;

    /// <summary> The physical representation of the Environment </summary>
    Terrain _terrain;

    TerrainBuilder _terrainBuilder;

    /// <summary> The space where the physical Terrain is located </summary>
    World _world;

    /// <summary> Constructor for the Terrain Environment </summary>
    public Environment(Transform parent) {
        _parent = parent;
    }

    public void CreateWorld() {
        // 1) Create World     - Environment gets a space to exist inside
        // Debug.Log("[Environment] Construct new World");
        _world = new World();
    }

    public void CreateTerrainBuilder() {
        // 2) Create Terrain   - Environment now has a physical 3D form
        // Debug.Log("[Environment] Construct new Terrain");
        _terrainBuilder = new TerrainBuilder(_world);
    }

    public void SetTerrainBuilderConfiguration() {
        _terrainBuilder.SetConfigPath(1);
    }

    public void ReadConfiguration() {
        _terrainBuilder.ReadConfig();
    }

    public void CreateLayersInConfiguration() {
        _terrainBuilder.DeserializeConfiguration();
    }

    public void InitializeTerrainBuilder() {
        _terrainBuilder.InitializeBaseResults();
    }

    public void BuildTerrain() {
        // 2) Create Terrain   - Environment now has a physical 3D form
        // Debug.Log("[Environment] Construct new Terrain");
        _terrain = _terrainBuilder.RunEffectManagers();
    }

    public void GenerateTerrain() {
        var resultsFactory = new ResultsFactory(_terrain, _world.Map);
        resultsFactory.GenerateChunkedTerrain(_parent);
    }
}
}