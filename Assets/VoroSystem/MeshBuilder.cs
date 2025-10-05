using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace VoroSystem {
public static class MeshBuilder {
    /// <summary>
    ///     produce Vertex data out of the computed data
    ///     compute result only stores position data which must be converted into collection of vertices
    /// </summary>
    /// <param name="data">computed diagram map</param>
    /// <param name="vtxInfo">vertices within the data</param>
    public static void BuildVertices(ComputeResult data, out VertexInfo vtxInfo) {
        Debug.Log("Building Vertices");
        var sw = new Stopwatch();
        sw.Start();

        vtxInfo = new VertexInfo(data);

        sw.Stop();
        Debug.Log($"took {sw.ElapsedMilliseconds}ms to build vertex info");
    }

    /// <summary>
    ///     produces mesh data out of the vertices
    ///     the vertex info only holds collection of vertices, which must be turned into actual 3D geometry
    /// </summary>
    /// <param name="vtxInfo">vertex array</param>
    /// <param name="meshData">3D geometry data to produce a mesh</param>
    public static void BuildMesh(VertexInfo vtxInfo, out MeshData meshData) {
        Debug.Log("Building Mesh");
        var sw = new Stopwatch();
        sw.Start();

        meshData = new MeshData(vtxInfo);

        sw.Stop();
        Debug.Log($"took {sw.ElapsedMilliseconds}ms to build mesh data");
    }
}
}