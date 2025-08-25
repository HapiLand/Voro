using UnityEngine;

namespace Internal {
/// <summary>
///     interface for instructions of the configuration
/// </summary>
public interface INode {
    float Solve(float height, Vector3 worldPoint);
}
}