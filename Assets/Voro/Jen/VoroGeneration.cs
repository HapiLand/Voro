using UnityEngine;
using Voro.Jen.Compute;
using Voro.UI;
using Voro.World;

namespace Voro.Jen {
/// <summary>
///     - Oversees the entire terrain generation process.
///     - Acts as the central control class of the system.
/// </summary>
public class VoroGeneration {
    readonly VoroCompute _compute;
    readonly Diagram _diagram;
    readonly VoroUI _userInterface;
    readonly VoroWorld _world;

    /// <summary>
    /// </summary>
    /// <param name="worldContainer">instantiate objects into this</param>
    /// <param name="voroCompute">terrain generation core</param>
    /// <param name="voroUI">handle events</param>
    public VoroGeneration(VoroWorld worldContainer, VoroCompute voroCompute, VoroUI voroUI, Diagram diagram) {
        _world = worldContainer;
        _compute = voroCompute;
        _userInterface = voroUI;
        _diagram = diagram;

        // handle UI events
        _userInterface.ClickedRecompute += OnComputeDiagram;

        // setup initial world scene
        ComputeInitial();
    }

    public void ComputeInitial() {
        // compute the initial terrain in order for VoroWorld to start with terrain content
        // each result produced is for a differnt tile in the map
        foreach (var result in _compute.ExecuteInitiate(_diagram)) {
            // for the computed result, generate a mesh object for every chunk point
            foreach (var point in result.GetPointList()) {
                // result -> VoroWorld
                // for each result point, look up the fbx mesh there, instantiate this object in VoroWorld
                _world.AddToWorld(point.GetMeshObject());
            }
        }
    }

    public void Dispose() {
        _userInterface.ClickedRecompute -= OnComputeDiagram;
    }

    void OnComputeDiagram() {
        Debug.Log("Compute Diagram");
        // execute compute to produce a result
        _compute.Execute(_diagram, out var result);
        // pass result to VoroWorld
        _world.GetComputeResult(result);
    }
}
}