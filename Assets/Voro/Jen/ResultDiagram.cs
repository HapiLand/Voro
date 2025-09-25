using Voro.Jen.Internal;
using Voro.World.Internal;

namespace Voro.Jen {
public class ResultDiagram {
    Diagram _diagram;
    public ChunkPoint[] Points;

    ResultDiagram(Diagram diagram, ChunkPoint[] points) {
        _diagram = diagram;
        Points = points;
    }

    public static ResultDiagram CreateInstance(Diagram diagram, ChunkPoint[] cp, PointData[] pd) {
        // convert the result PointData
        var chunkPoints = ResultExtensions.ToChunkPoints(pd);

        // PointData struct only stores Pos,ID
        // copy the original chunk point color
        for (var i = 0; i < chunkPoints.Length; i++) {
            chunkPoints[i].Color = cp[i].Color;
        }

        // Debug.Log($"ResultDiagram received {chunkPoints.Length} Computed Points");
        return new ResultDiagram(diagram, chunkPoints);
    }
}
}