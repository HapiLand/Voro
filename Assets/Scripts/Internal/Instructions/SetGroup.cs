using DataTypes;
using UnityEngine;

namespace Internal.Instructions {
public class SetGroup : INode {
    readonly IConfig config;

    public SetGroup(IConfig config) {
        this.config = config;
    }

    public float Solve(float height, Vector3 worldPos) {
        return 0f;
    }

    public void Solve(ref Cell cell, Vector3 worldPos) {
        // the group to set this cell as
        var groupValue = config.ConfigArr[0];
        // ToDo effects are limited that they only store floats, when they need to be able to use other types
        cell.GroupID = (int)groupValue;
    }
}
}