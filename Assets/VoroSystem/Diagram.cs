using System.Collections.Generic;
using System.Diagnostics;
using VoroSystem.Interface;
using Debug = UnityEngine.Debug;

namespace VoroSystem {
public class Diagram {
    /// <summary>
    ///     each graph contains a collection of effects
    ///     graph will be computed to produce a form of terrain generation
    /// </summary>
    public readonly List<Graph> Graphs;

    public Diagram(List<LayerData> layerContent) {
        Debug.Log("Creating Diagram");
        var sw = new Stopwatch();
        sw.Start();

        Graphs = new List<Graph>();
        foreach (var layer in layerContent) {
            Graphs.Add(new Graph(layer));
        }

        sw.Stop();
        Debug.Log($"took {sw.ElapsedMilliseconds}ms to create Diagram, it contains {Graphs.Count} Graphs");
    }
}
}