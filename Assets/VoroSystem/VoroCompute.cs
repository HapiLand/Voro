using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace VoroSystem {
public class VoroCompute {
    /// <summary>
    ///     carries out the voro compute to produce data to generate terrain
    /// </summary>
    /// <param name="tileMap">all the point data for the world to find height at</param>
    /// <param name="dg">diagram which contains all the layers and effects</param>
    /// <param name="result">computed output</param>
    public void ComputeDiagramMap(TileMap tileMap, Diagram dg, out ComputeResult result) {
        #region ComputeShader Dispatching

        Debug.Log("Computing Diagram Map");
        var sw = new Stopwatch();
        sw.Start();

        // create the point buffer that stores every position in the tile map
        var tuple = ToComputeBuffer();

        // each graph is a layer of terrain generation, storing a collection of effects
        foreach (var graph in dg.Graphs) {
            for (var i = 0; i < graph.Effects.Count; i++) {
                var effect = graph.Effects[i];
                // dispatch the effect for every point
                effect.Dispatch(tuple.cb, tuple.size);
            }
        }


        sw.Stop();
        Debug.Log($"took {sw.ElapsedMilliseconds}ms to compute {dg.Graphs.Count} Graphs to generate the final result");

        #endregion

        var bufferResult = new PointData[tuple.size];
        tuple.cb.GetData(bufferResult);
        tuple.cb.Release(); // free from memory

        result = new ComputeResult(bufferResult);

        return;

        (ComputeBuffer cb, int size) ToComputeBuffer() {
            var data = tileMap.AsPoints()
                .Select(pt => new PointData { p = pt.Position, id = pt.Id, col = pt.Color })
                .ToArray();
            var stride = sizeof(float) * 3 + sizeof(int) + sizeof(float) * 3;
            var buffer = new ComputeBuffer(data.Length, stride);
            buffer.SetData(data);
            return (buffer, data.Length);
        }
    }

}
}