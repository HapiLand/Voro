using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Voro.Jen.Internal;
using Voro.World.Internal;

namespace Voro.Jen {
/// <summary>
///     extension methods that are used for the compute output
/// </summary>
public static class ResultExtensions {
    public static List<ChunkPoint> GetPointList(this ResultDiagram result) {
        return result.Points.ToList();
    }

    public static GameObject GetMeshObject(this ChunkPoint point) {
        var instance = new GameObject($"{point.ID}");
        instance.transform.position = point.Position;
        var meshFilter = instance.AddComponent<MeshFilter>();
        var meshRenderer = instance.AddComponent<MeshRenderer>();

        // set mesh in object
        var variant = 0;
        meshFilter.sharedMesh = UnityEngine.Resources.Load<Mesh>($"Mesh/{point.ID}_{variant}");

        // set material
        var originalMat = UnityEngine.Resources.Load<Material>("FbxMat");
        var mat = new Material(originalMat);
        mat.color = point.Color;
        meshRenderer.material = mat;

        return instance;
    }

    /// <summary>
    ///     convert the chunk points to point data, so chunk points can be put into a buffer
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public static PointData[] ToPointDataArray(ChunkPoint[] points) {
        var result = new PointData[points.Length];
        for (var i = 0; i < points.Length; i++) {
            var chunkPoint = points[i];
            result[i] = new PointData
            {
                Position = chunkPoint.Position,
                ID = chunkPoint.ID
            };
        }

        return result;
    }

    public static ChunkPoint[] ToChunkPoints(PointData[] points) {
        var result = new ChunkPoint[points.Length];

        for (var i = 0; i < points.Length; i++) {
            var pointData = points[i];

            result[i] = new ChunkPoint
            {
                Position = pointData.Position,
                ID = pointData.ID,
                Color = Color.blueViolet
            };
        }

        return result;
    }
}
}