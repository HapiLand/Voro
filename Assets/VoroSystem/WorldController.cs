using System.Diagnostics;
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
        tileMap.SetSize(new Vector2Int(2,5)); // fixed map size, produce Tile[,]
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
}