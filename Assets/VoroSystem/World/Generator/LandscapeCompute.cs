using System.Collections.Generic;

namespace VoroSystem.World.Generator {
class LandscapeCompute {
    readonly VoroLandscapeGenerator _voroLandscapeGenerator;

    public LandscapeCompute(VoroLandscapeGenerator voroLandscapeGenerator) {
        _voroLandscapeGenerator = voroLandscapeGenerator;
    }

    /// <summary> compute results </summary>
    /// <param name="graph"> </param>
    public void Compute(Dictionary<string, List<int>> graph) {
        /*
         * final Compute method
         * 1) at each chunk make a heightmap texture
         * 2) create point cloud and offset position.y using heightmap
         * 3) create quad mesh for each chunk, vertical raycast to set ground-level elevation
         */

        _voroLandscapeGenerator.TilemapComponent.MapEffectSystem.CreateBaseResults(out var baseResultsList);
        _voroLandscapeGenerator.TilemapComponent.MapEffectSystem.RunEffects(baseResultsList, graph);
        _voroLandscapeGenerator.TilemapComponent.MapEffectSystem.CreateEndResults(baseResultsList, out var terrain);
        _voroLandscapeGenerator.ComputedVoroTerrain = terrain;
    }
}
}