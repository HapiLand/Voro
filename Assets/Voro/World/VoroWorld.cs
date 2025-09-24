using UnityEngine;
using Voro.Jen;

namespace Voro.World {
/// <summary>
///     - The environment container where generated terrain is instantiated.
/// </summary>
public class VoroWorld : MonoBehaviour {
    public readonly Diagram Diagram;

    void OnDisable() {
        Dispose();
    }

    void OnDestroy() {
        Dispose();
    }

    public void GetComputeResult(string result) {
        Debug.Log($"Received Result: {result}");
        // instantiate result to scene
        var instance = new GameObject($"Result {result}");
        instance.transform.SetParent(transform);
    }

    public void Dispose() {
        DestroyImmediate(this);
    }
}
}