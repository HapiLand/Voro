using UnityEngine;
using Voro.UI;
using Voro.World;

namespace Voro.Generation {
public class VoroGeneration {
    readonly Chunk _chunk;
    readonly VoroInstance _voroInstance;
    DiagramUI _diagramUI;

    public VoroGeneration(DiagramUI diagramUI) {
        _diagramUI = diagramUI;
        _chunk = new Chunk();
        _voroInstance = new GameObject("Map Instance Container").AddComponent<VoroInstance>();
    }

    /// <summary>
    ///     instantiate the objects
    /// </summary>
    public void CreateWorldMap() {
        // instance a chunk at every world position in the map
        _voroInstance.InstanceMap(_chunk);
    }
}
}