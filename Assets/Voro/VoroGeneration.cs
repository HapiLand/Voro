using UnityEngine;

namespace Voro {
/// <summary>
///     - Oversees the entire terrain generation process.
///     - Acts as the central control class of the system.
/// </summary>
public class VoroGeneration {
    readonly VoroCompute _compute;
    readonly VoroUI _userInterface;
    readonly VoroWorld _world;

    /// <summary>
    /// </summary>
    /// <param name="worldContainer">instantiate objects into this</param>
    /// <param name="voroCompute">terrain generation core</param>
    /// <param name="voroUI">handle events</param>
    public VoroGeneration(VoroWorld worldContainer, VoroCompute voroCompute, VoroUI voroUI) {
        _world = worldContainer;
        _compute = voroCompute;
        _userInterface = voroUI;

        // do the initial compute
        ComputeInitial();

        // handle UI events
        _userInterface.ClickedRecompute += OnComputeDiagram;
    }

    void ComputeInitial() {
        // compute the initial terrain in order for VoroWorld to start with terrain content
        Debug.Log($"initial compute: {_compute._diagram._chunk._points.Length}");
    }

    public void Dispose() {
        _userInterface.ClickedRecompute -= OnComputeDiagram;
    }

    void OnComputeDiagram() {
        Debug.Log("Compute Diagram");
        // execute compute to produce a result
        _compute.Execute(out var result);
        // pass result to VoroWorld
        _world.GetComputeResult(result);
    }
}
}