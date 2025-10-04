using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace VoroSystem {
[ExecuteAlways]
public class WorldController : MonoBehaviour {
    /// <summary>
    ///     instanced objects go here
    /// </summary>
    Transform _container;

    Chunk chunk;
    VoroEditor editor;
    TileMap tileMap;

    public void GenerateWorldMap() {
        Debug.Log("generating world map");
        var sw = new Stopwatch();
        sw.Start();

        // set up the world - copy Chunk points to the TileMap
        tileMap = new TileMap();
        tileMap.SetSize(100, 100); // fixed map size, produce Tile[,]
        tileMap.UpdateVisibility(); // update visibility
        _container = new GameObject("Container").transform;

        chunk = new Chunk();
        AssetLoader.BeginLoadingAssets(chunk); // routine to load asset library
        tileMap.Blit(chunk); // copy multi chunks to each visible tile position

        sw.Stop();
        Debug.Log($"world map took {sw.ElapsedMilliseconds}ms to generate");
    }

    public void LaunchEditor() {
        Debug.Log("launching editor");
        var sw = new Stopwatch();
        sw.Start();

        // set up the editor - load the initial preset template
        editor = VoroEditor.Instance;
        editor.RunDemoCamera(0.2f); // auto fly around the world, 20% speed
        // loading a preset populates the editor with its layer+node content
        editor.LoadPreset(1); // default preset no.1
        editor.ShowWindow(); // open the editor window to display the preset

        sw.Stop();
        Debug.Log($"took {sw.ElapsedMilliseconds}ms to launch the editor");
    }

    public void RunLifeCycleOnce() {
        Debug.Log("Running LifeCycle Once");
        var sw = new Stopwatch();
        sw.Start();

        #region Editor

        editor.CreateDiagram(out var dg); // turn the layers into the compute type

        #endregion

        #region Compute

        // compute the default terrain
        var compute = new VoroCompute();
        compute.ComputeDiagramMap(tileMap, dg, out var result); // dispatch

        #endregion

        #region Mesh

        // the compute data is used to build mesh data
        MeshBuilder.BuildVertices(result, out var vtxInfo); // translate result to vertices then
        MeshBuilder.BuildMesh(vtxInfo, out var meshData); // translate to mesh data

        #endregion

        #region Scene

        // the mesh data is used to create GameObjects
        WorldBuilder.GenerateWorldMap(_container, meshData); // instances the geometry where it should be

        #endregion

        sw.Stop();
        Debug.Log($"took {sw.ElapsedMilliseconds}ms to run");
    }
}

public static class WorldBuilder {
    /// <summary>
    ///     instantiates the mesh data into the unity scene
    ///     the mesh data is used to construct game objects in order for the geometry to appear in the scene
    /// </summary>
    /// <param name="meshData">3d geometry to produce game objects</param>
    public static void GenerateWorldMap(Transform parent, MeshData meshData) {
        Debug.Log("Generating World map");
        var sw = new Stopwatch();
        sw.Start();

        for (var i = 0; i < meshData.Data.Length; i++) {
            var item = meshData.Data[i];

            var instance = new GameObject("MeshPiece");
            instance.transform.position = item.pos;
            instance.transform.SetParent(parent);
            var meshFilter = instance.AddComponent<MeshFilter>();
            var meshRenderer = instance.AddComponent<MeshRenderer>();

            // set mesh in object
            meshFilter.sharedMesh = item.mesh;

            // set material
            var originalMat = Resources.Load<Material>("FbxMat");
            var mat = new Material(originalMat)
            {
                color = item.col
            };
            meshRenderer.material = mat;
        }

        sw.Stop();
        Debug.Log($"took {sw.ElapsedMilliseconds}ms to generate the world map");
    }
}

public class VertexInfo {
    public readonly Color[] Colors;
    public readonly int[] IDs;
    public readonly Vector3[] Vertices;

    public VertexInfo(ComputeResult data) {
        Vertices = new Vector3[data.Points.Length];
        Colors = new Color[data.Points.Length];
        IDs = new int[data.Points.Length];
        for (var i = 0; i < data.Points.Length; ++i) {
            Vertices[i] = data.Points[i].Position;
            Colors[i] = data.Points[i].Color;
            IDs[i] = data.Points[i].ID;
        }
    }
}

public class MeshData {
    public (Mesh mesh, Vector3 pos, Color col)[] Data;

    public MeshData(VertexInfo vtxInfo) {
        Data = new (Mesh, Vector3, Color)[vtxInfo.Vertices.Length];
        // for every vertex, load the .fbx instance that matches its ID
        for (var i = 0; i < Data.Length; i++) {
            var mesh = AssetLoader.GetMeshPiece(vtxInfo.IDs[i]);
            var pos = vtxInfo.Vertices[i];
            var col = vtxInfo.Colors[i];
            Data[i] = (mesh, pos, col);
        }

        Debug.Log($"produced {Data.Length} mesh pieces");
    }
}

/// <summary>
///     dispatching the shader to compute the diagram map produces this object
/// </summary>
public class ComputeResult {
    /// <summary>
    ///     the computed points
    /// </summary>
    public Chunk.Cell[] Points;

    public ComputeResult(VoroCompute.PointData[] bufferPoints) {
        Debug.Log("creating ComputeResult from the buffer result");
        Points = new Chunk.Cell[bufferPoints.Length];
        // create all the points from the buffer
        for (var i = 0; i < bufferPoints.Length; ++i) {
            var data = bufferPoints[i];
            Points[i] = new Chunk.Cell
            {
                Position = data.p,
                ID = data.id,
                Color = new Color(data.col.x, data.col.y, data.col.z, 1.0f)
            };
        }
    }
}

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

    public struct PointData {
        public Vector3 p;
        public int id;
        public Vector3 col;
    }
}
}