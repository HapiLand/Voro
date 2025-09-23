using UnityEngine;

namespace Voro.Jen {
/// <summary>
///     - Executes terrain generation.
///     - Generates the actual results based on Diagram instructions.
/// </summary>
public class VoroCompute {
    /// <summary>
    ///     read data from diagram to define the generation
    /// </summary>
    public Diagram _diagram;

    /// <summary>
    /// </summary>
    /// <param name="diagram">blueprint for terrain generation</param>
    public VoroCompute(Diagram diagram) {
        _diagram = diagram;
    }

    public void Execute(out string result) {
        Debug.Log("Execute VoroCompute System :D");
        result = "PlaceholderResult";
    }
}
}