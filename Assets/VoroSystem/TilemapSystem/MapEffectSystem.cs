using System.Collections.Generic;
using VoroSystem.World.FX.Base;
using VoroSystem.World.FX.Managers;
using VoroSystem.World.Generate;

namespace VoroSystem.TilemapSystem {
public class MapEffectSystem {
    readonly BasicTilemapComponent _basicTilemapComponent;

    public MapEffectSystem(BasicTilemapComponent basicTilemapComponent) {
        _basicTilemapComponent = basicTilemapComponent;
    }

    public void CreateBaseResults(out List<BaseResult> results) {
        var list = new List<BaseResult>();
        _basicTilemapComponent.CompMap.Tilemap.ForEach(tile => { list.Add(tile.TileMeshResult.BaseResult); });
        results = list;
    }

    public void RunEffects(List<BaseResult> baseResults, Dictionary<string, List<int>> rawGraph) {
        var design = new Dictionary<string, List<EffectManager>>();
        foreach (var (name, effectManagers) in rawGraph) {
            design.Add(name, new List<EffectManager>
            {
                new SlopeEffectManager()
            });
        }

        // for each Layer
        foreach (var (name, effectManagers) in design) {
            // for each chunk
            baseResults.ForEach(baseResult => {
                // for each Effect Manager
                effectManagers.ForEach(fx => {
                    // compute
                    fx.RunEffect(baseResult);
                });
            });
        }
    }

    public void CreateEndResults(List<BaseResult> baseResults, out VoroTerrain voroTerrain) {
        var endResults = new List<EndResult>();
        baseResults.ForEach(br => { endResults.Add(br.CreateEndResult()); });
        voroTerrain = new VoroTerrain(endResults);
    }
}
}