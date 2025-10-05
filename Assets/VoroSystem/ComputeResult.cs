using UnityEngine;
using VoroSystem.Extensions;
using VoroSystem.GridSystem;

namespace VoroSystem {
/// <summary>
///     dispatching the shader to compute the diagram map produces this object
/// </summary>
public class ComputeResult {
    /// <summary>
    ///     the computed points
    /// </summary>
    public Cell[] Points;

    public ComputeResult(PointData[] bufferPoints) {
        Debug.Log("using output from dispatch to create new ComputeResult");
        Points = new Cell[bufferPoints.Length];
        // create all the points from the buffer
        for (var i = 0; i < bufferPoints.Length; ++i) {
            var data = bufferPoints[i];
            Points[i] = data.ToCell();
        }
    }
}
}