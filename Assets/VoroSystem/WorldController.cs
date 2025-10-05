using System.Diagnostics;
using UnityEngine;
using VoroSystem.GridSystem;
using VoroSystem.UserInterface;
using Debug = UnityEngine.Debug;

namespace VoroSystem {
[ExecuteAlways]
public class WorldController : MonoBehaviour {
    /// <summary>
    ///     instanced objects go here
    /// </summary>
    Transform _container;


    public void GenerateWorldMap() {
        var sw = Stopwatch.StartNew();

        _tileMap = new TileMap();
        _gridSystem = new GridSystemMediator(_tileMap);
        var size = new Vector2Int(2, 5);
        _gridSystem.Initialize(size);
        _container = new GameObject("Container").transform;

        sw.Stop();
        LogConstructionTime(sw.ElapsedMilliseconds);
        return;

        void LogConstructionTime(long elapsedMilliseconds) {
            Debug.Log($"World Map generated in {elapsedMilliseconds} ms");
        }
    }

    public void LaunchEditor() {
        var sw = Stopwatch.StartNew();

        _editor = new VoroEditor();
        _userInterface = new UserInterfaceMediator(_editor);
        var preset = 1;
        _userInterface.Initialize(preset);
        _userInterface.OpenWindow();

        sw.Stop();
        LogConstructionTime(sw.ElapsedMilliseconds);
        return;

        void LogConstructionTime(long elapsedMilliseconds) {
            Debug.Log($"Editor launched in {elapsedMilliseconds} ms");
        }
    }

    public void RunLifeCycleOnce() {
        Debug.Log("Running LifeCycle Once");
        var sw = new Stopwatch();
        sw.Start();

        #region Editor

        _editor.CreateDiagram(out var dg); // turn the layers into the compute type

        #endregion

        #region Compute

        // compute the default terrain
        var compute = new VoroCompute();
        compute.ComputeDiagramMap(_tileMap, dg, out var result); // dispatch

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

    #region Grid System

    GridSystemMediator _gridSystem;
    TileMap _tileMap;

    #endregion

    #region User Interface

    UserInterfaceMediator _userInterface;
    VoroEditor _editor;

    #endregion
}
}