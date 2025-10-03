using System;
using UnityEngine;

namespace Voro.UI {
public struct LayerData : IEquatable<LayerData> {
    public string Name;

    public LayerData(string name) {
        Debug.Log($"new LayerData {name}");
        Name = name;
    }

    public bool Equals(LayerData other) {
        return Name == other.Name;
    }

    public override bool Equals(object obj) {
        return obj is LayerData other && Equals(other);
    }

    public override int GetHashCode() {
        return Name != null ? Name.GetHashCode() : 0;
    }
}
}