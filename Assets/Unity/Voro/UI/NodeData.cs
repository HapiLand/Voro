using UnityEngine;

namespace Voro.UI {
public struct NodeData {
    public string Name;

    public NodeData(string name) {
        Debug.Log($"new NodeData {name}");
        Name = name;
    }
}
}