using Voro.Core.World;

namespace Voro.Core.VoroSystem {
/// <summary> Provides methods which interact with the Voro Systems </summary>
class VoroSystem {
    Environment _environment;

    public void LoadConfiguration() {
        _environment.CreateTerrainBuilder();
        _environment.SetTerrainBuilderConfiguration();
        _environment.ReadConfiguration();
    }

    public void CreateLayers() {
        _environment.CreateLayersInConfiguration();
    }

    public void InitializeGeneration() {
        _environment.InitializeTerrainBuilder();
    }

    public void RunGeneration() {
        _environment.BuildTerrain();
    }

    public void ConstructTerrain() {
        _environment.GenerateTerrain();
    }
}
}