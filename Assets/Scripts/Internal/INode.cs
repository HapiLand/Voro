using UnityEngine;

namespace Internal {
/// <summary>
/// interface for instructions of the configuration
/// </summary>
public interface INode {
    void ComputeHeight(IConfig configuration, Vector3 worldPos, out float height);
}
}