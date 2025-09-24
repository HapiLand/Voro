using UnityEngine;
using Voro.UI;
using Voro.World;

namespace Voro.Jen {
/// <summary>
///     - Oversees the entire terrain generation process.
///     - Acts as the central control class of the system.
/// </summary>
public class VoroGeneration {
    readonly VoroCompute _compute;
    readonly VoroUI _userInterface;
    readonly VoroWorld _world;
    readonly Diagram _diagram;

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
        Debug.Log("initial compute");
        _compute.Execute(_diagram, out var result);
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