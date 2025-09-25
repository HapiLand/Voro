using System.Collections.Generic;
using UnityEngine;
using Voro.Grids;
using Voro.Jen.Compute.FX;
using Voro.Jen.Compute.FX.Base;
using Voro.Jen.Compute.FX.Internal;
using Voro.Jen.Compute.Internal;
using Voro.Jen.Internal;
using Voro.UI.EditorTabs.Nodes;
using Voro.World;

namespace Voro.Jen.Compute {
/// <summary>
///     - Executes terrain generation.
///     - Generates the actual results based on Diagram instructions.
/// </summary>
public class VoroCompute {
    /// <summary>
    ///     initiate the world terrain to set up its very first form
    /// </summary>
    /// <param name="diagram"></param>
    public IEnumerable<ResultDiagram> ExecuteInitiate(Diagram diagram) {
        Debug.Log("Execute VoroCompute System :D");

        // create initial Effects
        // only do a constant height
        var effects = new List<IEffect> { new ConstantHeight(new ConstantHeightData()) };


        // get each tile within the world map
        // tile is the chunk origin
        foreach (var tile in diagram.Map.AsEnumerable()) {
            #region Point Buffer for Chunk

            var chunkPoints = diagram.Chunk.ToWorldCoordinate(tile.Coordinate);
            var pointDataArray = ResultExtensions.ToPointDataArray(chunkPoints);
            var pointCount = pointDataArray.Length;
            var stride = ComputeHelper.GetStride<PointData>();
            var buffer = new ComputeBuffer(pointCount, stride);
            buffer.SetData(pointDataArray);

            #endregion

            #region Compute every Effect

            // compute every effect
            foreach (var effect in effects) {
                // dispatch the effect 
                effect.Dispatch(buffer, chunkPoints.Length);
            }

            #endregion

            #region Result of all Effects

            // ComputeBuffer -> ResultDiagram
            var bufferResult = new PointData[chunkPoints.Length];
            buffer.GetData(bufferResult);

            // release buffer from memory
            buffer.Release();

            // create the result diagram with the world data
            var result = ResultDiagram.CreateInstance(diagram, chunkPoints, bufferResult);
            // return the result for this tile
            yield return result;

            #endregion
        }

        Debug.Log("Compute Done");
    }

    public void Execute(Diagram diagram, out string result) {
        result = "PlaceholderResult";
    }
}
}