using System;

namespace DefaultNamespace {
internal interface ILayer {
    // layer.Effects.OrderAscending(sortOrder).ForEach(fx).ExecuteInstruction()
    int ExecuteEffects();
}

internal class SimpleOcean : ILayer {
    public int ExecuteEffects() {
        // the ocean has a sort order of 0, the bottom-most Layer
        // a flat surface at sea level, empty with only water
        return 5;
    }
}

internal class Landmass : ILayer {
    readonly ILayer _mLayer;

    public Landmass(ILayer layer) {
        _mLayer = layer ?? throw new ArgumentNullException(nameof(layer), "layer should not be null");
    }

    public int ExecuteEffects() {
        // a sort order of 1, it is above the ocean
        // a roughly circular shape piece of land
        // appearance is mostly flat, with a chance for some small hills
        // vegetation and trees are lightly scattered around, trees typically exist in groups
        // the perimeter is a beach due to the land existing next to the ocean
        return _mLayer.ExecuteEffects() + 1;
    }
}

internal class Volcano : ILayer {
    readonly ILayer _mLayer;

    public Volcano(ILayer layer) {
        _mLayer = layer ?? throw new ArgumentNullException(nameof(layer), "layer should not be null");
    }

    public int ExecuteEffects() {
        // sort order of 2, it is on top of the layer
        // the volcano originates at the center of the landmass
        // the volcano is moderately sized, and is easy to scale the side of
        // within the interior of the volcano, players can burn to death
        // there exists no vegetation where the volcano is
        // a river exists in the landmass Layer but local to the location of the volcano
        return _mLayer.ExecuteEffects() + 1;
    }
}

internal class Runtime {
    void UseFacade() {
        var overseer = new OverseerFacade(new Overseer());
        overseer.Start(); // New Map.
        overseer.Update(); // Landscape Runtime. Instancing Terrain.
        Console.WriteLine();
    }

    class Overseer {
        public void CreateWorldMap() {
            Console.Write("New Map.");
        }

        public void UpdateLandscape() {
            Console.Write("Landscape Runtime.");
            ComputeGraph();
            return;

            void ComputeGraph() {
                var oceanLayer = new SimpleOcean();
                Console.WriteLine($"{oceanLayer.ExecuteEffects():c}");

                var oceanWithLandmass = new Landmass(oceanLayer);
                Console.WriteLine($"{oceanWithLandmass.ExecuteEffects():c}");

                var oceanWithLandmassWithVolcano = new Volcano(oceanWithLandmass);
                Console.WriteLine($"{oceanWithLandmassWithVolcano.ExecuteEffects():c}");
            }
        }

        public void TerrainFactory() {
            Console.Write("Instancing Terrain.");
        }
    }

    class OverseerFacade {
        readonly Overseer mOverseer;

        public OverseerFacade(Overseer overseer) {
            mOverseer = overseer ?? throw new ArgumentNullException(nameof(overseer), "overseer cannot be null");
        }

        public void Start() {
            mOverseer.CreateWorldMap();
            mOverseer.UpdateLandscape();
        }

        public void Update() {
            mOverseer.UpdateLandscape();
            mOverseer.TerrainFactory();
        }
    }
}
}