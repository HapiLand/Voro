using UnityEngine;

namespace Voro {
/// <summary>
///     - The environment container where generated terrain is instantiated.
/// </summary>
public class VoroWorld : MonoBehaviour {
    void OnDisable() {
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