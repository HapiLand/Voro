using DataTypes;
using UnityEngine;

namespace Internal.Instructions {
public class Null : INode {
    readonly IConfig config;

    public Null(IConfig config) {
        this.config = config;
    }

    public float Solve(float height, Vector3 worldPos) {
        return 0f;
    }

    public void Solve(ref Cell cell, Vector3 worldPoint) {
        throw new System.NotImplementedException();
    }
}
}