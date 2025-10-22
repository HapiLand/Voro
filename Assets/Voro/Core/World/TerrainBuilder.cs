using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace Voro.Core.World {
class TerrainBuilder {
    /// <summary> World gives access to TileMap </summary>
    public readonly World World;

    /// <summary> Configuration gives access to list of Layers </summary>
    string _configurationPath;

    string _configurationText;

    List<TerrainLayer> _layers = new();

    public TerrainBuilder(World world) {
        World = world;
    }

    bool HasConfiguration => !string.IsNullOrEmpty(_configurationPath);

    List<BaseResult> TileBaseResults {
        get
        {
            var baseResults = new List<BaseResult>();
            World.Map.ForEach(tile => {
                var br = new BaseResult(tile);
                baseResults.Add(br);
            });
            return baseResults;
        }
    }

    /// <summary> Sets the configuration to load a .json </summary>
    /// <param name="index"> config[INDEX].json to access </param>
    public TerrainBuilder SetConfigPath(int index) {
        _configurationPath = $"config{index}";
        return this;
    }

    /// <summary> Loads the config.json and extracts the Layers inside it </summary>
    public TerrainBuilder ReadConfig() {
        if (!HasConfiguration) {
            Debug.LogWarning("[Terrain Builder] Config not set");
            return this;
        }

        _configurationText = Resources.Load<TextAsset>(_configurationPath).text;

        return this;
    }

    public void DeserializeConfiguration() {
        if (string.IsNullOrEmpty(_configurationText)) {
            Debug.LogError($"[Terrain Builder] File {_configurationPath} is empty");
            return;
        }

        var config = JsonConvert.DeserializeObject<Configuration>(_configurationText);
        Debug.Log($"[Terrain Builder] Loaded {config.ConfigName}. {config.Layers.Count} Layers");

        _layers = new List<TerrainLayer>();
        foreach (var layer in config.Layers) {
            _layers.Add(new TerrainLayer(layer));
        }

        if (_layers.Count == 0) {
            Debug.LogError("[Terrain Builder] No Layers found");
        }
    }

    /// <summary> produce all the base results that will run their effects </summary>
    public void InitializeBaseResults() { }

    /// <summary> dispatch the effect managers to produce terrain </summary>
    /// <returns> </returns>
    public Terrain RunEffectManagers() {
        return _layers.Count switch
        {
            0 => BuildFlatTerrain(),
            1 => BuildSingleLayerTerrain(_layers.First()),
            _ => BuildMultiLayerTerrain(_layers)
        };
    }


    /// <summary> Create a Terrain where each Chunk returns at a fixed height=0 </summary>
    Terrain BuildFlatTerrain() {
        Debug.LogWarning("[Terrain Builder] No Layers found, Building flat Terrain");

        // produce a BaseResult for every Tile, storing its initial quad Mesh to compute height for
        var baseResults = TileBaseResults;

        // 2) turn the BaseResults into the EndResults, Terrain converts these into Chunks
        var endResults = new List<EndResult>();
        baseResults.ForEach(br => {
            var er = new EndResult(br);
            endResults.Add(er);
        });

        return new Terrain(endResults);
    }

    /// <summary> Create a Terrain with only a single Layer drives the height of each Chunk </summary>
    Terrain BuildSingleLayerTerrain(TerrainLayer layer) {
        Debug.LogWarning("[Terrain Builder] 1 Layer found, Building Single-Layer Terrain");

        // produce a BaseResult for every Tile, storing its initial quad Mesh to compute height for
        var baseResults = TileBaseResults;

        // for each BaseResult, get each Effect in the Layer, compute the new height value
        foreach (var bResult in baseResults) {
            foreach (var fxMan in layer.EffectManagers) {
                // foreach (var fxResult in layer.Effects.Select(fx => fx.Compute(bResult))) { }
                fxMan.RunEffect(bResult);
            }
        }

        // convert the mutated BaseResult into the final EndResult
        var endResults = new List<EndResult>();
        baseResults.ForEach(br => { endResults.Add(br.CreateEndResult()); });

        return new Terrain(endResults);
    }

    /// <summary> Create a Terrain where multiple Layers drives the height of each Chunk </summary>
    Terrain BuildMultiLayerTerrain(List<TerrainLayer> layers) {
        Debug.Log($"[Terrain Builder] {layers.Count} Layers found, Building Multi-Layer Terrain");

        // produce a BaseResult for every Tile, storing its initial quad Mesh to compute height for
        var baseResults = TileBaseResults;

        // for each BaseResult, find a height value from the multiple Layers
        foreach (var bResult in baseResults) {
            // get each Effect in the Layer, compute the new height value
            foreach (var layer in layers) {
                foreach (var fxMan in layer.EffectManagers) {
                    fxMan.RunEffect(bResult);
                    //foreach (var fxResult in layers.SelectMany(layer => layer.Effects.Select(fx => fx.Compute(bResult)))) { }
                }
            }
        }

        // convert the mutated BaseResult into the final EndResult
        var endResults = new List<EndResult>();
        baseResults.ForEach(br => { endResults.Add(br.CreateEndResult()); });

        return new Terrain(endResults);
    }
}
}